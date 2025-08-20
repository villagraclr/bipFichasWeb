using log4net;
using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Core.Providers;
using MDS.Dto;
using MDS.Svc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SistemasBIPS.Models
{
    public class DescargaBaseDatosModels
    {
        private static ISistemasBIPSSvc bips = null;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IProviderConstante constantes = null;

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public DescargaBaseDatosModels()
        {
            bips = (ISistemasBIPSSvc)Activator.CreateInstance(typeof(SistemasBIPSSvc));
            constantes = (IProviderConstante)Activator.CreateInstance(typeof(ProviderConstante));
        }

        public Task<IList<TablaPreguntasRespuestasDto>> getDataBD(int tipoFormulario, int ano, EnumAccionRealizar categoria)
        {
            IList<TablaPreguntasRespuestasDto> data = new List<TablaPreguntasRespuestasDto>();
            try {
                ViewDto<TablaPreguntasRespuestasDto> datos = new ViewDto<TablaPreguntasRespuestasDto>();
                datos = bips.BuscarPreguntasRespuestas(new ContextoDto(), new TablaPreguntasRespuestasFiltroDto() { IdTipoFormulario = tipoFormulario, Ano = ano, IdEstado = decimal.Parse(constantes.GetValue("Activo")) }, categoria);
                if (datos.HasElements())
                    data = datos.Dtos.ToList();
            }catch(Exception ex) {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(data);
        }

        public async Task<DescargaBDViewModels> getDatosVista()
        {
            DescargaBDViewModels datos = new DescargaBDViewModels();
            FormulariosModels model = new FormulariosModels();
            try {
                datos.categorias = await model.getListaParametros(new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("CategoriasDescargaBD")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                datos.formularios = await model.getListaParametros(new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("TiposFormulariosBD")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                datos.anos = await model.getListaParametros(new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("AnoDescargaBD")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
            }
            catch(Exception ex) {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return (datos);
        }

        public async Task<DataTable> getDataTable(TablaParametrosFiltroDto categoria, TablaParametrosFiltroDto columna, int pestaña, int ano, int tipoFormulario, EnumAccionRealizar opcion)
        {
            DataTable data = new DataTable();
            FormulariosModels model = new FormulariosModels();
            try {
                IList<TablaParametrosDto> pestañas = await model.getListaParametros(categoria);
                if (pestañas.Count > 0){
                    String hoja = pestañas.FirstOrDefault(p => p.Valor == pestaña).Descripcion;
                    data = new DataTable(hoja);
                }                    
                IList<TablaParametrosDto> columnas = await model.getListaParametros(columna);
                if (columnas.Count > 0)
                    foreach(var c in columnas.Where(p=>p.IdParametro!=p.IdCategoria).OrderBy(p => p.Orden)){
                        data.Columns.Add(c.Descripcion);
                    }
                IList<TablaPreguntasRespuestasDto> preguntasRespuestas = new List<TablaPreguntasRespuestasDto>();
                preguntasRespuestas = await getDataBD(tipoFormulario, ano, opcion);
                if (preguntasRespuestas.Count > 0)
                {
                    foreach (var datos in preguntasRespuestas)
                    {
                        if (pestaña == (int)EnumAccionRealizar.BuscarAntecedentes){
                            data.Rows.Add(datos.IdBips, datos.Nombre, datos.Ministerio, datos.Servicio, datos.TipoFormulario, datos.Etapa, datos.Version, datos.Origen, datos.TipoOferta,
                                        datos.AnosComparables, datos.TieneCalificacionExAnte, datos.UltimoAnoEvaluado, datos.UltimaCalificacion, datos.UnidadResponsable, datos.PaginaWeb, datos.Encargado,
                                        datos.Cargo, datos.Telefono, datos.Email, datos.ContraparteMonitoreo, datos.CargoContraparte, datos.TelefonoContraparte, datos.EmailContraparte, datos.AnoInicio, datos.AnoTermino,
                                        datos.Permanente, datos.ObjetivoEstrategico, datos.MarcoNormativo, datos.PlanAccion, datos.NombrePlanAccion);
                        }else if (pestaña == (int)EnumAccionRealizar.BuscarDiagnostico){
                            data.Rows.Add(datos.IdBips, datos.Nombre, datos.Ministerio, datos.Servicio, datos.TipoFormulario, datos.ProblemaPrincipal, datos.PropositoPrograma);
                        }else if (pestaña == (int)EnumAccionRealizar.BuscarEvalAnteriores){
                            data.Rows.Add(datos.IdBips, datos.Nombre, datos.Ministerio, datos.Servicio, datos.TipoFormulario, datos.EvaluacionesExternas, datos.Cuantas,
                                        datos.Institucion1, datos.NombreEvaluacion1, datos.AnoEvaluacion1, datos.TipoEvaluacion1, datos.SitioWeb1,
                                        datos.Institucion2, datos.NombreEvaluacion2, datos.AnoEvaluacion2, datos.TipoEvaluacion2, datos.SitioWeb2,
                                        datos.Institucion3, datos.NombreEvaluacion3, datos.AnoEvaluacion3, datos.TipoEvaluacion3, datos.SitioWeb3,
                                        datos.Institucion4, datos.NombreEvaluacion4, datos.AnoEvaluacion4, datos.TipoEvaluacion4, datos.SitioWeb4,
                                        datos.Institucion5, datos.NombreEvaluacion5, datos.AnoEvaluacion5, datos.TipoEvaluacion5, datos.SitioWeb5);
                        }else if (pestaña == (int)EnumAccionRealizar.BuscarPobPotencial){
                            data.Rows.Add(datos.IdBips, datos.Nombre, datos.Ministerio, datos.Servicio, datos.TipoFormulario, datos.DescPoblacionPotencial, datos.PobPotencial, datos.FuenteInformacion, datos.UnidadMedida);
                        }
                    }
                }
            }
            catch(Exception ex) {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return (data);
        }
    }
}