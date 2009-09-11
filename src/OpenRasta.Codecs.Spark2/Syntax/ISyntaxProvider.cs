using System;

namespace OpenRasta.Codecs.Spark2.Syntax
{
	public interface ISyntaxProvider
	{
		string CreateUriExpression(string targetResource);
	}

	public class CSharpSyntaxProvider : ISyntaxProvider
	{
		public string CreateUriExpression(string targetResource)
		{
			throw new NotImplementedException();
		}
	}
}