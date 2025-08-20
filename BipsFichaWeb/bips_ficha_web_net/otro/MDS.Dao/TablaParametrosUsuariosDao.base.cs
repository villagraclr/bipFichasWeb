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
	///	Clase encargada de la gestion de informacion del objeto CB_PARAMETROS_USUARIOS
	/// </summary>
    public partial class TablaParametrosUsuariosDao : ITablaParametrosUsuariosDao
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
        public TablaParametrosUsuariosDao()
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
		/// <param name="p_Dr">registro con informacion del objeto CB_PARAMETROS_USUARIOS obtenido</param>
		/// <returns>dto conformado en base a la informacion entregada</returns>
		private TablaParametrosUsuariosDto Create(IDataReader p_Dr)
        {
            TablaParametrosUsuariosDto vObj = new TablaParametrosUsuariosDto();
            try
            {
                vObj.IdParametro = (p_Dr["ID_PARAMETRO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_PARAMETRO"];
                vObj.IdPlataforma = (p_Dr["ID_PLATAFORMA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_PLATAFORMA"];
                vObj.IdUsuario = (p_Dr["ID_USUARIO"] is DBNull) ? null : (String)p_Dr["ID_USUARIO"];
                vObj.Ano = (p_Dr["ANO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ANO"];                
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
		/// <param name="p_Parametros">objeto en el cual se cargara la informacion</param>
		/// <returns>view dto conformado en base a la informacion entregada</returns>		
        private void CreateViewDto(OracleDataReader p_Dr, ref ViewDto<TablaParametrosUsuariosDto> p_Respuestas)
        {
            FieldInfo fi = p_Dr.GetType().GetField("m_rowSize", BindingFlags.Instance | BindingFlags.NonPublic);
            int rowsize = Convert.ToInt32(fi.GetValue(p_Dr));
            p_Dr.FetchSize = rowsize * 100;
            List<TablaParametrosUsuariosDto> listDto = new List<TablaParametrosUsuariosDto>();
            if (p_Dr != null)
            {
                while (p_Dr.Read())
                {
                    listDto.Add(Create(p_Dr));
                }
            }
            if (listDto.Count > 0)
            {
                p_Respuestas = new ViewDto<TablaParametrosUsuariosDto>(listDto);
            }
        }
        #endregion

        #region metodos publicos
        /// <summary>
		/// metodo que permite crear un nuevo registro de cb_parametros_usuarios
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Parametros">usuario a crear</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaParametrosUsuariosDto> Insertar(ContextoDto p_Contexto, TablaParametrosUsuariosDto p_Datos)
        {
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaParametrosUsuariosDto> viewResponse = new ViewDto<TablaParametrosUsuariosDto>();
            int vCont = 0;
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBPARAMETROSUSUARIOS.prcInsertar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PARAMETRO", Direction = ParameterDirection.Input, Value = p_Datos.IdParametro });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PLATAFORMA", Direction = ParameterDirection.Input, Value = p_Datos.IdPlataforma });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_ID_USUARIO", Direction = ParameterDirection.Input, Value = p_Datos.IdUsuario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ANO", Direction = ParameterDirection.Input, Value = p_Datos.Ano });
                vCont = dbCommand.ExecuteNonQuery();
                if (vCont > 0)
                    viewResponse.Dtos = new List<TablaParametrosUsuariosDto> { p_Datos };
                else
                    throw new Exception("Error insert PCKCBPARAMETROSUSUARIOS.prcInsertar");
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
		/// metodo que permite buscar los registros de cb_parametros_usuarios existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaParametrosUsuariosDto> Buscar(ContextoDto p_Contexto, TablaParametrosUsuariosFiltroDto p_Filtro)
        {
            OracleDataReader dr = null;
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaParametrosUsuariosDto> viewResponse = new ViewDto<TablaParametrosUsuariosDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBPARAMETROSUSUARIOS.prcBuscar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PARAMETRO", Direction = ParameterDirection.Input, Value = p_Filtro.IdParametro });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PLATAFORMA", Direction = ParameterDirection.Input, Value = p_Filtro.IdPlataforma });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_ID_USUARIO", Direction = ParameterDirection.Input, Value = p_Filtro.IdUsuario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ANO", Direction = ParameterDirection.Input, Value = p_Filtro.Ano });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.RefCursor, ParameterName = "ResultsCursor", Direction = ParameterDirection.Output });
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
