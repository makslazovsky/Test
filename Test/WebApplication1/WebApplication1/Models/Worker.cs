using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Worker
    {
        public int WorkerId { get; set; }

        public int DepartmentId { get; set; }
        public string WorkerName { get; set; }

        public string Email { get; set;}
    }
}
