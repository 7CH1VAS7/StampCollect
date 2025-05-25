using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]  // Страница создания Коллекции
        public IActionResult Index()
        {
            var stamps = _context.Stamps.ToList();
            var collectors = _context.Collectors.ToList();

            ViewBag.Stamps = new SelectList(stamps, "Id", "Name");
            ViewBag.Collector = new SelectList(collectors, "Id", "FullName");
            ViewBag.StampDictionary = stamps.ToDictionary(s => s.Id, s => s.Name);

            return View(new Collection());
        }


        [HttpGet] // Страница просмотра Коллекций
        public async Task<IActionResult> GetAllColection()
        {
            var stamps = _context.Stamps.ToList();
            var collectors = _context.Collectors.ToList();
           /* ViewBag.Stamps = new SelectList(stamps, "Id", "Name");
            ViewBag.Collector = new SelectList(collectors, "Id", "FullName");*/

            var list = await _dataManager.collectionRepository.GetCollectionAllAsync();
            return View(list);
        }
        [Authorize(Roles = "Admin")] // Запрос на удаление
        [HttpPost]
        public async Task<IActionResult> Del(int id)
        {
            await _dataManager.collectionRepository.DeleteCollectionAsync(id);
            return RedirectToAction("GetAllColection");
        }

        [HttpPost] // Сохранение отредактированной сущьности коллекции
        public async Task<IActionResult> ModyfyColectionnn(Collection collection, List<int> stampIds)
        {
            collection.Stamps.Clear();
            var selectedStamps = await _context.Stamps.Where(s => stampIds.Contains(s.Id)).ToListAsync();
            foreach (var stamp in selectedStamps)
            {
                stamp.LastBy = DateTime.Now;
            }
            await _dataManager.collectionRepository.UpperStamp(collection, stampIds);
            return RedirectToAction("Index");
        }
        [HttpPost] // Первичное сохранение для новой Коллекции
        public async Task<IActionResult> SaveColectionnn(Collection collection, List<int> stampIds)
        {
            collection.Stamps.Clear();
            var selectedStamps = await _context.Stamps.Where(s => stampIds.Contains(s.Id)).ToListAsync();

            foreach (var stamp in selectedStamps) 
            {
                stamp.LastBy = DateTime.Now;
            }
            collection.Stamps = selectedStamps;
            await _dataManager.collectionRepository.SaveCollectionAsync(collection);

            return RedirectToAction("Index");
        }
        
        [HttpGet] //Для загрузки данных в представление для редактирования Коллекции
        public async Task<IActionResult> ModCollect(int id)
        {
            Collection? collection = await _dataManager.collectionRepository.GetCollectionByIdAsync(id);
            
            if (collection == null) return NotFound();

            ViewBag.Collector = new SelectList(_context.Collectors, "Id", "FullName"); // Нужно переделать что бы небыло связи с контекстом БД
            ViewBag.StampDictionary = _context.Stamps.ToDictionary(s => s.Id, s => s.Name); // Нужно переделать что бы небыло связи с контекстом БД

            return View(collection);
        }
    }
}
