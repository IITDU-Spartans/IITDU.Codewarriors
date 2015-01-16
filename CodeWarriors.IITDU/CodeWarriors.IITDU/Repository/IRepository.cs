using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodeWarriors.IITDU.Repository
{
    public interface IRepository<T>
    {
        bool Insert(T model);
        bool Update(T model, int id);
        T Get(int id);
    }
}