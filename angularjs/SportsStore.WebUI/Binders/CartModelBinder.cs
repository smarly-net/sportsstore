using System;
using System.Web.Mvc;
using SportsStore.Domain.Entities;
namespace SportsStore.WebUI.Binders
{
	public class CartModelBinder : IModelBinder
	{
		private const string sessionKey = "Cart";

		//note 10. В случае если необходимо протестировать методы действия использующие Session, можно создать SessionProvider используя паттерн кружающий контекст (Ambient Context)
		//более подробно принцип можено прочитать на примере тестирования, эмулируя необходимое реальное время http://smarly.net/dependency-injection-in-net/di-catalog/di-patterns/ambient-context#text-22955
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			// get the Cart from the session
			Cart cart = (Cart)controllerContext.HttpContext.Session[sessionKey];

			// create the Cart if there wasn't one in the session data
			if (cart == null)
			{
				cart = new Cart();
				controllerContext.HttpContext.Session[sessionKey] = cart;
			}

			// return the cart
			return cart;
		}
	}
}