using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KressaFashionHub.Models.Data
{
    public class Database : DbContext
    {
            //the base accepts the name of the connection string provided in the web.config as a parameter
            //public Database()
            //    : base("Database")
            //{
            ////disable initializer
            //Database.SetInitializer<Database>(null);
            //}

            public DbSet<PageDTO> Pages { get; set; }
            public DbSet<SidebarDTO> Sidebar { get; set; }
            public DbSet<CategoriesDTO> Categories { get; set; }
            public DbSet<ProductDTO> Products { get; set; }
    }
   
}
    

