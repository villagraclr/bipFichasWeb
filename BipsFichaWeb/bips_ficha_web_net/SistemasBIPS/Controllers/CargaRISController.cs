using log4net;
using MDS.Core.Providers;
using MDS.Dto;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SistemasBIPS.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SistemasBIPS.Controllers
{
    public class CargaRISController : Controller
    {
        private IProviderConstante constantes = null;
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(MantenedoresController));

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public CargaRISController()
        {
            this.constantes = (IProviderConstante)Activator.CreateInstance(typeof(ProviderConstante));
        }

        // GET: CargaRIS
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetPanelCargaRIS()
        {
            IList<TablaProgramasDto> objCargaRIS = new List<TablaProgramasDto>();
            try
            {
                CargaRISModels programas = new CargaRISModels();
                objCargaRIS = await programas.getPanelCargaRIS(new TablaProgramasFiltroDto() { Ano = DateTime.Now.Year, IdPlataforma = decimal.Parse(constantes.GetValue("ProgramasRIS")), TipoFormulario = decimal.Parse(constantes.GetValue("TipoMonitoreo2023")) }, User.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                return View("Error", ex);
            }
            return Content(JsonConvert.SerializeObject(objCargaRIS, Formatting.None));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetCargaBenefRIS(int idPrograma, HttpPostedFileBase beneficiarios, String justificacion, String tieneBeneficiarios, String tipoJustificacion)
        {
            string estado = string.Empty;
            try
            {
                CargaRISModels cargaBeneficiarios = new CargaRISModels();
                estado = await cargaBeneficiarios.envioBeneficiariosRIS(idPrograma, User.Identity.GetUserId(), Server.MapPath(constantes.GetValue("CarpetaBeneficiariosRIS")), beneficiarios, justificacion, tieneBeneficiarios, tipoJustificacion, User.Identity.GetUserId());
            }
            catch (Exception ex)
            {
                estado = ex.Message;
            }
            return Json(estado, JsonRequestBehavior.AllowGet);
        }
    }
}