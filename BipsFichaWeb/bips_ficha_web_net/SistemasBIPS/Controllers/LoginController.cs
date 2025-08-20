using System;
using System.Web;
using System.Web.Mvc;
using log4net;
using MDS.Dto;
using Microsoft.AspNet.Identity.Owin;
using SistemasBIPS.Models;
using System.Threading.Tasks;
using System.Linq;
using MDS.Core.Providers;

namespace SistemasBIPS.Controllers
{
    public class LoginController : Controller
    {
        #region Variables globales
        private static readonly ILog log = log4net.LogManager.GetLogger((typeof(LoginController)));
        #endregion        

        #region 3.0
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IProviderConstante constantes = null;

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public LoginController() {
            constantes = (IProviderConstante)Activator.CreateInstance(typeof(ProviderConstante));
        }

        public LoginController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [AllowAnonymous]
        public ActionResult HomeExante() {
            return View();
        }

        /// <summary>
        /// GET: Inicio login
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns>Url</returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (!User.Identity.IsAuthenticated){
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }else{
                return RedirectToLocal(returnUrl);
            }            
        }

        /// <summary>
        /// POST: Login
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid){
                return View(model);
            }
            UsuariosModels buscaUsuario = new UsuariosModels();
            var retornoUsuario = await buscaUsuario.getUsuariosFiltro(new TablaUsuariosFiltroDto() { IdEstado = decimal.Parse(constantes.GetValue("Activo")), UserName = model.Email });
            if (retornoUsuario.Count() > 0)
            {
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, shouldLockout: true);
                switch (result)
                {
                    case SignInStatus.Success:
                        LogUsuarioModels usuario = new LogUsuarioModels();
                        ApplicationUser user = await UserManager.FindByNameAsync(model.Email);
                        var buscar = await usuario.buscaLogFormularios(new TablaLogFormulariosFiltroDto { IdUsuario = user.Id, EstadoAcceso = decimal.Parse(constantes.GetValue("Activo")) });
                        if (buscar.Count > 0)
                        {
                            var liberaProg = await usuario.actualizaLogFormularios(new TablaLogFormulariosDto() { IdUsuario = user.Id, EstadoAcceso = decimal.Parse(constantes.GetValue("Inactivo")), TipoUpdate = decimal.Parse(constantes.GetValue("UpdateLiberaFormUsu")) });
                            if (!liberaProg)
                                log.Error("Error al liberar formularios al inicio de sesion");
                        }
                        return RedirectToLocal(returnUrl);
                    case SignInStatus.LockedOut:
                        return View("Bloqueo");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("");
                    case SignInStatus.Failure:
                        ModelState.AddModelError("", "Usuario o contraseña incorrecta");
                        return View(model);
                    default:
                        ModelState.AddModelError("", "Intento de inicio de sesión no válido");
                        return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Intento de inicio de sesión no válido");
                return View(model);
            }
        }        

        /// <summary>
        /// GET: Olvido contraseña
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult OlvidoContraseña()
        {
            return View();
        }

        /// <summary>
        /// POST: Olvido contraseña
        /// </summary>
        /// <param name="model"></param>
        /// <returns>model</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> OlvidoContraseña(OlvidoContraseñaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    // No revelar que el usuario no existe
                    return View();
                }
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("RestablecerPass", "Login", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id,
                                                "Restablecer contraseña",
                                                String.Format(new CuerpoEmail().emailHtml, "Se ha solicitado restablecer la contraseña de su usuario en el portal de Monitoreo de Programas Sociales.", "", "Para restablecer la contraseña, haga clic <a href =\"" + callbackUrl + "\">aquí</a>")
                                                );
                return View("ConfirmOlvidoPass");
            }
            return View(model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Programas", "Programa");
        }
        
        /// <summary>
        /// GET: /Login/ConfirmEmail 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error", new HandleErrorInfo(new Exception("Id o código de confirmación nulo"),"Login", "ConfirmEmail"));
            }
            if (!await UserManager.IsEmailConfirmedAsync(userId))
            {
                var result = await UserManager.ConfirmEmailAsync(userId, code);
                if (result.Succeeded)
                    return View("ConfirmEmail", new CambioPassViewModel() { idUsuario = userId });
                else
                    return View("Error", new HandleErrorInfo(new Exception(result.Errors.FirstOrDefault()), "Login", "ConfirmEmail"));
            }else {
                return RedirectToAction("Login", "Login");
            }            
        }

        /// <summary>
        /// GET: Restablecer contraseña
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult RestablecerPass(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error", new HandleErrorInfo(new Exception("Id o código nulo"), "Login", "RestablecerPass"));
            }            
            return View("RestablecerPass", new ResetPassViewModel() { idUsuario = userId, code = code });            
        }

        /// <summary>
        /// POST: Restablecer contraseña
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RestablecerPass(ResetPassViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (String.IsNullOrEmpty(model.idUsuario))
                    ModelState.AddModelError("", "Usuario no identificado");

                return View("RestablecerPass", model);
            }
            if (model.NewPassword != model.ConfirmNewPassword)
            {
                ModelState.AddModelError("", "Las contraseñas ingresadas no coinciden");
                return View("RestablecerPass", model);
            }
            var result = await UserManager.ResetPasswordAsync(model.idUsuario, model.code, model.NewPassword);
            if (result.Succeeded)
                return View("ConfirmResetPass");
            else
                return View("Error", new HandleErrorInfo(new Exception("Error al restablecer contraseña"), "Login", "RestablecerPass"));
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CambioPass(CambioPassViewModel model)
        {
            if (!ModelState.IsValid){
                if (String.IsNullOrEmpty(model.idUsuario))
                    ModelState.AddModelError("","Usuario no identificado");

                return View("ConfirmEmail", model);
            }
            if (model.NewPassword != model.ConfirmNewPassword){
                ModelState.AddModelError("", "Las contraseñas ingresadas no coinciden");
                return View("ConfirmEmail", model);
            }
            var result = await UserManager.ChangePasswordAsync(model.idUsuario, model.Password, model.NewPassword);
            if (result.Succeeded)
                return View("ConfirmNewPass");
            else
                return View("Error", new HandleErrorInfo(new Exception("Error al cambiar contraseña"), "Login", "CambioPass"));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }
        #endregion
    }
}