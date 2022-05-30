using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly IToastNotification _toastNotification;

        public CategoryController(ApplicationDbContext db, IToastNotification toastNotification)
        {
            _db = db;
            _toastNotification = toastNotification;
        }

        public IActionResult Index()
        {
           IEnumerable<Category> objCategoryList = _db.Categories;
           var objeto = objCategoryList.Select(x => new CategoryViewModel()
            {
                Name = x.Name,
                DisplayOrder = x.DisplayOrder,
                Id = x.Id
            }) ;
            return View(objeto);
        }

        //GET

        
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create (CategoryViewModel obj) 
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The DisplayOrder can't have the same value of Name");
            }

            if (ModelState.IsValid) 
            {
                var categoryToDb = new Category(obj.Name, obj.DisplayOrder);

                _db.Categories.Add(categoryToDb);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ErroCreate = "deu algum erro aí amigão";
            return View(obj);
        }

        //PUT

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound("Id is null or 0 please select a existent object");
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDb = _db.Categories.FirstOrDefault(c => c.Id == id);

            if (categoryFromDb == null) return NotFound();

            var categoryFromDbViewModel = new CategoryViewModel()
            {
                Id = categoryFromDb.Id,
                Name = categoryFromDb.Name,
                DisplayOrder=categoryFromDb.DisplayOrder,
            };


            return View(categoryFromDbViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryViewModel obj)
        {
            if (ModelState.IsValid) 
            {
                var category = _db.Categories.Find(obj.Id);

                if (category == null) return NotFound();

                category.AtualizarDados(obj.Name, obj.DisplayOrder);

                _db.Update(category);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            ViewBag.Erros = "Erro cacete, que saco, vê aí oq deu errado é dev msm";
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {

            var CategoryFromDb = _db.Categories.Find(id);

            if (CategoryFromDb == null) return NotFound();

            _db.Categories.Remove(CategoryFromDb);
            _db.SaveChanges();
            _toastNotification.AddSuccessToastMessage("O item foi excluído com sucesso");
            return RedirectToAction(nameof(Index));
        }


    }
}
