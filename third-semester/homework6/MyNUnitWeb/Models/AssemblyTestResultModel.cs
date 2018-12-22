using System.Collections.Generic;
using MyNUnit;

namespace MyNUnitWeb.Models
{
    /// <summary>
    /// Represents assembly test result model
    /// </summary>
    public class AssemblyTestResultModel
    {
        /// <summary>
        /// Model id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Name of the assembly
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of succeeded tests
        /// </summary>
        public List<TestMethodResultModel> Succeeded { get; set; }

        /// <summary>
        /// List of failed tests
        /// </summary>
        public List<TestMethodResultModel> Failed { get; set; }

        /// <summary>
        /// List of ignored tests
        /// </summary>
        public List<TestMethodResultModel> Ignored { get; set; }
    }
}