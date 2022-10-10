using System;
using System.Collections.Generic;

namespace NethereumWebUI.Data
{
    public partial class ToDoList
    {
        public int ToDoId { get; set; }
        public string? UserName { get; set; }
        public string? Description { get; set; }
        public bool Task { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
