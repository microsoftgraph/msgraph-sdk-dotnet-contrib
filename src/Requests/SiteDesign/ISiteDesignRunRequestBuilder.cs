using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
  public interface ISiteDesignRunRequestBuilder:IBaseRequestBuilder
  {
    ISiteDesignRunRequest Request();
  }
}
