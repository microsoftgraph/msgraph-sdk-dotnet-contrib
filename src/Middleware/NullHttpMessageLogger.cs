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
