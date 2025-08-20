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
	///	Clase encargada de la gestion de informacion del objeto CB_RESPUESTAS
	/// </summary>
    public partial class TablaRespuestasDao : ITablaRespuestasDao
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
        public TablaRespuestasDao()
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
		/// <param name="p_Dr">registro con informacion del objeto CB_RESPUESTAS obtenido</param>
		/// <returns>dto conformado en base a la informacion entregada</returns>
		private TablaRespuestasDto Create(IDataReader p_Dr)
        {
            TablaRespuestasDto vObj = new TablaRespuestasDto();
            try
            {
                vObj.IdPregunta = (p_Dr["ID_PREGUNTA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_PREGUNTA"];
                vObj.IdFormulario = (p_Dr["ID_FORMULARIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_FORMULARIO"];
                vObj.IdTab = (p_Dr["ID_TAB"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_TAB"];
                vObj.Respuesta = (p_Dr["RESPUESTA"] is DBNull) ? null : (Object)p_Dr["RESPUESTA"];
                vObj.TipoPregunta = (p_Dr["TIPO_PREGUNTA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["TIPO_PREGUNTA"];
                vObj.IdCategoria = (p_Dr["ID_CATEGORIA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_CATEGORIA"];
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
		private void CreateViewDto(OracleDataReader p_Dr, ref ViewDto<TablaRespuestasDto> p_Respuestas)
        {
            FieldInfo fi = p_Dr.GetType().GetField("m_rowSize", BindingFlags.Instance | BindingFlags.NonPublic);
            int rowsize = Convert.ToInt32(fi.GetValue(p_Dr));
            p_Dr.FetchSize = rowsize * 100;
            List<TablaRespuestasDto> listDto = new List<TablaRespuestasDto>();
            if (p_Dr != null)
            {
                while (p_Dr.Read())
                {
                    listDto.Add(Create(p_Dr));
                }
            }
            if (listDto.Count > 0)
            {
                p_Respuestas = new ViewDto<TablaRespuestasDto>(listDto);
            }
        }
        #endregion

        #region metodos publicos
        /// <summary>
		/// metodo que permite buscar los registros de CB_RESPUESTAS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaRespuestasDto> Buscar(ContextoDto p_Contexto, TablaRespuestasFiltroDto p_Filtro)
        {
            OracleDataReader dr = null;
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaRespuestasDto> viewResponse = new ViewDto<TablaRespuestasDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBRESPUESTAS.prcBuscar";                
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PREGUNTA", Direction = ParameterDirection.Input, Value = p_Filtro.IdPregunta });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Filtro.IdFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_TAB", Direction = ParameterDirection.Input, Value = p_Filtro.IdTab });                
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

        /// <summary>
        /// metodo que permite crear un nuevo registro de CB_RESPUESTAS
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_RESPUESTAS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaRespuestasDto> Insertar(ContextoDto p_Contexto, TablaRespuestasDto p_Datos)
        {
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaRespuestasDto> viewResponse = new ViewDto<TablaRespuestasDto>();
            //int vCont = 0;
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBRESPUESTAS.prcInsertar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PREGUNTA", Direction = ParameterDirection.Input, Value = p_Datos.IdPregunta });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Datos.IdFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_TAB", Direction = ParameterDirection.Input, Value = p_Datos.IdTab });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Clob, ParameterName = "p_RESPUESTA", Direction = ParameterDirection.Input, Value = p_Datos.Respuesta });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO_INSERT", Direction = ParameterDirection.Input, Value = p_Datos.TipoInsert });
                dbCommand.ExecuteNonQuery();
                viewResponse.Dtos = new List<TablaRespuestasDto> { p_Datos };                
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            finally
            {
                dbConn.Close();
                if (dbCommand != null)
                    dbCommand.Dispose();
                if (dbConn != null)
                    dbConn.Dispose();
            }
            return viewResponse;
        }        

        /// <summary>
        /// metodo que permite eliminar un registro de CB_RESPUESTAS existente
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_RESPUESTAS a eliminar</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaRespuestasDto> Eliminar(ContextoDto p_Contexto, TablaRespuestasDto p_Datos)
        {
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaRespuestasDto> viewResponse = new ViewDto<TablaRespuestasDto>();
            //int vCont = 0;
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBRESPUESTAS.prcBorrar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Datos.IdFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PREGUNTA", Direction = ParameterDirection.Input, Value = p_Datos.IdPregunta });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_TAB", Direction = ParameterDirection.Input, Value = p_Datos.IdTab });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO_BORRADO", Direction = ParameterDirection.Input, Value = p_Datos.TipoDelete });
                OracleTransaction tx = dbConn.BeginTransaction();
                dbCommand.ExecuteNonQuery();
                tx.Commit();
                viewResponse.Dtos = new List<TablaRespuestasDto> { p_Datos };
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            finally
            {
                dbConn.Close();
                if (dbCommand != null)
                    dbCommand.Dispose();
                if (dbConn != null)
                    dbConn.Dispose();
            }
            return viewResponse;
        }
        #endregion
    }
}
