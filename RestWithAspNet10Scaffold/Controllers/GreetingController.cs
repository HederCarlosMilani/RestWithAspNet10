using Microsoft.AspNetCore.Mvc;
using RestWithAspNet10Scaffold.Model;

namespace RestWithAspNet10Scaffold.Controllers
{
    [ApiController]
    [Route("[controller]")]
	public class GreetingController : Controller
    {
        private static long _counter = 0;
        private static readonly string _template = "Hello, {0}!";

		[HttpGet]
        public Greeting Get([FromQuery] string name = "World")
        {
            var id = System.Threading.Interlocked.Increment(ref _counter);
            var content = string.Format(_template, name);

			return new Greeting(id, content);
		}
	}
}
