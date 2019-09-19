/**
 * -------------------------------------------------------------------------------------------
 * Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.
 * See License in the project root for license information.
 * -------------------------------------------------------------------------------------------
*/

using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Graph.Community
{
	public class TestingHandlerOption : IMiddlewareOption
	{
		public TestingStrategy TestingStrategy { get; set; }
		public HttpStatusCode? StatusCode { get; set; }
		public Error ErrorResource { get; set; }

		/// <summary>
		/// Constructs a new <see cref="TestingHandlerOption"/> with <see cref="TestingStrategy"/> set to <see cref="TestingStrategy.Random"/>
		/// </summary>
		public TestingHandlerOption()
		{
			this.TestingStrategy = TestingStrategy.Random;
		}

		/// <summary>
		/// Constructs a new <see cref="TestingHandlerOption"/> with <see cref="TestingStrategy"/> set to <see cref="TestingStrategy.Manual"/>
		/// </summary>
		/// <param name="statusCode">The <see cref="HttpStatusCode"/> to set on the response</param>
		/// <param name="errorResource">The <see cref="Error"/> resource to return in the response body</param>
		public TestingHandlerOption(HttpStatusCode statusCode, Error errorResource = null)
		{
			this.TestingStrategy = TestingStrategy.Manual;
			this.StatusCode = statusCode;
			this.ErrorResource = errorResource;
		}
	}
}
