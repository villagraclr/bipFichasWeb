using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using log4net;
using MDS.Dto;
using SistemasBIPS.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MDS.Core.Providers;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using MDS.Core.Enum;
using System.IO;

namespace SistemasBIPS.Controllers
{
    public class MantenedoresController : Controller
    {
        private ApplicationUserManager _userManager;
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
        private IProviderConstante constantes = null;
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(MantenedoresController));

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public MantenedoresController()
        {
            this.constantes = (IProviderConstante)Activator.CreateInstance(typeof(ProviderConstante));
        }
        #endregion

        #region Usuarios
        [Authorize]
        public async Task<ActionResult> Users()
        {
            UsuariosModels usuarios = new UsuariosModels();
            if (await usuarios.getPerfilesPermisos(int.Parse(constantes.GetValue("PermisoUsuarios")), User.Identity.GetUserId())){
                UsuariosViewModels viewModelUsuarios = new UsuariosViewModels();
                ProgramasModels ministeriosServicios = new ProgramasModels();
                viewModelUsuarios.listaPermisosPerfiles = await usuarios.getPerfilesPermisos(User.Identity.GetUserId());
                viewModelUsuarios.listaMinistServ = await ministeriosServicios.getListaMinistServ();
                viewModelUsuarios.listaPerfiles = await usuarios.getPerfiles(User.Identity.GetUserId());
                viewModelUsuarios.listaAnos = await usuarios.getAnosPermisos(User.Identity.GetUserId());//await ministeriosServicios.getAnos();
                FormulariosModels formularios = new FormulariosModels();
                viewModelUsuarios.tiposGrupos = await formularios.getListaParametros(new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("GruposFormularios")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                viewModelUsuarios.plantillas = await usuarios.getPlantillasFormularios(new TablaExcepcionesPlantillasFiltroDto() { Estado = decimal.Parse(constantes.GetValue("Activo")) }, User.Identity.GetUserId());
                viewModelUsuarios.tipos = await formularios.getListaParametros(new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("TiposFormularios")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                viewModelUsuarios.dataUsuario = await usuarios.getDatosUsuarios(User.Identity.GetUserId());
                return View(viewModelUsuarios);
            }else{
                return View("AccesoRestringido", new AccesoRestringido() { Vista = "Usuarios" });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetUsuarios(Nullable<decimal> idPerfil = null, Nullable<decimal> idGore = null, Nullable<decimal> idPerfilGore = null)
        {
            List<TablaUsuariosDto> objUsuarios = new List<TablaUsuariosDto>();
            try{                
                UsuariosModels usuarios = new UsuariosModels();
                objUsuarios = await usuarios.getUsuariosFiltro(new TablaUsuariosFiltroDto() { IdEstado = decimal.Parse(constantes.GetValue("Activo")), IdPerfil = idPerfil, IdGore = idGore });
            }
            catch (Exception ex){
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objUsuarios, Formatting.None));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetUsuariosGrupos(int idGrupo)
        {
            List<TablaUsuariosDto> objUsuarios = new List<TablaUsuariosDto>();
            try
            {
                GruposModels userGrupos = new GruposModels();
                objUsuarios = await userGrupos.getUsuariosGrupos(new TablaUsuariosFiltroDto() { IdGrupo = idGrupo });                
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objUsuarios, Formatting.None));
        }

        [Authorize]
        public async Task<ActionResult> NuevoUsuario(string _id = null)
        {            
            UsuariosModels usuarios = new UsuariosModels();
            if (await usuarios.getPerfilesPermisos(int.Parse(constantes.GetValue("PermisoNuevosUsuarios")), User.Identity.GetUserId()))
            {
                UsuariosViewModels viewModelUsuarios = new UsuariosViewModels();
                ProgramasModels ministeriosServicios = new ProgramasModels();
                viewModelUsuarios.listaMinistServ = await ministeriosServicios.getListaMinistServ();
                viewModelUsuarios.listaPerfiles = await usuarios.getPerfiles(User.Identity.GetUserId());
                ProgramasModels utiles = new ProgramasModels();
                viewModelUsuarios.listaAnos = await utiles.getAnos();
                if (!String.IsNullOrEmpty(_id))
                    viewModelUsuarios.dataUsuario = await usuarios.getDatosUsuarios(_id);
                
                return View(viewModelUsuarios);
            }
            else
            {
                return View("AccesoRestringido", new AccesoRestringido() { Vista = "Nuevo Usuario" });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetPermisos(string id)
        {
            List<TablaPerfilesDto> objPermisos = new List<TablaPerfilesDto>();
            try
            {
                UsuariosModels usuarios = new UsuariosModels();
                objPermisos = await usuarios.getPerfilesPermisos(new TablaPerfilesFiltroDto() { IdPerfil = decimal.Parse(id), Estado = decimal.Parse(constantes.GetValue("Activo")) });
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objPermisos, Formatting.None));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetGruposFormularios(string id = null)
        {
            List<TablaGruposFormulariosDto> objGruposFormularios = new List<TablaGruposFormulariosDto>();
            try
            {                
                UsuariosModels usuarios = new UsuariosModels();
                objGruposFormularios = await usuarios.getGruposFormularios(new TablaGruposFormulariosFiltroDto() { Estado = decimal.Parse(constantes.GetValue("Activo")) }, id);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objGruposFormularios, Formatting.None));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetFormulariosGrupos(string id)
        {
            List<TablaFormulariosGruposDto> objFormulariosGrupos = new List<TablaFormulariosGruposDto>();
            try
            {
                UsuariosModels usuarios = new UsuariosModels();
                objFormulariosGrupos = await usuarios.getFormulariosGrupos(new TablaFormulariosGruposFiltroDto() { IdGrupoFormulario = decimal.Parse(id), Estado = decimal.Parse(constantes.GetValue("Activo")) });
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objFormulariosGrupos, Formatting.None));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetPerfiles()
        {
            List<TablaPerfilesDto> objPerfiles = new List<TablaPerfilesDto>();
            try
            {
                UsuariosModels usuarios = new UsuariosModels();
                objPerfiles = await usuarios.getPerfiles(new TablaPerfilesFiltroDto() { Estado = decimal.Parse(constantes.GetValue("Activo")) });
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objPerfiles, Formatting.None));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GuardaUsuario(NuevaDataUsuario data)
        {
            String estado = string.Empty;
            try {
                UsuariosModels usuarios = new UsuariosModels();
                if (await usuarios.getPerfilesPermisos(int.Parse(constantes.GetValue("PermisoNuevosUsuarios")), User.Identity.GetUserId())){
                    var datosUser = new ApplicationUser
                    {
                        UserName = data.Email,
                        Email = data.Email,
                        Ministerio = data.Ministerio,
                        Servicio = data.Servicio,                        
                        Nombre = data.Nombre,
                        Perfil = data.Perfil,
                        Gore = data.IdGore,
                        PerfilGore = data.IdPerfilGore
                    };
                    var usuario = await UserManager.FindByNameAsync(data.Email);
                    if (usuario == null && data.IdUser == null){
                        datosUser.Estado = int.Parse(constantes.GetValue("Activo"));
                        string pass = usuarios.creaContraseña(int.Parse(constantes.GetValue("LargoPass")));
                        var resultado = await UserManager.CreateAsync(datosUser, pass);
                        if (resultado.Succeeded){
                            //Envia email a usuario creado
                            var nuevoIdUser = await UserManager.FindByNameAsync(data.Email);
                            string code = await UserManager.GenerateEmailConfirmationTokenAsync(nuevoIdUser.Id);
                            var callbackUrl = Url.Action("ConfirmEmail", "Login", new { userId = nuevoIdUser.Id, code = code }, protocol: Request.Url.Scheme);
                            string msj1 = "Se ha creado su usuario en el portal de Monitoreo de la Oferta Pública.";
                            string msj2 = "Sus datos de registros son los siguientes:<br/> Usuario: <b>{0}</b><br/> Contraseña: <b>{1}</b>";
                            string msj3 = "Para confirmar la cuenta, haga clic <a href =\"{0}\">aquí</a>";
                            await UserManager.SendEmailAsync(nuevoIdUser.Id,
                                                            "Activación de Usuario",
                                                            String.Format(new CuerpoEmail().emailHtml, msj1, String.Format(msj2, nuevoIdUser.UserName, pass), String.Format(msj3, callbackUrl)));
                            estado = "ok";
                        } else{
                            estado = resultado.Errors.FirstOrDefault();
                        }
                    }else{                        
                        if (usuario.Estado == int.Parse(constantes.GetValue("Inactivo"))){
                            estado = await usuarios.actualizaEstadoUser(new TablaUsuariosDto() { Id = usuario.Id, IdEstado = decimal.Parse(constantes.GetValue("Activo")), TipoUpdate = decimal.Parse(constantes.GetValue("UpdateEstadoUser")) });
                        }else{
                            datosUser.Id = usuario.Id;
                            var resultado = await UserManager.UpdateAsync(datosUser);
                            if (resultado.Succeeded)
                                estado = "ok";
                            else
                                estado = resultado.Errors.FirstOrDefault();
                        }                                                    
                    }
                }else{
                    estado = "Sin permiso para realizar esta acción";
                }
            }catch (Exception ex) {
                estado = ex.Message;
            }            
            return Json(estado, JsonRequestBehavior.AllowGet);

            //UsuariosModels usuarios = new UsuariosModels();
            //List<TablaUsuariosDto> listaUsuarios = new List<TablaUsuariosDto>();            
            //listaUsuarios = await usuarios.getUsuariosFiltro(new TablaUsuariosFiltroDto() { Email = model.dataUsuario.Email });            
            //if (listaUsuarios == null)
            //{
            //    var usuario = new ApplicationUser {
            //        UserName = model.dataUsuario.Email,
            //        Email = model.dataUsuario.Email,
            //        Ministerio = model.dataUsuario.Ministerio,
            //        Servicio = model.dataUsuario.Servicio,
            //        Estado = int.Parse(constantes.GetValue("Activo")),
            //        Nombre = model.dataUsuario.Nombre,
            //        Perfil = model.dataUsuario.Perfil
            //    };
            //    string pass = usuarios.creaContraseña(6);
            //    var resultado = await UserManager.CreateAsync(usuario, pass);
            //    if (resultado.Succeeded)
            //    {
            //        listaUsuarios = await usuarios.getUsuariosFiltro(new TablaUsuariosFiltroDto() { Email = model.dataUsuario.Email });
            //        if (!await usuarios.registraGrupFormUsuarios(listaUsuarios, model))
            //            return View("Error", new Exception("Error al registrar formularios usuarios"));

            //        string code = await UserManager.GenerateEmailConfirmationTokenAsync(listaUsuarios.SingleOrDefault().Id);
            //        var callbackUrl = Url.Action("ConfirmEmail", "Login", new { userId = listaUsuarios.SingleOrDefault().Id, code = code }, protocol: Request.Url.Scheme);
            //        await UserManager.SendEmailAsync(listaUsuarios.SingleOrDefault().Id,
            //                                        "Activación de Usuario", 
            //                                        String.Format(new CuerpoEmail().emailHtml, "Se ha creado su usuario en el portal de Monitoreo de Programas Sociales.", "Sus datos de registros son los siguientes:<br/> Usuario: <b>" + listaUsuarios.SingleOrDefault().UserName + "</b><br/> Contraseña: <b>" + pass + "</b>", "Para confirmar la cuenta, haga clic <a href =\"" + callbackUrl + "\">aquí</a>")
            //                                        );
            //        return RedirectToAction("Users", "Mantenedores");
            //    }
            //}else{
            //    var usuario = new ApplicationUser {
            //        UserName = model.dataUsuario.Email,
            //        Email = model.dataUsuario.Email,
            //        Ministerio = model.dataUsuario.Ministerio,
            //        Servicio = model.dataUsuario.Servicio,
            //        Nombre = model.dataUsuario.Nombre,
            //        Id = listaUsuarios.SingleOrDefault().Id,
            //        Perfil = model.dataUsuario.Perfil
            //    };
            //    var resultado = await UserManager.UpdateAsync(usuario);
            //    if (resultado.Succeeded)
            //    {
            //        bool borrado = await usuarios.borrarGrupFormUsuarios(new TablaFormulariosUsuariosDto() { IdUsuario = listaUsuarios.SingleOrDefault().Id });
            //        if (borrado)
            //        {
            //            if (!await usuarios.registraGrupFormUsuarios(listaUsuarios, model))
            //                return View("Error", new Exception("Error al registrar formularios usuarios"));

            //            return RedirectToAction("Users", "Mantenedores");
            //        }
            //    }
            //}
            //return RedirectToAction("NuevoUsuario", "Mantenedores");
        }

        [Authorize]
        [HttpPost]
        public ActionResult RenuevaSesion()
        {
            String sesion = "sesion renovada";
            return Json(sesion, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetGruposUsers(string id)
        {
            List<TablaGruposFormulariosDto> objGruposUsers = new List<TablaGruposFormulariosDto>();
            try
            {
                UsuariosModels usuarios = new UsuariosModels();
                objGruposUsers = await usuarios.getGruposUsuariosFiltros(new TablaGruposFormulariosFiltroDto() { IdUsuario = id, Estado = decimal.Parse(constantes.GetValue("Activo")) });
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objGruposUsers, Formatting.None));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetPermisosUser(string id)
        {
            List<TablaProgramasDto> objGruposUsers = new List<TablaProgramasDto>();
            try
            {
                UsuariosModels usuarios = new UsuariosModels();
                objGruposUsers = await usuarios.getPermisosFormularios(new TablaProgramasFiltroDto() { Estado = decimal.Parse(constantes.GetValue("Activo")), IdUser = id });
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objGruposUsers, Formatting.None));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetFormulariosUser(string id, Nullable<decimal> tipoFormulario = null)
        {
            List<TablaProgramasDto> objFormulariosUser = new List<TablaProgramasDto>();
            try {
                UsuariosModels usuarios = new UsuariosModels();
                objFormulariosUser = await usuarios.getFormulariosPermisos(id, User.Identity.GetUserId(), new TablaProgramasFiltroDto() {
                    IdUser = id,
                    Estado = decimal.Parse(constantes.GetValue("Activo")),
                    TipoFormulario = tipoFormulario
                });
            }
            catch (Exception ex){
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objFormulariosUser, Formatting.None));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EliminaUsuarioEstado(string id)
        {
            string estado = string.Empty;
            try
            {
                UsuariosModels usuarios = new UsuariosModels();
                estado = await usuarios.eliminaUsuariosEstado(id);
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> ReenviaMail(string id)
        {
            string estado = string.Empty;
            try
            {
                UsuariosModels usuarios = new UsuariosModels();
                var usuario = await UserManager.FindByIdAsync(id);
                if (usuario != null){
                    string code = await UserManager.GeneratePasswordResetTokenAsync(usuario.Id);
                    var callbackUrl = Url.Action("RestablecerPass", "Login", new { userId = usuario.Id, code = code }, protocol: Request.Url.Scheme);
                    var urlSistema = Url.Action("Login", "Login", new { }, protocol: Request.Url.Scheme);
                    await UserManager.SendEmailAsync(usuario.Id,
                                                    "Restablecer contraseña",
                                                    String.Format(new CuerpoEmail().emailHtml, "Se ha solicitado restablecer la contraseña del usuario <b>" + usuario.UserName + "</b> en el portal de Monitoreo de la Oferta Pública (<a href =\"" + urlSistema  + "\">Sistema de carga BIPS</a>).", "", "Para restablecer la contraseña, haga clic <a href =\"" + callbackUrl + "\">aquí</a>")
                                                    );                    
                    estado = "ok";
                }else{
                    estado = "usuario no indentificado";
                }
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> NuevaPass(string id)
        {
            string estado = string.Empty;
            try{
                UsuariosModels usuarios = new UsuariosModels();
                var usuario = await UserManager.FindByIdAsync(id);
                if (usuario != null){
                    string pass = usuarios.creaContraseña(int.Parse(constantes.GetValue("LargoPass")));
                    string code = await UserManager.GeneratePasswordResetTokenAsync(usuario.Id);
                    var result = await UserManager.ResetPasswordAsync(usuario.Id, code, pass);                    
                    if (result.Succeeded)
                        estado = pass;
                }else{
                    estado = "usuario no indentificado";
                }
            }catch (Exception ex){
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EliminaGrupoUsuario(TablaGruposFormulariosDto data)
        {
            string estado = string.Empty;
            try
            {
                UsuariosModels usuarios = new UsuariosModels();
                estado = await usuarios.eliminaGruposUsuarios(data);
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Grupos
        [Authorize]
        public async Task<ActionResult> Grupos()
        {
            UsuariosModels usuarios = new UsuariosModels();
            if (await usuarios.getPerfilesPermisos(int.Parse(constantes.GetValue("PermisoGrupos")), User.Identity.GetUserId())){
                GruposViewModels viewModelGrupos = new GruposViewModels();
                viewModelGrupos.listaPermisosPerfiles = await usuarios.getPerfilesPermisos(User.Identity.GetUserId());
                ProgramasModels utiles = new ProgramasModels();
                viewModelGrupos.listaMinistServ = await utiles.getListaMinistServ();
                viewModelGrupos.listaAnos = await utiles.getAnos();
                FormulariosModels formularios = new FormulariosModels();
                viewModelGrupos.tipos = await formularios.getListaParametros(new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("TiposFormularios")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                viewModelGrupos.tiposGrupos = await formularios.getListaParametros(new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("GruposFormularios")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                return View(viewModelGrupos);
            }else{
                return View("AccesoRestringido", new AccesoRestringido() { Vista = "Grupos" });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetFormularios(int idGrupo)
        {
            List<TablaProgramasDto> objFormularios = new List<TablaProgramasDto>();
            try
            {
                GruposModels grupos = new GruposModels();
                objFormularios = await grupos.getFormulariosGrupos(new TablaProgramasFiltroDto() { Estado = decimal.Parse(constantes.GetValue("Activo")) }, idGrupo);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objFormularios, Formatting.None));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> NuevoGrupo(TablaGruposFormulariosDto data)
        {
            string estado = string.Empty;
            try {
                UsuariosModels usuarios = new UsuariosModels();
                if (await usuarios.getPerfilesPermisos(int.Parse(constantes.GetValue("PermisoNuevosGrupos")), User.Identity.GetUserId())) {
                    GruposModels grupos = new GruposModels();
                    estado = await grupos.registraGruposFormularios(data, (data.IdGrupoFormulario == null ? EnumAccionRealizar.Insertar : EnumAccionRealizar.Actualizar));
                }else{
                    estado = "Sin permiso para realizar esta acción";
                }
            }catch(Exception ex){
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EliminaGrupo(TablaGruposFormulariosDto data)
        {
            string estado = string.Empty;
            try{                
                GruposModels grupos = new GruposModels();                
                estado = await grupos.eliminaGrupoFormularios(data);
            }
            catch (Exception ex){
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EliminaPermiso(List<TablaExcepcionesPermisosDto> data)
        {
            string estado = string.Empty;
            try{
                UsuariosModels usuarios = new UsuariosModels();                
                estado = await usuarios.eliminaPermisosUsuarios(data);
            }catch (Exception ex){
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EliminaUsuario(TablaGruposFormulariosDto data)
        {
            string estado = string.Empty;
            try
            {
                GruposModels grupos = new GruposModels();
                data.Estado = decimal.Parse(constantes.GetValue("Inactivo"));
                estado = await grupos.registraGruposFormularios(data, EnumAccionRealizar.EliminarUserGrupo);
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GuardaUsuariosGrupos(List<TablaFormulariosUsuariosDto> data)
        {
            string estado = string.Empty;
            try
            {
                UsuariosModels usuarios = new UsuariosModels();
                if (await usuarios.getPerfilesPermisos(int.Parse(constantes.GetValue("PermisoAsignarUsersGroup")), User.Identity.GetUserId()))
                {
                    GruposModels grupos = new GruposModels();
                    estado = await grupos.registraUsuariosGrupos(data);
                }
                else
                {
                    estado = "Sin permiso para realizar esta acción";
                }
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GuardaUsuariosPermisos(List<TablaExcepcionesPermisosDto> data)
        {
            string estado = string.Empty;
            try
            {
                UsuariosModels usuarios = new UsuariosModels();
                if (await usuarios.getPerfilesPermisos(int.Parse(constantes.GetValue("PermisoAsignarPermisosUsers")), User.Identity.GetUserId()) && await usuarios.getPerfilesPermisosTotales(data, User.Identity.GetUserId()))                    
                    estado = await usuarios.registraPermisosUsuarios(data);
                else
                    estado = "Sin permiso para realizar esta acción";
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GuardaPermisosMasivo(TablaProgramasFiltroDto data)
        {
            string estado = string.Empty;
            try
            {
                UsuariosModels usuarios = new UsuariosModels();
                if (await usuarios.getPerfilesPermisos(int.Parse(constantes.GetValue("PermisoAsignarPermisosUsers")), User.Identity.GetUserId()))
                    estado = await usuarios.registraPermisosUsersMasivo(data, User.Identity.GetUserId());
                else
                    estado = "Sin permiso para realizar esta acción";
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetUsuariosGruposAsignados(int idGrupo)
        {
            List<TablaUsuariosDto> objUsuarios = new List<TablaUsuariosDto>();
            try
            {
                GruposModels grupos = new GruposModels();
                objUsuarios = await grupos.getUsuariosGruposAsignados(new TablaUsuariosFiltroDto() { IdGrupo = idGrupo, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objUsuarios, Formatting.None));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EliminaFormularioGrupo(TablaFormulariosGruposDto data)
        {
            string estado = string.Empty;
            try
            {
                GruposModels grupos = new GruposModels();
                data.Estado = decimal.Parse(constantes.GetValue("Inactivo"));
                estado = await grupos.registraFormulariosGrupos(data);
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EliminaFormGrupoMasivo(TablaProgramasFiltroDto data, int idGrupo)
        {
            string estado = string.Empty;
            try
            {
                GruposModels grupos = new GruposModels();
                estado = await grupos.eliminaFormGruposMasivo(data, idGrupo);
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GuardaFormulariosGrupo(List<TablaFormulariosGruposDto> data)
        {
            string estado = string.Empty;
            try
            {
                UsuariosModels usuarios = new UsuariosModels();
                if (await usuarios.getPerfilesPermisos(int.Parse(constantes.GetValue("PermisoAsignarFormGrupos")), User.Identity.GetUserId()))
                {
                    GruposModels grupos = new GruposModels();
                    estado = await grupos.registraFormulariosGrupos(data);
                }
                else
                {
                    estado = "Sin permiso para realizar esta acción";
                }
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GuardaFormulariosGrupoMasivo(TablaProgramasFiltroDto data, int idGrupo)
        {
            string estado = string.Empty;
            try{
                UsuariosModels usuarios = new UsuariosModels();
                if (await usuarios.getPerfilesPermisos(int.Parse(constantes.GetValue("PermisoAsignarFormGrupos")), User.Identity.GetUserId())){
                    GruposModels grupos = new GruposModels();                    
                    estado = await grupos.registraFormulariosGrupos(data, idGrupo);
                }else{
                    estado = "Sin permiso para realizar esta acción";
                }
            }catch (Exception ex){
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EliminaPermisosMasivo(TablaProgramasFiltroDto data)
        {
            string estado = string.Empty;
            try
            {                
                UsuariosModels usuarios = new UsuariosModels();
                estado = await usuarios.eliminaPermisosUsersMasivo(data);
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }
        #endregion        

        #region Formularios
        [Authorize]
        public async Task<ActionResult> Formularios()
        {
            UsuariosModels usuarios = new UsuariosModels();
            if (await usuarios.getPerfilesPermisos(int.Parse(constantes.GetValue("PermisoNuevosFormularios")), User.Identity.GetUserId()))
            {
                NuevoFormulariosViewModels viewModelNuevoFormulario = new NuevoFormulariosViewModels();
                viewModelNuevoFormulario.listaPermisosPerfiles = await usuarios.getPerfilesPermisos(User.Identity.GetUserId());
                ProgramasModels ministeriosServicios = new ProgramasModels();
                FormulariosModels formularios = new FormulariosModels();
                viewModelNuevoFormulario.listaMinistServ = await ministeriosServicios.getListaMinistServ();
                viewModelNuevoFormulario.tipos = await formularios.getListaParametros(new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("TiposFormularios")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                ProgramasModels utiles = new ProgramasModels();
                viewModelNuevoFormulario.listaAnos = await utiles.getAnos();
                FormulariosMantenedorModels formularioMantenedor = new FormulariosMantenedorModels();
                viewModelNuevoFormulario.listaGrupos = await formularioMantenedor.getGruposFormularios(new TablaGruposFormulariosFiltroDto() { Estado = decimal.Parse(constantes.GetValue("Activo")) });
                viewModelNuevoFormulario.listaAnosDestino = await formularioMantenedor.getAnosCreacionForm(User.Identity.GetUserId()); //await formularios.getAnosFormularios(int.Parse(constantes.GetValue("AnoInicioNuevoForm")), int.Parse(constantes.GetValue("AnoTMasNuevoForm")), User.Identity.GetUserId());
                viewModelNuevoFormulario.listaPlantillasTraspaso = await formularioMantenedor.getPlantillasTraspaso(new TablaPlantillasTraspasoFiltroDto() { Estado = decimal.Parse(constantes.GetValue("Activo")) });
                return View(viewModelNuevoFormulario);
            }
            else
            {
                return View("AccesoRestringido", new AccesoRestringido() { Vista = "Usuarios" });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetFormulariosMantenedor()
        {
            List<TablaProgramasDto> objFormularios = new List<TablaProgramasDto>();
            try
            {
                FormulariosMantenedorModels formularios = new FormulariosMantenedorModels();
                objFormularios = await formularios.getFormularios(new TablaProgramasFiltroDto() { Estado = decimal.Parse(constantes.GetValue("Activo")) });
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objFormularios, Formatting.None));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetFormulariosIteraciones()
        {
            List<TablaProgramasDto> objFormularios = new List<TablaProgramasDto>();
            try
            {
                FormulariosMantenedorModels formularios = new FormulariosMantenedorModels();
                objFormularios = await formularios.getFormularios(new TablaProgramasFiltroDto() {
                    Etapa = decimal.Parse(constantes.GetValue("EtapaCierreEvalExAnte")),
                    Estado = decimal.Parse(constantes.GetValue("Activo"))
                });
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objFormularios, Formatting.None));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GuardaFormulario(TablaProgramasDto data)
        {
            string estado = string.Empty;
            try
            {
                FormulariosMantenedorModels formularios = new FormulariosMantenedorModels();
                UsuariosModels usuarios = new UsuariosModels();
                if (await usuarios.getPerfilesPermisos(int.Parse(constantes.GetValue("PermisoNuevosFormularios")), User.Identity.GetUserId()))
                    estado = await formularios.registraFormularios(data);
                else
                    estado = "Sin permiso para realizar esta acción";
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GuardaFormulariosRestruc(List<TablaProgramasDto> data)
        {
            string estado = string.Empty;
            try
            {
                FormulariosMantenedorModels formularios = new FormulariosMantenedorModels();
                UsuariosModels usuarios = new UsuariosModels();
                if (await usuarios.getPerfilesPermisos(int.Parse(constantes.GetValue("PermisoNuevosFormularios")), User.Identity.GetUserId()))
                    estado = await formularios.registraFormularios(data);
                else
                    estado = "Sin permiso para realizar esta acción";
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GuardaFormularioRest(List<TablaProgramasDto> data1, TablaProgramasDto data2)
        {
            string estado = string.Empty;
            try {
                FormulariosMantenedorModels formularios = new FormulariosMantenedorModels();
                UsuariosModels usuarios = new UsuariosModels();
                if (await usuarios.getPerfilesPermisos(int.Parse(constantes.GetValue("PermisoNuevosFormularios")), User.Identity.GetUserId()))
                    estado = await formularios.registraFormularios(data1, data2);
            }
            catch (Exception ex) {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetFormFiltrosMantenedor(TablaProgramasFiltroDto filtros)
        {
            List<TablaProgramasDto> objFormularios = new List<TablaProgramasDto>();
            try
            {
                FormulariosMantenedorModels formularios = new FormulariosMantenedorModels();
                filtros.Estado = decimal.Parse(constantes.GetValue("Activo"));
                objFormularios = await formularios.getFormularios(filtros);
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objFormularios, Formatting.None));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetFormRestruccMantenedor(int filtro)
        {
            List<TablaProgramasDto> objFormularios = new List<TablaProgramasDto>();
            try
            {
                FormulariosMantenedorModels formularios = new FormulariosMantenedorModels();
                objFormularios = await formularios.getFormularios(new TablaProgramasFiltroDto() { Estado = decimal.Parse(constantes.GetValue("Activo")), Ano = (filtro == -1 ? (decimal?)null : filtro) });
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objFormularios, Formatting.None));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> EliminarFormulario(TablaProgramasDto data)
        {
            string estado = string.Empty;
            try {
                FormulariosMantenedorModels formularios = new FormulariosMantenedorModels();
                estado = await formularios.eliminarFormulario(data);
            }
            catch(Exception ex) {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetUserGruposFormularios(TablaProgramasFiltroDto data, string tipo)
        {
            List<TablaProgramasDto> objUserGruposForm = new List<TablaProgramasDto>();
            try{
                FormulariosMantenedorModels formularios = new FormulariosMantenedorModels();
                objUserGruposForm = await formularios.getUserGruposFormularios(data, tipo);
            }
            catch (Exception ex){
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objUserGruposForm, Formatting.None));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GuardaIteracionesFormularios(List<TablaProgramasDto> data)
        {
            string estado = string.Empty;
            try
            {
                UsuariosModels usuarios = new UsuariosModels();
                if (await usuarios.getPerfilesPermisos(int.Parse(constantes.GetValue("PermisoNuevosFormularios")), User.Identity.GetUserId()))
                {
                    FormulariosMantenedorModels formularios = new FormulariosMantenedorModels();
                    estado = await formularios.registraIteracionFormularios(data, User.Identity.GetUserId());
                }
                else
                {
                    estado = "Sin permiso para realizar esta acción";
                }
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> CreaArchivoJson(int tipo)
        {
            string estado = string.Empty;
            try
            {
                estado = "ok";
                FormulariosModels formularios = new FormulariosModels();
                //Crea formulario JSON
                NewFormulariosViewModels dataFormulario = await formularios.getFormularioJS(User.Identity.GetUserId(), tipo);
                string rutaArchivo = Path.Combine(Server.MapPath(constantes.GetValue("CarpetaArchivosSubidos")), Path.GetFileName(string.Format("formulario_{0}.json", tipo)));
                if (System.IO.File.Exists(rutaArchivo))
                    System.IO.File.Delete(rutaArchivo);
                System.IO.File.WriteAllText(rutaArchivo, JsonConvert.SerializeObject(dataFormulario, Formatting.None));
                //Crea funciones JSON
                IList<TablaFuncionesDependenciasDto> objFunciones = new List<TablaFuncionesDependenciasDto>();
                objFunciones = await formularios.getFuncionesFormulariosJSON(User.Identity.GetUserId(), tipo);
                rutaArchivo = Path.Combine(Server.MapPath(constantes.GetValue("CarpetaArchivosSubidos")), Path.GetFileName(string.Format("funciones_{0}.json", tipo)));
                if (System.IO.File.Exists(rutaArchivo))
                    System.IO.File.Delete(rutaArchivo);
                System.IO.File.WriteAllText(rutaArchivo, JsonConvert.SerializeObject(objFunciones, Formatting.None));
                //Crea excepciones JSON
                IList<TablaExcepcionesPreguntasDto> excepciones = new List<TablaExcepcionesPreguntasDto>();
                excepciones = await formularios.getExcepcionesPreguntas(new TablaExcepcionesPreguntasFiltroDto() { IdUsuario = User.Identity.GetUserId(), IdTipoFormulario = tipo, Estado = decimal.Parse(constantes.GetValue("Activo")) });                
                rutaArchivo = Path.Combine(Server.MapPath(constantes.GetValue("CarpetaArchivosSubidos")), Path.GetFileName(string.Format("excepciones_{0}.json", tipo)));
                if (System.IO.File.Exists(rutaArchivo))
                    System.IO.File.Delete(rutaArchivo);
                System.IO.File.WriteAllText(rutaArchivo, JsonConvert.SerializeObject(excepciones, Formatting.None));
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}