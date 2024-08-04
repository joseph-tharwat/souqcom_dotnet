using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace souqcomApp.Models
{
    public class MyIdentityUser : IdentityUser
    {
        public MyIdentityUser()
        { 
        } 
    }

    public class IdentityContext: IdentityDbContext<MyIdentityUser>
    {

        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }
    }

   
}
