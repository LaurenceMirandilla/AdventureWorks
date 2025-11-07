using AdventureWorks.UI.Models;
using AdventureWorksUI.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace AdventureWorks.UI.Controllers
{
    public class LocationController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5217/api/Location"; // change port if needed

        public LocationController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // INDEX
        public async Task<IActionResult> Index(string? search, int page = 1, int pageSize = 10)
        {
            var url = string.IsNullOrEmpty(search)
                ? $"{_baseUrl}?page={page}&pageSize={pageSize}"
                : $"{_baseUrl}?search={search}&page={page}&pageSize={pageSize}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Unable to load locations.";
                return View(new List<LocationViewModel>());
            }

            var content = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(content);
            var data = JsonConvert.DeserializeObject<List<LocationViewModel>>(result.data.ToString());

            return View(data);
        }

        // DETAILS
        public async Task<IActionResult> Details(short id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var location = JsonConvert.DeserializeObject<LocationViewModel>(json);
            return View(location);
        }

        // CREATE - GET
        public IActionResult Create() => View();

        // CREATE - POST
        [HttpPost]
        public async Task<IActionResult> Create(LocationViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var json = JsonConvert.SerializeObject(model);
            var response = await _httpClient.PostAsync(_baseUrl,
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to create location.";
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // EDIT - GET
        public async Task<IActionResult> Edit(short id)
        {
            try
            {
                var url = $"{_baseUrl}/{id}";
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    // Optionally read error content for logging
                    return NotFound();
                }

                var json = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(json))
                    return NotFound();

                var item = JsonConvert.DeserializeObject<LocationViewModel>(json);
                return item == null ? NotFound() : View(item);
            }
            catch (Exception ex)
            {
                // Add a model-level error so the view can show it (or log as needed)
                ModelState.AddModelError("", $"Error retrieving record: {ex.Message}");
                return View(new LocationViewModel());
            }
        }

        // EDIT - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, LocationViewModel model)
        {
            try
            {
                // Ensure the model has the correct id
                if (model == null)
                {
                    ModelState.AddModelError("", "Invalid data.");
                    return View(new LocationViewModel());
                }

                model.LocationId = id;

                if (!ModelState.IsValid) return View(model);

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{_baseUrl}/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                // Try to extract error details from response
                string errorDetail = string.Empty;
                try
                {
                    errorDetail = await response.Content.ReadAsStringAsync();
                }
                catch { /* ignore */ }

                var message = !string.IsNullOrEmpty(errorDetail)
                    ? $"Failed to update record: {errorDetail}"
                    : $"Failed to update record. Status: {response.StatusCode}";

                ModelState.AddModelError("", message);
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Exception while updating: {ex.Message}");
                return View(model);
            }
        }

        // DELETE - GET
        public async Task<IActionResult> Delete(short id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var location = JsonConvert.DeserializeObject<LocationViewModel>(json);
            return View(location);
        }

        // DELETE - POST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}

