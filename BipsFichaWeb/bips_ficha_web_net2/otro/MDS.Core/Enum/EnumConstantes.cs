using System.Runtime.Serialization;

namespace MDS.Core.Enum
{
    /// <summary>
    /// enumeracion que permite identificar las constantes del sistema a traves de su identificador
    /// </summary>
    [DataContract]
    public enum EnumConstantes : short
    {
        /// <summary>
        /// permite determinar si se debe notificar el evento
        /// </summary>
        [EnumMemberAttribute]
        Notificar = 0,
        /// <summary>
        /// permite determinar si se debe excluir el evento
        /// </summary>
        [EnumMemberAttribute]
        Excluir = 1,
        /// <summary>
        /// permite determinar el valor por defecto
        /// </summary>
        [EnumMemberAttribute]
        ValorPorDefecto = 2
    }
}
