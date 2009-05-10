using System;

namespace OpenRasta.Codecs.Spark
{
	public class ViewData
	{
		private readonly  object resource;

		public ViewData(object resource)
		{
			this.resource = resource;
		}


		public object Eval(string expression)
		{
			if(Matches(expression, "Resource"))
			{
				return resource;	
			}
			throw new Exception("Unrecognised expression " + expression);
		}

		private bool Matches(string expression, string name)
		{
			return string.Equals(expression, name, StringComparison.CurrentCultureIgnoreCase);
		}

		public string Eval(string expression, string format)
		{
			// not sure if this will be used;
			return string.Format(format, Eval(expression));
		}

	}
}