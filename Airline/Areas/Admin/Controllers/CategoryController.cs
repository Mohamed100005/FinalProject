using DataAccess.Repos.IRepos;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;

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
            var cityCategories = _cityCategoryRepo.Get(filter: e => e.CategoryId == id).ToList();
            List<City> cities = new();
            foreach (var item in cityCategories) {
                cities = _cityRepo.Get(filter: e => e.Id == item.CityId).ToList();
            }
            var viewModel = new CityCategoryVM() {
                Category = category,
                Cities = cities
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Edit(Category category) {
            if (ModelState.IsValid) { 
            _categoryRepo.Edit(category);
            _categoryRepo.Attemp();
            return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        public IActionResult Delete(int id) {
            var category = _categoryRepo.GetOne(filter: e => e.Id == id);
            _categoryRepo.Delete(category);
            _categoryRepo.Attemp();
            return RedirectToAction(nameof(Index));
        }
    }
}
