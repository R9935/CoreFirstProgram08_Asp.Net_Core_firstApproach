using System.ComponentModel.DataAnnotations;

namespace CoreFirstProgram08.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name  { get; set; }
        [Required]
        public string Mobile  { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public int Salary { get; set; }
        [Required]
        public string Dept { get; set; }
    }
}
