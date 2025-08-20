using System.Runtime.Serialization;

namespace MDS.Core.Enum
{
    /// <summary>
    /// enumeracion que permite identificar las severidades asociables a los errores del sistema
    /// </summary>
    [DataContract]
    public enum EnumSeveridad : byte
    {
        /// <summary>
        /// Severidad que permite catalogar un error como una advertencia
        /// </summary>
        [EnumMemberAttribute]
        Warning = 0,
        /// <summary>
        /// Severidad que permite informar la existencia de un error en la logica esperada del sistema
        /// </summary>
        [EnumMemberAttribute]
        Error = 1,
        /// <summary>
        /// Severidad que permite indicar la existencia de un problema critico que debe ser solucionado a la brevedad
        /// </summary>
        [EnumMemberAttribute]
        Fatal = 2
    }
}
