using System.Collections.Generic;
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
    /// <summary>
    /// Main application controller
    /// </summary>
    public class HomeController : Controller
    {
        private readonly TestResultContext _context;

        /// <summary>
        /// Initializes new instance of <see cref="HomeController"/>
        /// </summary>
        /// <param name="context">app DbContext object</param>
        public HomeController(TestResultContext context)
            => _context = context;

        /// <summary>
        /// Action for main page view
        /// </summary>
        /// <returns>main page view</returns>
        public IActionResult Index()
            => View();

        /// <summary>
        /// Action for handling uploading and testing assemblies
        /// </summary>
        /// <param name="model">files upload model</param>
        /// <returns>test result view</returns>
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

            var assemblyTestResults = assemblies.Select(assembly =>
            {
                TestSystem.RunTests(new List<Assembly> { assembly });
                var assemblyTestResult = new AssemblyTestResultModel()
                {
                    Name = assembly.GetName().Name,
                    Failed = TestSystem.Failed.Select(t => new TestMethodResultModel(t)).ToList(),
                    Succeeded = TestSystem.Succeeded.Select(t => new TestMethodResultModel(t)).ToList(),
                    Ignored = TestSystem.Ignored.Select(t => new TestMethodResultModel(t)).ToList(),
                };

                return assemblyTestResult;
            })
            .ToList();

            await _context.AssemblyTestResults.AddRangeAsync(assemblyTestResults);
            await _context.SaveChangesAsync();

            return View("Show", assemblyTestResults);
    }
        
        /// <summary>
        /// Action for test history
        /// </summary>
        /// <returns>test history view</returns>
        [HttpGet]
        public async Task<IActionResult> GetHistory()
        {
            var testResults = await _context.AssemblyTestResults
                .Include(t => t.Succeeded)
                .Include(t => t.Failed)
                .Include(t => t.Ignored)
                .ToListAsync();

            return View("History", testResults);
        }
    }
}