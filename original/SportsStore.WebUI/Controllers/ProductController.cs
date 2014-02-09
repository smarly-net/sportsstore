using System;
using System.Linq;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
	public class ProductController : Controller
	{
		//note 1. добавим readonly, обезопасив себя он подмены репозитория после внедрения в конструктор

		private IProductRepository repository;
		public int PageSize = 4;
		public ProductController(IProductRepository productRepository)
		{
			//note 2. добавим ограждающее условие, что бы быть уверенным -- внедрение прошло успешно
			//больше информации см. http://smarly.net/dependency-injection-in-net/di-catalog/di-patterns/constructor-injection#text-22427

			if (productRepository == null)
				throw new ArgumentNullException("productRepository");

			this.repository = productRepository;
		}

		public ViewResult List(int page = 1)
		{
			//note 3. Добавлен ToList() для получения списка продуктов до передачи данных в представление
			//Отложенные методы расширения LINQ http://smarly.net/pro-asp-net-mvc-4/introducing-asp-net-mvc-4/essential-language-features/performing-language-integrated-queries#text-14929

			ProductsListViewModel model = new ProductsListViewModel
			{
				Products = repository.Products
					.OrderBy(p => p.ProductID)
					.Skip((page - 1) * PageSize)
					.Take(PageSize).ToList(),
				PagingInfo = new PagingInfo
				{
					CurrentPage = page,
					ItemsPerPage = PageSize,
					TotalItems = repository.Products.Count()
				}
			};

			return View(model);
		}
	}
}