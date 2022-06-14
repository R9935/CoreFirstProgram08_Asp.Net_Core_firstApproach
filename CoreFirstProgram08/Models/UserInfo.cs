using System.ComponentModel.DataAnnotations;

namespace CoreFirstProgram08.Models
{
    public class UserInfo
    {
        [Key]
        public int ID { get; set; } 
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
