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
    public partial class TablaGruposFormulariosDao
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
		/// <param name="p_Dr">registro con informacion del objeto CB_GRUPOS_FORMULARIOS obtenido</param>
		/// <returns>dto conformado en base a la informacion entregada</returns>
		private TablaUsuariosDto CreateUsuarios(IDataReader p_Dr)
        {
            TablaUsuariosDto vObj = new TablaUsuariosDto();
            try
            {
                vObj.Id = (p_Dr["ID_USUARIO"] is DBNull) ? null : (String)p_Dr["ID_USUARIO"];
                vObj.Nombre = (p_Dr["NOMBRE"] is DBNull) ? null : (String)p_Dr["NOMBRE"];
                vObj.UserName = (p_Dr["USUARIO"] is DBNull) ? null : (String)p_Dr["USUARIO"];
                vObj.Email = (p_Dr["EMAIL"] is DBNull) ? null : (String)p_Dr["EMAIL"];
                vObj.IdMinisterio = (p_Dr["ID_MINISTERIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_MINISTERIO"];
                vObj.Ministerio = (p_Dr["MINISTERIO"] is DBNull) ? null : (String)p_Dr["MINISTERIO"];
                vObj.IdServicio = (p_Dr["ID_SERVICIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_SERVICIO"];
                vObj.Servicio = (p_Dr["SERVICIO"] is DBNull) ? null : (String)p_Dr["SERVICIO"];
                vObj.IdGrupo = (p_Dr["ID_GRUPO_FORMULARIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_GRUPO_FORMULARIO"];
                vObj.Grupo = (p_Dr["GRUPO"] is DBNull) ? null : (String)p_Dr["GRUPO"];
                vObj.DescripcionGrupo = (p_Dr["DESCRIPCION_GRUPO"] is DBNull) ? null : (String)p_Dr["DESCRIPCION_GRUPO"];
                vObj.IdTipoGrupo = (p_Dr["ID_TIPO_GRUPO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_TIPO_GRUPO"];
                vObj.TipoGrupo = (p_Dr["TIPO_GRUPO"] is DBNull) ? null : (String)p_Dr["TIPO_GRUPO"];
                vObj.IdEstado = (p_Dr["ESTADO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ESTADO"];
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
		/// <param name="p_Dr">registro con informacion del objeto CB_FORMULARIOS_USUARIOS obtenido</param>
		/// <returns>dto conformado en base a la informacion entregada</returns>
		private TablaGruposFormulariosDto CreateGruposUsuarios(IDataReader p_Dr)
        {
            TablaGruposFormulariosDto vObj = new TablaGruposFormulariosDto();
            try
            {
                vObj.IdUsuario = (p_Dr["ID_USUARIO"] is DBNull) ? null : (String)p_Dr["ID_USUARIO"];
                vObj.IdGrupoFormulario = (p_Dr["ID_GRUPO_FORMULARIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_GRUPO_FORMULARIO"];
                vObj.Nombre = (p_Dr["NOMBRE_GRUPO"] is DBNull) ? null : (String)p_Dr["NOMBRE_GRUPO"];
                vObj.Descripcion = (p_Dr["DESC_GRUPO_FORMULARIO"] is DBNull) ? null : (String)p_Dr["DESC_GRUPO_FORMULARIO"];
                vObj.IdTipoGrupo = (p_Dr["TIPO_GRUPO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["TIPO_GRUPO"];
                vObj.TipoGrupo = (p_Dr["DESC_TIPO_GRUPO"] is DBNull) ? null : (String)p_Dr["DESC_TIPO_GRUPO"];                
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
		/// <param name="p_Programas">objeto en el cual se cargara la informacion</param>
		/// <returns>view dto conformado en base a la informacion entregada</returns>
		private void CreateViewDtoUsuarios(OracleDataReader p_Dr, ref ViewDto<TablaUsuariosDto> p_Respuestas)
        {
            FieldInfo fi = p_Dr.GetType().GetField("m_rowSize", BindingFlags.Instance | BindingFlags.NonPublic);
            int rowsize = Convert.ToInt32(fi.GetValue(p_Dr));
            p_Dr.FetchSize = rowsize * 100;
            List<TablaUsuariosDto> listDto = new List<TablaUsuariosDto>();
            if (p_Dr != null)
            {
                while (p_Dr.Read())
                {
                    listDto.Add(CreateUsuarios(p_Dr));
                }
            }
            if (listDto.Count > 0)
            {
                p_Respuestas = new ViewDto<TablaUsuariosDto>(listDto);
            }
        }

        /// <summary>
		/// metodo que realiza conversion de los registros obtenidos a un array de dto
		/// </summary>
		/// <param name="p_Dr">informacion de las tareas obtenidas</param>
		/// <param name="p_Programas">objeto en el cual se cargara la informacion</param>
		/// <returns>view dto conformado en base a la informacion entregada</returns>
		private void CreateViewDtoGruposUsuarios(OracleDataReader p_Dr, ref ViewDto<TablaGruposFormulariosDto> p_Respuestas)
        {
            FieldInfo fi = p_Dr.GetType().GetField("m_rowSize", BindingFlags.Instance | BindingFlags.NonPublic);
            int rowsize = Convert.ToInt32(fi.GetValue(p_Dr));
            p_Dr.FetchSize = rowsize * 100;
            List<TablaGruposFormulariosDto> listDto = new List<TablaGruposFormulariosDto>();
            if (p_Dr != null)
            {
                while (p_Dr.Read())
                {
                    listDto.Add(CreateGruposUsuarios(p_Dr));
                }
            }
            if (listDto.Count > 0)
            {
                p_Respuestas = new ViewDto<TablaGruposFormulariosDto>(listDto);
            }
        }
        #endregion

        #region metodos publicos
        /// <summary>
		/// metodo que permite buscar los registros de CB_GRUPOS_FORMULARIOS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaUsuariosDto> BuscarUsuarios(ContextoDto p_Contexto, TablaUsuariosFiltroDto p_Filtro)
        {
            OracleDataReader dr = null;
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaUsuariosDto> viewResponse = new ViewDto<TablaUsuariosDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBGRUPOSFORMULARIOS.prcBuscarUsuarios";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_GRUPO_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Filtro.IdGrupo });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_ID_USUARIO", Direction = ParameterDirection.Input, Value = p_Filtro.Id });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.RefCursor, ParameterName = "RESULTSCURSOR", Direction = ParameterDirection.Output });
                dr = dbCommand.ExecuteReader();
                CreateViewDtoUsuarios(dr, ref viewResponse);
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
		/// metodo que permite eliminar un registro de CB_FORMULARIOS_USUARIOS
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Datos">usuario a eliminar</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		public ViewDto<TablaGruposFormulariosDto> EliminarUsuarioGrupo(ContextoDto p_Contexto, TablaGruposFormulariosDto p_Datos)
        {
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaGruposFormulariosDto> viewResponse = new ViewDto<TablaGruposFormulariosDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Datos);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBFORMULARIOSUSUARIOS.prcBorrarUsuariosFormularios";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_ID_USER", Direction = ParameterDirection.Input, Value = p_Datos.IdUsuario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_GRUPO_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Datos.IdGrupoFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ESTADO", Direction = ParameterDirection.Input, Value = p_Datos.Estado });
                OracleTransaction tx = dbConn.BeginTransaction();
                dbCommand.ExecuteNonQuery();
                tx.Commit();
                viewResponse.Dtos = new List<TablaGruposFormulariosDto> { p_Datos };
            }
            catch (Exception ex)
            {
                viewResponse.Error = new ErrorDto() { Mensaje = ex.Message, Detalle = ex.Message };
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
		/// metodo que permite buscar los registros de CB_GRUPOS_USUARIOS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaGruposFormulariosDto> BuscarGruposUsuarios(ContextoDto p_Contexto, TablaGruposFormulariosFiltroDto p_Filtro)
        {
            OracleDataReader dr = null;
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaGruposFormulariosDto> viewResponse = new ViewDto<TablaGruposFormulariosDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBFORMULARIOSUSUARIOS.prcBuscarGruposUsuarios";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_ID_USUARIO", Direction = ParameterDirection.Input, Value = p_Filtro.IdUsuario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_GRUPO_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Filtro.IdGrupoFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PLATAFORMA", Direction = ParameterDirection.Input, Value = p_Filtro.IdPlataforma });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO_GRUPO", Direction = ParameterDirection.Input, Value = p_Filtro.IdTipoGrupo });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_ESTADO", Direction = ParameterDirection.Input, Value = p_Filtro.Estado });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.RefCursor, ParameterName = "RESULTSCURSOR", Direction = ParameterDirection.Output });
                dr = dbCommand.ExecuteReader();
                CreateViewDtoGruposUsuarios(dr, ref viewResponse);
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
