using System;
using System.ComponentModel.DataAnnotations;

namespace AzureWebApp_SQL_Service.Controllers
{
    public class ShowModel
    {
        [Key]
        public Uri URI { get; set; }
    }
}