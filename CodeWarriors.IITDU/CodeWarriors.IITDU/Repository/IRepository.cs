using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeWarriors.IITDU.Models;

namespace CodeWarriors.IITDU.Repository
{
    public interface IRepository<T>
    {
        bool Add(T model);
        bool Remove(T model);
        bool Update(T model);
        List<T> GetAll();
        T Get(int id);
    }
    public class NullCatagory : Catagory
    {

    }

    public class NullSale : Sale
    {
        
    }

}