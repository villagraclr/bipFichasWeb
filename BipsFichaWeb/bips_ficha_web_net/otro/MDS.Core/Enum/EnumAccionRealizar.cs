using System.Runtime.Serialization;

namespace MDS.Core.Enum
{
    /// <summary>
    /// enumeracion que permite identificar las acciones a realizar sobre los objetos
    /// </summary>
    [DataContract]
    public enum EnumAccionRealizar : short
    {
        /// <summary>
        /// accion por defecto del objeto
        /// </summary>
        [EnumMemberAttribute]
        Ninguna = 0,
        /// <summary>
        /// accion que determina la insercion en el origen de datos del objeto
        /// </summary>
        [EnumMemberAttribute]
        Insertar = 1,
        /// <summary>
        /// accion que determina la actualizacion en el origen de datos del objeto
        /// </summary>
        [EnumMemberAttribute]
        Actualizar = 2,
        /// <summary>
        /// accion que determina la eliminacion en el origen de datos del objeto
        /// </summary>
        [EnumMemberAttribute]
        Eliminar = 3,
        /// <summary>
        /// accion que determina la busqueda normal en el origen de datos del objeto
        /// </summary>
        [EnumMemberAttribute]
        Buscar = 4,
        /// <summary>
        /// accion que determina la busqueda de años en el origen de datos del objeto
        /// </summary>
        [EnumMemberAttribute]
        BuscarAnos = 5,
        /// <summary>
        /// accion que determina la eliminacion de un usuario perteneciente a un grupo en el origen de datos del objeto
        /// </summary>
        [EnumMemberAttribute]
        EliminarUserGrupo = 6,
        /// <summary>
        /// accion que determina el cambio de etapa de una oferta social en el origen de datos del objeto
        /// </summary>
        [EnumMemberAttribute]
        CambiarEtapa = 7,
        /// <summary>
        /// accion que determina la busqueda de programas ex ante en el origen de datos del objeto
        /// </summary>
        [EnumMemberAttribute]
        BuscarExAnte = 8,
        /// <summary>
        /// accion que determina la ejecucion del calculo de eficiencia
        /// </summary>
        [EnumMemberAttribute]
        EjecutarCalculoEficiencia = 9,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña antecedentes
        /// </summary>
        [EnumMemberAttribute]
        BuscarAntecedentes = 1,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña diagnostico
        /// </summary>
        [EnumMemberAttribute]
        BuscarDiagnostico = 2,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña evaluaciones anteriores
        /// </summary>
        [EnumMemberAttribute]
        BuscarEvalAnteriores = 3,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña poblacion potencial
        /// </summary>
        [EnumMemberAttribute]
        BuscarPobPotencial = 4,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña poblacion objetivo
        /// </summary>
        [EnumMemberAttribute]
        BuscarPobObjetivo = 5,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña poblacion beneficiada
        /// </summary>
        [EnumMemberAttribute]
        BuscarPobBeneficiada = 6,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña estrategia
        /// </summary>
        [EnumMemberAttribute]
        BuscarEstrategia = 7,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña covid-19
        /// </summary>
        [EnumMemberAttribute]
        BuscarCovid19 = 8,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña ejecutores
        /// </summary>
        [EnumMemberAttribute]
        BuscarEjecutores = 9,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña presupuesto
        /// </summary>
        [EnumMemberAttribute]
        BuscarPresupuesto = 10,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña recursos ejecutados
        /// </summary>
        [EnumMemberAttribute]
        BuscarRecursosEjec = 11,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña gasto extrapresupuestario
        /// </summary>
        [EnumMemberAttribute]
        BuscarGastoExtra = 12,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña gasto fet
        /// </summary>
        [EnumMemberAttribute]
        BuscarGastoFet = 13,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña gastos componentes
        /// </summary>
        [EnumMemberAttribute]
        BuscarGastosComp = 14,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña detalle regionales gastos componentes
        /// </summary>
        [EnumMemberAttribute]
        BuscarDetRegGastosComp = 15,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña gastos administrativos
        /// </summary>
        [EnumMemberAttribute]
        BuscarGastosAdmin = 16,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña resumen recursos ejecutados
        /// </summary>
        [EnumMemberAttribute]
        BuscarResumenRecEjec = 17,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña indicadores proposito
        /// </summary>
        [EnumMemberAttribute]
        BuscarIndicProp = 18,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña indicadores complementarios
        /// </summary>
        [EnumMemberAttribute]
        BuscarIndicComp = 19,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña enfoque derechos humanos
        /// </summary>
        [EnumMemberAttribute]
        BuscarDDHH = 20,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña ods
        /// </summary>
        [EnumMemberAttribute]
        BuscarODS = 21,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña pobreza multidimensional
        /// </summary>
        [EnumMemberAttribute]
        BuscarPobMulti = 22,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña diseño
        /// </summary>
        [EnumMemberAttribute]
        BuscarDiseño = 23,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña poblacion
        /// </summary>
        [EnumMemberAttribute]
        BuscarPoblacion = 24,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña poblacion
        /// </summary>
        [EnumMemberAttribute]
        BuscarObsEstrategia = 25,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña poblacion
        /// </summary>
        [EnumMemberAttribute]
        BuscarObsIndic = 26,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña poblacion
        /// </summary>
        [EnumMemberAttribute]
        BuscarObsPresupuesto = 27,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña poblacion
        /// </summary>
        [EnumMemberAttribute]
        BuscarObsGenerales = 28,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña poblacion
        /// </summary>
        [EnumMemberAttribute]
        BuscarOfertaPublica = 29,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña poblacion
        /// </summary>
        [EnumMemberAttribute]
        BuscarCicloVida = 30,
        /// <summary>
        /// accion que determina la busqueda de datos de la pestaña poblacion
        /// </summary>
        [EnumMemberAttribute]
        BuscarGruposDest = 31,
        /// <summary>
        /// accion que determina la busqueda de programas para panel de ex ante en el origen de datos del objeto
        /// </summary>
        [EnumMemberAttribute]
        BuscarPanelExAnte = 32,
        /// <summary>
        /// accion que determina la busqueda de programas para panel de carga de beneficiarios RIS en el origen de datos del objeto
        /// </summary>
        [EnumMemberAttribute]
        BuscarPanelCargaRIS = 33,
        /// <summary>
        /// accion que determina la busqueda de indicadores para el dashboard en el origen de datos del objeto
        /// </summary>
        [EnumMemberAttribute]
        BuscarIndicDashboard = 34,
        /// <summary>
        /// accion que determina la busqueda de programas por iteracion en el origen de datos del objeto
        /// </summary>
        [EnumMemberAttribute]
        BuscarProgramasXIteracion = 35,
        /// <summary>
        /// accion que determina la busqueda de años en el origen de datos del objeto
        /// </summary>
        [EnumMemberAttribute]
        BuscarAnosGores = 36,
        /// <summary>
        /// accion que determina la busqueda de años en el origen de datos del objeto
        /// </summary>
        [EnumMemberAttribute]
        BuscarExAntePerfil = 37
    }
}
