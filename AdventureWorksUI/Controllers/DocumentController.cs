using AdventureWorks.UI.Models;
using AdventureWorksUI.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AdventureWorks.UI.Controllers
{
    public class DocumentController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5217/api/Document"; // ✅ change if API port differs

        public DocumentController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // ==========================================================
        // INDEX
        // ==========================================================
        public async Task<IActionResult> Index(string? search)
        {
            var url = string.IsNullOrEmpty(search) ? _baseUrl : $"{_baseUrl}?search={search}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Unable to load documents.";
                return View(new List<DocumentViewModel>());
            }

            var content = await response.Content.ReadAsStringAsync();

            // some APIs wrap results like { data: [...] }
            dynamic result = JsonConvert.DeserializeObject(content);
            string jsonData = result?.data != null ? result.data.ToString() : content;

            var documents = JsonConvert.DeserializeObject<List<DocumentViewModel>>(jsonData);
            return View(documents);
        }

        // ==========================================================
        // CREATE
        // ==========================================================
        public IActionResult Create()
        {
            return View(new DocumentViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DocumentViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // ensure ModifiedDate is set
            model.ModifiedDate = DateTime.Now;

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_baseUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = await response.Content.ReadAsStringAsync();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // ==========================================================
        // EDIT
        // ==========================================================
        public async Task<IActionResult> Edit(string documentNode)
        {
            if (string.IsNullOrEmpty(documentNode))
                return NotFound();

            var response = await _httpClient.GetAsync($"{_baseUrl}/{documentNode}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var document = JsonConvert.DeserializeObject<DocumentViewModel>(json);

            return View(document);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string documentNode, DocumentViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.ModifiedDate = DateTime.Now;

            var json = JsonConvert.SerializeObject(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_baseUrl}/{documentNode}", content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = await response.Content.ReadAsStringAsync();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // ==========================================================
        // DELETE
        // ==========================================================
        public async Task<IActionResult> Delete(string documentNode)
        {
            if (string.IsNullOrEmpty(documentNode))
                return NotFound();

            var response = await _httpClient.GetAsync($"{_baseUrl}/{documentNode}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var document = JsonConvert.DeserializeObject<DocumentViewModel>(json);

            return View(document);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string documentNode)
        {
            if (string.IsNullOrEmpty(documentNode))
                return NotFound();

            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{documentNode}");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = await response.Content.ReadAsStringAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // ==========================================================
        // DETAILS (optional)
        // ==========================================================
        public async Task<IActionResult> Details(string documentNode)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{documentNode}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var document = JsonConvert.DeserializeObject<DocumentViewModel>(json);

            return View(document);
        }
    }
}
///////////////////////////////////