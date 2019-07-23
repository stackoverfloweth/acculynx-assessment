using System;
using System.Web;
using System.Web.Http;

namespace Api {
    public class BaseApiController : ApiController {
        public BaseApiController() {
            try {
                UserId = HttpContext.Current.Request.UserHostAddress;
            }
            catch (Exception) {

            }
        }

        public string UserId { get; }
    }
}
