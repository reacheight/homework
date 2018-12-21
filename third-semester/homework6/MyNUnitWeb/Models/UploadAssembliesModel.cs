using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MyNUnitWeb.Models
{
    public class UploadAssembliesModel
    {
        [Required]
        public List<IFormFile> Assymblies { get; set; } = new List<IFormFile>();
    }
}