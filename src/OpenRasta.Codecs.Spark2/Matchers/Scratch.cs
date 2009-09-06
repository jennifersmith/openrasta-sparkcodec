using System;
using System.Collections.Generic;
using System.Linq;
using OpenRasta.Codecs.Spark2.Model;

namespace OpenRasta.Codecs.Spark2.Matchers
{
	public class Scratch
	{
		public ElementTransformerSpecification CreateSpecification()
		{
			var builder = new ElementTransformerSpecificationBuilder();

			builder.MatchAll(Tag.AnchorTag)
				.Do(
					Convert.ToAttributeToHref(),
					Convert.ToTypeAttributeToHref()
					);
			builder.MatchAll(Tag.FormTag)
				.Do(
					Convert.ToAttributeToHref(),
					Convert.ToTypeAttributeToHref()
					);
			builder.MatchAll(Tag.AllInputTags)
				.Do(
					Convert.ToAttributeToHref(),
					Convert.ToTypeAttributeToHref()
					);
			return builder.Build();
		}
	}

	public class ElementTransformerSpecificationBuilder
	{
		List<ElementTransformerActionsByMatchBuilder> _builders = new List<ElementTransformerActionsByMatchBuilder>();
		public void AddTranformationBuilder(ElementTransformerActionsByMatchBuilder builder)
		{
			_builders.Add(builder);
		}	

		public ElementTransformerSpecification Build()
		{
			return new ElementTransformerSpecification(_builders.Select(x => x.Build()));
		}
	}

	public class ElementTransformerSpecification
	{
		private readonly IEnumerable<ElementTransformerActionsByMatch> _elementTransformerActionsByMatchs;

		public ElementTransformerSpecification(IEnumerable<ElementTransformerActionsByMatch> elementTransformerActionsByMatchs)
		{
			_elementTransformerActionsByMatchs = elementTransformerActionsByMatchs;
		}

		public IEnumerable<IElementTransformerAction> GetActionsForElement(IElement element)
		{
			return null;
		}
	}

	public struct Tag
	{
		private readonly string _name;

		private Tag(string name)
		{
			_name = name;
		}

		public readonly static Tag AnchorTag = new Tag("A");
		public readonly static Tag FormTag = new Tag("form");
		public static IEnumerable<Tag> AllInputTags = new []{new Tag("Input"), new Tag("select"), new Tag("textarea")};

		public string Name
		{
			get { return _name; }
		}

	}
	public static class ElementMatchSpecificationExtensions
	{
		public static ElementTransformerActionsByMatchBuilder MatchAll(this ElementTransformerSpecificationBuilder config, params Tag[] tags)
		{
			return MatchAll(config, (IEnumerable<Tag>) tags);
		}
		public static ElementTransformerActionsByMatchBuilder MatchAll(this ElementTransformerSpecificationBuilder config, IEnumerable<Tag> tags)
		{
			var builder = new ElementTransformerActionsByMatchBuilder(tags.Select(x => new NodeMatcher(x.Name)));
			config.AddTranformationBuilder(builder);
			return builder;
		}
	}

	public class ElementTransformerActionsByMatchBuilder
	{
		private readonly IEnumerable<NodeMatcher> _matcher;
		private List<IElementTransformerAction> actions = new List<IElementTransformerAction>();
		public ElementTransformerActionsByMatchBuilder(IEnumerable<NodeMatcher> matcher)
		{
			_matcher = matcher;
		}

		public void AddAction(IElementTransformerAction ElementTransformerAction)
		{
			actions.Add(ElementTransformerAction);
		}
		public ElementTransformerActionsByMatch Build()
		{
			return new ElementTransformerActionsByMatch(_matcher, actions);
		}

	}
	
	public class ElementTransformerActionsByMatch
	{
		public ElementTransformerActionsByMatch(IEnumerable<NodeMatcher> nodeMatchers, IEnumerable<IElementTransformerAction> elementTransformerActions)
		{
			throw new NotImplementedException();
		}
	}

	public static class ElementTransformerExtensions
	{
		public static ElementTransformerActionsByMatchBuilder Do(this ElementTransformerActionsByMatchBuilder elementTransformerActionsByMatchBuilder, params IElementTransformerAction[] actions)
		{
			actions.ForEach(elementTransformerActionsByMatchBuilder.AddAction);
			return elementTransformerActionsByMatchBuilder;
		}
	}
	public static class Convert
	{
		public static IElementTransformerAction ToAttributeToHref()
		{
			return new ConvertAttributeToUri("to", "href");
		}
		public static IElementTransformerAction ToTypeAttributeToHref()
		{
			return new ConvertAttributeToUri("totype", "href");
		}

	}

	public class ConvertAttributeToUri : IElementTransformerAction
	{
		public ConvertAttributeToUri(string originalAttribute, string destinationAttribute)
		{
			
		}

		public void Do(IElement Element)
		{
			throw new NotImplementedException();
		}
	}

	public interface IElementTransformerAction
	{
		void Do(IElement Element);
	}

	public class ElementTransformerAction : IElementTransformerAction
	{
		private readonly Action<IElement> action;

		public ElementTransformerAction(Action<IElement> action)
		{
			this.action = action;
		}
		public void Do(IElement Element)
		{
			action(Element);
		}
	}

	//public void what()
	//{
	//    // replace the to attribute with a href
	//    //
	//    Match.All(AnchorTags).Do(tag =>{
	//                                        Transform(tag.ForAttribute()).With(Transforms.ResourceToUri);
	//                                    });
	//}
}