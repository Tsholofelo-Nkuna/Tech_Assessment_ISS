using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Question3.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question3.DataAccessLayer
{
    public class WebDbContext : IdentityDbContext
    {
        public WebDbContext(DbContextOptions<WebDbContext> options) : base(options) { }

        public virtual DbSet<ClientEntity> Clients { get; set; }

        public virtual DbSet<ContactPersonEntity> Contacts { get; set; }
    }
}
