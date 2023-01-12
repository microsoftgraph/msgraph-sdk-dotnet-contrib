using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Graph;

namespace Graph.Community
{
  public class SitePageFileInfo : BaseItem
  {
    public new int Id
    {
      get
      {
        var idElement = GetListItemFieldElement("ID");
        if (idElement.ValueKind == JsonValueKind.Number)
        {
          return idElement.GetInt32();
        }
        return -1;
      }
      set { }
    }

    public string Title { get; set; }

    public new string Description
    {
      get
      {
        var idElement = GetListItemFieldElement("Description");
        if (idElement.ValueKind == JsonValueKind.String)
        {
          return idElement.GetString();
        }
        return null;
      }
      set { }
    }

    public SitePagePromotedState PromotedState { get; set; }

    public SitePageModerationStatus? ModerationStatus
    {
      get
      {
        var moderationStatusJsonElement = GetListItemFieldElement("OData__ModerationStatus");
        if (moderationStatusJsonElement.ValueKind == JsonValueKind.Number)
        {
          var moderationStatusInt = moderationStatusJsonElement.Deserialize<int>();
          return (SitePageModerationStatus)moderationStatusInt;
        }
        return null;
      }
    }

    public DateTimeOffset? FirstPublishedDateTime
    {
      get
      {
        var firstPublishedDateJsonElement = GetListItemFieldElement("FirstPublishedDate");
        if (firstPublishedDateJsonElement.ValueKind == JsonValueKind.String)
        {
          // we don't get a timezone offset. so forcing to utc
          var forcedOffsetCreated = firstPublishedDateJsonElement.ToString() + "Z";
          return DateTimeOffset.Parse(forcedOffsetCreated);
        }
        return null;
      }
      set { }
    }

    [JsonPropertyName("TimeLastModified")]
    public new DateTimeOffset? LastModifiedDateTime { get; set; }

    public string FileName
    {
      get { return this.Name; }
      set { }
    }

    public string UniqueId { get; set; }

    public List<int> ModernAudienceTargetUsers
    {
      get
      {
        var audienceJsonElement = GetListItemFieldElement("OData__ModernAudienceTargetUserFieldId");
        if (audienceJsonElement.ValueKind == JsonValueKind.Array)
        {
          return audienceJsonElement.Deserialize<List<int>>();
        }
        return null;
      }
    }

    [JsonPropertyName("TimeCreated")]
    public new DateTimeOffset? CreatedDateTime { get; set; }

    public UserInfo Author
    {
      get
      {
        var idElement = GetListItemFieldElement("AuthorId");
        if (idElement.ValueKind == JsonValueKind.Number)
        {
          var id = idElement.GetInt32();
          return new UserInfo() { Id = id };
        }
        return null;
      }
      set { }
    }

    public UserInfo Editor
    {
      get
      {
        var idElement = GetListItemFieldElement("EditorId");
        if (idElement.ValueKind == JsonValueKind.Number)
        {
          var id = idElement.GetInt32();
          return new UserInfo() { Id = id };
        }
        return null;
      }
      set { }
    }

    public SitePageCheckoutType CheckoutType { get; set; }

    public UserInfo CheckoutUser
    {
      get
      {
        var checkOutUserElement = GetListItemFieldElement("CheckoutUserId");
        if (checkOutUserElement.ValueKind == JsonValueKind.Number)
        {
          var id = checkOutUserElement.GetInt32();
          return new UserInfo() { Id = id };
        }
        return null;
      }
      set { }
    }

    public string ServerRelativeUrl { get; set; }

    private JsonElement GetListItemFieldElement(string fieldName)
    {
      JsonElement fieldElement = new JsonElement();
      if (this.AdditionalData.TryGetValue("ListItemAllFields", out object listItemAllFieldsRaw))
      {
        if (listItemAllFieldsRaw is JsonElement listItemAllFieldsJsonElement &&
            listItemAllFieldsJsonElement.ValueKind == JsonValueKind.Object)
        {
          _ = listItemAllFieldsJsonElement.TryGetProperty(fieldName, out fieldElement);
        }
      }
      return fieldElement;
    }
  }
}
