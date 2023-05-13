using Microsoft.AspNetCore.Mvc;

namespace RedisCacheTest.RestApi.Controllers
{
	public class RedisCacheController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
