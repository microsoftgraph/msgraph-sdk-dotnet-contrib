using System.Threading;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace Graph.Community
{
  public interface IListFieldRequest : IBaseRequest
  {
    Task<Field> GetAsync();
    Task<Field> GetAsync(CancellationToken cancellationToken); 
  }
}
