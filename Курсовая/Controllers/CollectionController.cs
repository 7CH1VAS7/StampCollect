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
            ViewBag.StampDictionary = stamps.ToDictionary(s => s.Id, s => s.Name);

            return View(new Collection());
        }


        [HttpGet]
        public async Task<IActionResult> GetAllColection()
        {
            var stamps = _context.Stamps.ToList();
            var collectors = _context.Collectors.ToList();
           /* ViewBag.Stamps = new SelectList(stamps, "Id", "Name");
            ViewBag.Collector = new SelectList(collectors, "Id", "FullName");*/

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
        public async Task<IActionResult> ModyfyColectionnn(Collection collection, List<int> stampIds)
        {
            await _dataManager.collectionRepository.UpperStamp(collection, stampIds);
            return RedirectToAction("Index");

            //Ошибка трекинга

            /*var sre = await _dataManager.collectionRepository.GetCollectionByIdAsync(collection.Id);
            sre.Stamps.Clear();
            await _dataManager.collectionRepository.SaveCollectionAsync(sre);*/


            /* var selectedStamps = await _context.Stamps.Where(s => stampIds.Contains(s.Id)).ToListAsync();

             collection.Stamps = selectedStamps;
             await _dataManager.collectionRepository.SaveCollectionAsync(collection);

             return RedirectToAction("Index");*/
        }







        [HttpPost]
        public async Task<IActionResult> SaveColectionnn(Collection collection, List<int> stampIds)
        {
            collection.Stamps.Clear();
            var selectedStamps = await _context.Stamps.Where(s => stampIds.Contains(s.Id)).ToListAsync();
            
            collection.Stamps = selectedStamps;
            await _dataManager.collectionRepository.SaveCollectionAsync(collection);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> ModCollect(int id)
        {
            Collection? collection = await _dataManager.collectionRepository.GetCollectionByIdAsync(id);
            /*Collection? collect = await _context.Collections
                .Include(c => c.Stamps) // Важно: загружаем связанные марки
                .FirstOrDefaultAsync(c => c.Id == id);*/

            if (collection == null) return NotFound();

            ViewBag.Collector = new SelectList(_context.Collectors, "Id", "FullName"); // Нужно переделать что бы небыло связи с контекстом БД
            ViewBag.StampDictionary = _context.Stamps.ToDictionary(s => s.Id, s => s.Name); // Нужно переделать что бы небыло связи с контекстом БД

            return View(collection);
        }
    }
}
