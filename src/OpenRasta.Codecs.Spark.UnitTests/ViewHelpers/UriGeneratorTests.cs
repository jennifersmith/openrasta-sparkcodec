using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenRasta.Codecs.Spark2.ViewHelpers;
using OpenRasta.Configuration;
using OpenRasta.DI;
using OpenRasta.Web;
using Rhino.Mocks;

namespace OpenRasta.Codecs.Spark.UnitTests.ViewHelpers
{
	[TestFixture]
	public class UriGeneratorTests
	{	
		[SetUp]
		public void SetUp()
		{
			Context = new UriGeneratorTestContext {Target = new UriGenerator()};

			var internalDependencyResolver = new InternalDependencyResolver();
			ICommunicationContext communicationContext = MockRepository.GenerateStub<ICommunicationContext>();
			Context.BaseUri = new Uri("http://localhost/");
			communicationContext.Stub(x => x.ApplicationBaseUri).Return(Context.BaseUri);
			internalDependencyResolver.AddDependencyInstance<ICommunicationContext>(communicationContext);
			Context.UriResolver = MockRepository.GenerateStub<IUriResolver>();
			internalDependencyResolver.AddDependencyInstance<IUriResolver>(Context.UriResolver);

			DependencyManager.SetResolver(internalDependencyResolver);
		}
		[Test]
		public void CreateUriShouldUseUriResolverFromOpenRasta()
		{
			object o = new object();
			string theUri = "TheUri";
			GivenAUriResolverResult<object>(theUri);
			WhenCreateUricalledFor(o);
			ThenTheResultShouldbe(new Uri(Context.BaseUri, theUri));
		}
		[Test]
		public void CreateUriShould()
		{
			object o = new object();
			string theUri = "TheUri";
			GivenAUriResolverResult<object>(theUri);
			WhenCreateUricalledFor(o);
			ThenTheResultShouldbe(new Uri(Context.BaseUri, theUri));
		}
		[Test]
		public void CreateUriShouldJustReturnUriIfUriIsPassedIn()
		{
			Uri theUri = new Uri("http://theuri");
			WhenCreateUricalledFor(theUri);
			ThenTheResultShouldbe(theUri);
		}

		private void ThenTheResultShouldbe(Uri expected)
		{
			Context.CreateUriResult.ShouldEqual(expected);
		}
		private void WhenCreateUricalledFor(Uri value)
		{
			Context.CreateUriResult = Context.Target.Create(value);
		}
		private void WhenCreateUricalledFor(object o)
		{
			Context.CreateUriResult = Context.Target.Create(o);
		}

		private void GivenAUriResolverResult<T>(string uri)
		{
			Context.UriResolver.Stub(x => x.CreateUriFor(Context.BaseUri, typeof(T), null, new NameValueCollection())).Return(new Uri(Context.BaseUri, uri));
		}


		public UriGeneratorTestContext Context { get; set; }

		public class UriGeneratorTestContext
		{
			public UriGenerator Target { get; set; }


			public object Resource { get; set; }

			public Uri CreateUriResult { get; set; }

			public IUriResolver UriResolver { get; set; }

			public Uri BaseUri { get; set; }
		}
	}
}
