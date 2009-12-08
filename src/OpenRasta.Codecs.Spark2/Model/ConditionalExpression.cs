namespace OpenRasta.Codecs.Spark2.Model
{
	public struct CodeExpression
	{
		private readonly string _expression;

		public CodeExpression(string expression)
		{
			_expression = expression;
		}

		public override string ToString()
		{
			return string.Format("Expression: {0}", _expression??"EMPTY");
		}

		public string Render()		
		{
			return _expression;
		}
	}
	public struct ConditionalExpression
	{
		private readonly string _condition;
		private readonly string _expression;

		public ConditionalExpression(string condition, string expression)
		{	
			_condition = condition;
			_expression = expression;
		}

		public string Condition
		{
			get { return _condition; }
		}

		public string Expression
		{
			get { return _expression; }
		}

		public override string ToString()
		{
			return string.Format("Condition: {0}, Expression: {1}", _condition, _expression);
		}
	}
}