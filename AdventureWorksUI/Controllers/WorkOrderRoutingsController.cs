using AdventureWorksUI.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AdventureWorks.UI.Controllers
{
    public class WorkOrderRoutingController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5217/api/WorkOrderRouting";

        public WorkOrderRoutingController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // ✅ INDEX
        public async Task<IActionResult> Index(int? workOrderId = null)
        {
            var url = workOrderId.HasValue ? $"{_baseUrl}?workOrderId={workOrderId}" : _baseUrl;
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to fetch data.";
                return View(new List<WorkOrderRoutingViewModel>());
            }

            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<WorkOrderRoutingViewModel>>(content);
            return View(data);
        }

        // ✅ DETAILS
        public async Task<IActionResult> Details(int workOrderId, short operationSequence)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{workOrderId}?operationSequence={operationSequence}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<WorkOrderRoutingViewModel>(json);
            return View(model);
        }

        // ✅ CREATE (GET)
        public IActionResult Create() => View();

        // ✅ CREATE (POST)
        [HttpPost]
        public async Task<IActionResult> Create(WorkOrderRoutingViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var json = JsonConvert.SerializeObject(model);
            var response = await _httpClient.PostAsync(_baseUrl, new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to create record.";
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // ✅ EDIT (GET)
        public async Task<IActionResult> Edit(int workOrderId, short operationSequence)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{workOrderId}?operationSequence={operationSequence}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<WorkOrderRoutingViewModel>(json);
            return View(model);
        }

        // ✅ EDIT (POST)
        [HttpPost]
        public async Task<IActionResult> Edit(int workOrderId, short operationSequence, WorkOrderRoutingViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var json = JsonConvert.SerializeObject(model);
            var response = await _httpClient.PutAsync($"{_baseUrl}/{workOrderId}?operationSequence={operationSequence}", new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Failed to update record.";
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        // ✅ DELETE (GET)
        public async Task<IActionResult> Delete(int workOrderId, short operationSequence)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{workOrderId}?operationSequence={operationSequence}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<WorkOrderRoutingViewModel>(json);
            return View(model);
        }

        // ✅ DELETE (POST)
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int workOrderId, short operationSequence)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{workOrderId}?operationSequence={operationSequence}");
            if (!response.IsSuccessStatusCode)
                ViewBag.Error = "Failed to delete record.";

            return RedirectToAction(nameof(Index));
        }
    }
}

