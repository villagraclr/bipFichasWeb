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
    /// Clase encargada de la gestion de informacion del objeto CB_BENEFICIARIOS_RIS
    /// </summary>
    public partial class TablaBeneficiariosRisDao : ITablaBeneficiariosRisDao
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
        public TablaBeneficiariosRisDao()
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
		/// <param name="p_Dr">registro con informacion del objeto CB_BENEFICIARIOS_RIS obtenido</param>
		/// <returns>dto conformado en base a la informacion entregada</returns>
		private TablaBenefiariosRisDto Create(IDataReader p_Dr)
        {
            TablaBenefiariosRisDto vObj = new TablaBenefiariosRisDto();
            try
            {
                vObj.IdPrograma = (p_Dr["ID_PROGRAMA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_PROGRAMA"];
                vObj.NombreArchivo = (p_Dr["NOMBRE_ARCHIVO"] is DBNull) ? null : (String)p_Dr["NOMBRE_ARCHIVO"];
                vObj.TamanoArchivo = (p_Dr["TAMANO_ARCHIVO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["TAMANO_ARCHIVO"];
                vObj.CargaBeneficiarios = (p_Dr["CARGA_BENEFICIARIOS"] is DBNull) ? null : (String)p_Dr["CARGA_BENEFICIARIOS"];
                vObj.Justificacion = (p_Dr["JUSTIFICACION"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["JUSTIFICACION"];
                vObj.TextoJustificacion = (p_Dr["TEXTO_JUSTIFICACION"] is DBNull) ? null : (String)p_Dr["TEXTO_JUSTIFICACION"];                
                vObj.UsuarioCarga = (p_Dr["USUARIO_CARGA"] is DBNull) ? null : (String)p_Dr["USUARIO_CARGA"];
                vObj.FechaCarga = (p_Dr["FECHA_CARGA"] is DBNull) ? (Nullable<DateTime>)null : (Nullable<DateTime>)p_Dr["FECHA_CARGA"];
                vObj.NombreEncode = (p_Dr["NOMBRE_ENCODE"] is DBNull) ? null : (String)p_Dr["NOMBRE_ENCODE"];
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
		private void CreateViewDto(OracleDataReader p_Dr, ref ViewDto<TablaBenefiariosRisDto> p_beneficiarios)
        {
            FieldInfo fi = p_Dr.GetType().GetField("m_rowSize", BindingFlags.Instance | BindingFlags.NonPublic);
            int rowsize = Convert.ToInt32(fi.GetValue(p_Dr));
            p_Dr.FetchSize = rowsize * 100;
            List<TablaBenefiariosRisDto> listDto = new List<TablaBenefiariosRisDto>();
            if (p_Dr != null)
            {
                while (p_Dr.Read())
                {
                    listDto.Add(Create(p_Dr));
                }
            }
            if (listDto.Count > 0)
            {
                p_beneficiarios = new ViewDto<TablaBenefiariosRisDto>(listDto);
            }
        }
        #endregion

        #region metodos publicos
        /// <summary>
		/// metodo que permite buscar los registros de CB_BENEFICIARIOS_RIS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaBenefiariosRisDto> Buscar(ContextoDto p_Contexto, TablaBenefiariosRisDto p_Filtro)
        {
            OracleDataReader dr = null;
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaBenefiariosRisDto> viewResponse = new ViewDto<TablaBenefiariosRisDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBBENEFICIARIOSRIS.prcBuscar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PROGRAMA", Direction = ParameterDirection.Input, Value = p_Filtro.IdPrograma });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_USUARIO_CARGA", Direction = ParameterDirection.Input, Value = p_Filtro.UsuarioCarga });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.TimeStamp, ParameterName = "p_FECHA_CARGA", Direction = ParameterDirection.Input, Value = p_Filtro.FechaCarga });
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
        /// metodo que permite crear un nuevo registro de CB_BENEFICIARIOS_RIS
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_CONSULTAS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaBenefiariosRisDto> Insertar(ContextoDto p_Contexto, TablaBenefiariosRisDto p_Datos)
        {
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaBenefiariosRisDto> viewResponse = new ViewDto<TablaBenefiariosRisDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBBENEFICIARIOSRIS.prcInsertar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PROGRAMA", Direction = ParameterDirection.Input, Value = p_Datos.IdPrograma });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_NOMBRE_ARCHIVO", Direction = ParameterDirection.Input, Value = p_Datos.NombreArchivo });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TAMANO_ARCHIVO", Direction = ParameterDirection.Input, Value = p_Datos.TamanoArchivo });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_CARGA_BENEFICIARIOS", Direction = ParameterDirection.Input, Value = p_Datos.CargaBeneficiarios });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_JUSTIFICACION", Direction = ParameterDirection.Input, Value = p_Datos.Justificacion });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_TEXTO_JUSTIFICACION", Direction = ParameterDirection.Input, Value = p_Datos.TextoJustificacion });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_USUARIO_CARGA", Direction = ParameterDirection.Input, Value = p_Datos.UsuarioCarga });                
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.TimeStamp, ParameterName = "p_FECHA_CARGA", Direction = ParameterDirection.Input, Value = p_Datos.FechaCarga });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_NOMBRE_ENCODE", Direction = ParameterDirection.Input, Value = p_Datos.NombreEncode });
                dbCommand.ExecuteNonQuery();
                viewResponse.Dtos = new List<TablaBenefiariosRisDto> { p_Datos };
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
        public ViewDto<TablaBenefiariosRisDto> Eliminar(ContextoDto p_Contexto, TablaBenefiariosRisDto p_Datos)
        {
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaBenefiariosRisDto> viewResponse = new ViewDto<TablaBenefiariosRisDto>();
            //int vCont = 0;
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBBENEFICIARIOSRIS.prcBorrar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PROGRAMA", Direction = ParameterDirection.Input, Value = p_Datos.IdPrograma });
                OracleTransaction tx = dbConn.BeginTransaction();
                dbCommand.ExecuteNonQuery();
                tx.Commit();
                viewResponse.Dtos = new List<TablaBenefiariosRisDto> { p_Datos };
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