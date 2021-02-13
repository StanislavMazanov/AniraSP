using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace AniraSP.WebAPI.ApiControllers {
    public class SitesController : ApiController {
        [HttpGet]
        [Route("sites/all")]
        public ActionResult<IEnumerable<SitesDto>> Get() {
            var sites = new[] {
                new SitesDto {
                    Id = 1,
                    Name = "Test"
                }
            };
            return new ActionResult<IEnumerable<SitesDto>>(sites);
        }
    }
}