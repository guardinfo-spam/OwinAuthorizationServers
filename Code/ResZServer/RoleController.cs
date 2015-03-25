using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using ApiSupport.Models;

namespace ResZServer
{
    [Authorize]
    [RoutePrefix("resz")]
    public class RoleController : ApiController
    {
        [Route("createRole")]
        [HttpPost]
        public HttpResponseMessage CreateRole(SimpleModel model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                Roles.CreateRole(model.Value);
                return Request.CreateResponse(HttpStatusCode.OK, "role_created");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [Route("DeleteRole")]
        [HttpPost]
        public HttpResponseMessage DeleteRole(SimpleModel model)
        {

            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                bool result = Roles.DeleteRole(model.Value);
                return Request.CreateResponse(HttpStatusCode.OK, "role_deleted");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [Route("GetAllRoles")]
        [HttpGet]
        public HttpResponseMessage GetRoles()
        {
            try
            {
                var roles = Roles.GetAllRoles();
                return Request.CreateResponse(HttpStatusCode.OK, roles);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }
    }
}
