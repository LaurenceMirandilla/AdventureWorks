using AdventureWorksUI.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AdventureWorks.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl = "http://localhost:5217/api/Product";

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient();
        }

        // ✅ INDEX — List all products with search
        public async Task<IActionResult> Index(string? search)
        {
            var response = await _client.GetAsync(_baseUrl + (string.IsNullOrEmpty(search) ? "" : $"?search={search}"));
            if (!response.IsSuccessStatusCode) return View(new List<ProductDTO>());

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<dynamic>(json);

            var products = data.data != null
                ? JsonConvert.DeserializeObject<List<ProductDTO>>(data.data.ToString())
                : JsonConvert.DeserializeObject<List<ProductDTO>>(json);

            ViewBag.Search = search;
            return View(products);
        }

        // ✅ DETAILS
        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductDTO>(json);
            return View(product);
        }

        // ✅ CREATE (GET)
        public IActionResult Create()
        {
            return View();
        }

        // ✅ CREATE (POST)
        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(_baseUrl, content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View(dto);
        }

        // ✅ EDIT (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductDTO>(json);
            return View(product);
        }

        // ✅ EDIT (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductDTO dto)
        {
            if (!ModelState.IsValid) return View(dto);

            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"{_baseUrl}/{id}", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            return View(dto);
        }

        // ✅ DELETE (GET)
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductDTO>(json);
            return View(product);
        }

        // ✅ DELETE (POST)
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _client.DeleteAsync($"{_baseUrl}/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}

