using DataAccess.Repos.IRepos;
using Microsoft.AspNetCore.Mvc;
using Models;
using Stripe;
namespace Airline.Areas.Admin.Controllers {
    public class CategoryController(ICategoryRepo categoryRepo , ICityRepo cityRepo, ICityCategoryRepo cityCategoryRepo) : Controller {
        private readonly ICategoryRepo _categoryRepo = categoryRepo;
        private readonly ICityCategoryRepo _cityCategoryRepo = cityCategoryRepo;
        private readonly ICityRepo _cityRepo = cityRepo;
        public IActionResult Index() {
            var categories = _categoryRepo.Get();
            return View(categories);
        }
        public IActionResult Create() {

            return View(new Category());
        }
        [HttpPost]
        public IActionResult Create(Category category) {
            if (ModelState.IsValid) { 
                _categoryRepo.Create(category);
                _categoryRepo.Attemp();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        public IActionResult Edit(int id) {
            var category = _categoryRepo.GetOne(filter: e => e.Id == id);
            ViewBag.SelectedCities = _cityCategoryRepo.Get(filter: e => e.CategoryId == id).Select(e => e.CityId).ToList();
            ViewBag.City = _cityRepo.Get().ToList();
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category , int cityId) {
            if (ModelState.IsValid) {
                var finded = _cityCategoryRepo.GetOne(filter: e => e.CategoryId == category.Id && e.CityId == cityId);
                if (finded == null) {
                    _cityCategoryRepo.Create(new CityCategory() {
                        CategoryId = category.Id,
                        CityId = cityId
                    });
                    _categoryRepo.Edit(category);
                }
                else {
                    _categoryRepo.Edit(category);
                    _categoryRepo.Attemp();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(category);
        }
        public IActionResult Delete(int id) {
            var category = _categoryRepo.GetOne(filter: e => e.Id == id);

            _categoryRepo.Delete(category);
            _categoryRepo.Attemp();
            return RedirectToAction(nameof(Index));
        }
        //public IActionResult Details(int id) { 
        //var cities = _cityCategoryRepo.Get(filter: e => e.CategoryId == id).ToList();
        //    List<int> ints = new();
        //    foreach (var item in cities) {
        //        ints.Add(item.CityId);
        //    }
        //    foreach (var item in ints) {
        //        ViewBag.Cities = _cityRepo.Get(filter: e => e.Id != item).ToList();
        //    }
        //}
    }
}
