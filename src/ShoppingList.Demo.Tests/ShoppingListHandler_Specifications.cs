using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenRasta.Demo.Handlers;
using OpenRasta.Demo.Resources;
using OpenRasta.TestingUtilities;
using OpenRasta.Web;
using Rhino.Mocks;
using ShoppingListViewer.Demo.Tests;

namespace ShoppingListHandler_Specifications
{
	public class BaseShoppingListContext : BaseContext<ShoppingListHandler>
	{
		public IShoppingListService ShoppingListService
		{
			get; private set;
		}

		public IShoppingList ShoppingList { get; set; }

		public override void CreateContext()
		{
			base.CreateContext();
			ShoppingListService = MockRepository.GenerateStub<IShoppingListService>();

		}
		protected override ShoppingListHandler CreateTarget()
		{
			return new ShoppingListHandler(ShoppingListService);
		}

		protected void WithItems(params ShoppingListItem[] items)
		{
			ShoppingList = MockRepository.GenerateStub<IShoppingList>();
			ShoppingList.Stub(x => x.GetEnumerator()).Return(items.ToList().GetEnumerator());
			ShoppingListService.Stub(x => x.GetShoppingList()).Return(ShoppingList);
		}
	}
	public class BaseGetContext : BaseShoppingListContext
	{
		public override object ExecuteScenario()
		{
			return Target.Get();
		}
		public new IShoppingList ScenarioResult
		{
			get
			{
				return base.ScenarioResult.As<IShoppingList>();
			}
		}
	}
	public class BasePostContext : BaseShoppingListContext
	{
		public override void CreateContext()
		{
			base.CreateContext();
			ItemToBeAdded = new ShoppingListItem();
		}

		protected ShoppingListItem ItemToBeAdded { get; set; }

		public override object ExecuteScenario()
		{
			return Target.Post(ItemToBeAdded);
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
				WithItems();
			}
			[Test]
			public void It_should_return_an_empty_list()
			{
				ScenarioResult.ShouldBeEmpty();
			}
		}
		[TestFixture]
		public class When_I_add_a_new_item : BasePostContext
		{
			public override void CreateContext()
			{
				base.CreateContext();
				WithItems();
			}
			[Test]
			public void It_should_add_item_to_shopping_list()
			{
				ShoppingList.AssertWasCalled(x => x.Add(ItemToBeAdded));
			}
			[Test]
			public void It_should_redirect_the_user_to_the_shopping_list_page()
			{
				//ScenarioResult.ShouldBeRedirectTo(ShoppingList);
				Assert.Fail();
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
				Item1 = new ShoppingListItem();
				Item2 = new ShoppingListItem();
				WithItems(Item1, Item2);
			}

			protected ShoppingListItem Item2 { get; set; }


			protected ShoppingListItem Item1 { get; set; }

			[Test]
			public void It_should_return_a_list_with_two_items()
			{
				ScenarioResult.Count().ShouldEqual(2);
			}
			[Test]
			public void Item_one_should_be_somewhere_in_the_list()
			{
				ScenarioResult.ShouldContain(Item1);
			}
			[Test]
			public void Item_two_should_be_somewhere_in_the_list()
			{
				ScenarioResult.ShouldContain(Item2);
			}
		}
	}
}
