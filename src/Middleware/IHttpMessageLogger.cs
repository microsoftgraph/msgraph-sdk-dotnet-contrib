using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Graph.Community
{
  public interface IHttpMessageLogger
  {
    Task WriteLine(string value);
  }
}
