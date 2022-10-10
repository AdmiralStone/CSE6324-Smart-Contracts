using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NethereumUI.Models
{
    public class ToDoListModel
    {
        public int ToDoId { get; set; }
        public int Count { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public bool Task { get; set; }
        public string ModifiedDate { get; set; }
        public List<ToDoListModel> toDoLists { get; set; }
    }
}