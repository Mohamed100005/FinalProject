using DataAccess.Repos.IRepos;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repos
{
    public class CategoryRepo : Repo<Category>, ICategoryRepo
    {
        public CategoryRepo(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
