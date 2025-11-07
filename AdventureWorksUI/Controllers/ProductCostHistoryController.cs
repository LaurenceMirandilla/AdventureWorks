using AdventureWorks.UI.Models;
using AdventureWorksUI.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AdventureWorks.UI.Controllers
{
    public class ProductCostHistoryController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5217/api/ProductCostHistory";

        public ProductCostHistoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // ✅ INDEX
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string? sortBy = null, string direction = "asc")
        {
            var url = $"{_baseUrl}?page={page}&pageSize={pageSize}&sortBy={sortBy}&direction={direction}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to fetch data.";
                return View(new List<ProductCostHistoryViewModel>());
            }

            var content = await response.Content.ReadAsStringAsync();
            var jsonObj = JsonConvert.DeserializeObject<dynamic>(content);
            var data = JsonConvert.DeserializeObject<List<ProductCostHistoryViewModel>>(Convert.ToString(jsonObj.data));

            return View(data);
        }

        // ✅ DETAILS
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ProductCostHistoryViewModel>(json);
            return View(model);
        }

        // ✅ CREATE (GET)
        public IActionResult Create() => View();

        // ✅ CREATE (POST)
        [HttpPost]
        public async Task<IActionResult> Create(ProductCostHistoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var json = JsonConvert.SerializeObject(model);
            var response = await _httpClient.PostAsync(_baseUrl, new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to create record.";
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // ✅ EDIT (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ProductCostHistoryViewModel>(json);
            return View(model);
        }

        // ✅ EDIT (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductCostHistoryViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var json = JsonConvert.SerializeObject(model);
            var response = await _httpClient.PutAsync($"{_baseUrl}/{id}", new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to update record.";
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // ✅ DELETE (GET)
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ProductCostHistoryViewModel>(json);
            return View(model);
        }

        // ✅ DELETE (POST)
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode)
                ViewBag.Error = "Failed to delete record.";

            return RedirectToAction(nameof(Index));
        }
    }
}