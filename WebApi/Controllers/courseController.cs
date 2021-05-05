using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using SchedulerModel;
using ApiRepo;

namespace WebApi.Controllers
{
    [RoutePrefix("Courses")]
    public class courseController : ApiController
    {
        //[HttpGet, Route("BookList")]
        //public IEnumerable<Course> GetBooks()
        //{
        //    //IRepository<Course> book_repo = Repos.get_book_repo();

        //    //List<book> books = book_repo.FindAll();

        //    //return books;
        //}
    }

    class configFile
    {
        public static string getSetting(string key)
        {
            return System.Web.Configuration.WebConfigurationManager.AppSettings[key];
        }
    }
}
