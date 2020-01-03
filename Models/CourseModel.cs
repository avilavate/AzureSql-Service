using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzureWebApp_SQL_Service.Models
{
    public class CourseModel
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        //   public ICollection<Module> Modules { get; set; }
    }

    //public class Module
    //{
    //    public string Name { get; set; }
    //    public ICollection<Clip> Clips { get; set; }
    //}

    //public class Clip
    //{
    //    public string Name { get; set; }
    //    public int Length { get; set; }
    //}
}
