using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;

namespace MDS.Logic.Dom
{
    public partial interface ITablaExcepcionesPreguntasDom
    {
        /// <summary>
		/// metodo que permite buscar los elementos de tipo cb_excepciones_plantillas existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaProgramasDto> BuscarPermisosFormularios(ContextoDto p_Contexto, TablaProgramasFiltroDto p_Filtro);

        /// <summary>
		/// metodo que permite buscar los elementos de tipo cb_excepciones_plantillas existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaExcepcionesPlantillasDto> BuscarPlantillasFormularios(ContextoDto p_Contexto, TablaExcepcionesPlantillasFiltroDto p_Filtro);

        /// <summary>
		/// metodo que permite registrar elementos de tipo cb_excepciones_formularios en el sistema
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Datos">excepciones formularios a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaExcepcionesPermisosDto> Registrar(ContextoDto p_Contexto, TablaExcepcionesPermisosDto p_Datos, EnumAccionRealizar p_Accion);

        /// <summary>
		/// metodo que permite registrar elementos de tipo cb_excepciones_plantillas en el sistema
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Datos">excepciones formularios a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaExcepcionesPlantillasDto> RegistrarExcepciones(ContextoDto p_Contexto, TablaExcepcionesPlantillasDto p_Datos, EnumAccionRealizar p_Accion);

        /// <summary>
		/// metodo que permite registrar elementos de tipo cb_excepciones_plantillas_form en el sistema
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Datos">excepciones formularios a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaExcepcionesPlantillasFormDto> RegistrarExcepcionesForm(ContextoDto p_Contexto, TablaExcepcionesPlantillasFormDto p_Datos, EnumAccionRealizar p_Accion);

        /// <summary>
		/// metodo que permite buscar los elementos de tipo cb_excepciones_plantillas_form existentes
		/// </summary>
		/// <param name="p_Contexto">informacion del contexto</param>
		/// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
		/// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>
		ViewDto<TablaExcepcionesPlantillasFormDto> BuscarExcepcionesPlantillas(ContextoDto p_Contexto, TablaExcepcionesPlantillasFormDto p_Filtro);
    }
}
