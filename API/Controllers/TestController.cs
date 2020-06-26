using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        
        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Response Get()
        {
            Response r = new Response();
            r.Resp = "Test";
            return r;
        }

        [HttpPost]
        public ActionResult<Response> Post([FromBody] RequestData data)
        {
            Response r = new Response();
            r.Resp = data.foo + data.bar;
            return r;
        }
    }

    public class RequestData
    {
        public string foo{get; set;}
        public string bar{get; set;}
    }

    public class Response
    {
        public String Resp {get; set;}
    }
}