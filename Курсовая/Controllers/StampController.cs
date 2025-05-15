using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Курсовая.Domain;
using Курсовая.Domain.Entity;

namespace Курсовая.Controllers
{
    public class StampController : Controller
    {
        DataManager dataManager;
        public StampController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }




        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Details(Stamp stamp) {
            await dataManager.stampRepository.SaveStampAsync(stamp);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var a = await dataManager.stampRepository.GetStampAllAsync();
            return View(a);
        }
        [HttpPost]
        public async Task <IActionResult> Del(int id)
        {
            await dataManager.stampRepository.DelStamp(id);
            return RedirectToAction("Get");
        }
        [HttpGet]
        public async Task <IActionResult> Redact(int id)
        {
            Stamp? stamp = await dataManager.stampRepository.GetStampByIdAsync(id);
            return View(stamp);
        }


    }
}
