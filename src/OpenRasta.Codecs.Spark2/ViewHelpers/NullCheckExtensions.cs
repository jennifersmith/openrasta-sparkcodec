namespace OpenRasta.Codecs.Spark2.ViewHelpers
{
	public static class NullCheckExtensions
	{
		public static bool IsNull(this object target)
		{
			return target == null;
		}
	}
}