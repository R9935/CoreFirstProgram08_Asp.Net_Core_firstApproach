using CoreFirstProgram08.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreFirstProgram08.DB_Content
{
    public class Add_G:DbContext
    {
        internal readonly object UserInfo;

        public Add_G(DbContextOptions<Add_G> options): base(options)
        {

        } 
        public DbSet<Employee> Employees { get; set; }  
        public DbSet<UserInfo> Users { get; set; }  
    }
}
