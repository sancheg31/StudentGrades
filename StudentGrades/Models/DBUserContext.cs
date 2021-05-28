using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace StudentGrades.Models
{
    public class DBUserContext: IdentityDbContext<User>
    {
        public DBUserContext(DbContextOptions<DBUserContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
