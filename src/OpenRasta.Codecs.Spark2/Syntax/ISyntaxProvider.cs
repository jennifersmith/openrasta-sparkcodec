using System;

namespace OpenRasta.Codecs.Spark2.Syntax
{
	public interface ISyntaxProvider
	{
		string CreateUriExpression(string targetResource);
		string CreateNullCheckExpression(string targetResource);
	}

	public class CSharpSyntaxProvider : ISyntaxProvider
	{
		public string CreateUriExpression(string targetResource)
		{
			return string.Format("Uris.Create({0})", targetResource);
		}

		public string CreateNullCheckExpression(string targetResource)
		{
			return string.Format("{0}.IsNull()", targetResource);
		}
	}
}