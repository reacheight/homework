using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace MyNUnitWeb.Models
{
    public class UploadAssembliesModel
    {
        public List<IFormFile> Assymblies { get; set; }
    }
}