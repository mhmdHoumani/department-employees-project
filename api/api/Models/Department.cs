using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Department
    {
        [Key]
        public int DeptID { get; set; }
        public string Name { get; set; }
    }
}
