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
                    CategoryID = t.Category.CategoryID,
                    CategoryName = t.Category.CategoryName,
                    CategoryDescription = t.Category.CategoryDescription
                }


            }).ToList<ToDoViewModel>();

            if (toDo.Count == 0)

            {
                return NotFound();
            }//end if
            return Ok(toDo);

        }//end GetToDo

        public IHttpActionResult GetToDo(int id)
        {
            ToDoViewModel toDo = db.ToDoItems.Include("Category").Where(t => t.ToDoId == id).Select(t => new ToDoViewModel()
            {
                ToDoId = t.ToDoId,
                Action = t.Action,
                Done = t.Done,
                CategoryId = t.CategoryId,
                Category = new CategoryViewModel()
                {
                    CategoryID = t.Category.CategoryID,
                    CategoryName = t.Category.CategoryName,
                    CategoryDescription = t.Category.CategoryDescription
                }
            }).FirstOrDefault();

            if (toDo == null)
                return NotFound();

            return Ok(toDo);


        }//end GetResource



    }//end class
}//end namespace
