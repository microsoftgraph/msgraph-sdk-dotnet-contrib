﻿using System;
using System.Text.Json.Serialization;
using Microsoft.Graph;

namespace Graph.Community
{
  public class SitePageVersion : BaseItem
  {
    public new int Id { get; set; }

    //public string CheckInComment { get; set; }
    public DateTime Created { get; set; }
    public bool IsCurrentVersion { get; set; }

    // Refer to https://github.com/pnp/pnpcore/issues/581
    public string Length { get; set; }

    [JsonIgnore]
    public long Size
    {
      get
      {
        if (long.TryParse(Length, out var size))
        {
          return size;
        }
        return 0;
      }

    }

    public string VersionLabel { get; set; }
  }
}
