using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp.Model
{
    public class TodoItem
    {
        public TodoItem(string description)
        {
            this.Descrption = description;
        }
        public string Id { get; set; }

        public string Descrption { get; set; }

        public bool IsCompleted { get; set; }
    }
}
