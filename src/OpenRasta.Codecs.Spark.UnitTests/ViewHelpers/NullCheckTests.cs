using NUnit.Framework;
using OpenRasta.Codecs.Spark2.ViewHelpers;

namespace OpenRasta.Codecs.Spark.UnitTests.ViewHelpers
{
	[TestFixture]
	public class NullCheckTests
	{
		[SetUp]
		public void SetUp()
		{
			Context = new NullCheckContext();
		}

		protected NullCheckContext Context { get; set; }

		[Test]
		public void NullCheckShouldReturnTrueIfTargetIsNull()
		{
			GivenATarget(null);
			WhenIsNullIsCalled();
			ThenTheResultShouldBe(true);
		}

		private void ThenTheResultShouldBe(bool value)
		{
			Context.IsNullResult.ShouldEqual(value);
		}

		private void WhenIsNullIsCalled()
		{
			Context.IsNullResult = Context.TargetItem.IsNull();
		}

		private void GivenATarget(object target)
		{
			Context.TargetItem = target;
		}

		public class NullCheckContext
		{
			public object TargetItem { get; set; }

			public bool IsNullResult { get; set; }
		}
	}
}