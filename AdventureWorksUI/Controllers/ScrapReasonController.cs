using AdventureWorksUI.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AdventureWorks.UI.Controllers
{
    public class ScrapReasonController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5217/api/ScrapReason";

        public ScrapReasonController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // ✅ INDEX
        public async Task<IActionResult> Index(string? search, string? sort)
        {
            var url = _baseUrl;
            var queryParams = new List<string>();
            if (!string.IsNullOrEmpty(search)) queryParams.Add($"search={search}");
            if (!string.IsNullOrEmpty(sort)) queryParams.Add($"sort={sort}");
            if (queryParams.Count > 0) url += "?" + string.Join("&", queryParams);

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to fetch data.";
                return View(new List<ScrapReasonViewModel>());
            }

            var content = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(content);
            var data = JsonConvert.DeserializeObject<List<ScrapReasonViewModel>>(result.items.ToString());

            return View(data);
        }

        // ✅ DETAILS
        public async Task<IActionResult> Details(short id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ScrapReasonViewModel>(json);
            return View(model);
        }

        // ✅ CREATE (GET)
        public IActionResult Create() => View();

        // ✅ CREATE (POST)
        [HttpPost]
        public async Task<IActionResult> Create(ScrapReasonViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var json = JsonConvert.SerializeObject(model);
            var response = await _httpClient.PostAsync(_baseUrl,
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to create record.";
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // ✅ EDIT (GET)
        public async Task<IActionResult> Edit(short id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ScrapReasonViewModel>(json);
            return View(model);
        }

        // ✅ EDIT (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(short id, ScrapReasonViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var json = JsonConvert.SerializeObject(model);
            var response = await _httpClient.PutAsync($"{_baseUrl}/{id}",
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to update record.";
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // ✅ DELETE (GET)
        public async Task<IActionResult> Delete(short id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ScrapReasonViewModel>(json);
            return View(model);
        }

        // ✅ DELETE (POST)
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode)
                ViewBag.Error = "Failed to delete record.";

            return RedirectToAction(nameof(Index));
        }
    }
}

