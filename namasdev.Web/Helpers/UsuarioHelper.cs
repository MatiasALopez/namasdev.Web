using System.Linq;
using System.Web;

using Microsoft.AspNet.Identity;

namespace namasdev.Web.Helpers
{
	public class UsuarioHelper
	{
		private HttpContextBase _context;

        public UsuarioHelper(HttpContextBase context)
		{
			_context = context;
		}

		public bool UsuarioLogueado
		{
			get { return _context.User.Identity.IsAuthenticated; }
		}

        private string _usuarioId;
        public string UsuarioId
        {
            get
            {
                return
                    _usuarioId
                    ?? (_usuarioId = UsuarioLogueado
                            ? _context.User.Identity.GetUserId()
                            : null);
            }
        }

		public bool PerteneceAlRol(string rol)
		{
			return _context.User.IsInRole(rol);
		}

		public bool PerteneceAAlgunoDeLosRoles(params string[] roles)
		{
			if (roles == null || !roles.Any())
			{
				return false;
			}

			return roles.Any(r => _context.User.IsInRole(r));
		}

        public bool NoPerteneceAAlgunoDeLosRoles(params string[] roles)
        {
            if (roles == null || !roles.Any())
            {
                return false;
            }

            return !roles.Any(r => _context.User.IsInRole(r));
        }
    }
}