using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyNUnit;
using MyNUnitWeb.Models;
using System.IO;

namespace MyNUnitWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload(UploadAssembliesModel model)
        {
            var assemblies = model.Assymblies.Where(x => x.FileName.EndsWith(".dll"));
            Directory.CreateDirectory("tmp");
            var result = new TestResultModel();
            foreach (var assembly in assemblies)
            {
                using (var file = System.IO.File.Create("tmp/" + assembly.FileName))
                {
                    assembly.CopyTo(file);
                }
                
                TestSystem.RunTests("tmp");
                new DirectoryInfo("tmp").GetFiles().ToList().ForEach(x => x.Delete());
                
                var assemblyTestResult = new AssemblyTestResultModel()
                {
                    Name = assembly.FileName,
                    Succeeded = TestSystem.Succeeded.ToList(),
                    Failed = TestSystem.Failed.ToList(),
                    Ignored = TestSystem.Ignored.ToList()
                };

                result.AssemblyTestResults.Add(assemblyTestResult);
            }
            
            new DirectoryInfo("tmp").Delete(true);

            return View("Show", result);
        }
    }
}