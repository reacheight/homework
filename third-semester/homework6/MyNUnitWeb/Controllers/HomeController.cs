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
using Microsoft.EntityFrameworkCore;

namespace MyNUnitWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly TestResultContext _context;

        public HomeController(TestResultContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(UploadAssembliesModel model)
        {
            var assemblyFiles = model.Assymblies.Where(x => x.FileName.EndsWith(".dll"));
            var assemblies = assemblyFiles.Select(a =>
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
            await _context.TestResults.AddAsync(result);
            await _context.SaveChangesAsync();

            foreach (var assembly in assemblies)
            {
                TestSystem.RunTests(new List<Assembly> { assembly });
                var assemblyTestResult = new AssemblyTestResultModel()
                {
                    Name = assembly.GetName().Name,
                    Failed = TestSystem.Failed.Select(t => new TestMethodResultModel(t)).ToList(),
                    Succeeded = TestSystem.Succeeded.Select(t => new TestMethodResultModel(t)).ToList(),
                    Ignored = TestSystem.Ignored.Select(t => new TestMethodResultModel(t)).ToList()
                };

                assemblyTestResult.TestResult = result;
                await _context.AssemblyTestResults.AddAsync(assemblyTestResult);
                await _context.SaveChangesAsync();
            }

            return View("Show", result);
    }
        
        [HttpGet]
        public async Task<IActionResult> GetHistory()
        {
            var testResults = await _context.TestResults
                .Include(t => t.AssemblyTestResults)
                    .ThenInclude(a => a.Succeeded)
                .Include(t => t.AssemblyTestResults)
                    .ThenInclude(a => a.Failed)
                .Include(t => t.AssemblyTestResults)
                    .ThenInclude(a => a.Ignored)
                 .AsNoTracking().ToListAsync();

            return View("History", testResults);
        }
    }
}