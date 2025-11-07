using AdventureWorksUI.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AdventureWorks.UI.Controllers
{
    public class ProductModelProductDescriptionCultureController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5217/api/ProductModelProductDescriptionCulture";

        public ProductModelProductDescriptionCultureController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // ✅ INDEX
        public async Task<IActionResult> Index(string? cultureId)
        {
            var url = _baseUrl;
            if (!string.IsNullOrEmpty(cultureId))
                url += $"?cultureId={cultureId}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to fetch data.";
                return View(new List<ProductModelProductDescriptionCultureViewModel>());
            }

            var content = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(content);
            var data = JsonConvert.DeserializeObject<List<ProductModelProductDescriptionCultureViewModel>>(result.ToString());
            return View(data);
        }

        // ✅ DETAILS (by composite key)
        public async Task<IActionResult> Details(int productModelId, int productDescriptionId, string cultureId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{productModelId}/{productDescriptionId}/{cultureId}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<ProductModelProductDescriptionCultureViewModel>(json);
            return View(item);
        }

        // ✅ CREATE (GET)
        public IActionResult Create() => View();

        // ✅ CREATE (POST)
        [HttpPost]
        public async Task<IActionResult> Create(ProductModelProductDescriptionCultureViewModel model)
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
        public async Task<IActionResult> Edit(int productModelId, int productDescriptionId, string cultureId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{productModelId}/{productDescriptionId}/{cultureId}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ProductModelProductDescriptionCultureViewModel>(json);
            return View(model);
        }

        // ✅ EDIT (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(int productModelId, int productDescriptionId, string cultureId, ProductModelProductDescriptionCultureViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var json = JsonConvert.SerializeObject(model);
            var response = await _httpClient.PutAsync($"{_baseUrl}/{productModelId}/{productDescriptionId}/{cultureId}",
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to update record.";
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }



        // ✅ DELETE (GET)
        public async Task<IActionResult> Delete(int productModelId, int productDescriptionId, string cultureId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{productModelId}/{productDescriptionId}/{cultureId}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ProductModelProductDescriptionCultureViewModel>(json);
            return View(model);
        }

        // ✅ DELETE (POST)
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int productModelId, int productDescriptionId, string cultureId)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{productModelId}/{productDescriptionId}/{cultureId}");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to delete record.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
