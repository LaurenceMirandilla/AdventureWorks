using Microsoft.AspNetCore.Mvc;
using AdventureWorks.UI.Models;
using System.Net.Http.Json;

namespace AdventureWorks.UI.Controllers
{
    public class BillOfMaterialsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5217/api/BillOfMaterials";

        public BillOfMaterialsController(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient();
        }

        // GET: /BillOfMaterials
        public async Task<IActionResult> Index(int page = 1)
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponse<List<BillOfMaterialsViewModel>>>($"{_baseUrl}?page={page}");
            return View(response?.data ?? new List<BillOfMaterialsViewModel>());
        }

        // GET: /BillOfMaterials/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var item = await _httpClient.GetFromJsonAsync<BillOfMaterialsViewModel>($"{_baseUrl}/{id}");
            return item == null ? NotFound() : View(item);
        }

        // GET: /BillOfMaterials/Create
        public IActionResult Create() => View();

        // POST: /BillOfMaterials/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BillOfMaterialsViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _httpClient.PostAsJsonAsync(_baseUrl, model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Failed to create record.");
            return View(model);
        }

        // GET: /BillOfMaterials/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _httpClient.GetFromJsonAsync<BillOfMaterialsViewModel>($"{_baseUrl}/{id}");
            return item == null ? NotFound() : View(item);
        }

        // POST: /BillOfMaterials/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BillOfMaterialsViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            ModelState.AddModelError("", "Failed to update record.");
            return View(model);
        }


        // GET: /BillOfMaterials/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
                if (!response.IsSuccessStatusCode)
                    return NotFound();

                var item = await response.Content.ReadFromJsonAsync<BillOfMaterialsViewModel>();
                return View(item);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error loading record: {ex.Message}";
                return View("Error");
            }
        }

        // POST: /BillOfMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Bill of Materials deleted successfully.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to delete Bill of Materials.";
                    return RedirectToAction(nameof(Delete), new { id });
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting record: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        private class ApiResponse<T>
        {
            public int total { get; set; }
            public T data { get; set; }
        }
    }
}