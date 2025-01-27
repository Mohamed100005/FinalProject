using DataAccess.Repos.IRepos;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Airline.Areas.Admin.Controllers {
    public class CityController(ICityRepo cityRepo , ICountryRepo countryRepo) : Controller {
        private readonly ICityRepo _cityRepo = cityRepo;
        private readonly ICountryRepo _countryRepo = countryRepo;

        public IActionResult Index() {
            var cities = _cityRepo.Get().ToList();
            return View(cities);
        }
        public IActionResult Create() {
            var countries = _countryRepo.Get().ToList();
            ViewBag.City = countries;
            return View(new City());
        }
        [HttpPost]
        public IActionResult Create(City city) {
            if (ModelState.IsValid) {
                _cityRepo.Create(city);
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }
        public IActionResult Edit(int id) {
            var city = _cityRepo.GetOne(filter: e => e.Id == id);
            return View(city);
        }
        [HttpPost]
        public IActionResult Edit(City city) {
            if (ModelState.IsValid) {
                _cityRepo.Edit(city);
                return RedirectToAction(nameof(Index));
            }
            return View(city);
        }
        public IActionResult Delete(int id) {
            var city = _cityRepo.GetOne(filter: e => e.Id == id);
            _cityRepo.Delete(city);
            return RedirectToAction(nameof(Index));
        }
    }
}
