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
    public partial class TablaProgramasDao
    {
        #region campos privados
        #endregion

        #region campos publicos
        #endregion

        #region constructores
        #endregion

        #region metodos privados
        /// <summary>
        /// Metodo que realiza conversion del registro obtenido a dto
        /// </summary>
        /// <param name="p_Dr">Registro con informacion del objeto PROGRAMAS obtenido</param>
        /// <returns>dto conformado en base a la informacion entregada</returns>
        private TablaProgramasDto CreateExAnte(IDataReader p_Dr)
        {
            TablaProgramasDto vObj = new TablaProgramasDto();
            try
            {
                vObj.IdPrograma = (p_Dr["ID_PROGRAMA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_PROGRAMA"];
                vObj.IdBips = (p_Dr["ID_BIPS"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_BIPS"];
                vObj.Ano = (p_Dr["ANO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ANO"];
                vObj.Tipo = (p_Dr["TIPO_FORMULARIO"] is DBNull) ? null : (String)p_Dr["TIPO_FORMULARIO"];
                vObj.IdTipoFormulario = (p_Dr["ID_TIPO_FORMULARIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_TIPO_FORMULARIO"];
                vObj.Nombre = (p_Dr["NOMBRE"] is DBNull) ? null : (String)p_Dr["NOMBRE"];
                vObj.Ministerio = (p_Dr["MINISTERIO"] is DBNull) ? null : (String)p_Dr["MINISTERIO"];
                vObj.Servicio = (p_Dr["SERVICIO"] is DBNull) ? null : (String)p_Dr["SERVICIO"];
                vObj.IdEtapa = (p_Dr["ID_ETAPA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_ETAPA"];
                vObj.IdEstado = (p_Dr["ID_ESTADO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_ESTADO"];
                vObj.Calificacion = (p_Dr["CALIFICACION"] is DBNull) ? null : (String)p_Dr["CALIFICACION"];
                vObj.IdMinisterio.IdParametro = (p_Dr["ID_MINISTERIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_MINISTERIO"];
                vObj.IdServicio.IdParametro = (p_Dr["ID_SERVICIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_SERVICIO"];
                vObj.Version = (p_Dr["VERSION"] is DBNull) ? null : (String)p_Dr["VERSION"];
                vObj.IdEvaluador1 = (p_Dr["EVALUADOR_1"] is DBNull) ? null : (String)p_Dr["EVALUADOR_1"];
                vObj.IdEvaluador2 = (p_Dr["EVALUADOR_2"] is DBNull) ? null : (String)p_Dr["EVALUADOR_2"];
                vObj.IdEncriptado = EncriptaDato.RijndaelSimple.Encriptar(vObj.IdPrograma.ToString());
                vObj.PuntajeFinal = (p_Dr["PUNTAJE"] is DBNull) ? null : (String)p_Dr["PUNTAJE"];
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR_CREATE_DTO", ex);
            }
            return vObj;
        }

        /// <summary>
        /// Metodo que realiza conversion del registro obtenido a dto
        /// </summary>
        /// <param name="p_Dr">Registro con informacion del objeto PROGRAMAS obtenido</param>
        /// <returns>dto conformado en base a la informacion entregada</returns>
        private TablaProgramasDto CreateExAntePerfil(IDataReader p_Dr)
        {
            TablaProgramasDto vObj = new TablaProgramasDto();
            try
            {
                vObj.IdPrograma = (p_Dr["ID_PROGRAMA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_PROGRAMA"];
                vObj.IdBips = (p_Dr["ID_BIPS"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_BIPS"];
                vObj.IdMinisterio.IdParametro = (p_Dr["ID_GORE"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_GORE"];
                vObj.Ministerio = (p_Dr["GORE"] is DBNull) ? null : (String)p_Dr["GORE"];
                vObj.IdTipoFormulario = (p_Dr["ID_TIPO_FORMULARIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_TIPO_FORMULARIO"];
                vObj.Tipo = (p_Dr["TIPO_FORMULARIO"] is DBNull) ? null : (String)p_Dr["TIPO_FORMULARIO"];
                vObj.Nombre = (p_Dr["NOMBRE"] is DBNull) ? null : (String)p_Dr["NOMBRE"];
                vObj.IdEtapa = (p_Dr["ID_ETAPA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_ETAPA"];
                vObj.EtapaDesc = (p_Dr["ETAPA"] is DBNull) ? null : (String)p_Dr["ETAPA"];
                vObj.Ano = (p_Dr["ANO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ANO"];
                vObj.IdEstado = (p_Dr["ID_ESTADO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_ESTADO"];
                vObj.Calificacion = (p_Dr["CALIFICACION"] is DBNull) ? null : (String)p_Dr["CALIFICACION"];
                vObj.CalificacionPrograma = (p_Dr["CALIFICACION_PROGRAMA"] is DBNull) ? null : (String)p_Dr["CALIFICACION_PROGRAMA"];
                vObj.Version = (p_Dr["VERSION"] is DBNull) ? null : (String)p_Dr["VERSION"];
                vObj.IdEvaluador1 = (p_Dr["EVALUADOR_1"] is DBNull) ? null : (String)p_Dr["EVALUADOR_1"];
                vObj.IdEvaluador2 = (p_Dr["EVALUADOR_2"] is DBNull) ? null : (String)p_Dr["EVALUADOR_2"];
                vObj.TipoGeneral = (p_Dr["TIPO_OFERTA"] is DBNull) ? null : (decimal?)Convert.ToDecimal(p_Dr["TIPO_OFERTA"].ToString());
                vObj.IdEncriptado = EncriptaDato.RijndaelSimple.Encriptar(vObj.IdPrograma.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR_CREATE_DTO", ex);
            }
            return vObj;
        }

        /// <summary>
        /// Metodo que realiza conversion del registro obtenido a dto
        /// </summary>
        /// <param name="p_Dr">Registro con informacion del objeto PROGRAMAS obtenido</param>
        /// <returns>dto conformado en base a la informacion entregada</returns>
        private TablaProgramasDto CreatePanelExAnte(IDataReader p_Dr)
        {
            TablaProgramasDto vObj = new TablaProgramasDto();
            try
            {
                vObj.IdPrograma = (p_Dr["ID_PROGRAMA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_PROGRAMA"];
                vObj.IdBips = (p_Dr["ID_BIPS"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_BIPS"];
                vObj.Ministerio = (p_Dr["MINISTERIO"] is DBNull) ? null : (String)p_Dr["MINISTERIO"];
                vObj.Servicio = (p_Dr["SERVICIO"] is DBNull) ? null : (String)p_Dr["SERVICIO"];
                vObj.Nombre = (p_Dr["NOMBRE"] is DBNull) ? null : (String)p_Dr["NOMBRE"];
                vObj.Tipo = (p_Dr["TIPO_FORMULARIO"] is DBNull) ? null : (String)p_Dr["TIPO_FORMULARIO"];
                vObj.Version = (p_Dr["VERSION"] is DBNull) ? null : (String)p_Dr["VERSION"];
                vObj.FecSolicitudEval = (p_Dr["fec_solicitud_eval"] is DBNull) ? null : (String)p_Dr["fec_solicitud_eval"];
                vObj.FecAsigEval1 = (p_Dr["fec_asig_eval_1"] is DBNull) ? null : (String)p_Dr["fec_asig_eval_1"];
                vObj.FecAsigEval2 = (p_Dr["fec_asig_eval_2"] is DBNull) ? null : (String)p_Dr["fec_asig_eval_2"];
                vObj.FecEnvioSect = (p_Dr["fec_envio_sectorialista"] is DBNull) ? null : (String)p_Dr["fec_envio_sectorialista"];
                vObj.FecEnvioComentSect = (p_Dr["fec_envio_coment_sect"] is DBNull) ? null : (String)p_Dr["fec_envio_coment_sect"];
                vObj.FecCorreccion = (p_Dr["fec_eval_corregida"] is DBNull) ? null : (String)p_Dr["fec_eval_corregida"];                
                vObj.FecComentMonitoreo = (p_Dr["fec_coment_monitoreo"] is DBNull) ? null : (String)p_Dr["fec_coment_monitoreo"];
                vObj.FecComentEstudios = (p_Dr["fec_coment_estudios"] is DBNull) ? null : (String)p_Dr["fec_coment_estudios"];
                vObj.FecEvalFinalizada = (p_Dr["fec_eval_finalizada"] is DBNull) ? null : (String)p_Dr["fec_eval_finalizada"];
                vObj.Calificacion = (p_Dr["calificacion"] is DBNull) ? null : (String)p_Dr["calificacion"];
                vObj.TotalDiasIteracion = (p_Dr["total_dias_iteracion"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["total_dias_iteracion"];
                vObj.IdEncriptado = EncriptaDato.RijndaelSimple.Encriptar(vObj.IdPrograma.ToString());
                vObj.PuntajeFinal = (p_Dr["puntaje_final"] is DBNull) ? null : (String)p_Dr["puntaje_final"];
                vObj.NombreEvaluador1 = (p_Dr["evaluador_1"] is DBNull) ? null : (String)p_Dr["evaluador_1"];
                vObj.NombreEvaluador2 = (p_Dr["evaluador_2"] is DBNull) ? null : (String)p_Dr["evaluador_2"];
                vObj.EtapaDesc = (p_Dr["ETAPA"] is DBNull) ? null : (String)p_Dr["ETAPA"];
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR_CREATE_DTO", ex);
            }
            return vObj;
        }

        /// <summary>
        /// Metodo que realiza conversion del registro obtenido a dto
        /// </summary>
        /// <param name="p_Dr">Registro con informacion del objeto PROGRAMAS obtenido</param>
        /// <returns>dto conformado en base a la informacion entregada</returns>
        private TablaProgramasDto CreatePanelCargaRIS(IDataReader p_Dr)
        {
            TablaProgramasDto vObj = new TablaProgramasDto();
            try
            {
                vObj.IdPrograma = (p_Dr["ID_PROGRAMA"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_PROGRAMA"];
                vObj.IdBips = (p_Dr["ID_BIPS"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_BIPS"];
                vObj.IdMinisterio.IdParametro = (p_Dr["ID_MINISTERIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_MINISTERIO"];                
                vObj.Ministerio = (p_Dr["MINISTERIO"] is DBNull) ? null : (String)p_Dr["MINISTERIO"];
                vObj.IdServicio.IdParametro = (p_Dr["ID_SERVICIO"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["ID_SERVICIO"];
                vObj.Servicio = (p_Dr["SERVICIO"] is DBNull) ? null : (String)p_Dr["SERVICIO"];
                vObj.Nombre = (p_Dr["NOMBRE"] is DBNull) ? null : (String)p_Dr["NOMBRE"];                
                vObj.Version = (p_Dr["VERSION"] is DBNull) ? null : (String)p_Dr["VERSION"];
                vObj.FecSolicitudEval = (p_Dr["FECHA_CARGA"] is DBNull) ? null : (String)p_Dr["FECHA_CARGA"];
                if (vObj.IdPrograma != null)
                    vObj.IdEncriptado = EncriptaDato.RijndaelSimple.Encriptar(vObj.IdPrograma.ToString());
                vObj.Tomado = (p_Dr["MARCA_RIS"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["MARCA_RIS"];
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR_CREATE_DTO", ex);
            }
            return vObj;
        }

        /// <summary>
        /// Metodo que realiza conversion del registro obtenido a dto
        /// </summary>
        /// <param name="p_Dr">Registro con informacion del objeto PROGRAMAS obtenido</param>
        /// <returns>dto conformado en base a la informacion entregada</returns>
        private TablaProgramasDto CreateProgramasIteracion(IDataReader p_Dr)
        {
            TablaProgramasDto vObj = new TablaProgramasDto();
            try
            {
                vObj.IdPrograma = (p_Dr["VERSION"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["VERSION"];
                vObj.IdBips = (p_Dr["TOTAL"] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr["TOTAL"];                
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR_CREATE_DTO", ex);
            }
            return vObj;
        }

        /// <summary>
        /// Metodo que realiza conversion del registro obtenido a dto
        /// </summary>
        /// <param name="p_Dr">Registro con informacion del objeto PROGRAMAS obtenido</param>
        /// <returns>dto conformado en base a la informacion entregada</returns>
        private TablaProgramasDto CreateIndicadoresDashboard(IDataReader p_Dr, string tipo)
        {
            TablaProgramasDto vObj = new TablaProgramasDto();
            try
            {
                string campo_1 = string.Empty;
                string campo_2 = string.Empty;
                string campo_3 = string.Empty;
                string campo_4 = string.Empty;
                string campo_5 = string.Empty;
                string campo_6 = string.Empty;
                string campo_7 = string.Empty;
                string campo_8 = string.Empty;
                string campo_9 = string.Empty;
                switch (tipo)
                {
                    case "1":
                        campo_1 = "prog_ingresados";
                        campo_2 = "prog_evaluados";
                        campo_3 = "prog_en_evaluacion";
                        campo_4 = "prom_dias_prog_eval";
                        campo_5 = "total_iteraciones";
                        campo_6 = "promedio_puntaje";
                        campo_7 = "promedio_atingencia";
                        campo_8 = "promedio_coherencia";
                        campo_9 = "promedio_consistencia";
                        break;
                    case "2":
                        campo_1 = "ID_PARAMETRO";
                        campo_2 = "tipo_programa";
                        campo_3 = "total";
                        break;
                    case "3":
                        campo_1 = "ID_PARAMETRO";
                        campo_2 = "CALIFICACION";
                        campo_3 = "total";
                        break;
                    case "4":
                        campo_1 = "ID_PARAMETRO";
                        campo_2 = "ETAPA";
                        campo_3 = "total";
                        break;
                }
                vObj.IdPrograma = (p_Dr[campo_1] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr[campo_1];
                if (tipo == "1")
                {
                    vObj.IdBips = (p_Dr[campo_2] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr[campo_2];
                    vObj.IdEstado = (p_Dr[campo_4] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr[campo_4];
                    vObj.IdGrupoFormulario = (p_Dr[campo_5] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr[campo_5];
                    vObj.IdExcepcion = (p_Dr[campo_6] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr[campo_6];
                    vObj.IdPerfil = (p_Dr[campo_7] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr[campo_7];
                    vObj.IdPlataforma = (p_Dr[campo_8] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr[campo_8];
                    vObj.IdTipoFormulario = (p_Dr[campo_9] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr[campo_9];
                }
                else {
                    vObj.Tipo = (p_Dr[campo_2] is DBNull) ? null : (String)p_Dr[campo_2];
                }
                vObj.IdEtapa = (p_Dr[campo_3] is DBNull) ? (Nullable<Decimal>)null : (Nullable<Decimal>)p_Dr[campo_3];
            }
            catch (Exception ex)
            {
                throw new Exception("ERROR_CREATE_DTO", ex);
            }
            return vObj;
        }

        /// <summary>
        /// Metodo que realiza conversion de los registros obtenidos a un array de dto
        /// </summary>
        /// <param name="p_Dr">informacion de las tareas obtenidas</param>
        /// <param name="p_Programas">objeto en el cual se cargara la informacion</param>
        /// <returns>view dto conformado en base a la informacion entregada</returns>
        private void CreateViewDtoExAnte(OracleDataReader p_Dr, ref ViewDto<TablaProgramasDto> p_Programas)
        {
            FieldInfo fi = p_Dr.GetType().GetField("m_rowSize", BindingFlags.Instance | BindingFlags.NonPublic);
            int rowsize = Convert.ToInt32(fi.GetValue(p_Dr));
            p_Dr.FetchSize = rowsize * 100;
            List<TablaProgramasDto> listDto = new List<TablaProgramasDto>();
            if (p_Dr != null)
            {
                while (p_Dr.Read())
                {
                    listDto.Add(CreateExAnte(p_Dr));
                }
            }
            if (listDto.Count > 0)
            {
                p_Programas = new ViewDto<TablaProgramasDto>(listDto);
            }
        }

        /// <summary>
        /// Metodo que realiza conversion de los registros obtenidos a un array de dto
        /// </summary>
        /// <param name="p_Dr">informacion de las tareas obtenidas</param>
        /// <param name="p_Programas">objeto en el cual se cargara la informacion</param>
        /// <returns>view dto conformado en base a la informacion entregada</returns>
        private void CreateViewDtoExAntePerfil(OracleDataReader p_Dr, ref ViewDto<TablaProgramasDto> p_Programas)
        {
            FieldInfo fi = p_Dr.GetType().GetField("m_rowSize", BindingFlags.Instance | BindingFlags.NonPublic);
            int rowsize = Convert.ToInt32(fi.GetValue(p_Dr));
            p_Dr.FetchSize = rowsize * 100;
            List<TablaProgramasDto> listDto = new List<TablaProgramasDto>();
            if (p_Dr != null)
            {
                while (p_Dr.Read())
                {
                    listDto.Add(CreateExAntePerfil(p_Dr));
                }
            }
            if (listDto.Count > 0)
            {
                p_Programas = new ViewDto<TablaProgramasDto>(listDto);
            }
        }

        /// <summary>
        /// Metodo que realiza conversion de los registros obtenidos a un array de dto
        /// </summary>
        /// <param name="p_Dr">informacion de las tareas obtenidas</param>
        /// <param name="p_Programas">objeto en el cual se cargara la informacion</param>
        /// <returns>view dto conformado en base a la informacion entregada</returns>
        private void CreateViewDtoPanelExAnte(OracleDataReader p_Dr, ref ViewDto<TablaProgramasDto> p_Programas)
        {
            FieldInfo fi = p_Dr.GetType().GetField("m_rowSize", BindingFlags.Instance | BindingFlags.NonPublic);
            int rowsize = Convert.ToInt32(fi.GetValue(p_Dr));
            p_Dr.FetchSize = rowsize * 100;
            List<TablaProgramasDto> listDto = new List<TablaProgramasDto>();
            if (p_Dr != null)
            {
                while (p_Dr.Read())
                {
                    listDto.Add(CreatePanelExAnte(p_Dr));
                }
            }
            if (listDto.Count > 0)
            {
                p_Programas = new ViewDto<TablaProgramasDto>(listDto);
            }
        }

        /// <summary>
        /// Metodo que realiza conversion de los registros obtenidos a un array de dto
        /// </summary>
        /// <param name="p_Dr">informacion de las tareas obtenidas</param>
        /// <param name="p_Programas">objeto en el cual se cargara la informacion</param>
        /// <returns>view dto conformado en base a la informacion entregada</returns>
        private void CreateViewDtoPanelCargaRIS(OracleDataReader p_Dr, ref ViewDto<TablaProgramasDto> p_Programas)
        {
            FieldInfo fi = p_Dr.GetType().GetField("m_rowSize", BindingFlags.Instance | BindingFlags.NonPublic);
            int rowsize = Convert.ToInt32(fi.GetValue(p_Dr));
            p_Dr.FetchSize = rowsize * 100;
            List<TablaProgramasDto> listDto = new List<TablaProgramasDto>();
            if (p_Dr != null)
            {
                while (p_Dr.Read())
                {
                    listDto.Add(CreatePanelCargaRIS(p_Dr));
                }
            }
            if (listDto.Count > 0)
            {
                p_Programas = new ViewDto<TablaProgramasDto>(listDto);
            }
        }

        /// <summary>
        /// Metodo que realiza conversion de los registros obtenidos a un array de dto
        /// </summary>
        /// <param name="p_Dr">informacion de las tareas obtenidas</param>
        /// <param name="p_Programas">objeto en el cual se cargara la informacion</param>
        /// <returns>view dto conformado en base a la informacion entregada</returns>
        private void CreateViewDtoProgramasIteracion(OracleDataReader p_Dr, ref ViewDto<TablaProgramasDto> p_Programas)
        {
            FieldInfo fi = p_Dr.GetType().GetField("m_rowSize", BindingFlags.Instance | BindingFlags.NonPublic);
            int rowsize = Convert.ToInt32(fi.GetValue(p_Dr));
            p_Dr.FetchSize = rowsize * 100;
            List<TablaProgramasDto> listDto = new List<TablaProgramasDto>();
            if (p_Dr != null)
            {
                while (p_Dr.Read())
                {
                    listDto.Add(CreateProgramasIteracion(p_Dr));
                }
            }
            if (listDto.Count > 0)
            {
                p_Programas = new ViewDto<TablaProgramasDto>(listDto);
            }
        }

        /// <summary>
        /// Metodo que realiza conversion de los registros obtenidos a un array de dto
        /// </summary>
        /// <param name="p_Dr">informacion de las tareas obtenidas</param>
        /// <param name="p_Programas">objeto en el cual se cargara la informacion</param>
        /// <returns>view dto conformado en base a la informacion entregada</returns>
        private void CreateViewDtoIndicadoresDashboard(OracleDataReader p_Dr, ref ViewDto<TablaProgramasDto> p_Programas, string tipo)
        {
            FieldInfo fi = p_Dr.GetType().GetField("m_rowSize", BindingFlags.Instance | BindingFlags.NonPublic);
            int rowsize = Convert.ToInt32(fi.GetValue(p_Dr));
            p_Dr.FetchSize = rowsize * 100;
            List<TablaProgramasDto> listDto = new List<TablaProgramasDto>();
            if (p_Dr != null)
            {
                while (p_Dr.Read())
                {
                    listDto.Add(CreateIndicadoresDashboard(p_Dr, tipo));
                }
            }
            if (listDto.Count > 0)
            {
                p_Programas = new ViewDto<TablaProgramasDto>(listDto);
            }
        }
        #endregion

        #region metodos publicos
        /// <summary>
        /// metodo de trapaso de data origen-destino
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_RESPUESTAS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaProgramasDto> InsertarOrigenDestino(ContextoDto p_Contexto, TablaProgramasDto p_Datos)
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
                dbCommand.CommandText = "PCKCBPROGRAMAS.prcOrigenDestino";                
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_PLANTILLA", Direction = ParameterDirection.Input, Value = p_Datos.IdTipoFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_FORMULARIO_ORIGEN", Direction = ParameterDirection.Input, Value = p_Datos.IdBips });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_FORMULARIO_DESTINO", Direction = ParameterDirection.Input, Value = p_Datos.IdPrograma });
                dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
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
        /// Metodo de creación de nueva iteración
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_PROGRAMAS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaProgramasDto> CrearIteracion(ContextoDto p_Contexto, TablaProgramasDto p_Datos)
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
                dbCommand.CommandText = "PCKCBPROGRAMAS.prcCreaIteracion";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_FORMULARIO_ORIGEN", Direction = ParameterDirection.Input, Value = p_Datos.IdPrograma });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_VERSION", Direction = ParameterDirection.Input, Value = p_Datos.VersionActual });
                //dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_VERSION", Direction = ParameterDirection.Input, Value = p_Datos.IdBips });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_FORMULARIO", Direction = ParameterDirection.Output });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Datos.TipoFormulario.IdParametro });
                dbCommand.ExecuteNonQuery();
                vCont = decimal.Parse(dbCommand.Parameters["p_ID_FORMULARIO"].Value.ToString());
                if (vCont > 0){
                    p_Datos.IdPrograma = vCont;
                    viewResponse.Dtos = new List<TablaProgramasDto> { p_Datos };
                }
                else {
                    throw new Exception("Error insert PCKCBPROGRAMAS.prcCreaIteracion");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
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
        /// Metodo de busqueda de programas en evaluaciòn ex ante
        /// </summary>
        /// <param name="p_Contexto"></param>
        /// <param name="p_Filtro"></param>
        /// <returns>Dto con programas Ex Ante</returns>
        public ViewDto<TablaProgramasDto> BuscarExAnte(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro)
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
                dbCommand.CommandText = "PCKCBPROGRAMAS.prcProgramasExAnte";                
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ANO", Direction = ParameterDirection.Input, Value = p_Filtro.Ano });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_EVALUADOR", Direction = ParameterDirection.Input, Value = p_Filtro.Nombre });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.RefCursor, ParameterName = "RESULTSCURSOR", Direction = ParameterDirection.Output });
                dr = dbCommand.ExecuteReader();
                CreateViewDtoExAnte(dr, ref viewResponse);
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
        /// metodo de trapaso de data origen-destino
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_RESPUESTAS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        public ViewDto<TablaProgramasDto> CalculoEficiencia(ContextoDto p_Contexto, TablaProgramasDto p_Datos)
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
                dbCommand.CommandText = "PCKCBPROGRAMAS.prcCalculoEficiencia";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ID_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Datos.IdPrograma });
                dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
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
        /// Metodo de busqueda de programas en panel de evaluación ex ante
        /// </summary>
        /// <param name="p_Contexto"></param>
        /// <param name="p_Filtro"></param>
        /// <returns>Dto con programas Ex Ante</returns>
        public ViewDto<TablaProgramasDto> PanelExAnte(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro)
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
                dbCommand.CommandText = "PCKCBPROGRAMAS.prcPanelExAnte";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ANO", Direction = ParameterDirection.Input, Value = p_Filtro.Ano });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO", Direction = ParameterDirection.Input, Value = p_Filtro.TipoFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.RefCursor, ParameterName = "RESULTSCURSOR", Direction = ParameterDirection.Output });
                dr = dbCommand.ExecuteReader();
                CreateViewDtoPanelExAnte(dr, ref viewResponse);
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
        /// Metodo de busqueda de programas en panel de carga de beneficiarios RIS
        /// </summary>
        /// <param name="p_Contexto"></param>
        /// <param name="p_Filtro"></param>
        /// <returns>Dto con programas carga RIS</returns>
        public ViewDto<TablaProgramasDto> CargaRIS(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro)
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
                dbCommand.CommandText = "PCKCBPROGRAMAS.prcPanelCargaBIPS";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ANO", Direction = ParameterDirection.Input, Value = p_Filtro.Ano });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO", Direction = ParameterDirection.Input, Value = p_Filtro.TipoFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_CATEGORIA", Direction = ParameterDirection.Input, Value = p_Filtro.IdPlataforma });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.RefCursor, ParameterName = "RESULTSCURSOR", Direction = ParameterDirection.Output });
                dr = dbCommand.ExecuteReader();
                CreateViewDtoPanelCargaRIS(dr, ref viewResponse);
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
        /// Metodo de carga de indicadores dashboard
        /// </summary>
        /// <param name="p_Contexto"></param>
        /// <param name="p_Filtro"></param>
        /// <returns>Dto con indicadores dashboard</returns>
        public ViewDto<TablaProgramasDto> IndicadoresDashboard(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro)
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
                string procedimiento = string.Empty;
                switch (p_Contexto.Login)
                {
                    case "1":
                        procedimiento = "PCKCBPROGRAMAS.prcPanelDashboard";
                        break;
                    case "2":
                        procedimiento = "PCKCBPROGRAMAS.prcTipoProgramasExAnte";
                        break;
                    case "3":
                        procedimiento = "PCKCBPROGRAMAS.prcCalificacionesExAnte";
                        break;
                    case "4":
                        procedimiento = "PCKCBPROGRAMAS.prcEstadoProgramasExAnte";
                        break;
                }
                dbCommand.CommandText = procedimiento;
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ANO", Direction = ParameterDirection.Input, Value = p_Filtro.Ano });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Filtro.TipoFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ESTADO", Direction = ParameterDirection.Input, Value = p_Filtro.Estado });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.RefCursor, ParameterName = "RESULTSCURSOR", Direction = ParameterDirection.Output });
                dr = dbCommand.ExecuteReader();
                CreateViewDtoIndicadoresDashboard(dr, ref viewResponse, p_Contexto.Login);
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
        /// Metodo de busqueda de programas agrupados por iteración
        /// </summary>
        /// <param name="p_Contexto"></param>
        /// <param name="p_Filtro"></param>
        /// <returns>Dto con programas carga RIS</returns>
        public ViewDto<TablaProgramasDto> ProgramasXIteracion(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro)
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
                dbCommand.CommandText = "PCKCBPROGRAMAS.prcProgramasIteracion";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ANO", Direction = ParameterDirection.Input, Value = p_Filtro.Ano });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Filtro.TipoFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.RefCursor, ParameterName = "RESULTSCURSOR", Direction = ParameterDirection.Output });
                dr = dbCommand.ExecuteReader();
                CreateViewDtoProgramasIteracion(dr, ref viewResponse);
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
        /// Metodo de busqueda de programas en evaluación ex ante gore (perfil)
        /// </summary>
        /// <param name="p_Contexto"></param>
        /// <param name="p_Filtro"></param>
        /// <returns>Dto con programas Ex Ante</returns>
        public ViewDto<TablaProgramasDto> BuscarExAnteEvalPerfil(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro)
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
                dbCommand.CommandText = "PCKCBPROGRAMAS.prcProgramasExAntePerfil";
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_ANO", Direction = ParameterDirection.Input, Value = p_Filtro.Ano });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Varchar2, ParameterName = "p_EVALUADOR", Direction = ParameterDirection.Input, Value = p_Filtro.Nombre });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.Decimal, ParameterName = "p_TIPO_FORMULARIO", Direction = ParameterDirection.Input, Value = p_Filtro.TipoFormulario });
                dbCommand.Parameters.Add(new OracleParameter() { OracleDbType = OracleDbType.RefCursor, ParameterName = "RESULTSCURSOR", Direction = ParameterDirection.Output });
                dr = dbCommand.ExecuteReader();
                CreateViewDtoExAntePerfil(dr, ref viewResponse);
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