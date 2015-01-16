using System.Web;
using System.Web.Mvc;

namespace CodeWarriors.IITDU
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}