using AdventureWorks.UI.Models;
using AdventureWorksUI.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace AdventureWorksUI.Controllers
{
    public class ProductInventoryController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5217/api/ProductInventory"; // ⚠️ adjust port if needed

        public ProductInventoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // ✅ INDEX
        public async Task<IActionResult> Index(string? location, int page = 1, int pageSize = 10)
        {
            var url = string.IsNullOrEmpty(location)
                ? $"{_baseUrl}?page={page}&pageSize={pageSize}"
                : $"{_baseUrl}?keyword={location}&page={page}&pageSize={pageSize}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Unable to load inventories.";
                return View(new List<ProductInventoryViewModel>());
            }

            var content = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(content);
            var data = JsonConvert.DeserializeObject<IEnumerable<ProductInventoryViewModel>>(result.data.ToString());
            return View(data);
        }

        // ✅ DETAILS
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<ProductInventoryViewModel>(json);
            return View(item);
        }

        // ✅ CREATE - GET
        public IActionResult Create() => View();

        // ✅ CREATE - POST
        [HttpPost]
        public async Task<IActionResult> Create(ProductInventoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.ModifiedDate = DateTime.Now;

            var json = JsonConvert.SerializeObject(model);
            var response = await _httpClient.PostAsync(_baseUrl,
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to create product inventory.";
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // ✅ EDIT - GET
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<ProductInventoryViewModel>(json);
            return View(item);
        }

        // ✅ EDIT - POST
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductInventoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var json = JsonConvert.SerializeObject(model);
            var response = await _httpClient.PutAsync($"{_baseUrl}/{id}",
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to update product inventory.";
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // ✅ DELETE - GET
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<ProductInventoryViewModel>(json);
            return View(item);
        }

        // ✅ DELETE - POST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to delete product inventory.";
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

