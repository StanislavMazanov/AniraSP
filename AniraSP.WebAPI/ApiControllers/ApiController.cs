using Microsoft.AspNetCore.Mvc;

namespace AniraSP.WebAPI.ApiControllers {
    [ApiController]
    [Route("api")]
    public abstract class ApiController : ControllerBase {
        [ActionContext] public ActionContext ActionContext { get; set; }
    }
}