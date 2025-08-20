using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace MDS.Core.Dto
{
    /// <summary>
    /// clase que permite mover la informacion de entre las capas del proyecto
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataContract]
    public class ViewDto<T>
    {
        #region campos privados
        private List<T> _dtos;
        private ErrorDto _error;
        #endregion

        #region propiedades publicas
        /// <summary>
        /// propiedad que almacena el listado de dtos
        /// </summary>
        [DataMember]
        public List<T> Dtos
        {
            get
            {
                return _dtos;
            }
            set
            {
                _dtos = value;
            }
        }

        /// <summary>
        /// propiedad que almacena error producido
        /// </summary>
        [DataMember]
        public ErrorDto Error
        {
            get
            {
                return _error;
            }
            set
            {
                _error = value;
            }
        }
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        [DebuggerStepThrough]
        public ViewDto()
        {
            _error = (ErrorDto)Activator.CreateInstance(typeof(ErrorDto));
        }

        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public ViewDto(T p_Obj)
        {
            this.Dtos = new List<T>();
            this.Dtos.Add(p_Obj);
        }

        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public ViewDto(List<T> p_List)
        {
            this.Dtos = p_List;
        }
        #endregion

        #region metodos publicos
        /// <summary>
        /// metodo que determina si existen elementos en la coleccion
        /// </summary>
        /// <returns>valor boleano que indica existencia de error</returns>
        public bool HasElements()
        {
            bool result = false;
            if (Dtos != null && Dtos.Count > 0)
                result = true;
            return result;
        }

        /// <summary>
        /// metodo que determina si se ha producido un error al obtener la informacion
        /// </summary>
        /// <returns>valor boleano que indica existencia de error</returns>
        public bool HasError()
        {
            bool result = false;
            if (Error != null && Error.Codigo != null)
                result = true;
            return result;
        }

        /// <summary>
        /// metodo que determina si se ha obtenido la informacion exitosamente
        /// </summary>
        /// <returns>valor boleano que indica exito de la operacion realizada</returns>
        public bool Sucess()
        {
            bool result = false;
            if (!HasError())
                result = true;
            return result;
        }
        #endregion
    }
}
