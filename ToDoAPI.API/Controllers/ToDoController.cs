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

        //public IHttpActionResult PutCategory(CategoryViewModel cats)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Invalid Data");
        //    }
        //    Category exisitingCats = db.Categories.Where(c => c.CategoryID == cats.CategoryID).FirstOrDefault();
        //    if (existingCats != null)
        //    {
        //        exisitingCats.CategoryName = cats.CategoryName;
        //        exisitingCats.CategoryDescription = cats.CategoryDescription;
        //    }
        //}





    }//end class
}//end namespace
