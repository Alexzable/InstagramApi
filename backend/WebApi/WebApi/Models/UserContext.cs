using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions<UserContext> options):base(options)
        {

        }
        public DbSet<User> UserContexts { get; set; }
    }
}
