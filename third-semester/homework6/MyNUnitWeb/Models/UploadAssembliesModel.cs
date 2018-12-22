using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MyNUnitWeb.Models
{
    /// <summary>
    /// Represents assembly upload model
    /// </summary>
    public class UploadAssembliesModel
    {
        /// <summary>
        /// List of uploaded files
        /// </summary>
        public List<IFormFile> Assemblies { get; set; } = new List<IFormFile>();
    }
}