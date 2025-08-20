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
    /// Clase encargada de la gestion de informacion del objeto CB_RELACION_FORMULARIOS
    /// </summary>
    public partial class TablaRelacionFormulariosDao : ITablaRelacionFormulariosDao
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
        public TablaRelacionFormulariosDao()
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
		/// <param name="p_Dr">registro con informacion del objeto CB_RELACION_FORMULARIOS obtenido</param>
		/// <returns>dto conformado en base a la informacion entregada</returns>
		private TablaRelacionFormulariosDto Create(IDataReader p_Dr)
        {
            TablaRelacionFormulariosDto vObj = new TablaRelacionFormulariosDto();
            try
            {
                vObj.IdRelacionFormulario = (p_Dr["ID_RELACION_FORMULARIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_RELACION_FORMULARIO"];
                vObj.IdBips = (p_Dr["ID_BIPS"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_BIPS"];
                vObj.IdFormulario = (p_Dr["ID_FORMULARIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_FORMULARIO"];
                vObj.Ano = (p_Dr["ANO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ANO"];
                vObj.Nombre = (p_Dr["NOMBRE"] is DBNull) ? null : (String)p_Dr["NOMBRE"];
                vObj.IdMinisterio = (p_Dr["ID_MINISTERIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_MINISTERIO"];
                vObj.Ministerio = (p_Dr["MINISTERIO"] is DBNull) ? null : (String)p_Dr["MINISTERIO"];
                vObj.IdServicio = (p_Dr["ID_SERVICIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_SERVICIO"];
                vObj.Servicio = (p_Dr["SERVICIO"] is DBNull) ? null : (String)p_Dr["SERVICIO"];
                vObj.IdFormularioAnterior = (p_Dr["ID_FORMULARIO_ANT"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_FORMULARIO_ANT"];
                vObj.AnoAnterior = (p_Dr["ANO_ANT"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ANO_ANT"];
                vObj.NombreAnterior = (p_Dr["NOMBRE_ANT"] is DBNull) ? null : (String)p_Dr["NOMBRE_ANT"];
                vObj.IdMinisterioAnterior = (p_Dr["ID_MINISTERIO_ANT"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_MINISTERIO_ANT"];
                vObj.MinisterioAnterior = (p_Dr["MINISTERIO_ANT"] is DBNull) ? null : (String)p_Dr["MINISTERIO_ANT"];
                vObj.IdServicioAnterior = (p_Dr["ID_SERVICIO_ANT"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_SERVICIO_ANT"];
                vObj.ServicioAnterior = (p_Dr["SERVICIO_ANT"] is DBNull) ? null : (String)p_Dr["SERVICIO_ANT"];
                vObj.Orden = (p_Dr["ORDEN"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ORDEN"];
                vObj.FechaRegistro = (p_Dr["FECHA_REGISTRO"] is DBNull) ? (Nullable<DateTime>)null : (Nullable<DateTime>)p_Dr["FECHA_REGISTRO"];
                vObj.Observaciones = (p_Dr["OBSERVACIONES"] is DBNull) ? null : (String)p_Dr["OBSERVACIONES"];
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
		private void CreateViewDto(OracleDataReader p_Dr, ref ViewDto<TablaRelacionFormulariosDto> p_Respuestas)
        {
            FieldInfo fi = p_Dr.GetType().GetField("m_rowSize", BindingFlags.Instance | BindingFlags.NonPublic);
            int rowsize = Convert.ToInt32(fi.GetValue(p_Dr));
            p_Dr.FetchSize = rowsize * 100;
            List<TablaRelacionFormulariosDto> listDto = new List<TablaRelacionFormulariosDto>();
            if (p_Dr != null)
            {
                while (p_Dr.Read())
                {
                    listDto.Add(Create(p_Dr));
                }
            }
            if (listDto.Count > 0)
            {
                p_Respuestas = new ViewDto<TablaRelacionFormulariosDto>(listDto);
            }
        }
        #endregion

        #region metodos publicos
        /// <summary>
		/// metodo que permite buscar los registros de CB_RELACION_FORMULARIOS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaRelacionFormulariosDto> Buscar(ContextoDto p_Contexto, TablaRelacionFormulariosFiltroDto p_Filtro)
        {
            OracleDataReader dr = null;
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaRelacionFormulariosDto> viewResponse = new ViewDto<TablaRelacionFormulariosDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBRELACIONFORMULARIOS.prcBuscar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_RELACION_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Filtro.IdRelacionFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Filtro.IdFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_FORMULARIO_ANT", Direction = ParameterDirection.Input, Value = p_Filtro.IdFormularioAnterior });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_BIPS", Direction = ParameterDirection.Input, Value = p_Filtro.IdBips });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ANO", Direction = ParameterDirection.Input, Value = p_Filtro.Ano });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ANO_ANT", Direction = ParameterDirection.Input, Value = p_Filtro.AnoAnterior });
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
        /// metodo que permite crear un nuevo registro de CB_RELACION_FORMULARIOS
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_GRUPOS_FORMULARIOS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaRelacionFormulariosDto> Insertar(ContextoDto p_Contexto, TablaRelacionFormulariosDto p_Datos)
        {
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaRelacionFormulariosDto> viewResponse = new ViewDto<TablaRelacionFormulariosDto>();
            Decimal vCont = 0;
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBRELACIONFORMULARIOS.prcInsertar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Datos.IdFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_BIPS", Direction = ParameterDirection.Input, Value = p_Datos.IdBips });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_FORMULARIO_ANTERIOR", Direction = ParameterDirection.Input, Value = p_Datos.IdFormularioAnterior });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_OBSERVACION", Direction = ParameterDirection.Input, Value = p_Datos.Observaciones });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO_RELACION_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Datos.TipoRelacionFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_RELACION_FORMULARIO", Direction = ParameterDirection.Output });
                dbCommand.ExecuteNonQuery();
                vCont = decimal.Parse(dbCommand.Parameters["p_ID_RELACION_FORMULARIO"].Value.ToString());
                if (vCont > 0)
                {
                    p_Datos.IdRelacionFormulario = vCont;
                    viewResponse.Dtos = new List<TablaRelacionFormulariosDto> { p_Datos };
                }
                else
                {
                    throw new Exception("Error insert PCKCBRELACIONFORMULARIOS.prcInsertar");
                }
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
