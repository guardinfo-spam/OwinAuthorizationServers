using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using ApiSupport.Models;

namespace ResZServer
{
    [Authorize]
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        [Route("createUser")]
        [HttpPost]
        public HttpResponseMessage CreateUser(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                MembershipCreateStatus status;
                
                Membership.CreateUser(model.Username, model.Password, model.Username,
                    "this is my very safe password question", "this is my very safe password answer", true, out status);
                
                return Request.CreateResponse(HttpStatusCode.OK, status.ToString());
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [Route("validateUser")]
        [HttpPost]
        public HttpResponseMessage ValidateUser(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                if (!Membership.ValidateUser(model.Username, model.Password))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "validation_failed");
                }

                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}
