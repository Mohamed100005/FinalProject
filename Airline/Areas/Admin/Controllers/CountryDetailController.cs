using DataAccess.Repos.IRepos;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Airline.Areas.Admin.Controllers
{
    public class CountryDetailController(ICountryRepo countryRepo) : Controller
    {
        private readonly ICountryRepo _countryRepo = countryRepo;
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
                return RedirectToAction(nameof(Index));
            }
            return View(country);

        }
    }
}
