using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Курсовая.Domain;
using Курсовая.Domain.Entity;
using Курсовая.Models;

namespace Курсовая.Controllers
{
    public class CollectionController : Controller
    {
        DataManager _dataManager;
        AppDbContext _context;
        public CollectionController(DataManager dataManager, AppDbContext context) 
        {
            _dataManager = dataManager;
            _context = context;

        }

        public IActionResult Index()
        {
            var stamps = _context.Stamps.ToList();
            var collectors = _context.Collectors.ToList();

            ViewBag.Stamps = new SelectList(stamps, "Id", "Name");
            ViewBag.Collector = new SelectList(collectors, "Id", "FullName");

            return View(new Collection());
        }


        [HttpGet]
        public async Task<IActionResult> GetAllColection()
        {
            var stamps = _context.Stamps.ToList();
            var collectors = _context.Collectors.ToList();
            ViewBag.Stamps = new SelectList(stamps, "Id", "Name");
            ViewBag.Collector = new SelectList(collectors, "Id", "FullName");


            var list = await _dataManager.collectionRepository.GetCollectionAllAsync();
            return View(list);
        }
        [HttpPost]
        public async Task<IActionResult> Del(int id)
        {
            await _dataManager.collectionRepository.DeleteCollectionAsync(id);
            return RedirectToAction("GetAllColection");
        }
        [HttpPost]
        public async Task<IActionResult> SaveColectionnn(Collection collection, List<int> stampIds)
        {
            var selectedStamps = await _context.Stamps.Where(s => stampIds.Contains(s.Id)).ToListAsync();

            collection.Stamps = selectedStamps;

            await _dataManager.collectionRepository.SaveCollectionAsync(collection);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> ModCollect(int id)
        {
            Collection? collect = await _dataManager.collectionRepository.GetCollectionByIdAsync(id);
            var stamps = _context.Stamps.ToList();
            var collectors = _context.Collectors.ToList();

            ViewBag.Stamps = new SelectList(stamps, "Id", "Name");
            ViewBag.Collector = new SelectList(collectors, "Id", "FullName");
            return View(collect);
        }
    }
}
