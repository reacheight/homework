using System.Collections.Generic;

namespace MyNUnitWeb.Models
{
    public class TestResultModel
    {
        public long Id { get; set; }
        public List<AssemblyTestResultModel> AssemblyTestResults { get; set; } = new List<AssemblyTestResultModel>();
    }
}