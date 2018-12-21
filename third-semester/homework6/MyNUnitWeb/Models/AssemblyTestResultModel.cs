using System.Collections.Generic;
using MyNUnit;

namespace MyNUnitWeb.Models
{
    public class AssemblyTestResultModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<TestMethodResultModel> Succeeded { get; set; }
        public List<TestMethodResultModel> Failed { get; set; }
        public List<TestMethodResultModel> Ignored { get; set; }

        public long TestResultId { get; set; }
        public TestResultModel TestResult { get; set; }
    }
}