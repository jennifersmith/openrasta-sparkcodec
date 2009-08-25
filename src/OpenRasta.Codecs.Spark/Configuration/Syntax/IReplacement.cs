using System.Collections.Generic;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark.Configuration.Syntax
{
	internal interface IReplacement
	{
		void DoReplace(ElementNode node, IList<Node> body);
	}
}