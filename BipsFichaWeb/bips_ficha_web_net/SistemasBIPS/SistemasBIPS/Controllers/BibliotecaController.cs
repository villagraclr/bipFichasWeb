using ClosedXML.Excel;
using MDS.Core.Enum;
using MDS.Core.Providers;
using MDS.Dto;
using Newtonsoft.Json;
using SistemasBIPS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SistemasBIPS.Controllers
{
    public class BibliotecaController : Controller
    {
        private DescargaBaseDatosModels descargaBD = null;
        private IProviderConstante constantes = null;

        /// <summary>
        /// Constructor
        /// </summary>
        public BibliotecaController()
        {
            this.descargaBD = new DescargaBaseDatosModels();
            this.constantes = (IProviderConstante)Activator.CreateInstance(typeof(ProviderConstante));
        }

        // GET: Biblioteca
        [Authorize]
        public async Task<ActionResult> Index()
        {            
            DescargaBDViewModels vista = new DescargaBDViewModels();
            vista = await descargaBD.getDatosVista();
            return View(vista);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Descargar(int ano, List<int> categorias, int tipoFormulario)
        {            
            List<DataTable> listaPestañas = new List<DataTable>();
            var opcion = new EnumAccionRealizar();
            if (ano != -1 && categorias.Count > 0 && tipoFormulario > 0){
                int idCategoria = 0;                 
                foreach (var categ in categorias){
                    DataTable dt = new DataTable();
                    switch (categ)
                    {
                        case (int)EnumAccionRealizar.BuscarAntecedentes:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasAntecedentesBD"));
                            opcion = EnumAccionRealizar.BuscarAntecedentes;
                            break;
                        case (int)EnumAccionRealizar.BuscarDiagnostico:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasDiagnosticoBD"));
                            opcion = EnumAccionRealizar.BuscarDiagnostico;
                            break;
                        case (int)EnumAccionRealizar.BuscarEvalAnteriores:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasEvalAnterioresBD"));
                            opcion = EnumAccionRealizar.BuscarEvalAnteriores;
                            break;
                        case (int)EnumAccionRealizar.BuscarPobPotencial:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasPobPotencialBD"));
                            opcion = EnumAccionRealizar.BuscarPobPotencial;
                            break;
                        case (int)EnumAccionRealizar.BuscarPobObjetivo:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasPobObjetivoBD"));
                            opcion = EnumAccionRealizar.BuscarPobObjetivo;
                            break;
                        case (int)EnumAccionRealizar.BuscarPobBeneficiada:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasPobBenefBD"));
                            opcion = EnumAccionRealizar.BuscarPobBeneficiada;
                            break;
                        case (int)EnumAccionRealizar.BuscarEstrategia:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasEstrategiaBD"));
                            opcion = EnumAccionRealizar.BuscarEstrategia;
                            break;
                        case (int)EnumAccionRealizar.BuscarCovid19:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasCovid19BD"));
                            opcion = EnumAccionRealizar.BuscarCovid19;
                            break;
                        case (int)EnumAccionRealizar.BuscarEjecutores:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasEjecutoresBD"));
                            opcion = EnumAccionRealizar.BuscarEjecutores;
                            break;
                        case (int)EnumAccionRealizar.BuscarPresupuesto:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasPresupuestoBD"));
                            opcion = EnumAccionRealizar.BuscarPresupuesto;
                            break;
                        case (int)EnumAccionRealizar.BuscarRecursosEjec:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasRecursosEjecBD"));
                            opcion = EnumAccionRealizar.BuscarRecursosEjec;
                            break;
                        case (int)EnumAccionRealizar.BuscarGastoExtra:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasGastoExtraBD"));
                            opcion = EnumAccionRealizar.BuscarGastoExtra;
                            break;
                        case (int)EnumAccionRealizar.BuscarGastoFet:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasGastoFetBD"));
                            opcion = EnumAccionRealizar.BuscarGastoFet;
                            break;
                        case (int)EnumAccionRealizar.BuscarGastosComp:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasGastoCompBD"));
                            opcion = EnumAccionRealizar.BuscarGastosComp;
                            break;
                        case (int)EnumAccionRealizar.BuscarDetRegGastosComp:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasDetRegGastoCompBD"));
                            opcion = EnumAccionRealizar.BuscarDetRegGastosComp;
                            break;
                        case (int)EnumAccionRealizar.BuscarGastosAdmin:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasGastosAdminBD"));
                            opcion = EnumAccionRealizar.BuscarGastosAdmin;
                            break;
                        case (int)EnumAccionRealizar.BuscarResumenRecEjec:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasResumRecursosEjecBD"));
                            opcion = EnumAccionRealizar.BuscarResumenRecEjec;
                            break;
                        case (int)EnumAccionRealizar.BuscarIndicProp:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasIndicPropBD"));
                            opcion = EnumAccionRealizar.BuscarIndicProp;
                            break;
                        case (int)EnumAccionRealizar.BuscarIndicComp:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasIndicCompBD"));
                            opcion = EnumAccionRealizar.BuscarIndicComp;
                            break;
                        case (int)EnumAccionRealizar.BuscarDDHH:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasDDHHBD"));
                            opcion = EnumAccionRealizar.BuscarDDHH;
                            break;
                        case (int)EnumAccionRealizar.BuscarODS:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasODSBD"));
                            opcion = EnumAccionRealizar.BuscarODS;
                            break;
                        case (int)EnumAccionRealizar.BuscarPobMulti:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasPobMultiBD"));
                            opcion = EnumAccionRealizar.BuscarPobMulti;
                            break;
                        case (int)EnumAccionRealizar.BuscarDiseño:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasDiseñoBD"));
                            opcion = EnumAccionRealizar.BuscarDiseño;
                            break;
                        case (int)EnumAccionRealizar.BuscarPoblacion:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasPoblacionBD"));
                            opcion = EnumAccionRealizar.BuscarPoblacion;
                            break;
                        case (int)EnumAccionRealizar.BuscarObsEstrategia:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasObsEstrategiaBD"));
                            opcion = EnumAccionRealizar.BuscarObsEstrategia;
                            break;
                        case (int)EnumAccionRealizar.BuscarObsIndic:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasObsIndicadoresBD"));
                            opcion = EnumAccionRealizar.BuscarObsIndic;
                            break;
                        case (int)EnumAccionRealizar.BuscarObsPresupuesto:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasObsPresupuestoBD"));
                            opcion = EnumAccionRealizar.BuscarObsPresupuesto;
                            break;
                        case (int)EnumAccionRealizar.BuscarObsGenerales:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasObsGeneralesBD"));
                            opcion = EnumAccionRealizar.BuscarObsGenerales;
                            break;
                        case (int)EnumAccionRealizar.BuscarOfertaPublica:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasOfertaPublicaBD"));
                            opcion = EnumAccionRealizar.BuscarOfertaPublica;
                            break;
                        case (int)EnumAccionRealizar.BuscarCicloVida:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasCicloVidaBD"));
                            opcion = EnumAccionRealizar.BuscarCicloVida;
                            break;
                        case (int)EnumAccionRealizar.BuscarGruposDest:
                            idCategoria = int.Parse(constantes.GetValue("ColumnasGruposDestiBD"));
                            opcion = EnumAccionRealizar.BuscarGruposDest;
                            break;
                    }
                    dt = await descargaBD.getDataTable(new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("CategoriasDescargaBD")), Estado = decimal.Parse(constantes.GetValue("Activo")) },
                        new TablaParametrosFiltroDto() { IdCategoria = idCategoria, Estado = decimal.Parse(constantes.GetValue("Activo")) },
                        categ,
                        ano,
                        tipoFormulario, opcion);
                    listaPestañas.Add(dt);
                }
            }                        
            string handle = Guid.NewGuid().ToString();
            using (XLWorkbook wb = new XLWorkbook())
            {
                if (listaPestañas.Count > 0){
                    foreach(var data in listaPestañas)
                        wb.Worksheets.Add(data);
                }           
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    stream.Position = 0;
                    TempData[handle] = stream.ToArray();
                    //return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Monitoreo_2020.xlsx");
                    //application/vnd.ms-Excel
                }
            }
            var Data = new { FileGuid = handle, FileName = DateTime.Now.Date + "_Monitoreo_2020.xlsx" };
            return Content(JsonConvert.SerializeObject(Data, Formatting.None));
        }

        [HttpGet]
        public virtual ActionResult Download(string fileGuid, string fileName)
        {
            if (TempData[fileGuid] != null)
            {
                byte[] data = TempData[fileGuid] as byte[];
                return File(data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            else {
                // Problem - Log the error, generate a blank file,
                //           redirect to another controller action - whatever fits with your application
                return new EmptyResult();
            }
        }
        // GET: Repositorio
        [Authorize]
        public async Task<ActionResult> Repositorio()
        {
            DescargaBDViewModels vista = new DescargaBDViewModels();
            vista = await descargaBD.getDatosVista();
            return View(vista);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> GetRepositorio(string id = null)
        {
            DescargaBDViewModels datos = new DescargaBDViewModels();
            //datos = await descargaBD.getInstructivosVista();
            return Content(JsonConvert.SerializeObject(datos, Formatting.None));
        }
    }
}