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
    /// Clase encargada de la gestion de informacion del objeto CB_LOG_FORMULARIOS
    /// </summary>
    public partial class TablaLogFormulariosDao : ITablaLogFormulariosDao
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
        public TablaLogFormulariosDao()
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
		/// <param name="p_Dr">registro con informacion del objeto CB_LOG_FORMULARIOS obtenido</param>
		/// <returns>dto conformado en base a la informacion entregada</returns>
		private TablaLogFormulariosDto Create(IDataReader p_Dr)
        {
            TablaLogFormulariosDto vObj = new TablaLogFormulariosDto();
            try
            {
                vObj.IdFormulario = (p_Dr["ID_FORMULARIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_FORMULARIO"];
                vObj.IdUsuario = (p_Dr["ID_USUARIO"] is DBNull) ? null : (String)p_Dr["ID_USUARIO"];
                vObj.IdSesion = (p_Dr["ID_SESION"] is DBNull) ? null : (String)p_Dr["ID_SESION"];
                vObj.TipoAcceso = (p_Dr["TIPO_ACCESO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["TIPO_ACCESO"];
                vObj.FechaAcceso = (p_Dr["FECHA_ACCESO"] is DBNull) ? (Nullable<DateTime>)null : (Nullable<DateTime>)p_Dr["FECHA_ACCESO"];
                vObj.FechaSalida = (p_Dr["FECHA_SALIDA"] is DBNull) ? (Nullable<DateTime>)null : (Nullable<DateTime>)p_Dr["FECHA_SALIDA"];
                vObj.EstadoAcceso = (p_Dr["ESTADO_ACCESO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ESTADO_ACCESO"];                
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
		private void CreateViewDto(OracleDataReader p_Dr, ref ViewDto<TablaLogFormulariosDto> p_Respuestas)
        {
            FieldInfo fi = p_Dr.GetType().GetField("m_rowSize", BindingFlags.Instance | BindingFlags.NonPublic);
            int rowsize = Convert.ToInt32(fi.GetValue(p_Dr));
            p_Dr.FetchSize = rowsize * 100;
            List<TablaLogFormulariosDto> listDto = new List<TablaLogFormulariosDto>();
            if (p_Dr != null)
            {
                while (p_Dr.Read())
                {
                    listDto.Add(Create(p_Dr));
                }
            }
            if (listDto.Count > 0)
            {
                p_Respuestas = new ViewDto<TablaLogFormulariosDto>(listDto);
            }
        }
        #endregion

        #region metodos publicos
        /// <summary>
		/// metodo que permite buscar los registros de CB_LOG_FORMULARIOS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaLogFormulariosDto> Buscar(ContextoDto p_Contexto, TablaLogFormulariosFiltroDto p_Filtro)
        {
            OracleDataReader dr = null;
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaLogFormulariosDto> viewResponse = new ViewDto<TablaLogFormulariosDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBLOGFORMULARIOS.prcBuscar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Filtro.IdFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_ID_USUARIO", Direction = ParameterDirection.Input, Value = p_Filtro.IdUsuario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_ID_SESION", Direction = ParameterDirection.Input, Value = p_Filtro.IdSesion });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO_ACCESO", Direction = ParameterDirection.Input, Value = p_Filtro.TipoAcceso });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.TimeStamp, ParameterName = "p_FECHA_ACCESO", Direction = ParameterDirection.Input, Value = p_Filtro.FechaAcceso });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.TimeStamp, ParameterName = "p_FECHA_SALIDA", Direction = ParameterDirection.Input, Value = p_Filtro.FechaSalida });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ESTADO_ACCESO", Direction = ParameterDirection.Input, Value = p_Filtro.EstadoAcceso });
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
        /// metodo que permite crear un nuevo registro de CB_LOG_FORMULARIOS
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_LOG_FORMULARIOS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaLogFormulariosDto> Insertar(ContextoDto p_Contexto, TablaLogFormulariosDto p_Datos)
        {
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaLogFormulariosDto> viewResponse = new ViewDto<TablaLogFormulariosDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBLOGFORMULARIOS.prcInsertar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Datos.IdFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_ID_USUARIO", Direction = ParameterDirection.Input, Value = p_Datos.IdUsuario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_ID_SESION", Direction = ParameterDirection.Input, Value = p_Datos.IdSesion });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO_ACCESO", Direction = ParameterDirection.Input, Value = p_Datos.TipoAcceso });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ESTADO_ACCESO", Direction = ParameterDirection.Input, Value = p_Datos.EstadoAcceso });
                dbCommand.ExecuteNonQuery();
                viewResponse.Dtos = new List<TablaLogFormulariosDto> { p_Datos };
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
        /// metodo que permite actualizar un registro de CB_LOG_FORMULARIOS existente
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_LOG_FORMULARIOS a actualizar</param>
        /// <returns>objeto contenedor de la información generada por la accion ejecutada</returns>
        public ViewDto<TablaLogFormulariosDto> Actualizar(ContextoDto p_Contexto, TablaLogFormulariosDto p_Datos)
        {
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaLogFormulariosDto> viewResponse = new ViewDto<TablaLogFormulariosDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBLOGFORMULARIOS.prcActualizar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Datos.IdFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_ID_USUARIO", Direction = ParameterDirection.Input, Value = p_Datos.IdUsuario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_ID_SESION", Direction = ParameterDirection.Input, Value = p_Datos.IdSesion });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO_ACCESO", Direction = ParameterDirection.Input, Value = p_Datos.TipoAcceso });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.TimeStamp, ParameterName = "p_FECHA_ACCESO", Direction = ParameterDirection.Input, Value = p_Datos.FechaAcceso });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.TimeStamp, ParameterName = "p_FECHA_SALIDA", Direction = ParameterDirection.Input, Value = p_Datos.FechaSalida });                
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ESTADO_ACCESO", Direction = ParameterDirection.Input, Value = p_Datos.EstadoAcceso });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO_UPDATE", Direction = ParameterDirection.Input, Value = p_Datos.TipoUpdate });
                OracleTransaction tx = dbConn.BeginTransaction();
                dbCommand.ExecuteNonQuery();
                tx.Commit();
                viewResponse.Dtos = new List<TablaLogFormulariosDto> { p_Datos };
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
