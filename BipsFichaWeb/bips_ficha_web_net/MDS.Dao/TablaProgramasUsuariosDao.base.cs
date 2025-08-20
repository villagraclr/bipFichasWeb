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
	///	Clase encargada de la gestion de informacion del objeto CB_FORMULARIOS_USUARIOS
	/// </summary>
    public partial class TablaProgramasUsuariosDao : ITablaProgramasUsuariosDao
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
        public TablaProgramasUsuariosDao()
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
		/// <param name="p_Dr">registro con informacion del objeto CB_FORMULARIOS_USUARIOS obtenido</param>
		/// <returns>dto conformado en base a la informacion entregada</returns>
		private TablaProgramasDto Create(IDataReader p_Dr)
        {
            TablaProgramasDto vObj = new TablaProgramasDto();
            try
            {
                vObj.IdPrograma = (p_Dr["ID_PROGRAMA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_PROGRAMA"];
                vObj.IdBips = (p_Dr["ID_BIPS"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_BIPS"];
                vObj.Ano = (p_Dr["ANO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ANO"];
                vObj.IdTipoFormulario = (p_Dr["ID_TIPO_FORMULARIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_TIPO_FORMULARIO"];
                vObj.Tipo = (p_Dr["TIPO_FORMULARIO"] is DBNull) ? null : (String)p_Dr["TIPO_FORMULARIO"];
                vObj.TipoGeneral = (p_Dr["TIPO_GENERAL"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["TIPO_GENERAL"];
                vObj.Nombre = (p_Dr["NOMBRE"] is DBNull) ? null : (String)p_Dr["NOMBRE"];
                vObj.Ministerio = (p_Dr["MINISTERIO"] is DBNull) ? null : (String)p_Dr["MINISTERIO"];
                vObj.Servicio = (p_Dr["SERVICIO"] is DBNull) ? null : (String)p_Dr["SERVICIO"];
                vObj.IdEtapa = (p_Dr["ID_ETAPA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_ETAPA"];
                vObj.EtapaDesc = (p_Dr["ETAPA"] is DBNull) ? null : (String)p_Dr["ETAPA"];
                vObj.EstadoDesc = (p_Dr["ESTADO"] is DBNull) ? null : (String)p_Dr["ESTADO"];
                vObj.IdUser = (p_Dr["ID_USUARIO"] is DBNull) ? null : (String)p_Dr["ID_USUARIO"];
                vObj.IdPlataforma = (p_Dr["ID_PLATAFORMA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_PLATAFORMA"];
                vObj.Acceso = (p_Dr["ACCESO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ACCESO"];
                vObj.TipoGrupo = (p_Dr["TIPO_GRUPO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["TIPO_GRUPO"];
                vObj.IdGrupoFormulario = (p_Dr["ID_GRUPO_FORMULARIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_GRUPO_FORMULARIO"];
                vObj.NombreGrupo = (p_Dr["NOMBRE_GRUPO"] is DBNull) ? null : (String)p_Dr["NOMBRE_GRUPO"];
                vObj.DescTipoGrupo = (p_Dr["DESC_TIPO_GRUPO"] is DBNull) ? null : (String)p_Dr["DESC_TIPO_GRUPO"];
                vObj.DescGrupoFormulario = (p_Dr["DESC_GRUPO_FORMULARIO"] is DBNull) ? null : (String)p_Dr["DESC_GRUPO_FORMULARIO"];
                if (vObj.IdPrograma != null)
                    vObj.IdEncriptado = EncriptaDato.RijndaelSimple.Encriptar(vObj.IdPrograma.ToString());
                vObj.Tomado = (p_Dr["tomados"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["tomados"];
                vObj.Gore = (p_Dr["GORE"] is DBNull) ? null : (String)p_Dr["GORE"];
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
		private void CreateViewDto(OracleDataReader p_Dr, ref ViewDto<TablaProgramasDto> p_Respuestas)
        {
            FieldInfo fi = p_Dr.GetType().GetField("m_rowSize", BindingFlags.Instance | BindingFlags.NonPublic);
            int rowsize = Convert.ToInt32(fi.GetValue(p_Dr));
            p_Dr.FetchSize = rowsize * 100;
            List<TablaProgramasDto> listDto = new List<TablaProgramasDto>();
            if (p_Dr != null)
            {
                while (p_Dr.Read())
                {
                    listDto.Add(Create(p_Dr));
                }
            }
            if (listDto.Count > 0)
            {
                p_Respuestas = new ViewDto<TablaProgramasDto>(listDto);
            }
        }
        #endregion

        #region metodos publicos
        /// <summary>
		/// metodo que permite buscar los registros de CB_FORMULARIOS_USUARIOS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaProgramasDto> Buscar(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro)
        {
            OracleDataReader dr = null;
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaProgramasDto> viewResponse = new ViewDto<TablaProgramasDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBFORMULARIOSUSUARIOS.prcBuscar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ANO", Direction = ParameterDirection.Input, Value = p_Filtro.Ano });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_ID_USER", Direction = ParameterDirection.Input, Value = p_Filtro.IdUser });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_GRUPO_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Filtro.IdGrupoFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_MINISTERIO", Direction = ParameterDirection.Input, Value = p_Filtro.IdMinisterio });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_SERVICIO", Direction = ParameterDirection.Input, Value = p_Filtro.IdServicio });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PLATAFORMA", Direction = ParameterDirection.Input, Value = p_Filtro.IdPlataforma });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO_GRUPO", Direction = ParameterDirection.Input, Value = p_Filtro.TipoGrupo });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_ESTADO", Direction = ParameterDirection.Input, Value = p_Filtro.Estado });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Filtro.TipoFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PLANTILLA", Direction = ParameterDirection.Input, Value = p_Filtro.IdExcepcion });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Filtro.IdPrograma });
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
        /// metodo que permite crear un nuevo registro de CB_FORMULARIOS_USUARIOS
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_FORMULARIOS_USUARIOS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaFormulariosUsuariosDto> Insertar(ContextoDto p_Contexto, TablaFormulariosUsuariosDto p_Datos)
        {
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaFormulariosUsuariosDto> viewResponse = new ViewDto<TablaFormulariosUsuariosDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBFORMULARIOSUSUARIOS.prcInsertar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_ID_USER", Direction = ParameterDirection.Input, Value = p_Datos.IdUsuario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_GRUPO_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Datos.IdGrupoFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO_GRUPO", Direction = ParameterDirection.Input, Value = p_Datos.TipoGrupo });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PLATAFORMA", Direction = ParameterDirection.Input, Value = p_Datos.IdPlataforma });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_ESTADO", Direction = ParameterDirection.Input, Value = p_Datos.Estado });
                dbCommand.ExecuteNonQuery();
                viewResponse.Dtos = new List<TablaFormulariosUsuariosDto> { p_Datos };
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
        /// metodo que permite eliminar un registro de CB_FORMULARIOS_USUARIOS existente
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_FORMULARIOS_USUARIOS a eliminar</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaFormulariosUsuariosDto> Eliminar(ContextoDto p_Contexto, TablaFormulariosUsuariosDto p_Datos)
        {
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaFormulariosUsuariosDto> viewResponse = new ViewDto<TablaFormulariosUsuariosDto>();
            int vCont = 0;
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBFORMULARIOSUSUARIOS.prcBorrar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_ID_USER", Direction = ParameterDirection.Input, Value = p_Datos.IdUsuario });
                vCont = dbCommand.ExecuteNonQuery();
                if (vCont > 0)
                    viewResponse.Dtos = new List<TablaFormulariosUsuariosDto> { p_Datos };
                else
                    throw new Exception("Error insert PCKCBFORMULARIOSUSUARIOS.prcBorrar");
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
