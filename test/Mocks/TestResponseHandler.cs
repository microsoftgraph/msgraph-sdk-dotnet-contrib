using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Community.Test.Mocks
{
  internal class TestResponseHandler : IResponseHandler
  {
    public Task<T> HandleResponse<T>(HttpResponseMessage response)
    {
      throw new NotImplementedException();
    }
  }
}
