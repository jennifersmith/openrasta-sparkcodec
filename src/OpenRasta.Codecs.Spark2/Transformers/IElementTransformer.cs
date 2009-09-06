using System;
using System.Collections.Generic;
using OpenRasta.Codecs.Spark2.Matchers;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Specification;

namespace OpenRasta.Codecs.Spark2.Transformers
{
	public interface IElementTransformer
	{
		IElement Transform(IEnumerable<INode> body);
	}

	public class ElementTransformer : IElementTransformer
	{
		private readonly IElement _elementTarget;
		private readonly List<IElementTransformerAction> _elementTransformerActions = new List<IElementTransformerAction>();
		public IElement Transform(IEnumerable<INode> body)
		{
			_elementTransformerActions.ForEach(x=>x.Do(_elementTarget));
			return _elementTarget;
		}
		public ElementTransformer(IElement elementTarget, IEnumerable<IElementTransformerAction> actions )
		{
			_elementTarget = elementTarget;
			_elementTransformerActions.AddRange(actions);
		}

		public IEnumerable<IElementTransformerAction> GetActions()
		{
			return _elementTransformerActions.ToArray();
		}
	}
}