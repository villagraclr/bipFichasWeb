using System.Runtime.Serialization;

namespace MDS.Core.Enum
{
    /// <summary>
    /// enumeracion que contiene los mensajes predefinidos a registrar en los logs del sistema
    /// </summary>
    [DataContract]
    public enum EnumMessagesEntry : short
    {
        /// <summary>
        /// entrada por defecto para registrar el inicio de un metodo
        /// </summary>
        [EnumMemberAttribute]
        Inicio = 0,
        /// <summary>
        /// entrada para registrar el termino de un metodo
        /// </summary>
        [EnumMemberAttribute]
        Termino = 1,
        /// <summary>
        /// entrada para registrar el exito de una conexion a base de datos
        /// </summary>
        [EnumMemberAttribute]
        Conexion_Ok = 2,
        /// <summary>
        /// entrada para registrar la finalizacion de un metodo
        /// </summary>
        [EnumMemberAttribute]
        Ejecucion_Ok = 3
    }
}
