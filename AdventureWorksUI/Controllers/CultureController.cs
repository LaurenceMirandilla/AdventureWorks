using AdventureWorks.UI.Models;
using AdventureWorksUI.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AdventureWorks.UI.Controllers
{
    public class CultureController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl = "http://localhost:5217/api/Culture";

        public CultureController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: /Culture
        public async Task<IActionResult> Index(string? search)
        {
            var client = _httpClientFactory.CreateClient();
            var url = string.IsNullOrEmpty(search)
                ? _baseUrl
                : $"{_baseUrl}?search={search}";

            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to load cultures from API.";
                return View(new List<CultureViewModel>());
            }

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<CultureViewModel>>(json);

            return View(data ?? new List<CultureViewModel>());
        }

        // GET: /Culture/Details/en
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var culture = JsonConvert.DeserializeObject<CultureViewModel>(json);

            return View(culture);
        }

        // GET: /Culture/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Culture/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CultureViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(_baseUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to create culture.";
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Culture/Edit/en
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var culture = JsonConvert.DeserializeObject<CultureViewModel>(json);

            return View(culture);
        }

        // POST: /Culture/Edit/en
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, CultureViewModel model)
        {
            if (id != model.CultureId)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{_baseUrl}/{id}", content);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to update culture.";
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: /Culture/Delete/en
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_baseUrl}/{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var culture = JsonConvert.DeserializeObject<CultureViewModel>(json);

            return View(culture);
        }

        // POST: /Culture/Delete/en
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_baseUrl}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to delete culture.";
                return RedirectToAction(nameof(Delete), new { id });
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
