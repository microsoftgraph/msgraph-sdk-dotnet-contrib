using Microsoft.Graph.Core.Test.Mocks;
using Microsoft.Graph;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Graph.Community.Test
{
  public class ListFieldCollectionRequestTests
  {
    private readonly ITestOutputHelper output;

    private readonly string mockWebUrl = "https://mock.sharepoint.com/sites/mockSite";

    public ListFieldCollectionRequestTests(ITestOutputHelper output)
    {
      this.output = output;
    }

    [Fact]
    public void GeneratesCorrectRequestHeaders()
    {
      // ARRANGE
      var mockListId = Guid.NewGuid();
      using (var response = new HttpResponseMessage())
      using (var gsc = TestGraphServiceClient.Create(response))
      {
        // ACT
        var request = gsc.GraphServiceClient
                            .SharePointAPI(mockWebUrl)
                            .Web
                            .Lists[mockListId]
                            .Fields
                            .Request()
                            .GetHttpRequestMessage();

        // ASSERT
        Assert.Equal(SharePointAPIRequestConstants.Headers.AcceptHeaderValue, request.Headers.Accept.ToString());
        Assert.True(request.Headers.Contains(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName), $"Header does not contain {SharePointAPIRequestConstants.Headers.ODataVersionHeaderName} header");
        Assert.Equal(SharePointAPIRequestConstants.Headers.ODataVersionHeaderValue, string.Join(',', request.Headers.GetValues(SharePointAPIRequestConstants.Headers.ODataVersionHeaderName)));
      }

    }

    [Fact]
    public void GetFields_GeneratesCorrectRequest()
    {
      // ARRANGE
      var mockListId = Guid.NewGuid();
      var expectedUri = new Uri($"{mockWebUrl}/_api/web/lists('{mockListId}')/fields");

      using HttpResponseMessage response = new HttpResponseMessage();
      using TestGraphServiceClient gsc = TestGraphServiceClient.Create(response);

      // ACT
      var request = gsc.GraphServiceClient
                      .SharePointAPI(mockWebUrl)
                      .Web
                      .Lists[mockListId]
                      .Fields
                      .Request()
                      .GetAsync();

      // ASSERT
      gsc.HttpProvider.Verify(
        provider => provider.SendAsync(
          It.Is<HttpRequestMessage>(req =>
            req.Method == HttpMethod.Get &&
            req.RequestUri == expectedUri &&
            req.Headers.Authorization != null
          ),
          It.IsAny<HttpCompletionOption>(),
          It.IsAny<CancellationToken>()
        ),
        Times.Exactly(1)
      );
    }

    [Fact]
    public async Task GetFields_ReturnsCorrectDerivedClasses()
    {
      // ARRANGE
      var mockListId = Guid.NewGuid();
      var responseContent = ResourceManager.GetHttpResponseContent("GetListFieldsResponse.json");
      var responseMessage = new HttpResponseMessage()
      {
        StatusCode = HttpStatusCode.OK,
        Content = new StringContent(responseContent),
      };

      var mockAuthProvider = new MockAuthenticationProvider();
      var mockHttpProvider = new MockHttpProvider(responseMessage, new Graph.Community.Test.TestSerializer());
      var graphServiceClient = new GraphServiceClient(mockAuthProvider.Object, mockHttpProvider.Object);

      // ACT
      var response = await graphServiceClient
                    .SharePointAPI(mockWebUrl)
                    .Web
                    .Lists[mockListId]
                    .Fields
                    .Request()
                    .GetAsync();
      var actual = response.CurrentPage;

      responseMessage.Dispose();

      // ASSERT
      Assert.Equal(20, actual.Count);

      Assert.IsType<Field>(actual[0]);
      Assert.Equal("ID", actual[0].Title);
      Assert.Equal("ID", actual[0].InternalName);
      Assert.Equal("Counter", actual[0].TypeAsString);

      Assert.IsType<FieldText>(actual[3]);
      Assert.Equal("Title", actual[3].Title);
      Assert.Equal(255, (actual[3] as FieldText).MaxLength);

      Assert.IsType<FieldDateTime>(actual[4]);
      var actualDateTime = actual[4] as FieldDateTime;
      Assert.Equal("Modified", actualDateTime.Title);
      Assert.Null(actualDateTime.DateFormat);
      Assert.Equal(DateTimeFieldFormatType.DateTime, actualDateTime.DisplayFormat);

      Assert.IsType<FieldUser>(actual[6]);
      var actualUser = actual[6] as FieldUser;
      Assert.NotNull(actualUser.LookupList);
      Assert.Equal(FieldUserSelectionMode.PeopleAndGroups, actualUser.SelectionMode);

      Assert.IsType<FieldNumber>(actual[8]);
      var actualNumber = actual[8] as FieldNumber;
      Assert.True(actualNumber.CommaSeparator);
      Assert.Equal(1.7976931348623157E+308, actualNumber.MaximumValue);

      Assert.IsType<FieldChoice>(actual[10]);
      var actualChoice = actual[10] as FieldChoice;
      Assert.False(actualChoice.FillInChoice);
      Assert.Equal(5, actualChoice.Choices.Length);

      Assert.IsType<FieldMultiLineText>(actual[11]);
      var actualMultiLineText = actual[11] as FieldMultiLineText;
      Assert.Equal(6, actualMultiLineText.NumberOfLines);

      Assert.IsType<FieldLookup>(actual[13]);
      var actualLookup = actual[13] as FieldLookup;
      Assert.IsType<Guid>(actualLookup.LookupWebId);
      Assert.Equal("ID", actualLookup.PrimaryFieldId);
      Assert.False(actualLookup.AllowMultipleValues);

      Assert.IsType<FieldTaxonomy>(actual[15]);
      var actualTaxonomy = actual[15] as FieldTaxonomy;
      Assert.IsType<Guid>(actualTaxonomy.SspId);
      Assert.NotEqual(Guid.Empty, actualTaxonomy.TermSetId);
    }
  }
}
