using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Community
{
  class NullHttpMessageLogger : IHttpMessageLogger
  {
    public Task WriteLine(string value)
    {
      return Task.CompletedTask;
    }
  }
}
