using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ToDoAPI.API.Models;
using ToDoAPI.DATA.EF;

namespace ToDoAPI.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CategoriesController : ApiController
    {
        ToDoEntities db = new ToDoEntities();

        public IHttpActionResult GetCategories()
        {

            List<CategoryViewModel> cats = db.Categories.Select(c => new CategoryViewModel()
            {
                CategoryID = c.CategoryID,
                CategoryName = c.CategoryName,
                CategoryDescription = c.CategoryDescription

            }).ToList<CategoryViewModel>();

            if (cats.Count == 0)
            {
                return NotFound();
            }
            return Ok(cats);

        }//end categories


    }
}
