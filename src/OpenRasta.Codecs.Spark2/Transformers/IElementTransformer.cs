using System;
using System.Collections.Generic;
using OpenRasta.Codecs.Spark2.Matchers;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Specification;

namespace OpenRasta.Codecs.Spark2.Transformers
{
	public interface IElementTransformer
	{
		IElement Transform(IElement element);
	}

	public class ElementTransformer : IElementTransformer
	{
		private readonly List<IElementTransformerAction> _elementTransformerActions = new List<IElementTransformerAction>();
		public IElement Transform(IElement element)
		{
			_elementTransformerActions.ForEach(x => x.Do(element));
			return element;
		}
		public ElementTransformer(IEnumerable<IElementTransformerAction> actions)
		{
			_elementTransformerActions.AddRange(actions);
		}

		public IEnumerable<IElementTransformerAction> GetActions()
		{
			return _elementTransformerActions.ToArray();
		}
	}
}