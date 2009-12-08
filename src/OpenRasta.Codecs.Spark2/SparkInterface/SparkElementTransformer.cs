using System;
using System.Collections.Generic;
using OpenRasta.Codecs.Spark2.Model;
using OpenRasta.Codecs.Spark2.Transformers;
using Spark.Parser.Markup;

namespace OpenRasta.Codecs.Spark2.SparkInterface
{
	public class SparkElementTransformer : ISparkElementTransformer
	{
		private readonly IElementTransformer wrappedTransformer;

		public SparkElementTransformer(IElementTransformer wrappedTransformer)
		{
			this.wrappedTransformer = wrappedTransformer;
		}

		public IElementTransformer WrappedTransformer
		{
			get { return wrappedTransformer; }
		}

		public void Transform(IElement element)
		{
			wrappedTransformer.Transform(element);
		}
	}
}