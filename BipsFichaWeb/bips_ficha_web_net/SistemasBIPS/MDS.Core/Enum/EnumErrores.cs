using System.Runtime.Serialization;

namespace MDS.Core.Enum
{
    /// <summary>
    /// enumeracion que permite identificar los errores del sistema a traves de su identificador
    /// </summary>
    [DataContract]
    public enum EnumErrores : short
    {
        /// <summary>
        /// error base de conexion de base de datos
        /// </summary>
        [EnumMemberAttribute]
        DAL_ERROR_CONEXION_BBDD = 0,
        /// <summary>
        /// error base de carga de instancias
        /// </summary>
        [EnumMemberAttribute]
        CORE_ERROR_CARGA_INSTANCES = 1,
        /// <summary>
        /// error de parametro vacio
        /// </summary>
        [EnumMemberAttribute]
        CORE_PARAM_VACIO = 2,
        /// <summary>
        /// error de instancia nula
        /// </summary>
        [EnumMemberAttribute]
        CORE_INSTANCIA_NULA = 3,
        /// <summary>
        /// sentencia de acceso a datos incorrecta
        /// </summary>
        [EnumMemberAttribute]
        DAL_SENTENCIA_INCORRECTA = 4,
        /// <summary>
        /// error base de carga de constantes
        /// </summary>
        [EnumMemberAttribute]
        CORE_ERROR_CARGA_CONSTANTES = 5,
        /// <summary>
        /// mensaje de error para consulta a base de datos sin retorno de informacion
        /// </summary>
        [EnumMemberAttribute]
        BLL_CONSULTA_SIN_DATOS = 6,
        /// <summary>
        /// error generico de proceso de negocio
        /// </summary>
        [EnumMemberAttribute]
        BLL_ERROR_GENERICO_PROCESO = 7,
        /// <summary>
        /// error generico de proceso de negocio que recibe informacion no valida
        /// </summary>
        [EnumMemberAttribute]
        BLL_ERROR_INFORMACION_NO_VALIDA = 8,
        /// <summary>
        /// error generico de proceso de acceso a datos al convertir un registro en dto
        /// </summary>
        [EnumMemberAttribute]
        CORE_ERROR_CREACION_DTO = 9,
        /// <summary>
        /// error de insercion sin codigo indicado por usuario
        /// </summary>
        [EnumMemberAttribute]
        BLL_INSERCION_SIN_CODIGO = 10,
        /// <summary>
        /// error de insercion sin nombre indicado por usuario
        /// </summary>
        [EnumMemberAttribute]
        BLL_INSERCION_SIN_NOMBRE = 11,
        /// <summary>
        /// error de codigo duplicado
        /// </summary>
        [EnumMemberAttribute]
        BLL_CODIGO_DUPLICADO = 12,
        /// <summary>
        /// error de insercion sin descripcion indicado por usuario
        /// </summary>
        [EnumMemberAttribute]
        BLL_INSERCION_SIN_DESCRIPCION = 13,
        /// <summary>
        /// error de descripcion duplicado
        /// </summary>
        [EnumMemberAttribute]
        BLL_DESCRIPCION_DUPLICADA = 14,
        /// <summary>
        /// error de nombre duplicado
        /// </summary>
        [EnumMemberAttribute]
        BLL_NOMBRE_DUPLICADO = 15,
        /// <summary>
        /// error de insercion sin nombre indicado por usuario
        /// </summary>
        [EnumMemberAttribute]
        BLL_INSERCION_SIN_URL = 16,
        /// <summary>
        /// error de insercion de informacion duplicada
        /// </summary>
        [EnumMemberAttribute]
        BLL_REGISTRO_DUPLICADO = 17,
        /// <summary>
        /// error de insercion sin usuario
        /// </summary>
        [EnumMemberAttribute]
        BLL_REGISTRO_SIN_USUARIO = 18,
        /// <summary>
        /// error de insercion sin sistema
        /// </summary>
        [EnumMemberAttribute]
        BLL_REGISTRO_SIN_SISTEMA = 19,
        /// <summary>
        /// error de insercion, usuario no existente
        /// </summary>
        [EnumMemberAttribute]
        BLL_REGISTRO_USUARIO_INEXISTENTE = 20,
        /// <summary>
        /// error de insercion, sistema no existente
        /// </summary>
        [EnumMemberAttribute]
        BLL_REGISTRO_SISTEMA_INEXISTENTE = 21,
        /// <summary>
        /// error de insercion, usuario no activo
        /// </summary>
        [EnumMemberAttribute]
        BLL_REGISTRO_USUARIO_INACTIVO = 22,
        /// <summary>
        /// error de insercion, sistema no activo
        /// </summary>
        [EnumMemberAttribute]
        BLL_REGISTRO_SISTEMA_INACTIVO = 23,
        /// <summary>
        /// error de busqueda, usuario no existente
        /// </summary>
        [EnumMemberAttribute]
        BLL_USUARIO_AD_INEXISTENTE = 24,
        /// <summary>
        /// error de busqueda, usuario no activo
        /// </summary>
        [EnumMemberAttribute]
        BLL_USUARIO_AD_INACTIVO = 25,
        /// <summary>
        /// error de registro sin rol requerido
        /// </summary>
        [EnumMemberAttribute]
        BLL_REGISTRO_SIN_ROL = 26,
        /// <summary>
        /// error de registro sin funciones especificadas
        /// </summary>
        [EnumMemberAttribute]
        BLL_REGISTRO_SIN_FUNCIONES = 27,
        /// <summary>
        /// error de registro con rol no existente
        /// </summary>
        [EnumMemberAttribute]
        BLL_REGISTRO_ROL_INEXISTENTE = 28,
        /// <summary>
        /// error de registro con rol no activo
        /// </summary>
        [EnumMemberAttribute]
        BLL_REGISTRO_ROL_INACTIVO = 29,
        /// <summary>
        /// error de registro con funcion no existente
        /// </summary>
        [EnumMemberAttribute]
        BLL_REGISTRO_FUNCION_INEXISTENTE = 30,
        /// <summary>
        /// error de registro con funcion no activo
        /// </summary>
        [EnumMemberAttribute]
        BLL_REGISTRO_FUNCION_INACTIVO = 32,
        /// <summary>
        /// error de registro con rolfuncion no existente
        /// </summary>
        [EnumMemberAttribute]
        BLL_REGISTRO_ROLFUNCION_INEXISTENTE = 33,
        /// <summary>
        /// error de registro con rolfuncion no existente
        /// </summary>
        [EnumMemberAttribute]
        BLL_REGISTRO_CERTIFICADO_PATHREPORTES_INEXISTENTE = 34
    }
}
