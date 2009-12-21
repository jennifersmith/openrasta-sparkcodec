using System;
using OpenRasta.Codecs.Spark2.Model;

namespace OpenRasta.Codecs.Spark2.Syntax
{
	public interface ISyntaxProvider
	{
		string CreateUriExpression(string targetResource);
		string CreateUriFromTypeExpression(string targetResource);
		string CreateNullCheckExpression(string targetResource);
		string CreateGetPropertyPathExpression(string propertyPath);
		string CreateNullCheckAndEvalExpression(string targetResource);
	}

	public class CSharpSyntaxProvider : ISyntaxProvider
	{
		public string CreateUriExpression(string targetResource)
		{
			return string.Format("Uris.Create({0})", targetResource);
		}
		public string CreateUriFromTypeExpression(string targetResource)
		{
			return string.Format("Uris.Create<{0}>()", targetResource);
		}

		public string CreateNullCheckExpression(string targetResource)
		{
			return string.Format("{0}.IsNull()==false", targetResource);
		}

		public string CreateGetPropertyPathExpression(string propertyPath)
		{
			return string.Format("PropertyPaths.PathFor(()=>{0})", propertyPath);
		}

		public string CreateNullCheckAndEvalExpression(string targetResource)
		{

			return string.Format("({0}.IsNull()==false)&&{0}", targetResource);
		}
	}
}