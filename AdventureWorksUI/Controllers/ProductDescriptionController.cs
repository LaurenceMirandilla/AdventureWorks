using AdventureWorks.UI.Models;
using AdventureWorksUI.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AdventureWorksUI.Controllers
{
    public class ProductDescriptionController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5217/api/ProductDescription"; // adjust port if needed

        public ProductDescriptionController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // INDEX (List)
        public async Task<IActionResult> Index(string? keyword, int page = 1, int pageSize = 10)
        {
            var url = string.IsNullOrEmpty(keyword)
                ? $"{_baseUrl}?page={page}&pageSize={pageSize}"
                : $"{_baseUrl}?keyword={keyword}&page={page}&pageSize={pageSize}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to load product descriptions.";
                return View(new List<ProductDescriptionViewModel>());
            }

            var content = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(content);
            var data = JsonConvert.DeserializeObject<List<ProductDescriptionViewModel>>(result.data.ToString());

            return View(data);
        }

        // DETAILS
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ProductDescriptionViewModel>(json);
            return View(model);
        }

        // CREATE (GET)
        public IActionResult Create() => View();

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDescriptionViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var json = JsonConvert.SerializeObject(model);
            var response = await _httpClient.PostAsync(_baseUrl,
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to create product description.";
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // EDIT (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ProductDescriptionViewModel>(json);
            return View(model);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductDescriptionViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var json = JsonConvert.SerializeObject(model);
            var response = await _httpClient.PutAsync($"{_baseUrl}/{id}",
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to update product description.";
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // DELETE (GET)
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ProductDescriptionViewModel>(json);
            return View(model);
        }

        // DELETE (POST)
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

