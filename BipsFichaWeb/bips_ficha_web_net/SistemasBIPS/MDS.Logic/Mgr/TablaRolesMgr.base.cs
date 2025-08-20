using System;
using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;
using MDS.Logic.Dom;

namespace MDS.Logic.Mgr
{
    public partial class SistemasBIPSMgr : ITablaRolesMgr
    {
        #region campos publicos
        /// <summary>
        /// campo publico que contiene una instancia del objeto de negocio 
        /// </summary>
        public ITablaRolesDom iTablaRolesDom;
        #endregion        

        #region metodos publicos
        /// <summary>
        /// metodo que permite buscar los elementos de tipo cb_roles existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        public ViewDto<TablaRolesDto> BuscarRoles(ContextoDto p_Contexto, TablaRolesFiltroDto p_Filtro)
        {
            ViewDto<TablaRolesDto> viewResponse = new ViewDto<TablaRolesDto>();
            try
            {
                viewResponse = iTablaRolesDom.Buscar(p_Contexto, p_Filtro);
                if (!viewResponse.Sucess())
                {
                    //TODO: Generar error con especificacion de mensaje de proceso defectuoso
                    iProviderError.LoadError(ref viewResponse, EnumErrores.BLL_ERROR_GENERICO_PROCESO);
                }
                else if (viewResponse.Error.Severidad == EnumSeveridad.Warning)
                {
                    //TODO: Generar error con especificacion de mensaje de datos no encontrados
                    iProviderError.LoadError(ref viewResponse, EnumErrores.BLL_CONSULTA_SIN_DATOS);
                }
            }
            catch (Exception ex)
            {
                iProviderError.LoadError(ref viewResponse, ex);
            }
            return viewResponse;
        }

        /// <summary>
        /// metodo que permite registrar elementos de tipo cb_roles en el sistema
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Roles">cb_roles a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>        
        public ViewDto<TablaRolesDto> RegistrarRoles(ContextoDto p_Contexto, TablaRolesDto p_Roles, EnumAccionRealizar p_Accion)
        {
            ViewDto<TablaRolesDto> viewResponse = new ViewDto<TablaRolesDto>();
            try
            {
                viewResponse = iTablaRolesDom.Registrar(p_Contexto, p_Roles, p_Accion);
                if (!viewResponse.Sucess())
                {
                    //TODO: Generar error con especificacion de mensaje de proceso defectuoso
                    iProviderError.LoadError(ref viewResponse, EnumErrores.BLL_ERROR_GENERICO_PROCESO);
                }
            }
            catch (Exception ex)
            {
                iProviderError.LoadError(ref viewResponse, ex);
            }
            return viewResponse;
        }
        #endregion
    }
}
