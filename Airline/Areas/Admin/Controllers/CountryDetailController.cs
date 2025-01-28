using DataAccess.Repos;
using DataAccess.Repos.IRepos;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Airline.Areas.Admin.Controllers
{
    public class CountryDetailController(ICountryRepo countryRepo, ICityRepo cityRepo) : Controller
    {
        private readonly ICountryRepo _countryRepo = countryRepo;
        private readonly ICityRepo _cityRepo = cityRepo;
        public IActionResult Index()
        {
            var countries = _countryRepo.Get().ToList();
                return View(countries);
        }
        public IActionResult Create() {
            return View(new Country());
        }
        [HttpPost]
        public IActionResult Create(Country country) {
            if (ModelState.IsValid) { 
            _countryRepo.Create(country);
                _countryRepo.Attemp();
                return RedirectToAction(nameof(Index));
            }
            return View(country);

        }
        public IActionResult Edit(int id) {
            var country = _countryRepo.GetOne(filter: e => e.Id == id);
            return View(country);
        }
        [HttpPost]
        public IActionResult Edit(Country country) {
            if (ModelState.IsValid) {
                _countryRepo.Edit(country);
                _countryRepo.Attemp();
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }
        public IActionResult Delete(int id) {
            var category = _countryRepo.GetOne(filter: e => e.Id == id);
            _countryRepo.Delete(category);
            _countryRepo.Attemp();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Details(int id) {
            var country = _countryRepo.GetOne(e => e.Id == id);
            ViewBag.City = _cityRepo.Get(e => e.CountryId == id);
            return View(country);
        }
    }
}
