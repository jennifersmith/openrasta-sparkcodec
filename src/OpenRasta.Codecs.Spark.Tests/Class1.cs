using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenRasta.Codecs;
using OpenRasta.Codecs.Spark;
using OpenRasta.Codecs.Spark.Configuration;
using OpenRasta.Configuration;
using OpenRasta.DI;
using OpenRasta.Web;
using OpenRasta.Web.Pipeline;
using PanelSystem.WorkingDays.Tests;
using Rhino.Mocks;

namespace  configuration_specifications
{
	namespace with_scenario_of
	{ 
		public class configuring_open_rasta_to_use_spark : BaseContext
		{
			public override void CreateContext()
			{
				base.CreateContext();
				CodecRepository = new CodecRepository();
				DependencyManager.AutoRegisterDependencies = true;
				var dependencyResolver = new TestResolver();
				var pipeline = MockRepository.GenerateStub<IPipeline>();
				var uris = MockRepository.GenerateStub<IUriResolver>();
				dependencyResolver.Stub(x => x.Resolve<ICodecRepository>()).Return(CodecRepository);
				dependencyResolver.Stub(x => x.Resolve<IUriResolver>()).Return(uris);
				dependencyResolver.Stub(x => x.Resolve<IPipeline>()).Return(pipeline);
				DependencyManager.SetResolver(dependencyResolver);
			}

			public CodecRepository CodecRepository { get; private set; }
		}

		internal class TestResolver	 : IDependencyResolver
		{
			public object Resolve(Type type)
			{
				throw new System.NotImplementedException();
			}

			public void HandleIncomingRequestProcessed()
			{
				throw new System.NotImplementedException();
			}

			public bool HasDependency(Type serviceType)
			{
				throw new System.NotImplementedException();
			}

			public bool HasDependencyImplementation(Type serviceType, Type concreteType)
			{
				throw new System.NotImplementedException();
			}

			public void AddDependency(Type concreteType, DependencyLifetime lifetime)
			{
				throw new System.NotImplementedException();
			}

			public void AddDependency(Type serviceType, Type concreteType, DependencyLifetime dependencyLifetime)
			{
				throw new System.NotImplementedException();
			}

			public void AddDependencyInstance(Type registeredType, object value, DependencyLifetime dependencyLifetime)
			{
				throw new System.NotImplementedException();
			}

			public void AddDependencyInstance(Type registeredType, object value)
			{
				throw new System.NotImplementedException();
			}

			public IEnumerable<TService> ResolveAll<TService>()
			{
				throw new System.NotImplementedException();
			}
		}
	}

	namespace given_that_openrasta_resource_is_being_configured
	{
		[TestFixture]
		public class when_user_attempts_to_configure_resource_to_use_spark : with_scenario_of.configuring_open_rasta_to_use_spark
		{
			public override void When()
			{
				base.When();
				using (OpenRastaConfiguration.Manual)
				{
					ResourceSpace.Has.ResourcesOfType<MyResource>()
						.AtUri("/MyResource")
						.HandledBy<MyResourceHandler>()
						.AndRenderedBySpark("MyResourceView.spark");
				}
			}

			[Test]
			public void Spark_codec_is_registered_to_transcode_resource()
			{
				CodecRepository.FindMediaTypeWriter(typeof(MyResource), new[]{new MediaType("text/html")}).First().CodecType.ShouldEqual(typeof(SparkCodec));
			}
		}
	}

	public class MyResource
	{

	}
	public class MyResourceHandler
	{

	}
}
