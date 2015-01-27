using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeWarriors.IITDU.Models;

namespace CodeWarriors.IITDU.Repository
{
    interface ICategoryRepository
    {
        bool AddCatagory(Category category);
        bool RemoveCatagory(Category category);
        bool UpdateCatagory(Category product);
        List<Category> GetAllCatagory();
        Category GetCatagoryByCatagoryId(int productId);

    }
}
