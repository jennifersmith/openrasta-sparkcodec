using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Specification;
using OpenRasta.Codecs.Spark2.Specification.Syntax;

namespace OpenRasta.Codecs.Spark2.Transformers
{
	public class ElementTransformerService : IElementTransformerService
	{
		private readonly IElementTransformerSpecification _elementTransformerSpecification;

		public ElementTransformerService(ISpecificationProvider specificationProvider)
		{
			_elementTransformerSpecification = specificationProvider.CreateSpecification();
		}

		public IElementTransformer GetTransformerFor(IElement element)
		{
			throw new NotImplementedException();
		}

		public IElementTransformer GetTransformerFor(Tag tag)
		{
			if (IsTransformable(tag) == false)
			{
				throw new ArgumentException("Element is not transformable");
			}
			return new ElementTransformer(_elementTransformerSpecification.GetActionsForTag(tag));
		}

		public bool IsTransformable(IElement element)
		{
			throw new NotImplementedException();
		}
		public bool IsTransformable(Tag tag)
		{
			return HasAtLeastOneTransform(tag);
		}

		private bool HasAtLeastOneTransform(Tag tag)
		{
			return _elementTransformerSpecification.GetActionsForTag(tag).Any();
		}
	}
}
