using System;
using System.Linq;
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
			//note 3. Добавлен ToList() для получения списка продуктов до передачи данных в представление
			//Отложенные методы расширения LINQ http://smarly.net/pro-asp-net-mvc-4/introducing-asp-net-mvc-4/essential-language-features/performing-language-integrated-queries#text-14929
			return View(repository.Products.ToList());
		}
	}
}