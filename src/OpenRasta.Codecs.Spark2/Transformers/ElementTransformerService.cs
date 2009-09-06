using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenRasta.Codecs.Spark2.Model;

namespace OpenRasta.Codecs.Spark2.Transformers
{
	public class ElementTransformerService : IElementTransformerService
	{
		public ElementTransformerService(object elementTransformsConfiguration)
		{
		}

		public IElementTransformer GetTransformerFor(IElement element)
		{
			return null;
		}

		public bool IsTransformable(IElement element)
		{
			throw new NotImplementedException();
		}
	}
	public class NoElementTransform : IElementTransformer
	{
		public static readonly NoElementTransform Instance = new NoElementTransform();
		private NoElementTransform()
		{
		}

		public IElement Transform(IEnumerable<INode> body)
		{
			throw new NotImplementedException();
		}
	}


	public interface IElementTransformsConfiguration
	{
	}
}
