using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPSchoolAPI.Controllers
{
    [ApiController]
    [Route("api/sample")]
    public class SampleController:ControllerBase
    {
        public string Get()
        {
            return "Sample controller - Get";
        }

        [Route("msg")]
        public string GetMsg()
        {
            return "Sample controller - GetMsg";
        }

    }
}
