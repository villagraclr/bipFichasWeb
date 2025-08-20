using log4net;
using MDS.Core.Dto;
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
	///	Clase encargada de la gestion de informacion del objeto CB_FUNCIONES_DEPENDENCIAS
	/// </summary>
    public partial class TablaFuncionesDependenciasDao : ITablaFuncionesDependenciasDao
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
        public TablaFuncionesDependenciasDao()
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
		private TablaFuncionesDependenciasDto Create(IDataReader p_Dr)
        {
            TablaFuncionesDependenciasDto vObj = new TablaFuncionesDependenciasDto();
            try
            {
                vObj.IdFuncionDependencia = (p_Dr["ID_FUNCION_DEPENDENCIA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_FUNCION_DEPENDENCIA"];
                vObj.IdFuncion = (p_Dr["ID_FUNCION"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_FUNCION"];
                vObj.TipoFormulario = (p_Dr["TIPO_FORMULARIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["TIPO_FORMULARIO"];
                vObj.IdEvento = (p_Dr["ID_EVENTO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_EVENTO"];
                vObj.Evento = (p_Dr["EVENTO"] is DBNull) ? null : (String)p_Dr["EVENTO"];
                vObj.IdCategoriaEvento = (p_Dr["ID_CATEGORIA_EVENTO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_CATEGORIA_EVENTO"];
                vObj.ValorEvento = (p_Dr["VALOR_EVENTO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["VALOR_EVENTO"];
                vObj.Valor2Evento = (p_Dr["VALOR2_EVENTO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["VALOR2_EVENTO"];
                vObj.IdPregunta = (p_Dr["ID_PREGUNTA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_PREGUNTA"];
                vObj.Pregunta = (p_Dr["PREGUNTA"] is DBNull) ? null : (String)p_Dr["PREGUNTA"];
                vObj.TipoPregunta = (p_Dr["TIPO_PREGUNTA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["TIPO_PREGUNTA"];
                vObj.ValorPregunta = (p_Dr["VALOR_PREGUNTA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["VALOR_PREGUNTA"];
                vObj.CategoriaPregunta = (p_Dr["CATEGORIA_PREGUNTA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["CATEGORIA_PREGUNTA"];
                vObj.IdPreguntaDependiente = (p_Dr["ID_PREGUNTA_DEPENDIENTE"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_PREGUNTA_DEPENDIENTE"];
                vObj.PreguntaDependiente = (p_Dr["PREGUNTA_DEPENDIENTE"] is DBNull) ? null : (String)p_Dr["PREGUNTA_DEPENDIENTE"];
                vObj.TipoPreguntaDependiente = (p_Dr["TIPO_PREGUNTA_DEPENDIENTE"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["TIPO_PREGUNTA_DEPENDIENTE"];
                vObj.ValorPreguntaDependiente = (p_Dr["VALOR_PREGUNTA_DEPENDIENTE"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["VALOR_PREGUNTA_DEPENDIENTE"];
                vObj.CategoriaPreguntaDependiente = (p_Dr["CATEGORIA_PREGUNTA_DEPENDIENTE"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["CATEGORIA_PREGUNTA_DEPENDIENTE"];
                vObj.ValorFuncion = (p_Dr["VALOR_FUNCION"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["VALOR_FUNCION"];                
                vObj.IdEstado = (p_Dr["ID_ESTADO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_ESTADO"];
                vObj.Estado = (p_Dr["ESTADO"] is DBNull) ? null : (String)p_Dr["ESTADO"];
                vObj.IdMenu = (p_Dr["ID_MENU"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_MENU"];
                vObj.TipoFuncion = (p_Dr["TIPO_FUNCION"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["TIPO_FUNCION"];
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
		private void CreateViewDto(OracleDataReader p_Dr, ref ViewDto<TablaFuncionesDependenciasDto> p_PreguntasTablas)
        {
            FieldInfo fi = p_Dr.GetType().GetField("m_rowSize", BindingFlags.Instance | BindingFlags.NonPublic);
            int rowsize = Convert.ToInt32(fi.GetValue(p_Dr));
            p_Dr.FetchSize = rowsize * 100;
            List<TablaFuncionesDependenciasDto> listDto = new List<TablaFuncionesDependenciasDto>();
            if (p_Dr != null)
            {
                while (p_Dr.Read())
                {
                    listDto.Add(Create(p_Dr));
                }
            }
            if (listDto.Count > 0)
            {
                p_PreguntasTablas = new ViewDto<TablaFuncionesDependenciasDto>(listDto);
            }
        }
        #endregion

        #region metodos publicos
        /// <summary>
		/// metodo que permite buscar los registros de CB_FUNCIONES_DEPENDENCIAS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaFuncionesDependenciasDto> Buscar(ContextoDto p_Contexto, TablaFuncionesDependenciasFiltroDto p_Filtro)
        {
            OracleDataReader dr = null;
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaFuncionesDependenciasDto> viewResponse = new ViewDto<TablaFuncionesDependenciasDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBFUNCIONESDEPENDENCIAS.prcBuscar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_FUNCION_DEPENDENCIA", Direction = ParameterDirection.Input, Value = p_Filtro.IdFuncionDependencia });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_FUNCION", Direction = ParameterDirection.Input, Value = p_Filtro.IdFuncion });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Filtro.TipoFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PREGUNTA", Direction = ParameterDirection.Input, Value = p_Filtro.IdPregunta });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PREGUNTA_DEPENDIENTE", Direction = ParameterDirection.Input, Value = p_Filtro.IdPreguntaDependiente });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_EVENTO", Direction = ParameterDirection.Input, Value = p_Filtro.IdEvento });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_ESTADO", Direction = ParameterDirection.Input, Value = p_Filtro.IdEstado });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO_FUNCION", Direction = ParameterDirection.Input, Value = p_Filtro.TipoFuncion });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.RefCursor, ParameterName = "RESULTSCURSOR", Direction = ParameterDirection.Output });
                dr = dbCommand.ExecuteReader();
                CreateViewDto(dr, ref viewResponse);
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
