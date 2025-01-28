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
            ViewBag.Country = _countryRepo.Get().ToList();
            return View(new City());
        }
        [HttpPost]
        public IActionResult Create(City city) {
            ModelState.Remove("Country");
            if (ModelState.IsValid) {
                _cityRepo.Create(city);
                _cityRepo.Attemp();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Country = _countryRepo.Get().ToList();
            return View(city);
        }
        public IActionResult Edit(int id) {
            var city = _cityRepo.GetOne(filter: e => e.Id == id);
            ViewBag.Country = _countryRepo.Get().ToList();
            return View(city);
        }
        [HttpPost]
        public IActionResult Edit(City city) {
            ModelState.Remove("Country");
            if (ModelState.IsValid) {
                _cityRepo.Edit(city);
                _cityRepo.Attemp();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Country = _countryRepo.Get().ToList();
            return View(city);
        }
        public IActionResult Delete(int id) {
            var city = _cityRepo.GetOne(filter: e => e.Id == id);
            _cityRepo.Delete(city);
            _cityRepo.Attemp();
            return RedirectToAction(nameof(Index));
        }
    }
}
