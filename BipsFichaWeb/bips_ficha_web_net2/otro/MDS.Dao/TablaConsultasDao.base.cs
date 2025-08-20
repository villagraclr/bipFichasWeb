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
    /// Clase encargada de la gestion de informacion del objeto CB_CONSULTAS
    /// </summary>
    public partial class TablaConsultasDao : ITablaConsultasDao
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
        public TablaConsultasDao()
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
		/// <param name="p_Dr">registro con informacion del objeto CB_CONSULTAS obtenido</param>
		/// <returns>dto conformado en base a la informacion entregada</returns>
		private TablaConsultasDto Create(IDataReader p_Dr)
        {
            TablaConsultasDto vObj = new TablaConsultasDto();
            try
            {
                vObj.IdConsulta = (p_Dr["ID_CONSULTA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_CONSULTA"];
                vObj.IdTema = (p_Dr["ID_TEMA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_TEMA"];
                vObj.IdTipo = (p_Dr["ID_TIPO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_TIPO"];
                vObj.Tipo = (p_Dr["TIPO"] is DBNull) ? null : (String)p_Dr["TIPO"];
                vObj.IdUsuario = (p_Dr["ID_USUARIO"] is DBNull) ? null : (String)p_Dr["ID_USUARIO"];
                vObj.EmailUsuario = (p_Dr["EMAIL"] is DBNull) ? null : (String)p_Dr["EMAIL"];
                vObj.NombreUsuario = (p_Dr["NOMBRE_USUARIO"] is DBNull) ? null : (String)p_Dr["NOMBRE_USUARIO"];
                vObj.IdPrograma = (p_Dr["ID_PROGRAMA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_PROGRAMA"];
                vObj.IdMenu = (p_Dr["ID_MENU_PADRE"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_MENU_PADRE"];
                vObj.MenuPadre = (p_Dr["MENU_PADRE"] is DBNull) ? null : (String)p_Dr["MENU_PADRE"];
                vObj.IdMenuHijo = (p_Dr["ID_MENU_HIJO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_MENU_HIJO"];
                vObj.MenuHijo = (p_Dr["MENU_HIJO"] is DBNull) ? null : (String)p_Dr["MENU_HIJO"];
                vObj.Tema = (p_Dr["TEMA"] is DBNull) ? null : (String)p_Dr["TEMA"];
                vObj.Consulta = (p_Dr["CONSULTA"] is DBNull) ? null : (String)p_Dr["CONSULTA"];
                vObj.IdPregunta = (p_Dr["ID_PREGUNTA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_PREGUNTA"];
                vObj.Pregunta = (p_Dr["PREGUNTA"] is DBNull) ? null : (String)p_Dr["PREGUNTA"];
                vObj.IdTab = (p_Dr["ID_TAB"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_TAB"];
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
		private void CreateViewDto(OracleDataReader p_Dr, ref ViewDto<TablaConsultasDto> p_consultas)
        {
            FieldInfo fi = p_Dr.GetType().GetField("m_rowSize", BindingFlags.Instance | BindingFlags.NonPublic);
            int rowsize = Convert.ToInt32(fi.GetValue(p_Dr));
            p_Dr.FetchSize = rowsize * 100;
            List<TablaConsultasDto> listDto = new List<TablaConsultasDto>();
            if (p_Dr != null)
            {
                while (p_Dr.Read())
                {
                    listDto.Add(Create(p_Dr));
                }
            }
            if (listDto.Count > 0)
            {
                p_consultas = new ViewDto<TablaConsultasDto>(listDto);
            }
        }
        #endregion

        #region metodos publicos
        /// <summary>
		/// metodo que permite buscar los registros de CB_CONSULTAS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaConsultasDto> Buscar(ContextoDto p_Contexto, TablaConsultasDto p_Filtro)
        {
            OracleDataReader dr = null;
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaConsultasDto> viewResponse = new ViewDto<TablaConsultasDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBCONSULTAS.prcBuscar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_CONSULTA", Direction = ParameterDirection.Input, Value = p_Filtro.IdConsulta });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_TEMA", Direction = ParameterDirection.Input, Value = p_Filtro.IdTema });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_TIPO", Direction = ParameterDirection.Input, Value = p_Filtro.IdTipo });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_ID_USUARIO", Direction = ParameterDirection.Input, Value = p_Filtro.IdUsuario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PROGRAMA", Direction = ParameterDirection.Input, Value = p_Filtro.IdPrograma });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_MENU", Direction = ParameterDirection.Input, Value = p_Filtro.IdMenu });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.TimeStamp, ParameterName = "p_FECHA", Direction = ParameterDirection.Input, Value = p_Filtro.Fecha });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_MENU_HIJO", Direction = ParameterDirection.Input, Value = p_Filtro.IdMenuHijo });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PREGUNTA", Direction = ParameterDirection.Input, Value = p_Filtro.IdPregunta });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_TAB", Direction = ParameterDirection.Input, Value = p_Filtro.IdTab });
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
        /// metodo que permite crear un nuevo registro de CB_CONSULTAS
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_CONSULTAS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaConsultasDto> Insertar(ContextoDto p_Contexto, TablaConsultasDto p_Datos)
        {
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaConsultasDto> viewResponse = new ViewDto<TablaConsultasDto>();
            Decimal vCont = 0;
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBCONSULTAS.prcInsertar";                
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_ID_USUARIO", Direction = ParameterDirection.Input, Value = p_Datos.IdUsuario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PROGRAMA", Direction = ParameterDirection.Input, Value = p_Datos.IdPrograma });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_MENU", Direction = ParameterDirection.Input, Value = p_Datos.IdMenu });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_TEMA", Direction = ParameterDirection.Input, Value = p_Datos.Tema });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_CONSULTA", Direction = ParameterDirection.Input, Value = p_Datos.Consulta });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.TimeStamp, ParameterName = "p_FECHA", Direction = ParameterDirection.Input, Value = p_Datos.Fecha });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ESTADO", Direction = ParameterDirection.Input, Value = p_Datos.Estado });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_MENU_HIJO", Direction = ParameterDirection.Input, Value = p_Datos.IdMenuHijo });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PREGUNTA", Direction = ParameterDirection.Input, Value = p_Datos.IdPregunta });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_TAB", Direction = ParameterDirection.Input, Value = p_Datos.IdTab });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_CONSULTA", Direction = ParameterDirection.Output });
                dbCommand.ExecuteNonQuery();
                vCont = decimal.Parse(dbCommand.Parameters["p_ID_CONSULTA"].Value.ToString());
                if (vCont > 0)
                {
                    p_Datos.IdConsulta = vCont;                    
                    viewResponse.Dtos = new List<TablaConsultasDto> { p_Datos };
                }
                else { throw new Exception("Error insert PCKCBCONSULTAS.prcInsertar"); }
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
		/// metodo que permite eliminar un registro de CB_CONSULTAS
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Datos">formulario a eliminar</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaConsultasDto> Eliminar(ContextoDto p_Contexto, TablaConsultasDto p_Datos)
        {
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaConsultasDto> viewResponse = new ViewDto<TablaConsultasDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBCONSULTAS.prcBorrar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_CONSULTA", Direction = ParameterDirection.Input, Value = p_Datos.IdConsulta });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ESTADO", Direction = ParameterDirection.Input, Value = p_Datos.Estado });
                OracleTransaction tx = dbConn.BeginTransaction();
                dbCommand.ExecuteNonQuery();
                tx.Commit();
                viewResponse.Dtos = new List<TablaConsultasDto> { p_Datos };
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                viewResponse.Error = new ErrorDto() { Mensaje = ex.Message, Codigo = ex.Source };
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
		/// metodo que permite actualizar un registro de CB_CONSULTAS existente
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Datos">grupo a actualizar</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaConsultasDto> Actualizar(ContextoDto p_Contexto, TablaConsultasDto p_Datos)
        {
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaConsultasDto> viewResponse = new ViewDto<TablaConsultasDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBCONSULTAS.prcActualizar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_CONSULTA", Direction = ParameterDirection.Input, Value = p_Datos.IdConsulta });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_CONSULTA", Direction = ParameterDirection.Input, Value = p_Datos.Consulta });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.TimeStamp, ParameterName = "p_FECHA", Direction = ParameterDirection.Input, Value = p_Datos.Fecha });
                OracleTransaction tx = dbConn.BeginTransaction();
                dbCommand.ExecuteNonQuery();
                tx.Commit();
                viewResponse.Dtos = new List<TablaConsultasDto> { p_Datos };
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
                if (dbConn != null)
                    dbConn.Dispose();
            }
            return viewResponse;
        }
        #endregion
    }
}
