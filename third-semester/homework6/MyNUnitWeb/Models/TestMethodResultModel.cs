using MyNUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNUnitWeb.Models
{
    /// <summary>
    /// Represents method test result model
    /// </summary>
    public class TestMethodResultModel
    {
        /// <summary>
        /// Initializes new instance of <see cref="TestMethodResultModel"/>
        /// </summary>
        public TestMethodResultModel() { }


        /// <summary>
        /// Initializes new instance of <see cref="TestMethodResultModel"/>
        /// from <see cref="TestMethodResult"/> object
        /// </summary>
        /// <param name="result">method test result</param>
        public TestMethodResultModel(TestMethodResult result)
        {
            Name = result.Name;
            ExecutionTime = result.ExecutionTime;
            IgnoreMessage = result.IgnoreMessage;
        }

        /// <summary>
        /// Model id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Test name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Test execution time
        /// </summary>
        public long ExecutionTime { get; set; }

        /// <summary>
        /// Test ignore message
        /// </summary>
        public string IgnoreMessage { get; set; }
    }
}
