using System.Web;
using System.Web.Http;

namespace Api {
    public class BaseApiController : ApiController {
        public BaseApiController() {
            UserId = HttpContext.Current.Request.UserHostAddress;
        }

        public string UserId { get; }
    }
}
