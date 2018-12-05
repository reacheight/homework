using System.Collections.Generic;

namespace MyNUnitWeb.Models
{
    public class TestResultModel
    {
        public List<AssemblyTestResultModel> AssemblyTestResults { get; set; } = new List<AssemblyTestResultModel>();
    }
}