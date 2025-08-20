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
	///	Clase encargada de la gestion de informacion del objeto CB_PREGUNTAS_GRUPOS
	/// </summary>
    public partial class TablaPreguntasGruposDao : ITablaPreguntasGruposDao
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
        public TablaPreguntasGruposDao()
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
		/// <param name="p_Dr">registro con informacion del objeto CB_PREGUNTAS_GRUPOS obtenido</param>
		/// <returns>dto conformado en base a la informacion entregada</returns>
		private TablaPreguntasGruposDto Create(IDataReader p_Dr)
        {
            TablaPreguntasGruposDto vObj = new TablaPreguntasGruposDto();
            try
            {
                vObj.IdPreguntaGrupo = (p_Dr["ID_PREGUNTA_GRUPO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_PREGUNTA_GRUPO"];
                vObj.IdPregunta = (p_Dr["ID_PREGUNTA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_PREGUNTA"];
                vObj.Pregunta = (p_Dr["PREGUNTA"] is DBNull) ? null : (String)p_Dr["PREGUNTA"];
                vObj.IdGrupo = (p_Dr["ID_GRUPO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_GRUPO"];
                vObj.IdTipoFormulario = (p_Dr["ID_TIPO_FORMULARIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_TIPO_FORMULARIO"];
                vObj.TipoFormulario = (p_Dr["TIPO_FORMULARIO"] is DBNull) ? null : (String)p_Dr["TIPO_FORMULARIO"];                
                vObj.IdTipoPregunta = (p_Dr["ID_TIPO_PREGUNTA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_TIPO_PREGUNTA"];
                vObj.TipoPregunta = (p_Dr["TIPO_PREGUNTA"] is DBNull) ? null : (String)p_Dr["TIPO_PREGUNTA"];
                vObj.IdCategoriaPregunta = (p_Dr["ID_CATEGORIA_PREGUNTA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_CATEGORIA_PREGUNTA"];
                vObj.TipoPreguntaValor = (p_Dr["TIPO_PREGUNTA_VALOR"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["TIPO_PREGUNTA_VALOR"];
                vObj.TipoPreguntaValor2 = (p_Dr["TIPO_PREGUNTA_VALOR2"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["TIPO_PREGUNTA_VALOR2"];
                vObj.Valores = (p_Dr["VALORES"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["VALORES"];
                vObj.IdFuncion = (p_Dr["ID_FUNCION"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_FUNCION"];
                vObj.Funcion = (p_Dr["FUNCION"] is DBNull) ? null : (String)p_Dr["FUNCION"];
                vObj.FuncionValor = (p_Dr["FUNCION_VALOR"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["FUNCION_VALOR"];
                vObj.FuncionValor2 = (p_Dr["FUNCION_VALOR2"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["FUNCION_VALOR2"];
                vObj.ValorFuncion = (p_Dr["VALOR_FUNCION"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["VALOR_FUNCION"];
                vObj.IdMenu = (p_Dr["ID_MENU"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_MENU"];
                vObj.TipoMenu = (p_Dr["TIPO_MENU"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["TIPO_MENU"];
                vObj.Orden = (p_Dr["ORDEN"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ORDEN"];
                vObj.IdEstado = (p_Dr["ID_ESTADO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_ESTADO"];
                vObj.Estado = (p_Dr["ESTADO"] is DBNull) ? null : (String)p_Dr["ESTADO"];
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
		private void CreateViewDto(OracleDataReader p_Dr, ref ViewDto<TablaPreguntasGruposDto> p_MenuProgramas)
        {
            FieldInfo fi = p_Dr.GetType().GetField("m_rowSize", BindingFlags.Instance | BindingFlags.NonPublic);
            int rowsize = Convert.ToInt32(fi.GetValue(p_Dr));
            p_Dr.FetchSize = rowsize * 100;
            List<TablaPreguntasGruposDto> listDto = new List<TablaPreguntasGruposDto>();
            if (p_Dr != null)
            {
                while (p_Dr.Read())
                {
                    listDto.Add(Create(p_Dr));
                }
            }
            if (listDto.Count > 0)
            {
                p_MenuProgramas = new ViewDto<TablaPreguntasGruposDto>(listDto);
            }
        }
        #endregion

        #region metodos publicos
        /// <summary>
		/// metodo que permite buscar los registros de CB_PREGUNTAS_GRUPOS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaPreguntasGruposDto> Buscar(ContextoDto p_Contexto, TablaPreguntasGruposFiltroDto p_Filtro)
        {
            OracleDataReader dr = null;
            OracleConnection dbConn = null;
            OracleCommand dbCommand = null;
            ViewDto<TablaPreguntasGruposDto> viewResponse = new ViewDto<TablaPreguntasGruposDto>();
            try
            {
                AssertNull.NotNullOrEmpty(p_Filtro);
                dbConn = iProviderData.GetConexion2("DB_MDS2");
                dbConn.Open();
                dbCommand = dbConn.CreateCommand();
                dbCommand.CommandType = CommandType.StoredProcedure;
                dbCommand.CommandText = "PCKCBPREGUNTASGRUPOS.prcBuscar";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PREGUNTA_GRUPO", Direction = ParameterDirection.Input, Value = p_Filtro.IdPreguntaGrupo });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PREGUNTA", Direction = ParameterDirection.Input, Value = p_Filtro.IdPregunta });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_GRUPO", Direction = ParameterDirection.Input, Value = p_Filtro.IdGrupo });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_TIPO_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Filtro.IdTipoFormulario });                
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_MENU", Direction = ParameterDirection.Input, Value = p_Filtro.IdMenu });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ORDEN", Direction = ParameterDirection.Input, Value = p_Filtro.Orden });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_ESTADO", Direction = ParameterDirection.Input, Value = p_Filtro.IdEstado });
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
        #endregion
    }
}
