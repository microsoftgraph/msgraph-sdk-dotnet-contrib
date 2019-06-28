﻿using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.Community
{
	public class SiteDesignRequestBuilder : BaseRequestBuilder, ISiteDesignRequestBuilder
	{
		private IEnumerable<Option> options;

		public SiteDesignRequestBuilder(
				string requestUrl,
				IBaseClient client,
				IEnumerable<Option> options = null)
				: base(requestUrl, client)
		{
			this.options = options;
		}

		/// <summary>
		/// Builds the request.
		/// </summary>
		/// <returns>The built request.</returns>
		public ISiteDesignRequest Request()
		{
			return this.Request(this.options);
		}

		/// <summary>
		/// Builds the request.
		/// </summary>
		/// <param name="options">The query and header options for the request.</param>
		/// <returns>The built request.</returns>
		public ISiteDesignRequest Request(IEnumerable<Option> options)
		{
			return new SiteDesignRequest(this.RequestUrl, this.Client, options);
		}

		/// <summary>
		/// Gets an <see cref="ISiteDesignRequestBuilder"/> for the specified SiteDesign.
		/// </summary>
		/// <param name="id">The ID for the SiteDesign.</param>
		/// <returns>The <see cref="ISiteDesignRequestBuilder"/>.</returns>
		public ISiteDesignRequestBuilder this[Guid id]
		{
			get
			{
				List<QueryOption> options = new List<QueryOption>() { new QueryOption("id", id.ToString()) };
				return new SiteDesignRequestBuilder(this.RequestUrl, this.Client, options);
			}
		}

	}
}
