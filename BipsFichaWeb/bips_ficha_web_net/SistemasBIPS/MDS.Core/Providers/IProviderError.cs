using MDS.Core.Dto;
using MDS.Core.Enum;
using System;

namespace MDS.Core.Providers
{
    /// <summary>
    /// interfaz de objeto de arquitectura que permite administrar las peticiones de errores de los objetos de negocio
    /// </summary>
    public interface IProviderError
    {
        /// <summary>
        /// metodo que permite obtener el error asociado a una enumeracion
        /// </summary>
        /// <param name="p_Key">clave del error a obtener</param>
        /// <returns>error requerido</returns>
        ErrorDto GetError(string p_Key);
        /// <summary>
        /// metodo que permite obtener el error asociado a una enumeracion
        /// </summary>
        /// <param name="p_Key">clave del error a obtener</param>
        /// <returns>error requerido</returns>
        ErrorDto GetError(EnumErrores p_Key);
        /// <summary>
        /// metodo que permite controlar una exception. Se registra y se encapsula en un objeto ErrorDTO
        /// </summary>
        /// <param name="p_Ex">parametro que contiene la exception que se desea controlar</param>
        /// <returns>retorna un objeto del tipo ErrorDTO con la información del error traducido</returns>
        ErrorDto GetError(Exception p_Ex);
        /// <summary>
        /// metodo que permite establecer el error al objeto indicado
        /// </summary>
        /// <param name="p_ViewDto">objeto que contendra el error</param>
        /// <param name="p_ErrorKey">clave del error a obtener</param>
        /// <returns>error requerido</returns>
        void LoadError<T>(ref ViewDto<T> p_ViewDto, string p_ErrorKey);
        /// <summary>
        /// metodo que permite establecer el error al objeto indicado
        /// </summary>
        /// <param name="p_ViewDto">objeto que contendra el error</param>
        /// <param name="p_ErrorKey">clave del error a obtener</param>
        /// <returns>error requerido</returns>
        void LoadError<T>(ref ViewDto<T> p_ViewDto, EnumErrores p_ErrorKey);
        /// <summary>
        /// metodo que permite establecer el error al objeto indicado
        /// </summary>
        /// <param name="p_ViewDto">objeto que contendra el error</param>
        /// <param name="p_Ex">excepcion a encapsular dentro del objeto error</param>
        /// <returns>error requerido</returns>
        void LoadError<T>(ref ViewDto<T> p_ViewDto, Exception p_Ex);
    }
}