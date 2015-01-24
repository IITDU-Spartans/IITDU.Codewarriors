using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeWarriors.IITDU.Models;

namespace CodeWarriors.IITDU.Repository
{
    interface ICatagoryRepository
    {
        bool AddCatagory(Catagory catagory);
        bool RemoveCatagory(Catagory catagory);
        bool UpdateCatagory(Catagory product);
        List<Catagory> GetAllCatagory();
        Catagory GetCatagoryByCatagoryId(int productId);

    }
}
