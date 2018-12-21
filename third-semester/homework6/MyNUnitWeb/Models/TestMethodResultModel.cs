using MyNUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNUnitWeb.Models
{
    public class TestMethodResultModel
    {
        public TestMethodResultModel() { }

        public TestMethodResultModel(TestMethodResult result)
        {
            Name = result.Name;
            ExecutionTime = result.ExecutionTime;
            Status = result.Status;
            IgnoreMessage = result.IgnoreMessage;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public TestMethodStatus Status { get; set; }
        public long ExecutionTime { get; set; }
        public string IgnoreMessage { get; set; }
    }
}
