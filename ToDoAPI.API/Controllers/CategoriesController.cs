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

        //get single item by ID
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


        public IHttpActionResult PostCategory(CategoryViewModel cats)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }
            db.Categories.Add(new Category()
            {
                CategoryName = cats.CategoryName,
                CategoryDescription = cats.CategoryDescription


            });
            db.SaveChanges();
            return Ok();
        }//end post

        public IHttpActionResult PutCategory(CategoryViewModel cats)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }
            Category existingCats = db.Categories.Where(c => c.CategoryID == cats.CategoryID).FirstOrDefault();
            {
                if (existingCats != null)
                {
                    existingCats.CategoryName = cats.CategoryName;
                    existingCats.CategoryDescription = cats.CategoryDescription;
                    db.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }//end if


        }//end put
        public IHttpActionResult DeleteCategroy(int id)
        {
            Category cats = db.Categories.Where(c => c.CategoryID == id).FirstOrDefault();

            if (cats != null)
            {
                db.Categories.Remove(cats);
                db.SaveChanges();
                return Ok();
            }

            else
            {
                return NotFound();
            }
        }//end DeleteCategory
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


    }//end class
}//end namespace
