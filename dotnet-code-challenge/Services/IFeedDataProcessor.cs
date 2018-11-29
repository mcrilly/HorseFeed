using System.Collections.Generic;
using dotnet_code_challenge.Models;

namespace dotnet_code_challenge.Services
{
    public interface IFeedDataProcessor
    {
        List<Horse> ReadFile(string fileName);
    }
}
