using System;
using System.Collections.Generic;
using System.Security.Principal;
using OpenRasta.Web;
using OpenRasta.Web.Pipeline;

namespace OpenRasta.Codecs.Spark.Tests
{
	internal class TestCommunicationContext : ICommunicationContext
	{
		public const string AppUrl = "http://localhost/";
		public Uri ApplicationBaseUri
		{
			get { return new Uri(AppUrl); }
		}

		public IRequest Request
		{
			get { throw new System.NotImplementedException(); }
		}

		public IResponse Response
		{
			get { throw new System.NotImplementedException(); }
		}

		public OperationResult OperationResult
		{
			get { throw new System.NotImplementedException(); }
			set { throw new System.NotImplementedException(); }
		}

		public PipelineData PipelineData
		{
			get { throw new System.NotImplementedException(); }
		}

		public IList<Error> ServerErrors
		{
			get { throw new System.NotImplementedException(); }
		}

		public IPrincipal User
		{
			get { throw new System.NotImplementedException(); }
			set { throw new System.NotImplementedException(); }
		}
	}
}