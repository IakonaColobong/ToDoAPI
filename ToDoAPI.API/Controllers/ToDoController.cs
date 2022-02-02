using ToDoAPI.API.Models;
using ToDoAPI.DATA.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ToDoAPI.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ToDoController : ApiController
    {

        ToDoEntities db = new ToDoEntities();

        public IHttpActionResult GetToDo()
        {
            List<ToDoViewModel> toDo = db.ToDoItems.Include("Category").Select(t => new ToDoViewModel()
            {
                ToDoId = t.ToDoId,
                Action = t.Action,
                Done = t.Done,
                CategoryId = t.CategoryId,
            Category = new CategoryViewModel()
            {
                CategoryId = t.Category.CategoryId,
                CategoryName = t.Category.CategoryName,
                CategoryDescription = t.Categroy.CategoryDescription
            }


            }).ToList<ToDoViewModel>();

            if (toDo.Count == 0)

            {
                return NotFound();
            }//end if
            return Ok(toDo);

        }//end GetToDo



    }//end class
}//end namespace
