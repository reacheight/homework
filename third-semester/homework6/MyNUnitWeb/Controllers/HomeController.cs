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
            var assemblies = model.Assemblies
                .Where(file => file.FileName.EndsWith(".dll"))
                .Select(assemblyFile =>
                {
                    Assembly assembly;
                    using (var memoryStream = new MemoryStream())
                    {
                        assemblyFile.CopyTo(memoryStream);
                        assembly = Assembly.Load(memoryStream.ToArray());
                    }

                    return assembly;
                })
                .ToList();

            var result = new TestResultModel();
            await _context.TestResults.AddAsync(result);
            await _context.SaveChangesAsync();

            var assemblyTestResults = assemblies.Select(assembly =>
            {
                TestSystem.RunTests(new List<Assembly> { assembly });
                var assemblyTestResult = new AssemblyTestResultModel()
                {
                    Name = assembly.GetName().Name,
                    Failed = TestSystem.Failed.Select(t => new TestMethodResultModel(t)).ToList(),
                    Succeeded = TestSystem.Succeeded.Select(t => new TestMethodResultModel(t)).ToList(),
                    Ignored = TestSystem.Ignored.Select(t => new TestMethodResultModel(t)).ToList(),
                    TestResult = result
                };

                return assemblyTestResult;
            });

            await _context.AssemblyTestResults.AddRangeAsync(assemblyTestResults);
            await _context.SaveChangesAsync();

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