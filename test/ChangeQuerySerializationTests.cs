using Microsoft.Graph;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Graph.Community.Test
{
  public class ChangeQuerySerializationTests
  {
    private readonly ITestOutputHelper output;

    private readonly Serializer ser;

    public ChangeQuerySerializationTests(ITestOutputHelper output)
    {
      this.output = output;
      this.ser = new Serializer();
    }

    public static IEnumerable<object[]> GetChangeQueries()
    {
      yield return new object[]
      {
        new ChangeQuery(true,true),
				"{\"Add\":true,\"Alert\":true,\"ContentType\":true,\"DeleteObject\":true,\"Field\":true,\"File\":true,\"Folder\":true,\"Group\":true,\"GroupMembershipAdd\":true,\"GroupMembershipDelete\":true,\"Item\":true,\"List\":true,\"Move\":true,\"Navigation\":true,\"Rename\":true,\"Restore\":true,\"RoleAssignmentAdd\":true,\"RoleAssignmentDelete\":true,\"RoleDefinitionAdd\":true,\"RoleDefinitionDelete\":true,\"RoleDefinitionUpdate\":true,\"SecurityPolicy\":true,\"Site\":true,\"SystemUpdate\":true,\"Update\":true,\"User\":true,\"View\":true,\"Web\":true}"
      };
      yield return new object[]
      {
        new ChangeQuery(true,true){ ChangeTokenStart = new ChangeToken(){StringValue="1;2;7d111794-5955-4213-9cb2-a3de3e012d85;637648157869800000;597759288" } },
        "{\"Add\":true,\"Alert\":true,\"ChangeTokenStart\":{\"StringValue\":\"1;2;7d111794-5955-4213-9cb2-a3de3e012d85;637648157869800000;597759288\"},\"ContentType\":true,\"DeleteObject\":true,\"Field\":true,\"File\":true,\"Folder\":true,\"Group\":true,\"GroupMembershipAdd\":true,\"GroupMembershipDelete\":true,\"Item\":true,\"List\":true,\"Move\":true,\"Navigation\":true,\"Rename\":true,\"Restore\":true,\"RoleAssignmentAdd\":true,\"RoleAssignmentDelete\":true,\"RoleDefinitionAdd\":true,\"RoleDefinitionDelete\":true,\"RoleDefinitionUpdate\":true,\"SecurityPolicy\":true,\"Site\":true,\"SystemUpdate\":true,\"Update\":true,\"User\":true,\"View\":true,\"Web\":true}"
      };
      yield return new object[]
      {
        new ChangeQuery(){Add=true },
        "{\"Add\":true}"
      };
    }

    [Theory]
    [MemberData(nameof(GetChangeQueries))]
    public void EmptyProperties_SerializesCorrectly(ChangeQuery qry, string expectedSerialization)
    {
      // ARRANGE

      // ACT
      var actual = ser.SerializeObject(qry);

      // ASSERT
      Assert.Equal(expectedSerialization, actual);
    }

  }
}
