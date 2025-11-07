using AdventureWorks.UI.Models;
using AdventureWorksUI.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AdventureWorks.UI.Controllers
{
    public class ProductModelController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5217/api/ProductModel";

        public ProductModelController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // ✅ INDEX
        public async Task<IActionResult> Index(string? name)
        {
            var url = _baseUrl;
            if (!string.IsNullOrEmpty(name))
                url += $"?name={name}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to fetch data.";
                return View(new List<ProductModelViewModel>());
            }

            var content = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(content);
            var data = JsonConvert.DeserializeObject<List<ProductModelViewModel>>(result.data.ToString());

            return View(data);
        }

        // ✅ DETAILS
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<ProductModelViewModel>(json);
            return View(item);
        }

        // ✅ CREATE (GET)
        public IActionResult Create() => View();

        // ✅ CREATE (POST)
        [HttpPost]
        public async Task<IActionResult> Create(ProductModelViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

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
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ProductModelViewModel>(json);
            return View(model);
        }

        // ✅ EDIT (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductModelViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

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
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ProductModelViewModel>(json);
            return View(model);
        }

        // ✅ DELETE (POST)
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to delete record.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

