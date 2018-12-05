using System.Collections.Generic;
using MyNUnit;

namespace MyNUnitWeb.Models
{
    public class AssemblyTestResultModel
    {
        public string Name { get; set; }
        public List<TestMethodResult> Succeeded { get; set; }
        public List<TestMethodResult> Failed { get; set; }
        public List<TestMethodResult> Ignored { get; set; }
    }
}