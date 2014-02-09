using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;

namespace SportsStore.WebUI.Controllers
{
	public class NavController : Controller
	{
		//note 4. добавим readonly, обезопасив себя он подмены репозитория после внедрения в конструктор
		private readonly IProductRepository repository;

		public NavController(IProductRepository repo)
		{
			//note 5. добавим ограждающее условие, что бы быть уверенным -- внедрение прошло успешно
			//больше информации см. http://smarly.net/dependency-injection-in-net/di-catalog/di-patterns/constructor-injection#text-22427

			if (repo == null)
				throw new ArgumentNullException("repo");

			repository = repo;
		}

		public PartialViewResult Menu(string category = null)
		{
			//note 6. Добавлен ToList() для получения списка продуктов до передачи данных в представление
			//Отложенные методы расширения LINQ http://smarly.net/pro-asp-net-mvc-4/introducing-asp-net-mvc-4/essential-language-features/performing-language-integrated-queries#text-14929

			IEnumerable<string> categories = repository.Products
				.Select(x => x.Category)
				.Distinct()
				.OrderBy(x => x)
				.ToList();

			ViewBag.SelectedCategory = category;

			return PartialView(categories);
		}
	}
}