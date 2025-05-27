using Microsoft.AspNetCore.Authorization;
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


        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCollector(Collector collector)
        {
            await _dataManager.collectorRepository.SaveCollectorAsync(collector);
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]  ///////////////////////////////////////////////////////////////////////////////////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!  [HttpPost]
        public async Task<IActionResult> DellCollector(int id)
        {
            await _dataManager.collectorRepository.DelCollectorAsync(id);
            return RedirectToAction("GetCollectorAll");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Redact(int id)
        {
            Collector? collector = await _dataManager.collectorRepository.GetCollectorByIdAsync(id);
            return View(collector);
        }

        [HttpGet]
        public async Task<IActionResult> GetCollectorByStampRare()
        {
            var a = await _dataManager.collectorRepository.GetCollectorByStampRare();
            return View(a);

        }
        [HttpGet]
        public async Task<IActionResult> GetRich()
        {
            var Model = await _dataManager.collectorRepository.GetRichCollector();
            return View(Model);

        }
        [HttpGet]
        public async Task<IActionResult> BigRare()
        {
            var a = await _dataManager.collectorRepository.GetBigRare();
            return View(a);
        }
        [HttpGet]
        public async Task<IActionResult> TopCollectors()
        {
            var collectors = await _dataManager.collectorRepository.SortColl();
            return View(collectors);
        }
        [HttpGet]
        public async Task<IActionResult> GetOldStamps()
        {
            var collectors = await _dataManager.collectorRepository.OldStamps();
            return View(collectors);
        }

    }
}
