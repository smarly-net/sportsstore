using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
	public class CartController : Controller
	{
		//note 7. добавим readonly, обезопасив себя он подмены репозитория после внедрения в конструктор
		private readonly IProductRepository repository;

		public CartController(IProductRepository repo)
		{
			//note 8. добавим ограждающее условие, что бы быть уверенным -- внедрение прошло успешно
			//больше информации см. http://smarly.net/dependency-injection-in-net/di-catalog/di-patterns/constructor-injection#text-22427

			if (repo == null)
				throw new ArgumentNullException("repo");

			repository = repo;
		}

		public ViewResult Index(string returnUrl)
		{
			return View(new CartIndexViewModel
			{
				Cart = GetCart(),
				ReturnUrl = returnUrl
			});
		}

		public RedirectToRouteResult AddToCart(int productId, string returnUrl)
		{
			Product product = repository.Products
				.FirstOrDefault(p => p.ProductID == productId);
			if (product != null)
			{
				GetCart().AddItem(product, 1);
			}
			return RedirectToAction("Index", new {returnUrl});
		}

		public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl)
		{
			Product product = repository.Products
				.FirstOrDefault(p => p.ProductID == productId);
			if (product != null)
			{
				GetCart().RemoveLine(product);
			}
			return RedirectToAction("Index", new {returnUrl});
		}

		//note 9. В случае если необходимо протестировать методы действия использующие Session, можно создать SessionProvider используя паттерн кружающий контекст (Ambient Context)
		//более подробно принцип можено прочитать на примере тестирования, эмулируя необходимое реальное время http://smarly.net/dependency-injection-in-net/di-catalog/di-patterns/ambient-context#text-22955
		private Cart GetCart()
		{
			Cart cart = (Cart) Session["Cart"];
			if (cart == null)
			{
				cart = new Cart();
				Session["Cart"] = cart;
			}
			return cart;
		}
	}
}