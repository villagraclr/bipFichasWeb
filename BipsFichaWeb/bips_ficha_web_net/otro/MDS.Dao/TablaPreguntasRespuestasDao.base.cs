using log4net;
using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Core.Providers;
using MDS.Core.Util;
using MDS.Dto;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace MDS.Dao
{
    /// <summary>
	///	Clase encargada de la gestion de informacion del objeto VW_PREGUNTAS_RESPUESTAS
	/// </summary>
    public partial class TablaPreguntasRespuestasDao : ITablaPreguntasRespuestasDao
    {
        #region campos publicos
        /// <summary>
        /// campo publico que contiene una instancia de la clase que provee la logica de loging
        /// </summary>
        public IProviderLog iProviderLog;
        /// <summary>
        /// campo publico que contiene una instancia de la clase que provee la logica de generacion de errores
        /// </summary>
        public IProviderError iProviderError;
        /// <summary>
        /// campo publico que contiene una instancia de la clase que provee la logica de base de datos
        /// </summary>
        public IProviderData iProviderData;
        ///<summary>
        /// campo publico que contiene una instancia de la clase log4net        
        ///</summary>
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public TablaPreguntasRespuestasDao()
        {
            iProviderData = (IProviderData)Activator.CreateInstance(typeof(ProviderData));
            iProviderLog = (IProviderLog)Activator.CreateInstance(typeof(ProviderLog));
            iProviderError = (IProviderError)Activator.CreateInstance(typeof(ProviderError));
        }
        #endregion

        #region metodos privados
        /// <summary>
		/// metodo que realiza conversion del registro obtenido a dto
		/// </summary>
		/// <param name="p_Dr">registro con informacion del objeto CB_FUNCIONES_DEPENDENCIAS obtenido</param>
		/// <returns>dto conformado en base a la informacion entregada</returns>
		private TablaPreguntasRespuestasDto Create(IDataReader p_Dr, EnumAccionRealizar opcion)
        {
            TablaPreguntasRespuestasDto vObj = new TablaPreguntasRespuestasDto();
            try
            {
                vObj.IdBips = (p_Dr["ID_BIPS"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_BIPS"];
                vObj.Nombre = (p_Dr["NOMBRE"] is DBNull) ? null : (String)p_Dr["NOMBRE"];
                vObj.Ministerio = (p_Dr["MINISTERIO"] is DBNull) ? null : (String)p_Dr["MINISTERIO"];
                vObj.Servicio = (p_Dr["SERVICIO"] is DBNull) ? null : (String)p_Dr["SERVICIO"];
                vObj.TipoFormulario = (p_Dr["TIPO_FORMULARIO"] is DBNull) ? null : (String)p_Dr["TIPO_FORMULARIO"];
                if (opcion == EnumAccionRealizar.BuscarAntecedentes)
                {
                    vObj.Etapa = (p_Dr["ETAPA"] is DBNull) ? null : (String)p_Dr["ETAPA"];
                    vObj.Version = (p_Dr["version"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["version"];
                    vObj.Origen = (p_Dr["origen"] is DBNull) ? null : (String)p_Dr["origen"];
                    vObj.TipoOferta = (p_Dr["tipo_oferta"] is DBNull) ? null : (String)p_Dr["tipo_oferta"];
                    vObj.AnosComparables = (p_Dr["anos_comparables"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["anos_comparables"];
                    vObj.TieneCalificacionExAnte = (p_Dr["tiene_calificacion_ex_ante"] is DBNull) ? null : (String)p_Dr["tiene_calificacion_ex_ante"];
                    vObj.UltimoAnoEvaluado = (p_Dr["ultimo_ano_evaluado"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ultimo_ano_evaluado"];
                    vObj.UltimaCalificacion = (p_Dr["ultima_calificacion"] is DBNull) ? null : (String)p_Dr["ultima_calificacion"];
                    vObj.UnidadResponsable = (p_Dr["unidad_responsable"] is DBNull) ? null : (String)p_Dr["unidad_responsable"];
                    vObj.PaginaWeb = (p_Dr["pagina_web"] is DBNull) ? null : (String)p_Dr["pagina_web"];
                    vObj.Encargado = (p_Dr["encargado"] is DBNull) ? null : (String)p_Dr["encargado"];
                    vObj.Cargo = (p_Dr["cargo"] is DBNull) ? null : (String)p_Dr["cargo"];
                    vObj.Telefono = (p_Dr["telefono"] is DBNull) ? null : (String)p_Dr["telefono"];
                    vObj.Email = (p_Dr["email"] is DBNull) ? null : (String)p_Dr["email"];
                    vObj.ContraparteMonitoreo = (p_Dr["contraparte_monitoreo"] is DBNull) ? null : (String)p_Dr["contraparte_monitoreo"];
                    vObj.CargoContraparte = (p_Dr["cargo_contraparte"] is DBNull) ? null : (String)p_Dr["cargo_contraparte"];
                    vObj.TelefonoContraparte = (p_Dr["telefono_contraparte"] is DBNull) ? null : (String)p_Dr["telefono_contraparte"];
                    vObj.EmailContraparte = (p_Dr["email_contraparte"] is DBNull) ? null : (String)p_Dr["email_contraparte"];
                    vObj.AnoInicio = (p_Dr["ano_inicio"] is DBNull) ? null : (String)p_Dr["ano_inicio"];
                    vObj.AnoTermino = (p_Dr["ano_termino"] is DBNull) ? null : (String)p_Dr["ano_termino"];
                    vObj.Permanente = (p_Dr["permanente"] is DBNull) ? null : (String)p_Dr["permanente"];
                    vObj.ObjetivoEstrategico = (p_Dr["objetivo_estrategico"] is DBNull) ? null : (String)p_Dr["objetivo_estrategico"];
                    vObj.MarcoNormativo = (p_Dr["marco_normativo"] is DBNull) ? null : (String)p_Dr["marco_normativo"];
                    vObj.PlanAccion = (p_Dr["plan_accion"] is DBNull) ? null : (String)p_Dr["plan_accion"];
                    vObj.NombrePlanAccion = (p_Dr["nombre_plan_accion"] is DBNull) ? null : (String)p_Dr["nombre_plan_accion"];
                }
                else if (opcion == EnumAccionRealizar.BuscarDiagnostico)
                {
                    vObj.ProblemaPrincipal = (p_Dr["problema_principal"] is DBNull) ? null : (String)p_Dr["problema_principal"];
                    vObj.PropositoPrograma = (p_Dr["proposito_programa"] is DBNull) ? null : (String)p_Dr["proposito_programa"];
                }
                else if (opcion == EnumAccionRealizar.BuscarEvalAnteriores)
                {
                    vObj.EvaluacionesExternas = (p_Dr["evaluaciones_externas"] is DBNull) ? null : (String)p_Dr["evaluaciones_externas"];
                    vObj.Cuantas = (p_Dr["cuantas"] is DBNull) ? null : (String)p_Dr["cuantas"];
                    vObj.Institucion1 = (p_Dr["institucion_1"] is DBNull) ? null : (String)p_Dr["institucion_1"];
                    vObj.NombreEvaluacion1 = (p_Dr["nombre_evaluacion_1"] is DBNull) ? null : (String)p_Dr["nombre_evaluacion_1"];
                    vObj.AnoEvaluacion1 = (p_Dr["ano_evaluacion_1"] is DBNull) ? null : (String)p_Dr["ano_evaluacion_1"];
                    vObj.TipoEvaluacion1 = (p_Dr["tipo_evaluacion_1"] is DBNull) ? null : (String)p_Dr["tipo_evaluacion_1"];
                    vObj.SitioWeb1 = (p_Dr["sitio_web_1"] is DBNull) ? null : (String)p_Dr["sitio_web_1"];
                    vObj.Institucion2 = (p_Dr["institucion_2"] is DBNull) ? null : (String)p_Dr["institucion_2"];
                    vObj.NombreEvaluacion2 = (p_Dr["nombre_evaluacion_2"] is DBNull) ? null : (String)p_Dr["nombre_evaluacion_2"];
                    vObj.AnoEvaluacion2 = (p_Dr["ano_evaluacion_2"] is DBNull) ? null : (String)p_Dr["ano_evaluacion_2"];
                    vObj.TipoEvaluacion2 = (p_Dr["tipo_evaluacion_2"] is DBNull) ? null : (String)p_Dr["tipo_evaluacion_2"];
                    vObj.SitioWeb2 = (p_Dr["sitio_web_2"] is DBNull) ? null : (String)p_Dr["sitio_web_2"];
                    vObj.Institucion3 = (p_Dr["institucion_3"] is DBNull) ? null : (String)p_Dr["institucion_3"];
                    vObj.NombreEvaluacion3 = (p_Dr["nombre_evaluacion_3"] is DBNull) ? null : (String)p_Dr["nombre_evaluacion_3"];
                    vObj.AnoEvaluacion3 = (p_Dr["ano_evaluacion_3"] is DBNull) ? null : (String)p_Dr["ano_evaluacion_3"];
                    vObj.TipoEvaluacion3 = (p_Dr["tipo_evaluacion_3"] is DBNull) ? null : (String)p_Dr["tipo_evaluacion_3"];
                    vObj.SitioWeb3 = (p_Dr["sitio_web_3"] is DBNull) ? null : (String)p_Dr["sitio_web_3"];
                    vObj.Institucion4 = (p_Dr["institucion_4"] is DBNull) ? null : (String)p_Dr["institucion_4"];
                    vObj.NombreEvaluacion4 = (p_Dr["nombre_evaluacion_4"] is DBNull) ? null : (String)p_Dr["nombre_evaluacion_4"];
                    vObj.AnoEvaluacion4 = (p_Dr["ano_evaluacion_4"] is DBNull) ? null : (String)p_Dr["ano_evaluacion_4"];
                    vObj.TipoEvaluacion4 = (p_Dr["tipo_evaluacion_4"] is DBNull) ? null : (String)p_Dr["tipo_evaluacion_4"];
                    vObj.SitioWeb4 = (p_Dr["sitio_web_4"] is DBNull) ? null : (String)p_Dr["sitio_web_4"];
                    vObj.Institucion5 = (p_Dr["institucion_5"] is DBNull) ? null : (String)p_Dr["institucion_5"];
                    vObj.NombreEvaluacion5 = (p_Dr["nombre_evaluacion_5"] is DBNull) ? null : (String)p_Dr["nombre_evaluacion_5"];
                    vObj.AnoEvaluacion5 = (p_Dr["ano_evaluacion_5"] is DBNull) ? null : (String)p_Dr["ano_evaluacion_5"];
                    vObj.TipoEvaluacion5 = (p_Dr["tipo_evaluacion_5"] is DBNull) ? null : (String)p_Dr["tipo_evaluacion_5"];
                    vObj.SitioWeb5 = (p_Dr["sitio_web_5"] is DBNull) ? null : (String)p_Dr["sitio_web_5"];
                }
                else if (opcion == EnumAccionRealizar.BuscarPobPotencial) {
                    vObj.DescPoblacionPotencial = (p_Dr["desc_poblacion_potencial"] is DBNull) ? null : (String)p_Dr["desc_poblacion_potencial"];
                    vObj.PobPotencial = (p_Dr["pob_potencial"] is DBNull) ? null : (String)p_Dr["pob_potencial"];
                    vObj.FuenteInformacion = (p_Dr["fuente_informacion"] is DBNull) ? null : (String)p_Dr["fuente_informacion"];
                    vObj.UnidadMedida = (p_Dr["unidad_medida"] is DBNull) ? null : (String)p_Dr["unidad_medida"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR_CREATE_DTO", ex);
            }
            return vObj;
        }

        /// <summary>
		/// metodo que realiza conversion de los registros obtenidos a un array de dto
		/// </summary>
		/// <param name="p_Dr">informacion de las tareas obtenidas</param>
		/// <param name="p_Programas">objeto en el cual se cargara la informacion</param>
		/// <returns>view dto conformado en base a la informacion entregada</returns>
		private void CreateViewDto(OracleDataReader p_Dr, ref ViewDto<TablaPreguntasRespuestasDto> p_PreguntasRespuestas, EnumAccionRealizar opcion)
        {
            FieldInfo fi = p_Dr.GetType().GetField("m_rowSize", BindingFlags.Instance | BindingFlags.NonPublic);
            int rowsize = Convert.ToInt32(fi.GetValue(p_Dr));
            p_Dr.FetchSize = rowsize * 100;
            List<TablaPreguntasRespuestasDto> listDto = new List<TablaPreguntasRespuestasDto>();
            if (p_Dr != null)
            {
                while (p_Dr.Read())
                {
                    listDto.Add(Create(p_Dr, opcion));
                }
            }
            if (listDto.Count > 0)
            {
                p_PreguntasRespuestas = new ViewDto<TablaPreguntasRespuestasDto>(listDto);
            }
        }
        #endregion

        #region metodos publicos
        /// <summary>
		/// metodo que permite buscar los registros de VW_PREGUNTAS_RESPUESTAS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaPreguntasRespuestasDto> Buscar(ContextoDto p_Contexto, TablaPreguntasRespuestasFiltroDto p_Filtro, EnumAccionRealizar opcion)
        {
            OracleDataReader dr = null;
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaPreguntasRespuestasDto> viewResponse = new ViewDto<TablaPreguntasRespuestasDto>();
            try
            {
                string package = string.Empty;                
                AssertNull.NotNullOrEmpty(p_Filtro);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBPREGUNTASRESPUESTAS.prcBuscarAntecedentes";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ANO", Direction = ParameterDirection.Input, Value = p_Filtro.Ano });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PROGRAMA", Direction = ParameterDirection.Input, Value = p_Filtro.IdPrograma });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_ESTADO", Direction = ParameterDirection.Input, Value = p_Filtro.IdEstado });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_TIPO_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Filtro.IdTipoFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.RefCursor, ParameterName = "RESULTSCURSOR", Direction = ParameterDirection.Output });
                dr = dbCommand.ExecuteReader();
                CreateViewDto(dr, ref viewResponse, opcion);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            finally
            {
                dbConn.Close();
                if (dbCommand != null)
                    dbCommand.Dispose();
                if (dr != null)
                    dr.Dispose();
                if (dbConn != null)
                    dbConn.Dispose();
            }
            return viewResponse;
        }
        #endregion
    }
}
