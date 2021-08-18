using System;
using System.Threading.Tasks;

namespace Graph.Community.Samples
{
  class ConsoleHttpMessageLogger : IHttpMessageLogger
  {
    public async Task WriteLine(string value)
    {
      Console.WriteLine(value);
    }
  }
}
