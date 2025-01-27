using DataAccess.Repos.IRepos;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repos
{
    public class CountryRepo : Repo<Country>, ICountryRepo
    {
        public CountryRepo(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
