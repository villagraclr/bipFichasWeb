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
    /// Clase encargada de la gestion de informacion del objeto CB_RESPUESTAS_CONSULTAS
    /// </summary>
    public partial class TablaRespuestasConsultasDao : ITablaRespuestasConsultasDao
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
        public TablaRespuestasConsultasDao()
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
		/// <param name="p_Dr">registro con informacion del objeto CB_RESPUESTAS_CONSULTAS obtenido</param>
		/// <returns>dto conformado en base a la informacion entregada</returns>
		private TablaRespuestasConsultasDto Create(IDataReader p_Dr)
        {
            TablaRespuestasConsultasDto vObj = new TablaRespuestasConsultasDto();
            try
            {
                vObj.IdRespuesta = (p_Dr["ID_RESPUESTA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_RESPUESTA"];
                vObj.IdConsulta = (p_Dr["ID_CONSULTA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_CONSULTA"];                
                vObj.IdUsuario = (p_Dr["ID_USUARIO"] is DBNull) ? null : (String)p_Dr["ID_USUARIO"];
                vObj.Usuario = (p_Dr["USERNAME"] is DBNull) ? null : (String)p_Dr["USERNAME"];
                vObj.Respuesta = (p_Dr["RESPUESTA"] is DBNull) ? null : (String)p_Dr["RESPUESTA"];                
                vObj.Fecha = (p_Dr["FECHA"] is DBNull) ? (Nullable<DateTime>)null : (Nullable<DateTime>)p_Dr["FECHA"];
                vObj.Estado = (p_Dr["ESTADO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ESTADO"];
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
		/// <param name="p_consultas">objeto en el cual se cargara la informacion</param>
		/// <returns>view dto conformado en base a la informacion entregada</returns>
		private void CreateViewDto(OracleDataReader p_Dr, ref ViewDto<TablaRespuestasConsultasDto> p_consultas)
        {
            FieldInfo fi = p_Dr.GetType().GetField("m_rowSize", BindingFlags.Instance | BindingFlags.NonPublic);
            int rowsize = Convert.ToInt32(fi.GetValue(p_Dr));
            p_Dr.FetchSize = rowsize * 100;
            List<TablaRespuestasConsultasDto> listDto = new List<TablaRespuestasConsultasDto>();
            if (p_Dr != null)
            {
                while (p_Dr.Read())
                {
                    listDto.Add(Create(p_Dr));
                }
            }
            if (listDto.Count > 0)
            {
                p_consultas = new ViewDto<TablaRespuestasConsultasDto>(listDto);
            }
        }
        #endregion

        #region metodos publicos
        /// <summary>
		/// metodo que permite buscar los registros de CB_RESPUESTAS_CONSULTAS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaRespuestasConsultasDto> Buscar(ContextoDto p_Contexto, TablaRespuestasConsultasDto p_Filtro)
        {
            OracleDataReader dr = null;
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaRespuestasConsultasDto> viewResponse = new ViewDto<TablaRespuestasConsultasDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBRESPUESTASCONSULTAS.prcBuscar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_RESPUESTA", Direction = ParameterDirection.Input, Value = p_Filtro.IdRespuesta });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_CONSULTA", Direction = ParameterDirection.Input, Value = p_Filtro.IdConsulta });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_ID_USUARIO", Direction = ParameterDirection.Input, Value = p_Filtro.IdUsuario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.TimeStamp, ParameterName = "p_FECHA", Direction = ParameterDirection.Input, Value = p_Filtro.Fecha });                                
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ESTADO", Direction = ParameterDirection.Input, Value = p_Filtro.Estado });
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
        /// metodo que permite crear un nuevo registro de CB_RESPUESTAS_CONSULTAS
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_RESPUESTAS_CONSULTAS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaRespuestasConsultasDto> Insertar(ContextoDto p_Contexto, TablaRespuestasConsultasDto p_Datos)
        {
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaRespuestasConsultasDto> viewResponse = new ViewDto<TablaRespuestasConsultasDto>();
            Decimal vCont = 0;
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBRESPUESTASCONSULTAS.prcInsertar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_CONSULTA", Direction = ParameterDirection.Input, Value = p_Datos.IdConsulta });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_ID_USUARIO", Direction = ParameterDirection.Input, Value = p_Datos.IdUsuario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Long, ParameterName = "p_RESPUESTA", Direction = ParameterDirection.Input, Value = p_Datos.Respuesta });                
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.TimeStamp, ParameterName = "p_FECHA", Direction = ParameterDirection.Input, Value = p_Datos.Fecha });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ESTADO", Direction = ParameterDirection.Input, Value = p_Datos.Estado });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_RESPUESTA", Direction = ParameterDirection.Output });
                dbCommand.ExecuteNonQuery();
                vCont = decimal.Parse(dbCommand.Parameters["p_ID_RESPUESTA"].Value.ToString());
                if (vCont > 0)
                {
                    p_Datos.IdRespuesta = vCont;
                    viewResponse.Dtos = new List<TablaRespuestasConsultasDto> { p_Datos };
                }
                else { throw new Exception("Error insert PCKCBRESPUESTASCONSULTAS.prcInsertar"); }
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
