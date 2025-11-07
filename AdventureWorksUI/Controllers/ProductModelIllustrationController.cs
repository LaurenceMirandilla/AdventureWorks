using AdventureWorksUI.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AdventureWorks.UI.Controllers
{
    public class ProductModelIllustrationController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5217/api/ProductModelIllustration";

        public ProductModelIllustrationController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // ✅ INDEX
        public async Task<IActionResult> Index(int? productModelId)
        {
            var url = _baseUrl;
            if (productModelId.HasValue)
                url += $"?productModelId={productModelId.Value}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to fetch data.";
                return View(new List<ProductModelIllustrationViewModel>());
            }

            var content = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(content);
            var data = JsonConvert.DeserializeObject<List<ProductModelIllustrationViewModel>>(result.data.ToString());

            return View(data);
        }

        // ✅ DETAILS
        public async Task<IActionResult> Details(int productModelId, int illustrationId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{productModelId}/{illustrationId}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<ProductModelIllustrationViewModel>(json);
            return View(item);
        }

        // ✅ CREATE (GET)
        public IActionResult Create() => View();

        // ✅ CREATE (POST)
        [HttpPost]
        public async Task<IActionResult> Create(ProductModelIllustrationViewModel model)
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
        public async Task<IActionResult> Edit(int productModelId, int illustrationId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{productModelId}/{illustrationId}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ProductModelIllustrationViewModel>(json);
            return View(model);
        }

        // ✅ EDIT (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(int productModelId, int illustrationId, ProductModelIllustrationViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var json = JsonConvert.SerializeObject(model);
            var response = await _httpClient.PutAsync($"{_baseUrl}/{productModelId}/{illustrationId}",
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to update record.";
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // ✅ DELETE (GET)
        public async Task<IActionResult> Delete(int productModelId, int illustrationId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{productModelId}/{illustrationId}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<ProductModelIllustrationViewModel>(json);
            return View(model);
        }

        // ✅ DELETE (POST)
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int productModelId, int illustrationId)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{productModelId}/{illustrationId}");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to delete record.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

