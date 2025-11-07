using AdventureWorks.UI.Models;
using AdventureWorksUI.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AdventureWorks.UI.Controllers
{
    public class IllustrationController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5217/api/Illustration"; // adjust your API port

        public IllustrationController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // ✅ INDEX
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
        {
            string url = _baseUrl;
            if (startDate.HasValue && endDate.HasValue)
                url += $"?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Unable to load illustrations.";
                return View(new List<IllustrationViewModel>());
            }

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<IllustrationViewModel>>(json);
            return View(data);
        }

        // ✅ DETAILS
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var illustration = JsonConvert.DeserializeObject<IllustrationViewModel>(json);
            return View(illustration);
        }

        // ✅ CREATE - GET
        public IActionResult Create() => View();

        // ✅ CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IllustrationViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var json = JsonConvert.SerializeObject(model);
            var response = await _httpClient.PostAsync(_baseUrl,
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to create illustration.";
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // ✅ DELETE - GET
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var illustration = JsonConvert.DeserializeObject<IllustrationViewModel>(json);
            return View(illustration);
        }

        // ✅ DELETE - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}