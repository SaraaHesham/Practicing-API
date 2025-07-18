﻿using System.ComponentModel.DataAnnotations.Schema;

namespace API_ITI.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
