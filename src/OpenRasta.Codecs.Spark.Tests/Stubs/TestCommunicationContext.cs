using System;
using System.Collections.Generic;
using System.Security.Principal;
using OpenRasta.Web;
using OpenRasta.Web.Pipeline;

namespace OpenRasta.Codecs.Spark.Tests.Stubs
{
	internal class TestCommunicationContext : ICommunicationContext
	{
		public const string AppUrl = "http://localhost/";

		#region ICommunicationContext Members

		public Uri ApplicationBaseUri
		{
			get { return new Uri(AppUrl); }
		}

		public IRequest Request
		{
			get { throw new NotImplementedException(); }
		}

		public IResponse Response
		{
			get { throw new NotImplementedException(); }
		}

		public OperationResult OperationResult
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public PipelineData PipelineData
		{
			get { throw new NotImplementedException(); }
		}

		public IList<Error> ServerErrors
		{
			get { throw new NotImplementedException(); }
		}

		public IPrincipal User
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		#endregion
	}
}