using MDS.Core.Dto;
using MDS.Dto;

namespace MDS.Dao
{
    public partial interface ITablaExcepcionesPreguntasDao
    {
        /// <summary>
		/// metodo que permite buscar los registros de CB_EXCEPCIONES_PLANTILLAS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaProgramasDto> BuscarPermisosFormularios(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro);

        /// <summary>
		/// metodo que permite buscar los registros de CB_EXCEPCIONES_PLANTILLAS existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaExcepcionesPlantillasDto> BuscarPlantillasFormularios(ContextoDto p_Contexto, TablaExcepcionesPlantillasFiltroDto p_Filtro);

        /// <summary>
        /// metodo que permite crear un nuevo registro de CB_EXCEPCIONES_FORMULARIOS
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_EXCEPCIONES_FORMULARIOS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaExcepcionesPermisosDto> Insertar(ContextoDto p_Contexto, TablaExcepcionesPermisosDto p_Datos);

        /// <summary>
		/// metodo que permite eliminar un registro de CB_EXCEPCIONES_FORMULARIOS
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Datos">usuario a eliminar</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaExcepcionesPermisosDto> EliminarPermisosUsuarios(ContextoDto p_Contexto, TablaExcepcionesPermisosDto p_Datos);

        /// <summary>
        /// metodo que permite crear un nuevo registro de CB_EXCEPCIONES_PLANTILLAS
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_EXCEPCIONES_PLANTILLAS a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaExcepcionesPlantillasDto> InsertarPlantilla(ContextoDto p_Contexto, TablaExcepcionesPlantillasDto p_Datos);

        /// <summary>
        /// metodo que permite crear un nuevo registro de CB_EXCEPCIONES_PLANTILLAS_FORM
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_EXCEPCIONES_PLANTILLAS_FORM a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaExcepcionesPlantillasFormDto> InsertarPlantillaForm(ContextoDto p_Contexto, TablaExcepcionesPlantillasFormDto p_Datos);

        /// <summary>
        /// metodo que permite crear un nuevo registro de CB_EXCEPCIONES_PLANTILLAS_FORM
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Datos">CB_EXCEPCIONES_PLANTILLAS_FORM a crear</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
        ViewDto<TablaExcepcionesPlantillasFormDto> BuscarExcepciones(ContextoDto p_Contexto, TablaExcepcionesPlantillasFormDto p_Datos);
    }
}
