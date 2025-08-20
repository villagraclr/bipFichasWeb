using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace MDS.Core.Dto
{
    /// <summary>
    ///	Clase representativa del contexto de las operaciones
    /// </summary>
    [DataContract]
    public class ContextoDto : ICloneable
    {
        #region campos privados
        private String login;
        #endregion

        #region propiedades publicas
        /// <summary>
        /// nombre de usuario de quien realiza la peticion
        /// </summary>
        [DataMember]
        public String Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
            }
        }
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        [DebuggerStepThrough]
        public ContextoDto()
        {
        }
        #endregion

        #region metodos publicos

        #region ICloneable Members
        /// <summary>
        /// metodo que permite crear una copia de la actual instancia en memoria
        /// </summary>
        /// <returns>una copia del objeto existente en memoria</returns>
        public Object Clone()
        {
            ContextoDto objDto = (ContextoDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion Clone Method
        #endregion
    }
}
