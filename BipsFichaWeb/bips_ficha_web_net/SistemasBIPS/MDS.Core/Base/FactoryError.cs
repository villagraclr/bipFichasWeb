using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using MDS.Core.Dto;
using MDS.Core.Enum;

namespace MDS.Core.Base
{
    /// <summary>
    /// clase encargada de gestionar el proceso de obtencion de errores parametrizables del sistema
    /// </summary>
    internal class FactoryError
    {
        #region campo estaticos privados
        private static FactoryError _instance = null;
        #endregion

        #region campos privados
        private Dictionary<String, ErrorDto> errores = null;
        #endregion

        #region constructor
        private FactoryError()
        {
            errores = new Dictionary<string, ErrorDto>();
            ErrorConfigurationReader configurationReader = new ErrorConfigurationReader();
            configurationReader.ReadConfiguration(errores);
        }
        #endregion

        #region singleton
        /// <summary>
        /// metodo que permite obtener una instancia del objeto en memoria
        /// </summary>
        /// <returns></returns>
        internal static FactoryError GetInstance()
        {
            if (_instance != null && ErrorConfigurationReader.IsModify())
                _instance = null;

            if (_instance == null)
                _instance = new FactoryError();
            return _instance;
        }
        #endregion

        #region metodos privados
        /// <summary>
        /// metodo que permite controlar un error. Se registra y se encapsula en un objeto ErrorDTO
        /// </summary>
        /// <param name="p_Error">parametro que contiene el codigo del error que se desea controlar</param>
        /// <returns>retorna un objeto del tipo ErrorDTO con la información del error traducido</returns>
        private void LogError(ErrorDto p_Error)
        {
            Factory.Logs.LogInfo(string.Format("Se ha producido el error codigo {1}. {0}", p_Error.Mensaje, p_Error.Codigo));
        }

        /// <summary>
        /// metodo que permite recopilar la informacion incluida en la excepcion capturada
        /// </summary>
        /// <param name="p_Ex">Excepcion capturada</param>
        /// <returns>Mensajes de error encontrados</returns>
        private string GetExMsg(Exception p_Ex)
        {
            string msgError = " - " + p_Ex.Message + "\n";
            if (p_Ex.InnerException != null)
            {
                msgError += GetExMsg(p_Ex.InnerException);
            }
            return msgError;
        }
        #endregion

        #region metodos internos
        /// <summary>
        /// metodo que permite controlar una exception. Se registra y se encapsula en un objeto ErrorDTO
        /// </summary>
        /// <param name="p_Ex">parametro que contiene la exception que se desea controlar</param>
        /// <returns>retorna un objeto del tipo ErrorDTO con la información del error traducido</returns>
        internal ErrorDto HandleException(Exception p_Ex)
        {
            ErrorDto dtoError = new ErrorDto();
            Factory.Exceptiones.HandleException(p_Ex);

            if (p_Ex.Message == "CONNECTION_DATABASE_EXCEPTION")
                dtoError = GetError(EnumErrores.DAL_ERROR_CONEXION_BBDD);
            else if (p_Ex.Message == "ERROR_CREATE_DTO")
                dtoError = GetError(EnumErrores.CORE_ERROR_CREACION_DTO);
            else if (p_Ex.Message == "LOAD_FILE_ERROR_EXCEPTION")
                dtoError = GetError(EnumErrores.CORE_ERROR_CARGA_INSTANCES);
            else if (p_Ex.Message == "HANDLE_ERROR_EXCEPTION")
                dtoError = GetError(EnumErrores.BLL_ERROR_GENERICO_PROCESO);
            else if (p_Ex.Message == "PARAMETERS_NULL_OR_EMPTY" || p_Ex is NullReferenceException)
                dtoError = GetError(EnumErrores.CORE_PARAM_VACIO);
            else if (p_Ex is SqlException)
                dtoError = GetError(EnumErrores.DAL_SENTENCIA_INCORRECTA);
            else if (p_Ex.Message.Equals("LOAD_FILE_CONSTANTE_EXCEPTION"))
                dtoError = GetError(EnumErrores.CORE_ERROR_CARGA_CONSTANTES);
            else
                dtoError = GetError(EnumErrores.BLL_ERROR_GENERICO_PROCESO);

            dtoError.Detalle = GetExMsg(p_Ex);
            dtoError.Mensaje = p_Ex.ToString();
            //LogError(dtoError);
            return dtoError;
        }

        /// <summary>
        /// metodo que permite obtener el error asociado a una enumeracion
        /// </summary>
        /// <param name="p_Key">clave del error a obtener</param>
        /// <returns>error requerido</returns>
        internal ErrorDto GetError(string p_Key)
        {
            try
            {
                if (p_Key == null || p_Key.Trim().Length == 0)
                    throw new ArgumentNullException("null", string.Format("{0} - Debe indicar una clave valida existente en el diccionario interno", "FACTORY_GET_ERROR_EMPTY_TYPE"));
                if (!errores.ContainsKey(p_Key))
                    throw new ArgumentNullException(p_Key, string.Format("{0} - No se encuentra la clave {1} en el diccionario interno", "FACTORY_GET_ERROR_WRONG_TYPE", p_Key));
                ErrorDto dtoError = errores[p_Key];
                //LogError(dtoError);
                return dtoError;
            }
            catch (Exception ex)
            {
                Factory.Exceptiones.HandleException(ex);
                throw;
            }
        }

        /// <summary>
        /// metodo que permite obtener el error asociado a una enumeracion
        /// </summary>
        /// <param name="p_Key">clave del error a obtener</param>
        /// <returns>error requerido</returns>
        internal ErrorDto GetError(EnumErrores p_Key)
        {
            try
            {
                if (!errores.ContainsKey(p_Key.ToString()))
                    throw new ArgumentNullException(p_Key.ToString(), string.Format("{0} - No se encuentra la clave {1} en el diccionario interno", "FACTORY_GET_ERROR_WRONG_TYPE", p_Key));
                ErrorDto dtoError = errores[p_Key.ToString()];
                //LogError(dtoError);
                return dtoError;
            }
            catch (Exception ex)
            {
                Factory.Exceptiones.HandleException(ex);
                throw;
            }
        }
        #endregion
    }
}