using NUnit.Framework;

namespace ShoppingListViewer.Demo.Tests
{
	public abstract class BaseContext
	{
		[SetUp]
		public void SetUp()
		{
			CreateContext();
			AssignTarget();
			When();
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