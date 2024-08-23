using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.Owin.Security;

using namasdev.Web.Helpers;

namespace namasdev.Web.Controllers
{
    public class ControllerBase : Controller
    {
        private ControllerHelper _controllerHelper;
        protected ControllerHelper ControllerHelper
        {
            get { return _controllerHelper ?? (_controllerHelper = new ControllerHelper(this)); }
        }

        private UsuarioHelper _usuarioHelper;
        protected UsuarioHelper UsuarioHelper
        {
            get { return _usuarioHelper ?? (_usuarioHelper = new UsuarioHelper(this.HttpContext)); }
        }

        private IAuthenticationManager _authenticationManager;
        protected IAuthenticationManager AuthenticationManager
        {
            get { return _authenticationManager ?? (_authenticationManager = HttpContext.GetOwinContext().Authentication); }
        }

        private SessionHelper _sessionHelper;
        protected SessionHelper SessionHelper
        {
            get { return _sessionHelper ?? (_sessionHelper = new SessionHelper(this.HttpContext.Session)); }
        }

        private string _usuarioId;
        protected string UsuarioId
        {
            get { return _usuarioId ?? (_usuarioId = UsuarioHelper.UsuarioId); }
        }

        protected string UserName
        {
            get { return User.Identity.Name; }
        }

        public JsonResult CrearJsonResultOk(
            string mensaje = null,
            JsonRequestBehavior jsonRequestBehaviour = JsonRequestBehavior.DenyGet)
        {
            return CrearJsonResult(ok: true, mensaje: mensaje, jsonRequestBehaviour: jsonRequestBehaviour);
        }

        public JsonResult CrearJsonResultError(string mensaje,
            JsonRequestBehavior jsonRequestBehaviour = JsonRequestBehavior.DenyGet)
        {
            return CrearJsonResult(ok: false, mensaje: mensaje, jsonRequestBehaviour: jsonRequestBehaviour);
        }

        private JsonResult CrearJsonResult(bool ok, string mensaje,
            JsonRequestBehavior jsonRequestBehaviour = JsonRequestBehavior.DenyGet)
        {
            return Json(new { ok, mensaje }, jsonRequestBehaviour);
        }

        public void SignOutAndClearSession()
        {
            var types = AuthenticationManager.GetAuthenticationTypes();
            AuthenticationManager.SignOut(types.Select(t => t.AuthenticationType).ToArray());

            Session.RemoveAll();
        }
    }
}