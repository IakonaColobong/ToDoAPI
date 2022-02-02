using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //Added for access to annotations

namespace ToDoAPI.API.Models
{
    public class ToDoViewModel
    {
        [Key]
        public int ToDoId { get; set; }
        public string Action { get; set; }
        public bool Done { get; set; }
        public int CategoryId { get; set; }

        public virtual CategoryViewModel Category { get; set; }

    }

    public class CategoryViewModel
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
    }
}