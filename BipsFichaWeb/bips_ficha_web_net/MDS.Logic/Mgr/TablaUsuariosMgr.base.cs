using System;
using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Dto;
using MDS.Logic.Dom;

namespace MDS.Logic.Mgr
{
    /// <summary>
	///	Clase de usuarios mgr
	/// </summary>
    public partial class SistemasBIPSMgr : ITablaUsuariosMgr
    {
        #region campos publicos
        /// <summary>
        /// campo publico que contiene una instancia del objeto de negocio 
        /// </summary>
        public ITablaUsuariosDom iTablaUsuariosDom;
        #endregion        

        #region metodos publicos
        /// <summary>
        /// metodo que permite buscar los elementos de tipo b_usuarios existentes
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Filtro">informacion de filtrado para realizar la busqueda</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>	
        public ViewDto<TablaUsuariosDto> BuscarUsuarios(ContextoDto p_Contexto, TablaUsuariosFiltroDto p_Filtro)
        {
            ViewDto<TablaUsuariosDto> viewResponse = new ViewDto<TablaUsuariosDto>();
            try
            {
                viewResponse = iTablaUsuariosDom.Buscar(p_Contexto, p_Filtro);
                if (viewResponse.HasError())
                {
                    if (viewResponse.Error.Severidad == EnumSeveridad.Warning)
                    {
                        //TODO: Generar error con especificacion de mensaje de datos no encontrados
                        iProviderError.LoadError(ref viewResponse, EnumErrores.BLL_CONSULTA_SIN_DATOS);
                    }
                    else
                    {
                        //TODO: Generar error con especificacion de mensaje de proceso defectuoso
                        iProviderError.LoadError(ref viewResponse, EnumErrores.BLL_ERROR_GENERICO_PROCESO);
                    }
                }
            }
            catch (Exception ex)
            {
                iProviderError.LoadError(ref viewResponse, ex);
            }
            return viewResponse;
        }

        /// <summary>
        /// metodo que permite registrar elementos de tipo b_usuarios en el sistema
        /// </summary>
        /// <param name="p_Contexto">informacion del contexto</param>
        /// <param name="p_Usuarios">b_usuarios a registrar</param>
        /// <param name="p_Accion">accion a registrar (insertar, actualizar o eliminar)</param>
        /// <returns>objeto contenedor de la informacion generada por la accion ejecutada</returns>        
        public ViewDto<TablaUsuariosDto> RegistrarUsuarios(ContextoDto p_Contexto, TablaUsuariosDto p_Usuarios, EnumAccionRealizar p_Accion)
        {
            ViewDto<TablaUsuariosDto> viewResponse = new ViewDto<TablaUsuariosDto>();
            try
            {
                viewResponse = iTablaUsuariosDom.Registrar(p_Contexto, p_Usuarios, p_Accion);
                if (viewResponse.HasError())
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
