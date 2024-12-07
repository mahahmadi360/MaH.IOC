using MaH.IOC.SampleWeb.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace MaH.IOC.SampleWeb.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IScopedService _scopedService;

        public ValuesController(IScopedService scopedService)
        {
            _scopedService = scopedService;
        }
        // GET api/values
        public IEnumerable<string> Get()
        {
            return _scopedService.GetValues();
        }

    }
}
