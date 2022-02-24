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
        //getting all items in a table
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
        //getting a singular item
        public IHttpActionResult GetToDo(int id)
        {
            ToDoViewModel toDo = db.ToDoItems.Include("Category").Where(t => t.ToDoId == id).Select(t => new ToDoViewModel()
            {
                ToDoId = t.ToDoId,
                Action = t.Action,
                Done = t.Done,
                CategoryId = t.CategoryId,
                //Category = new CategoryViewModel()
                //{
                //    CategoryId = t.Category.CategoryID,
                //    CategoryName = t.Category.CategoryName,
                //    CategoryDescription = t.Category.CategoryDescription
                //}
            }).FirstOrDefault();

            if (toDo == null)
                return NotFound();

            return Ok(toDo);


        }//end GetResource

        public IHttpActionResult PostToDo(ToDoViewModel it)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }
            ToDoItem newToDoItem = new ToDoItem()
            {
                Action = it.Action,
                Done = it.Done,
                CategoryId = it.CategoryId
            };

            db.ToDoItems.Add(newToDoItem);
            db.SaveChanges();
            return Ok(newToDoItem);

        }//end post

        //begin PUT aka EDIT
        public IHttpActionResult PutToDoItem(ToDoViewModel toDoItem)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data");

            ToDoItem existingToDoItem = db.ToDoItems.Where(r => r.ToDoId == toDoItem.ToDoId).FirstOrDefault();

            if (existingToDoItem !=null)
            {
                existingToDoItem.ToDoId = toDoItem.ToDoId;
                existingToDoItem.Action = toDoItem.Action;
                existingToDoItem.Done = toDoItem.Done;
                existingToDoItem.CategoryId = toDoItem.CategoryId;
                db.SaveChanges();
                return Ok();

            }
            else
            {
                return NotFound();
            }
        }//end PUT

        //begin delete

        public IHttpActionResult DeleteToDoItem(int id)
        {
            //get the resource
            ToDoItem toDoItem = db.ToDoItems.Where(r => r.ToDoId == id).FirstOrDefault();

            //if the resource is not null, delete the resource
            if (toDoItem != null)
            {
                db.ToDoItems.Remove(toDoItem);
                db.SaveChanges();
                return Ok();
            }
            //if null - return NotFound()
            else
            {
                return NotFound();
            }
        }//end DeletResource

        //We use Dispose() below to dispose of any connections to the db after we are done with them - best practice to handle performance - dispose of instance of the controller and the instance of a db connection when we are done with it
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();//terminate the db object
            }
            //Below disposes of the instance of the controller
            base.Dispose(disposing);
        }





    }//end class
}//end namespace
