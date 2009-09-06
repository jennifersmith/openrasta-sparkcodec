using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Specification;

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
			if(IsTransformable(element)== false)
			{
				throw new ArgumentException("Element is not transformable");
			}
			return new ElementTransformer(element, _elementTransformerSpecification.GetActionsForElement(element));
		}

		public bool IsTransformable(IElement element)
		{
			return HasAtLeastOneTransform(element);
		}

		private bool HasAtLeastOneTransform(IElement element)
		{
			return _elementTransformerSpecification.GetActionsForElement(element).Any();
		}
	}
}
