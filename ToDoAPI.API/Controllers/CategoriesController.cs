using ToDoAPI.API.Models;
using ToDoAPI.DATA.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;//adding for access to the cross object references.

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
                return NotFound(); //404 error
            }

            return Ok();
        }//end GetCategories
        public IHttpActionResult GetCategory(int id)
        {
            //Create the resource object and assign it to the results in the db
            CategoryViewModel cat = db.Categories.Where(c => c.CategoryID == id).Select(c => new CategoryViewModel()
            {
                CategoryID = c.CategoryID,
                CategoryName = c.CategoryName,
                CategoryDescription = c.CategoryDescription

            }).FirstOrDefault();

            if (cat == null)
            {
                return NotFound();
            }

            return Ok(cat);
        }//end GetCategory

        
        
        public IHttpActionResult PostCategory(CategoryViewModel cat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }
            db.Categories.Add(new Category()
            {
                CategoryName = cat.CategoryName,
                CategoryDescription = cat.CategoryDescription,

            });

            db.SaveChanges();
            return Ok();
        }//end Post

        
        public IHttpActionResult PutCategory(CategoryViewModel cat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }
            //Category Object created to pass the information to the db
            Category existingCat = db.Categories.Where(c => c.CategoryID == cat.CategoryID).FirstOrDefault();
            if (existingCat != null)

            {
                existingCat.CategoryName = cat.CategoryName;
                existingCat.CategoryDescription = cat.CategoryDescription;
                db.SaveChanges();
                return Ok();

            }
            else
            {
                return NotFound();//404 error
            }

        }//end putCategory


       
        //api/categories/id

        public IHttpActionResult DeleteCategroy(int id)
        {
            Category cat = db.Categories.Where(c => c.CategoryID == id).FirstOrDefault();

            if (cat != null)
            {
                db.Categories.Remove(cat);
                db.SaveChanges();
                return Ok();
            }

            else
            {
                return NotFound();
            }


        }//end DeletCategory


       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }//end Class
}//end namespace
