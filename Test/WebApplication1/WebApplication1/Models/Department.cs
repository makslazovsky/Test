using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public int ParentId { get; set; }
    }
}
