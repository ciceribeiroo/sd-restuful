using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_restful.Models
{
    public class ToDoList
    {
        public int id { get; set; }
        public string description { get; set; }
        public DateTime deadLine { get; set; }
        public bool complete { get; set; }
    }
}
