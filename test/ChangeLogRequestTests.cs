using Microsoft.Graph;
using Microsoft.Graph.Core.Test.Mocks;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Graph.Community.Test
{
  public class ChangeLogRequestTests
  {
    private readonly ITestOutputHelper output;

    private readonly string mockWebUrl = "https://mock.sharepoint.com/sites/mockSite";

    public ChangeLogRequestTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public async Task GetChanges_ReturnsCorrectDerivedClasses()
    {
      // ARRANGE
      var responseContent = ResourceManager.GetHttpResponseContent("GetChangesResponse.json");
      var responseMessage = new HttpResponseMessage()
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(responseContent),
      };
      var query = new ChangeQuery()
      {
        Add = true
      };

      var mockAuthProvider = new MockAuthenticationProvider();
      var mockHttpProvider = new MockHttpProvider(responseMessage, new Graph.Community.Test.TestSerializer());
      var graphServiceClient = new GraphServiceClient(mockAuthProvider.Object, mockHttpProvider.Object);

      // ACT
      var response = await graphServiceClient
                    .SharePointAPI(mockWebUrl)
                    .Web
                    .Request()
                    .GetChangesAsync(query);
      var actual = response.CurrentPage;

      responseMessage.Dispose();

      // ASSERT
      Assert.Equal(5, actual.Count);
      Assert.IsType<ChangeSite>(actual[0]);
      Assert.IsType<ChangeUser>(actual[1]);
      Assert.IsType<ChangeItem>(actual[2]);
      Assert.IsType<ChangeWeb>(actual[3]);
      Assert.IsType<ChangeList>(actual[4]);
    }
  }
}
