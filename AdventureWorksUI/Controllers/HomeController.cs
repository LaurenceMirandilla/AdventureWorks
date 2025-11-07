using AdventureWorksUI.DTO;
using AdventureWorksUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace AdventureWorksUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory httpClientFactory;


        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            this.httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Only allow specific credentials locally
            if (model.Username != "admin" || model.Password != "admin123")
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(model);
            }

            var client = httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5217/api/Auth/login")
            {
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            // Log raw response for debugging
            _logger.LogDebug("API Response Content: {ResponseContent}", content);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    using var doc = JsonDocument.Parse(content);

                    if (TryExtractToken(doc.RootElement, out var token) && !string.IsNullOrWhiteSpace(token))
                    {
                        // Safe to set session now (token is non-null/non-empty)
                        HttpContext.Session.SetString("JWTToken", token);

                        var handler = new JwtSecurityTokenHandler();
                        var jwtToken = handler.ReadJwtToken(token);
                        var username = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
                        HttpContext.Session.SetString("Username", username ?? "Unknown");

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // Include response content to aid debugging why token wasn't found
                        ModelState.AddModelError(string.Empty, $"Login failed: Token not found in response. Response content: {content}");
                        return View(model);
                    }
                }
                catch (JsonException jex)
                {
                    ModelState.AddModelError(string.Empty, $"Login failed: Unable to parse response JSON. {jex.Message}");
                    return View(model);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Login failed: {ex.Message}");
                    return View(model);
                }
            }

            ModelState.AddModelError(string.Empty, $"Invalid login attempt. API returned {(int)response.StatusCode} {response.ReasonPhrase}. Response: {content}");
            return View(model);
        }

        private static bool TryExtractToken(JsonElement element, out string? token)
        {
            token = null;

            // If the element itself is a string, treat that as the token
            if (element.ValueKind == JsonValueKind.String)
            {
                token = element.GetString();
                return !string.IsNullOrWhiteSpace(token);
            }

            // If the element is an object, check direct properties first for common token names
            if (element.ValueKind == JsonValueKind.Object)
            {
                foreach (var prop in element.EnumerateObject())
                {
                    var name = prop.Name.Trim().ToLowerInvariant();
                    if (name == "token" || name == "access_token" || name == "accesstoken" || name == "access-token")
                    {
                        if (prop.Value.ValueKind == JsonValueKind.String)
                        {
                            token = prop.Value.GetString();
                            if (!string.IsNullOrWhiteSpace(token))
                                return true;
                        }
                        // If token node is not a string, attempt to extract recursively
                        if (TryExtractToken(prop.Value, out token) && !string.IsNullOrWhiteSpace(token))
                            return true;
                    }
                }

                // If not found in direct properties with expected names, search all properties recursively
                foreach (var prop in element.EnumerateObject())
                {
                    if (TryExtractToken(prop.Value, out token) && !string.IsNullOrWhiteSpace(token))
                        return true;
                }
            }

            // If the element is an array, search elements recursively
            if (element.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in element.EnumerateArray())
                {
                    if (TryExtractToken(item, out token) && !string.IsNullOrWhiteSpace(token))
                        return true;
                }
            }

            return false;
        }
    }
}
