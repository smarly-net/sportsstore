using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;

namespace SportsStore.WebUI.Controllers
{
	public class ProductController : Controller
	{
		//note 1. добавим readonly, обезопасив себя он подмены репозитория после внедрения в конструктор
		private IProductRepository repository;
		public ProductController(IProductRepository productRepository)
		{
			//note 2. добавим ограждающее условие, что бы быть уверенным -- внедрение прошло успешно
			//больше информации см. http://smarly.net/dependency-injection-in-net/di-catalog/di-patterns/constructor-injection#text-22427
			if (productRepository == null)
				throw new ArgumentNullException("productRepository");

			this.repository = productRepository;
		}

		public ViewResult List()
		{
			return View(repository.Products);
		}
	}
}