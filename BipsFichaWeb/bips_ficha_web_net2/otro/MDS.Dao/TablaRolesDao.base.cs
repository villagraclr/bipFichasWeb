using log4net;
using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Core.Providers;
using MDS.Core.Util;
using MDS.Dto;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;

namespace MDS.Dao
{
    /// <summary>
	///	Clase encargada de la gestion de informacion del objeto CB_ROLES
	/// </summary>
    public partial class TablaRolesDao : ITablaRolesDao
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
        private static readonly ILog log = LogManager.GetLogger((typeof(ITablaRolesDao)));
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public TablaRolesDao()
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
		/// <param name="p_Dr">registro con informacion del objeto CB_ROLES obtenido</param>
		/// <returns>dto conformado en base a la informacion entregada</returns>
		private TablaRolesDto Create(IDataReader p_Dr)
        {
            TablaRolesDto vObj = new TablaRolesDto();
            try
            {
                if (!(p_Dr["ID_ROL"] is System.DBNull))
                    vObj.Id = (System.Decimal?)p_Dr["ID_ROL"];
                if (!(p_Dr["DESCRIPCION"] is System.DBNull))
                    vObj.Descripcion = (System.String)p_Dr["DESCRIPCION"];
                if (!(p_Dr["ESTADO"] is System.DBNull))
                    vObj.Estado = (System.Decimal?)p_Dr["ESTADO"];
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
		/// <param name="p_Roles">objeto en el cual se cargara la informacion</param>
		/// <returns>view dto conformado en base a la informacion entregada</returns>
		private void CreateViewDto(IDataReader p_Dr, ref ViewDto<TablaRolesDto> p_Roles)
        {
            List<TablaRolesDto> listDto = new List<TablaRolesDto>();
            if (p_Dr != null)
                while (p_Dr.Read())
                    listDto.Add(Create(p_Dr));

            if (listDto.Count > 0)
                p_Roles = new ViewDto<TablaRolesDto>(listDto);
        }
        #endregion

        #region metodos publicos
        /// <summary>
		/// metodo que permite crear un nuevo registro de cb_roles
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Roles">usuario a crear</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaRolesDto> Insertar(ContextoDto p_Contexto, TablaRolesDto p_Roles)
        {
            Database dbConn = null;
            DbCommand dbCommand = null;
            ViewDto<TablaRolesDto> viewResponse = new ViewDto<TablaRolesDto>();
            System.Decimal vCont = 0;
            try
            {
                dbConn = iProviderData.GetConexion("DB_MDS");
                dbCommand = dbConn.GetStoredProcCommand("PCKCBROLES.prcInsertar");
                dbCommand.Parameters.Add(new OracleParameter() { OracleType = OracleType.VarChar, ParameterName = "p_DESCRIPCION", Direction = ParameterDirection.Input, Value = this.GetValueToBD(p_Roles.Descripcion) });
                dbCommand.Parameters.Add(new OracleParameter() { OracleType = OracleType.Number, ParameterName = "p_ESTADO", Direction = ParameterDirection.Input, Value = this.GetValueToBD(p_Roles.Estado) });
                dbCommand.Parameters.Add(new OracleParameter() { OracleType = OracleType.Number, ParameterName = "p_ID_ROL", Direction = ParameterDirection.Output });
                dbConn.ExecuteNonQuery(dbCommand);
                vCont = (System.Decimal)dbCommand.Parameters["p_ID_ROL"].Value;
                if (vCont > 0)
                {
                    p_Roles.Id = vCont;
                    viewResponse.Dtos = new List<TablaRolesDto> { p_Roles };
                }
                else
                {
                    log.Info(EnumErrores.DAL_SENTENCIA_INCORRECTA);
                    iProviderError.LoadError(ref viewResponse, EnumErrores.DAL_SENTENCIA_INCORRECTA);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                iProviderError.LoadError(ref viewResponse, ex);
            }
            finally
            {
                if (dbCommand != null)
                    dbCommand.Dispose();
                dbConn = null;
                dbCommand = null;
            }
            return viewResponse;
        }

        /// <summary>
		/// metodo que permite actualizar un registro de cb_roles existente
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Roles">b_usuariomds a actualizar</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaRolesDto> Actualizar(ContextoDto p_Contexto, TablaRolesDto p_Roles)
        {
            Database dbConn = null;
            DbCommand dbCommand = null;
            ViewDto<TablaRolesDto> viewResponse = new ViewDto<TablaRolesDto>();
            int vCont = 0;
            try
            {
                AssertNull.NotNullOrEmpty(p_Roles.Id);
                dbConn = iProviderData.GetConexion("DB_MDS");
                dbCommand = dbConn.GetStoredProcCommand("PCKCBROLES.prcActualizar");
                dbCommand.Parameters.Add(new OracleParameter() { OracleType = OracleType.Number, ParameterName = "p_ID_ROL", Direction = ParameterDirection.Input, Value = this.GetValueToBD(p_Roles.Id) });
                dbCommand.Parameters.Add(new OracleParameter() { OracleType = OracleType.VarChar, ParameterName = "p_DESCRIPCION", Direction = ParameterDirection.Input, Value = this.GetValueToBD(p_Roles.Descripcion) });
                dbCommand.Parameters.Add(new OracleParameter() { OracleType = OracleType.Number, ParameterName = "p_ESTADO", Direction = ParameterDirection.Input, Value = this.GetValueToBD(p_Roles.Estado) });
                vCont = dbConn.ExecuteNonQuery(dbCommand);
                if (vCont > 0)
                {
                    viewResponse.Dtos = new List<TablaRolesDto> { p_Roles };
                }
                else
                {
                    log.Info(EnumErrores.DAL_SENTENCIA_INCORRECTA);
                    iProviderError.LoadError(ref viewResponse, EnumErrores.DAL_SENTENCIA_INCORRECTA);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                iProviderError.LoadError(ref viewResponse, ex);
            }
            finally
            {
                if (dbCommand != null)
                    dbCommand.Dispose();
                dbConn = null;
                dbCommand = null;
            }
            return viewResponse;
        }

        /// <summary>
		/// metodo que permite eliminar un registro de cb_roles existente
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Roles">b_usuariomds a eliminar</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaRolesDto> Eliminar(ContextoDto p_Contexto, TablaRolesDto p_Roles)
        {
            Database dbConn = null;
            DbCommand dbCommand = null;
            ViewDto<TablaRolesDto> viewResponse = new ViewDto<TablaRolesDto>();
            int vCont = 0;
            try
            {
                AssertNull.NotNullOrEmpty(p_Roles.Id);
                dbConn = iProviderData.GetConexion("DB_MDS");
                dbCommand = dbConn.GetStoredProcCommand("PCKCBROLES.prcBorrar");
                dbCommand.Parameters.Add(new OracleParameter() { OracleType = OracleType.Number, ParameterName = "p_ID_ROL", Direction = ParameterDirection.Input, Value = this.GetValueToBD(p_Roles.Id) });
                dbCommand.Parameters.Add(new OracleParameter() { OracleType = OracleType.Number, ParameterName = "p_ESTADO", Direction = ParameterDirection.Input, Value = this.GetValueToBD(p_Roles.Estado) });
                vCont = dbConn.ExecuteNonQuery(dbCommand);
                if (vCont > 0)
                {
                    viewResponse.Dtos = new List<TablaRolesDto> { p_Roles };
                }
                else
                {
                    log.Info(EnumErrores.DAL_SENTENCIA_INCORRECTA);
                    iProviderError.LoadError(ref viewResponse, EnumErrores.DAL_SENTENCIA_INCORRECTA);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                iProviderError.LoadError(ref viewResponse, ex);
            }
            finally
            {
                if (dbCommand != null)
                    dbCommand.Dispose();
                dbConn = null;
                dbCommand = null;
            }
            return viewResponse;
        }

        /// <summary>
		/// metodo que permite buscar los registros de cb_roles existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
		public ViewDto<TablaRolesDto> Buscar(ContextoDto p_Contexto, TablaRolesFiltroDto p_Filtro)
        {
            Database dbConn = null;
            DbCommand dbCommand = null;
            IDataReader dataReader = null;
            ViewDto<TablaRolesDto> viewResponse = new ViewDto<TablaRolesDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                dbConn = iProviderData.GetConexion("DB_MDS");
                dbCommand = dbConn.GetStoredProcCommand("PCKCBROLES.prcBuscar");
                dbCommand.Parameters.Add(new OracleParameter() { OracleType = OracleType.Number, ParameterName = "p_ID_ROL", Direction = ParameterDirection.Input, Value = this.GetValueToBD(p_Filtro.Id) });
                dbCommand.Parameters.Add(new OracleParameter() { OracleType = OracleType.VarChar, ParameterName = "p_DESCRIPCION", Direction = ParameterDirection.Input, Value = this.GetValueToBD(p_Filtro.Descripcion) });
                dbCommand.Parameters.Add(new OracleParameter() { OracleType = OracleType.Number, ParameterName = "p_ESTADO", Direction = ParameterDirection.Input, Value = this.GetValueToBD(p_Filtro.Estado) });
                dbCommand.Parameters.Add(new OracleParameter() { OracleType = OracleType.Cursor, ParameterName = "ResultsCursor", Direction = ParameterDirection.Output });
                dataReader = dbConn.ExecuteReader(dbCommand);
                CreateViewDto(dataReader, ref viewResponse);
                if (!viewResponse.HasElements())
                {
                    log.Info(EnumErrores.BLL_CONSULTA_SIN_DATOS);
                    iProviderError.LoadError(ref viewResponse, EnumErrores.BLL_CONSULTA_SIN_DATOS);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                iProviderError.LoadError(ref viewResponse, ex);
            }
            finally
            {
                if (dbCommand != null)
                    dbCommand.Dispose();
                if (dataReader != null)
                    dataReader.Dispose();
                dbConn = null;
                dbCommand = null;
                dataReader = null;
            }
            return viewResponse;
        }
        #endregion
    }
}
