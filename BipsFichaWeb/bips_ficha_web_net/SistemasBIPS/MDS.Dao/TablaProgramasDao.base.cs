using log4net;
using MDS.Core.Dto;
using MDS.Core.Providers;
using MDS.Core.Util;
using MDS.Dto;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace MDS.Dao
{
    /// <summary>
	///	Clase encargada de la gestion de informacion del objeto CB_PROGRAMAS
	/// </summary>
    public partial class TablaProgramasDao : ITablaProgramasDao
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
        /// <summary>
        /// Tabla Parametros
        /// </summary>
        public ITablaParametrosDao iTablaParametros;
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public TablaProgramasDao()
        {
            iProviderData = (IProviderData)Activator.CreateInstance(typeof(ProviderData));
            iProviderLog = (IProviderLog)Activator.CreateInstance(typeof(ProviderLog));
            iProviderError = (IProviderError)Activator.CreateInstance(typeof(ProviderError));
            iTablaParametros = (ITablaParametrosDao)Activator.CreateInstance(typeof(TablaParametrosDao));
        }
        #endregion

        #region metodos privados
        /// <summary>
		/// metodo que realiza conversion del registro obtenido a dto
		/// </summary>
		/// <param name="p_Dr">registro con informacion del objeto CB_PROGRAMAS obtenido</param>
		/// <returns>dto conformado en base a la informacion entregada</returns>
		private TablaProgramasDto Create(IDataReader p_Dr)
        {
            TablaProgramasDto vObj = new TablaProgramasDto();
            try
            {
                if (!(p_Dr["ID_PROGRAMA"] is DBNull))
                    vObj.IdPrograma = (Nullable<Decimal>)p_Dr["ID_PROGRAMA"];

                if (!(p_Dr["ID_BIPS"] is DBNull))
                    vObj.IdBips = (Nullable<Decimal>)p_Dr["ID_BIPS"];

                if (!(p_Dr["ANO"] is DBNull))
                    vObj.Ano = (Nullable<Decimal>)p_Dr["ANO"];

                if (!(p_Dr["TIPO_FORMULARIO"] is DBNull))
                {
                    ViewDto<TablaParametrosDto> tipoFormulario = new ViewDto<TablaParametrosDto>();
                    tipoFormulario = iTablaParametros.Buscar(new ContextoDto(), new TablaParametrosFiltroDto() { IdParametro = (Nullable<Decimal>)p_Dr["TIPO_FORMULARIO"] });
                    vObj.TipoFormulario = tipoFormulario.HasElements() ? tipoFormulario.Dtos.FirstOrDefault() : new TablaParametrosDto() { IdParametro = (Nullable<Decimal>)p_Dr["TIPO_FORMULARIO"] };                    
                }
                    
                if (!(p_Dr["NOMBRE"] is DBNull))
                    vObj.Nombre = (String)p_Dr["NOMBRE"];

                if (!(p_Dr["ID_MINISTERIO"] is DBNull))
                {
                    ViewDto<TablaParametrosDto> ministerio = new ViewDto<TablaParametrosDto>();
                    ministerio = iTablaParametros.Buscar(new ContextoDto(), new TablaParametrosFiltroDto() { IdParametro = (Nullable<Decimal>)p_Dr["ID_MINISTERIO"] });
                    vObj.IdMinisterio = ministerio.HasElements() ? ministerio.Dtos.FirstOrDefault() : new TablaParametrosDto() { IdParametro = (Nullable<Decimal>)p_Dr["ID_MINISTERIO"] };                    
                }

                if (!(p_Dr["ID_SERVICIO"] is DBNull))
                {
                    ViewDto<TablaParametrosDto> servicio = new ViewDto<TablaParametrosDto>();
                    servicio = iTablaParametros.Buscar(new ContextoDto(), new TablaParametrosFiltroDto() { IdParametro = (Nullable<Decimal>)p_Dr["ID_SERVICIO"] });
                    vObj.IdServicio = servicio.HasElements() ? servicio.Dtos.FirstOrDefault() : new TablaParametrosDto() { IdParametro = (Nullable<Decimal>)p_Dr["ID_SERVICIO"] };
                }

                if (!(p_Dr["ETAPA"] is DBNull))
                {
                    ViewDto<TablaParametrosDto> etapa = new ViewDto<TablaParametrosDto>();
                    etapa = iTablaParametros.Buscar(new ContextoDto(), new TablaParametrosFiltroDto() { IdParametro = (Nullable<Decimal>)p_Dr["ETAPA"] });
                    vObj.Etapa = etapa.HasElements() ? etapa.Dtos.FirstOrDefault() : new TablaParametrosDto() { IdParametro = (Nullable<Decimal>)p_Dr["ETAPA"] };
                }

                if (!(p_Dr["ESTADO"] is DBNull))
                {
                    ViewDto<TablaParametrosDto> estado = new ViewDto<TablaParametrosDto>();
                    estado = iTablaParametros.Buscar(new ContextoDto(), new TablaParametrosFiltroDto() { IdParametro = (Nullable<Decimal>)p_Dr["ESTADO"] });
                    vObj.Estado = estado.HasElements() ? estado.Dtos.FirstOrDefault() : new TablaParametrosDto() { IdParametro = (Nullable<Decimal>)p_Dr["ESTADO"] };
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
		private void CreateViewDto(IDataReader p_Dr, ref ViewDto<TablaProgramasDto> p_Programas)
        {
            List<TablaProgramasDto> listDto = new List<TablaProgramasDto>();
            if (p_Dr != null) {
                while (p_Dr.Read()) {
                    listDto.Add(Create(p_Dr)); 
                } 
            }
            if (listDto.Count > 0){
                p_Programas = new ViewDto<TablaProgramasDto>(listDto);
            }
        }

        private TablaProgramasDto Create(OracleDataReader p_Dr, bool createAnos = false)
        {
            TablaProgramasDto vObj = new TablaProgramasDto();
            try
            {
                if (!createAnos)
                {
                    vObj.IdPrograma = (p_Dr["ID_PROGRAMA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_PROGRAMA"];
                    vObj.IdBips = (p_Dr["ID_BIPS"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_BIPS"];
                    vObj.Ano = (p_Dr["ANO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ANO"];
                    vObj.Tipo = (p_Dr["TIPO_FORMULARIO"] is DBNull) ? null : (String)p_Dr["TIPO_FORMULARIO"];
                    vObj.IdTipoFormulario = (p_Dr["ID_TIPO_FORMULARIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_TIPO_FORMULARIO"];
                    vObj.Nombre = (p_Dr["NOMBRE"] is DBNull) ? null : (String)p_Dr["NOMBRE"];
                    vObj.Ministerio = (p_Dr["MINISTERIO"] is DBNull) ? null : (String)p_Dr["MINISTERIO"];
                    vObj.Servicio = (p_Dr["SERVICIO"] is DBNull) ? null : (String)p_Dr["SERVICIO"];
                    vObj.IdEtapa = (p_Dr["ID_ETAPA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_ETAPA"]; ;
                    vObj.EtapaDesc = (p_Dr["ETAPA"] is DBNull) ? null : (String)p_Dr["ETAPA"];
                    vObj.IdEstado = (p_Dr["ID_ESTADO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_ESTADO"]; ;
                    vObj.EstadoDesc = (p_Dr["ESTADO"] is DBNull) ? null : (String)p_Dr["ESTADO"];
                    vObj.Acceso = (p_Dr["ACCESO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ACCESO"];
                    vObj.IdMinisterio.IdParametro = (p_Dr["ID_MINISTERIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_MINISTERIO"];
                    vObj.IdServicio.IdParametro = (p_Dr["ID_SERVICIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_SERVICIO"];
                    vObj.TotalUsuarios = (p_Dr["USUARIOS"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["USUARIOS"];
                    vObj.TotalGrupos = (p_Dr["GRUPOS"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["GRUPOS"];
                    if (vObj.IdPrograma != null)
                        vObj.IdEncriptado = EncriptaDato.RijndaelSimple.Encriptar(vObj.IdPrograma.ToString());
                }
                else
                {
                    vObj.Ano = (p_Dr["ANO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ANO"];
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR_CREATE_DTO", ex);
            }
            return vObj;
        }

        private void CreateViewDto(OracleDataReader p_Dr, ref ViewDto<TablaProgramasDto> p_Programas, bool anos = false)
        {
            FieldInfo fi = p_Dr.GetType().GetField("m_rowSize", BindingFlags.Instance | BindingFlags.NonPublic);
            int rowsize = Convert.ToInt32(fi.GetValue(p_Dr));
            p_Dr.FetchSize = rowsize * 100;
            List<TablaProgramasDto> listDto = new List<TablaProgramasDto>();
            if (p_Dr != null)
            {
                while (p_Dr.Read())
                {
                    listDto.Add(Create(p_Dr, anos));
                }
            }
            if (listDto.Count > 0)
            {
                p_Programas = new ViewDto<TablaProgramasDto>(listDto);
            }
        }
        #endregion

        #region metodos publicos
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
                dbCommand.CommandText = "PCKCBPROGRAMAS.prcBuscar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PROGRAMA", Direction = ParameterDirection.Input, Value = p_Filtro.IdPrograma });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_BIPS", Direction = ParameterDirection.Input, Value = p_Filtro.IdBips });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ANO", Direction = ParameterDirection.Input, Value = p_Filtro.Ano });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_TIPO_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Filtro.TipoFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_NOMBRE", Direction = ParameterDirection.Input, Value = p_Filtro.Nombre });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_MINISTERIO", Direction = ParameterDirection.Input, Value = p_Filtro.IdMinisterio });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_SERVICIO", Direction = ParameterDirection.Input, Value = p_Filtro.IdServicio });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_ETAPA", Direction = ParameterDirection.Input, Value = p_Filtro.Etapa });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_ESTADO", Direction = ParameterDirection.Input, Value = p_Filtro.Estado });
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

        public ViewDto<TablaProgramasDto> BuscarAnos(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro)
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
                dbCommand.CommandText = "PCKCBPROGRAMAS.prcAnos";                
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.RefCursor, ParameterName = "RESULTSCURSOR", Direction = ParameterDirection.Output });
                dr = dbCommand.ExecuteReader();
                CreateViewDto(dr, ref viewResponse, true);
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
        /// Metodo que permite buscar los registros de años en cb_programas existentes
        /// </summary>
        /// <param name="p_Contexto">Información del contexto</param>
        /// <param name="p_Filtro">Información de filtrado para realizar la busqueda</param>
        /// <returns>Objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaProgramasDto> BuscarAnosGores(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro)
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
                dbCommand.CommandText = "PCKCBPROGRAMAS.prcAnosGores";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.RefCursor, ParameterName = "RESULTSCURSOR", Direction = ParameterDirection.Output });
                dr = dbCommand.ExecuteReader();
                CreateViewDto(dr, ref viewResponse, true);
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
        /// metodo que permite crear un nuevo registro de CB_FORMULARIOS
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_FORMULARIOS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaProgramasDto> Insertar(ContextoDto p_Contexto, TablaProgramasDto p_Datos)
        {
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaProgramasDto> viewResponse = new ViewDto<TablaProgramasDto>();
            Decimal vCont = 0;
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBPROGRAMAS.prcInsertar";                
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ANO", Direction = ParameterDirection.Input, Value = p_Datos.Ano });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Datos.IdTipoFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_NOMBRE", Direction = ParameterDirection.Input, Value = p_Datos.Nombre });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_MINISTERIO", Direction = ParameterDirection.Input, Value = p_Datos.IdMinisterio.IdParametro });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_SERVICIO", Direction = ParameterDirection.Input, Value = p_Datos.IdServicio.IdParametro });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_BIPS", Direction = ParameterDirection.Input, Value = p_Datos.IdBips });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ESTADO", Direction = ParameterDirection.Input, Value = p_Datos.Estado.IdParametro });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_FORMULARIO", Direction = ParameterDirection.Output });
                dbCommand.ExecuteNonQuery();
                vCont = decimal.Parse(dbCommand.Parameters["p_ID_FORMULARIO"].Value.ToString());
                if (vCont > 0){
                    p_Datos.IdPrograma = vCont;
                    p_Datos.IdEncriptado = EncriptaDato.RijndaelSimple.Encriptar(vCont.ToString());
                    viewResponse.Dtos = new List<TablaProgramasDto> { p_Datos };
                } else { throw new Exception("Error insert PCKCBPROGRAMAS.prcInsertar"); }                    
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                viewResponse.Error = new ErrorDto() { Mensaje = ex.Message };
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
		/// metodo que permite eliminar un registro de CB_PROGRAMAS
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Datos">formulario a eliminar</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaProgramasDto> Eliminar(ContextoDto p_Contexto, TablaProgramasDto p_Datos)
        {
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaProgramasDto> viewResponse = new ViewDto<TablaProgramasDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBPROGRAMAS.prcBorrar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Datos.IdPrograma });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ESTADO", Direction = ParameterDirection.Input, Value = p_Datos.Estado.IdParametro });
                OracleTransaction tx = dbConn.BeginTransaction();
                dbCommand.ExecuteNonQuery();
                tx.Commit();
                viewResponse.Dtos = new List<TablaProgramasDto> { p_Datos };
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
		/// metodo que permite actualizar un registro de CB_PROGRAMAS existente
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Datos">grupo a actualizar</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaProgramasDto> Actualizar(ContextoDto p_Contexto, TablaProgramasDto p_Datos)
        {
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaProgramasDto> viewResponse = new ViewDto<TablaProgramasDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBPROGRAMAS.prcActualizar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Datos.IdPrograma });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ESTADO", Direction = ParameterDirection.Input, Value = p_Datos.Estado.IdParametro });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ETAPA", Direction = ParameterDirection.Input, Value = p_Datos.Etapa.IdParametro });
                OracleTransaction tx = dbConn.BeginTransaction();
                dbCommand.ExecuteNonQuery();
                tx.Commit();
                viewResponse.Dtos = new List<TablaProgramasDto> { p_Datos };
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
