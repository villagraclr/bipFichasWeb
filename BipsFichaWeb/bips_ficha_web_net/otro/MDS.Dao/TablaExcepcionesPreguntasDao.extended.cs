using MDS.Core.Dto;
using MDS.Core.Util;
using MDS.Dto;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace MDS.Dao
{
    public partial class TablaExcepcionesPreguntasDao
    {
        #region campos privados        
        #endregion

        #region campos publicos
        #endregion

        #region constructores
        #endregion

        #region metodos privados
        /// <summary>
		/// metodo que realiza conversion del registro obtenido a dto
		/// </summary>
		/// <param name="p_Dr">registro con informacion del objeto CB_EXCEPCIONES_PLANTILLAS obtenido</param>
		/// <returns>dto conformado en base a la informacion entregada</returns>
		private TablaProgramasDto CreatePermisosForm(IDataReader p_Dr)
        {
            TablaProgramasDto vObj = new TablaProgramasDto();
            try
            {
                vObj.IdUser = (p_Dr["ID_USUARIO"] is DBNull) ? null : (String)p_Dr["ID_USUARIO"];
                vObj.IdPerfil = (p_Dr["ID_PERFIL"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_PERFIL"];
                vObj.IdMinisterio.IdParametro = (p_Dr["ID_MINISTERIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_MINISTERIO"];
                vObj.Ministerio = (p_Dr["MINISTERIO"] is DBNull) ? null : (String)p_Dr["MINISTERIO"];
                vObj.IdServicio.IdParametro = (p_Dr["ID_SERVICIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_SERVICIO"];
                vObj.Servicio = (p_Dr["SERVICIO"] is DBNull) ? null : (String)p_Dr["SERVICIO"];
                vObj.IdPrograma = (p_Dr["ID_FORMULARIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_FORMULARIO"];
                vObj.IdBips = (p_Dr["ID_BIPS"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_BIPS"];
                vObj.Nombre = (p_Dr["NOMBRE"] is DBNull) ? null : (String)p_Dr["NOMBRE"];
                vObj.Ano = (p_Dr["ANO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ANO"];
                vObj.IdTipoFormulario = (p_Dr["ID_TIPO_FORMULARIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_TIPO_FORMULARIO"];
                vObj.TipoFormulario.Descripcion = (p_Dr["DESC_TIPO_FORMULARIO"] is DBNull) ? null : (String)p_Dr["DESC_TIPO_FORMULARIO"];
                vObj.IdExcepcion = (p_Dr["ID_EXCEPCION"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_EXCEPCION"];
                vObj.TipoExcepcion = (p_Dr["TIPO_EXCEPCION"] is DBNull) ? null : (String)p_Dr["TIPO_EXCEPCION"];
                vObj.DescExcepcion = (p_Dr["DESCRIPCION_EXCEPCION"] is DBNull) ? null : (String)p_Dr["DESCRIPCION_EXCEPCION"];
                vObj.Estado.IdParametro = (p_Dr["ESTADO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ESTADO"];
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR_CREATE_DTO", ex);
            }
            return vObj;
        }

        /// <summary>
		/// metodo que realiza conversion del registro obtenido a dto
		/// </summary>
		/// <param name="p_Dr">registro con informacion del objeto CB_EXCEPCIONES_PLANTILLAS obtenido</param>
		/// <returns>dto conformado en base a la informacion entregada</returns>
		private TablaExcepcionesPlantillasDto CreatePlantillasForm(IDataReader p_Dr)
        {
            TablaExcepcionesPlantillasDto vObj = new TablaExcepcionesPlantillasDto();
            try
            {                
                vObj.IdExcepcionPlantilla = (p_Dr["ID_EXCEPCION_PLANTILLA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_EXCEPCION_PLANTILLA"];
                vObj.Nombre = (p_Dr["NOMBRE"] is DBNull) ? null : (String)p_Dr["NOMBRE"];
                vObj.Descripcion = (p_Dr["DESCRIPCION"] is DBNull) ? null : (String)p_Dr["DESCRIPCION"];
                vObj.TipoFormulario = (p_Dr["TIPO_FORMULARIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["TIPO_FORMULARIO"];
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
		/// <param name="p_Respuestas">objeto en el cual se cargara la informacion</param>
		/// <returns>view dto conformado en base a la informacion entregada</returns>
		private void CreateViewDtoPermisosForm(OracleDataReader p_Dr, ref ViewDto<TablaProgramasDto> p_Respuestas)
        {
            FieldInfo fi = p_Dr.GetType().GetField("m_rowSize", BindingFlags.Instance | BindingFlags.NonPublic);
            int rowsize = Convert.ToInt32(fi.GetValue(p_Dr));
            p_Dr.FetchSize = rowsize * 100;
            List<TablaProgramasDto> listDto = new List<TablaProgramasDto>();
            if (p_Dr != null)
            {
                while (p_Dr.Read())
                {
                    listDto.Add(CreatePermisosForm(p_Dr));
                }
            }
            if (listDto.Count > 0)
            {
                p_Respuestas = new ViewDto<TablaProgramasDto>(listDto);
            }
        }

        /// <summary>
		/// metodo que realiza conversion de los registros obtenidos a un array de dto
		/// </summary>
		/// <param name="p_Dr">informacion de las tareas obtenidas</param>
		/// <param name="p_Respuestas">objeto en el cual se cargara la informacion</param>
		/// <returns>view dto conformado en base a la informacion entregada</returns>
		private void CreateViewDtoPlantillasForm(OracleDataReader p_Dr, ref ViewDto<TablaExcepcionesPlantillasDto> p_Respuestas)
        {
            FieldInfo fi = p_Dr.GetType().GetField("m_rowSize", BindingFlags.Instance | BindingFlags.NonPublic);
            int rowsize = Convert.ToInt32(fi.GetValue(p_Dr));
            p_Dr.FetchSize = rowsize * 100;
            List<TablaExcepcionesPlantillasDto> listDto = new List<TablaExcepcionesPlantillasDto>();
            if (p_Dr != null)
            {
                while (p_Dr.Read())
                {
                    listDto.Add(CreatePlantillasForm(p_Dr));
                }
            }
            if (listDto.Count > 0)
            {
                p_Respuestas = new ViewDto<TablaExcepcionesPlantillasDto>(listDto);
            }
        }

        /// <summary>
		/// metodo que realiza conversion del registro obtenido a dto
		/// </summary>
		/// <param name="p_Dr">registro con informacion del objeto CB_EXCEPCIONES_PLANTILLAS_FORM obtenido</param>
		/// <returns>dto conformado en base a la informacion entregada</returns>
		private TablaExcepcionesPlantillasFormDto CreateExcepciones(IDataReader p_Dr)
        {
            TablaExcepcionesPlantillasFormDto vObj = new TablaExcepcionesPlantillasFormDto();
            try
            {
                vObj.IdExcepcionPlantilla = (p_Dr["ID_EXCEPCION_PLANTILLA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_EXCEPCION_PLANTILLA"];
                vObj.IdPregunta = (p_Dr["ID_PREGUNTA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_PREGUNTA"];
                vObj.TipoExcepcion = (p_Dr["TIPO_EXCEPCION"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["TIPO_EXCEPCION"];                
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
		/// <param name="p_Respuestas">objeto en el cual se cargara la informacion</param>
		/// <returns>view dto conformado en base a la informacion entregada</returns>
		private void CreateViewDtoExcepciones(OracleDataReader p_Dr, ref ViewDto<TablaExcepcionesPlantillasFormDto> p_Respuestas)
        {
            FieldInfo fi = p_Dr.GetType().GetField("m_rowSize", BindingFlags.Instance | BindingFlags.NonPublic);
            int rowsize = Convert.ToInt32(fi.GetValue(p_Dr));
            p_Dr.FetchSize = rowsize * 100;
            List<TablaExcepcionesPlantillasFormDto> listDto = new List<TablaExcepcionesPlantillasFormDto>();
            if (p_Dr != null)
            {
                while (p_Dr.Read())
                {
                    listDto.Add(CreateExcepciones(p_Dr));
                }
            }
            if (listDto.Count > 0)
            {
                p_Respuestas = new ViewDto<TablaExcepcionesPlantillasFormDto>(listDto);
            }
        }
        #endregion

        #region metodos publicos
        /// <summary>
		/// metodo que permite buscar los registros de CB_EXCEPCIONES_PLANTILLAS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaProgramasDto> BuscarPermisosFormularios(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro)
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
                dbCommand.CommandText = "PCKCBEXCEPCIONESFORMULARIOS.prcBuscarPermisos";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_ID_USUARIO", Direction = ParameterDirection.Input, Value = p_Filtro.IdUser });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Filtro.IdPrograma });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_TIPO_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Filtro.TipoFormulario });                
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO_EXCEPCION", Direction = ParameterDirection.Input, Value = p_Filtro.IdExcepcion });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ESTADO", Direction = ParameterDirection.Input, Value = p_Filtro.Estado });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.RefCursor, ParameterName = "RESULTSCURSOR", Direction = ParameterDirection.Output });
                dr = dbCommand.ExecuteReader();
                CreateViewDtoPermisosForm(dr, ref viewResponse);
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
		/// metodo que permite buscar los registros de CB_EXCEPCIONES_PLANTILLAS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaExcepcionesPlantillasDto> BuscarPlantillasFormularios(ContextoDto p_Contexto, TablaExcepcionesPlantillasFiltroDto p_Filtro)
        {
            OracleDataReader dr = null;
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaExcepcionesPlantillasDto> viewResponse = new ViewDto<TablaExcepcionesPlantillasDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBEXCEPCIONESFORMULARIOS.prcBuscarPlantillas";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_EXCEPCION_PLANTILLA", Direction = ParameterDirection.Input, Value = p_Filtro.IdExcepcionPlantilla });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Filtro.TipoFormulario });                
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ESTADO", Direction = ParameterDirection.Input, Value = p_Filtro.Estado });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.RefCursor, ParameterName = "RESULTSCURSOR", Direction = ParameterDirection.Output });
                dr = dbCommand.ExecuteReader();
                CreateViewDtoPlantillasForm(dr, ref viewResponse);
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
        /// metodo que permite crear un nuevo registro de CB_EXCEPCIONES_FORMULARIOS
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_EXCEPCIONES_FORMULARIOS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaExcepcionesPermisosDto> Insertar(ContextoDto p_Contexto, TablaExcepcionesPermisosDto p_Datos)
        {
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaExcepcionesPermisosDto> viewResponse = new ViewDto<TablaExcepcionesPermisosDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBEXCEPCIONESFORMULARIOS.prcInsertar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_ID_USUARIO", Direction = ParameterDirection.Input, Value = p_Datos.IdUsuario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Datos.IdFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_EXCEPCION_PLANTILLA", Direction = ParameterDirection.Input, Value = p_Datos.IdPermiso });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_ESTADO", Direction = ParameterDirection.Input, Value = p_Datos.Estado });
                dbCommand.ExecuteNonQuery();
                viewResponse.Dtos = new List<TablaExcepcionesPermisosDto> { p_Datos };
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
		/// metodo que permite eliminar un registro de CB_EXCEPCIONES_FORMULARIOS
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Datos">usuario a eliminar</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaExcepcionesPermisosDto> EliminarPermisosUsuarios(ContextoDto p_Contexto, TablaExcepcionesPermisosDto p_Datos)
        {
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaExcepcionesPermisosDto> viewResponse = new ViewDto<TablaExcepcionesPermisosDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBEXCEPCIONESFORMULARIOS.prcBorrar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_ID_USUARIO", Direction = ParameterDirection.Input, Value = p_Datos.IdUsuario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Datos.IdFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ESTADO", Direction = ParameterDirection.Input, Value = p_Datos.Estado });
                OracleTransaction tx = dbConn.BeginTransaction();
                dbCommand.ExecuteNonQuery();
                tx.Commit();
                viewResponse.Dtos = new List<TablaExcepcionesPermisosDto> { p_Datos };
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

        /// <summary>
        /// metodo que permite crear un nuevo registro de CB_EXCEPCIONES_PLANTILLAS
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_EXCEPCIONES_PLANTILLAS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaExcepcionesPlantillasDto> InsertarPlantilla(ContextoDto p_Contexto, TablaExcepcionesPlantillasDto p_Datos)
        {
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaExcepcionesPlantillasDto> viewResponse = new ViewDto<TablaExcepcionesPlantillasDto>();
            Decimal vCont = 0;
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBEXCEPCIONESPLANTILLAS.prcInsertar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_NOMBRE", Direction = ParameterDirection.Input, Value = p_Datos.Nombre });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_DESCRIPCION", Direction = ParameterDirection.Input, Value = p_Datos.Descripcion });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Datos.TipoFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_ESTADO", Direction = ParameterDirection.Input, Value = p_Datos.Estado });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_EXCEPCION_PLANTILLA", Direction = ParameterDirection.Output });
                dbCommand.ExecuteNonQuery();
                vCont = decimal.Parse(dbCommand.Parameters["p_ID_EXCEPCION_PLANTILLA"].Value.ToString());
                if (vCont > 0)
                {
                    p_Datos.IdExcepcionPlantilla = vCont;
                    viewResponse.Dtos = new List<TablaExcepcionesPlantillasDto> { p_Datos };
                }
                else { throw new Exception("Error insert PCKCBEXCEPCIONESPLANTILLAS.prcInsertar"); }                
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
        /// metodo que permite crear un nuevo registro de CB_EXCEPCIONES_PLANTILLAS_FORM
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_EXCEPCIONES_PLANTILLAS_FORM a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaExcepcionesPlantillasFormDto> InsertarPlantillaForm(ContextoDto p_Contexto, TablaExcepcionesPlantillasFormDto p_Datos)
        {
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaExcepcionesPlantillasFormDto> viewResponse = new ViewDto<TablaExcepcionesPlantillasFormDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBEXCEPCIONESPLANTILLAS.prcInsertarForm";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_ID_EXCEPCION_PLANTILLA", Direction = ParameterDirection.Input, Value = p_Datos.IdExcepcionPlantilla });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PREGUNTA", Direction = ParameterDirection.Input, Value = p_Datos.IdPregunta });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO_EXCEPCION", Direction = ParameterDirection.Input, Value = p_Datos.TipoExcepcion });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_ESTADO", Direction = ParameterDirection.Input, Value = p_Datos.Estado });
                dbCommand.ExecuteNonQuery();
                viewResponse.Dtos = new List<TablaExcepcionesPlantillasFormDto> { p_Datos };
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
        /// metodo que permite crear un nuevo registro de CB_EXCEPCIONES_PLANTILLAS_FORM
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_EXCEPCIONES_PLANTILLAS_FORM a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaExcepcionesPlantillasFormDto> BuscarExcepciones(ContextoDto p_Contexto, TablaExcepcionesPlantillasFormDto p_Datos)
        {
            OracleDataReader dr = null;
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaExcepcionesPlantillasFormDto> viewResponse = new ViewDto<TablaExcepcionesPlantillasFormDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBEXCEPCIONESPLANTILLAS.prcBuscar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_ID_EXCEPCION_PLANTILLA", Direction = ParameterDirection.Input, Value = p_Datos.IdExcepcionPlantilla });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PREGUNTA", Direction = ParameterDirection.Input, Value = p_Datos.IdPregunta });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO_EXCEPCION", Direction = ParameterDirection.Input, Value = p_Datos.TipoExcepcion });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_ESTADO", Direction = ParameterDirection.Input, Value = p_Datos.Estado });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.RefCursor, ParameterName = "RESULTSCURSOR", Direction = ParameterDirection.Output });
                dr = dbCommand.ExecuteReader();
                CreateViewDtoExcepciones(dr, ref viewResponse);
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
