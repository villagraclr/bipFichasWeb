using MDS.Dto;
using System.Collections.Generic;

namespace SistemasBIPS.Models
{
    public class UsuariosViewModels
    {
        public IList<TablaPerfilesDto> listaPerfiles { get; set; }
        public IList<TablaGruposFormulariosDto> listaGruposFormularios { get; set; }
        public IList<TablaFormulariosGruposDto> listaFormulariosGrupos { get; set; }
        public IList<MinisterioServicios> listaMinistServ { get; set; }
        public DatosUsuarios dataUsuario { get; set; }
        public List<TablaPerfilesDto> listaPermisosPerfiles { get; set; }
        public IList<int> listaAnos { get; set; }
        public IList<TablaParametrosDto> tiposGrupos { get; set; }
        public IList<TablaExcepcionesPlantillasDto> plantillas { get; set; }
        public IList<TablaParametrosDto> tipos { get; set; }
        public List<TablaGruposFormulariosDto> listaGrupos { get; set; }
        public List<TablaParametrosUsuariosDto> listaPerfilesEspeciales { get; set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public UsuariosViewModels()
        {
            this.listaPerfiles = new List<TablaPerfilesDto>();
            this.listaGruposFormularios = new List<TablaGruposFormulariosDto>();
            this.listaFormulariosGrupos = new List<TablaFormulariosGruposDto>();
            this.listaMinistServ = new List<MinisterioServicios>();
            this.dataUsuario = new DatosUsuarios();
            this.listaPermisosPerfiles = new List<TablaPerfilesDto>();
            this.tiposGrupos = new List<TablaParametrosDto>();
            this.plantillas = new List<TablaExcepcionesPlantillasDto>();
            this.listaGrupos = new List<TablaGruposFormulariosDto>();
            this.listaPerfilesEspeciales = new List<TablaParametrosUsuariosDto>();
        }
    }

    public class DatosUsuarios
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Nombre { get; set; }
        public int Ministerio { get; set; }
        public int Servicio { get; set; }
        public int Perfil { get; set; }
        public int IdGore { get; set; }
        public int IdPerfilGore { get; set; }
        public List<int> MisFormularios { get; set; }
        public List<int> OtrosFormularios { get; set; }
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public DatosUsuarios()
        {
            this.MisFormularios = new List<int>();
            this.OtrosFormularios = new List<int>();
        }
    }

    public class NuevaDataUsuario
    {
        public string IdUser { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public int Ministerio { get; set; }
        public int Servicio { get; set; }
        public int Perfil { get; set; }
        public int IdGore { get; set; }
        public int IdPerfilGore { get; set; }
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public NuevaDataUsuario()
        {                        
        }
    }
}
