using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Курсовая.Domain;
using Курсовая.Domain.Entity;

namespace Курсовая.Controllers
{
    public class CollectorController : Controller
    {
        DataManager _dataManager;
        public CollectorController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }



        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetCollectorAll()
        {
            var list = await _dataManager.collectorRepository.GetCollectorAllAsync();
            return View(list);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCollector(Collector collector)
        {
            await _dataManager.collectorRepository.SaveCollectorAsync(collector);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DellCollector(int id)
        {
            await _dataManager.collectorRepository.DelCollectorAsync(id);
            return RedirectToAction("GetCollectorAll");
        }
        [HttpGet]
        public async Task<IActionResult> Redact(int id)
        {
            Collector? collector = await _dataManager.collectorRepository.GetCollectorByIdAsync(id);
            return View(collector);
        }


    }
}
