using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenRasta.Data;
using OpenRasta.Demo.Handlers;
using OpenRasta.Demo.Resources;
using OpenRasta.TestingUtilities;
using OpenRasta.TypeSystem;
using OpenRasta.Web;
using Rhino.Mocks;
using ShoppingListViewer.Demo.Tests;

namespace ShoppingListItemHandler_Specifications
{
	public class BaseShoppingListItemContext : BaseContext<ShoppingListItemHandler>
	{
		private ShoppingListItem _shoppingList;

		public IShoppingListService ShoppingListService
		{
			get;
			private set;
		}

		public ShoppingListItem ShoppingListItem
		{
			get { return _shoppingList; }
			set { _shoppingList = value; }
		}

		public override void CreateContext()
		{
			base.CreateContext();
			//TestUtilities.SetupForTesting();
			ShoppingListService = MockRepository.GenerateStub<IShoppingListService>();

		}
		protected override ShoppingListItemHandler CreateTarget()
		{
			return new ShoppingListItemHandler(ShoppingListService);
		}
	}
	public class BaseGetContext : BaseShoppingListItemContext
	{
		public override object ExecuteScenario()
		{
			return Target.Get(ItemDescription);
		}

		protected string ItemDescription
		{
			get
			{
				return "This is the item";
			}
		}

		public new ShoppingListItem ScenarioResult
		{
			get
			{
				return base.ScenarioResult.As<ShoppingListItem>();
			}
		}
	}
	public class BasePostContext : BaseShoppingListItemContext
	{
		public override void CreateContext()
		{
			base.CreateContext();
			ItemToBeUpdated = new ShoppingListItem()
			                  	{
			                  		Description = "This is the item to update"
			                  	};
		}

		protected ShoppingListItem ItemToBeUpdated { get; set; }

		public override object ExecuteScenario()
		{
			return Target.Post(ItemToBeUpdated.Description, null);
		}
		public new OperationResult ScenarioResult
		{
			get
			{
				return base.ScenarioResult.As<OperationResult>();
			}
		}
	}
	namespace Given_there_are_no_shopping_list_items_defined
	{
		[TestFixture]
		public class When_I_get_the_shopping_list : BaseGetContext
		{
			public override void CreateContext()
			{
				base.CreateContext();
				throw new Exception("todo");
				
			}
		}
		[TestFixture]
		public class When_I_add_a_new_item : BasePostContext
		{
			public override void CreateContext()
			{
				base.CreateContext();
				throw new Exception("todo");
			}

		}
	}
	namespace Given_there_are_already_two_items_in_the_list
	{

		[TestFixture]
		public class When_I_get_the_shopping_list : BaseGetContext
		{
			public override void CreateContext()
			{
				base.CreateContext();
				throw new Exception("todo");
			}

		}
	}
}
