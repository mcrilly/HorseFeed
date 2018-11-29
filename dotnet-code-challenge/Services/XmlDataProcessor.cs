using System.Collections.Generic;
using dotnet_code_challenge.Models;

namespace dotnet_code_challenge.Services
{
    public class XmlDataProcessor : IDataProcessor
    {
        /// <summary>
        /// Read the contents of the xml file and return the horses found
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public List<Horse> ReadFile(string fileName)
        {
            return new List<Horse>();
        }
    }
}
