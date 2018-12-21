using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyNUnit;
using MyNUnitWeb.Models;
using System.IO;
using System.Reflection;

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
            var assemlbyFiles = model.Assymblies.Where(x => x.FileName.EndsWith(".dll"));
            var assemblies = assemlbyFiles.Select(a =>
            {
                Assembly assembly;
                using (var ms = new MemoryStream())
                {
                    a.CopyTo(ms);
                    assembly = Assembly.Load(ms.ToArray());
                }

                return assembly;
            }).ToList();

            var result = new TestResultModel();

            foreach (var assembly in assemblies)
            {
                TestSystem.RunTests(new List<Assembly> { assembly });
                var assemblyTestResult = new AssemblyTestResultModel()
                {
                    Name = assembly.GetName().Name,
                    Failed = TestSystem.Failed.ToList(),
                    Succeeded = TestSystem.Succeeded.ToList(),
                    Ignored = TestSystem.Ignored.ToList()
                };

                result.AssemblyTestResults.Add(assemblyTestResult);
            }

            return View("Show", result);
        }
    }
}