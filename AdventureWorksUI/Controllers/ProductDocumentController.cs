
using AdventureWorks.UI.Models;
using AdventureWorksUI.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AdventureWorks.UI.Controllers
{
    public class ProductDocumentController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5217/api/ProductDocument"; // adjust if your API port differs

        public ProductDocumentController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // ✅ INDEX
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var url = $"{_baseUrl}?page={page}&pageSize={pageSize}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to load product documents.";
                return View(new List<ProductDocumentViewModel>());
            }

            var content = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(content);
            var data = JsonConvert.DeserializeObject<List<ProductDocumentViewModel>>(result.data.ToString());

            return View(data);
        }

        // ✅ DETAILS
        public async Task<IActionResult> Details(int productId, string documentNode)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{productId}/{documentNode}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var productDoc = JsonConvert.DeserializeObject<ProductDocumentViewModel>(json);
            return View(productDoc);
        }

        // ✅ CREATE - GET
        public IActionResult Create() => View();

        // ✅ CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDocumentViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var json = JsonConvert.SerializeObject(model);
            var response = await _httpClient.PostAsync(_baseUrl,
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to create product document.";
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // ✅ EDIT - GET
        public async Task<IActionResult> Edit(int productId, string documentNode)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{productId}/{documentNode}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var productDoc = JsonConvert.DeserializeObject<ProductDocumentViewModel>(json);
            return View(productDoc);
        }

        // ✅ EDIT - POST
        [HttpPost]
        public async Task<IActionResult> Edit(int productId, string documentNode, ProductDocumentViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var json = JsonConvert.SerializeObject(model);
            var response = await _httpClient.PutAsync($"{_baseUrl}/{productId}/{documentNode}",
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to update product document.";
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // ✅ DELETE - GET
        public async Task<IActionResult> Delete(int productId, string documentNode)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{productId}/{documentNode}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var productDoc = JsonConvert.DeserializeObject<ProductDocumentViewModel>(json);
            return View(productDoc);
        }

        // ✅ DELETE - POST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int productId, string documentNode)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/{productId}/{documentNode}");
            return RedirectToAction(nameof(Index));
        }
    }
}

