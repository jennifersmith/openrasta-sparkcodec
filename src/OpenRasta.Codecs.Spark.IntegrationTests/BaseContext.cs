using System;
using NUnit.Framework;
using OpenRasta.Tests.Integration;

namespace OpenRasta.Codecs.Spark.IntegrationTests
{
	public class ServerContext : server_context
	{
		
	}
	public class OpenRastaContext : BaseContext
	{
		private server_context _server = null;

		public server_context Server
		{
			get
			{
				return _server;
			}
		}
		public override void CreateContext()
		{
			base.CreateContext();
			_server = new ServerContext();
			_server.setup_context();
		}
		protected override void TeardownContext()
		{
			base.TeardownContext();
			if(_server!=null)
			{
				_server.tear();
			}
		}
	}

	public abstract class BaseContext
	{
		[SetUp]
		public void SetUp()
		{
			CreateContext();
			AssignTarget();
			When();
		}
		[TearDown]
		public void TearDown()
		{
			TeardownContext();
		}

		protected virtual void TeardownContext()
		{
		}

		public virtual void AssignTarget()
		{
		}


		public virtual void When()
		{
		}


		public virtual void CreateContext()
		{
		}
	}

	public abstract class BaseContext<TTarget> : BaseContext
	{
		public TTarget Target { get; private set; }

		public object ScenarioResult { get; private set; }

		public override void AssignTarget()
		{
			Target = CreateTarget();
		}

		protected abstract TTarget CreateTarget();

		public virtual object ExecuteScenario()
		{
			return null;
		}

		public override void CreateContext()
		{
		}

		public override void When()
		{
			ScenarioResult = ExecuteScenario();
		}
	}
}