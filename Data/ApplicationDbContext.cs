using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AzureWebApp_SQL_Service.Models;
using AzureWebApp_SQL_Service.Controllers;

namespace AzureWebApp_SQL_Service.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AzureWebApp_SQL_Service.Models.CourseModel> CourseModel { get; set; }
        public DbSet<AzureWebApp_SQL_Service.Controllers.ShowModel> ShowModel { get; set; }
    }
}
