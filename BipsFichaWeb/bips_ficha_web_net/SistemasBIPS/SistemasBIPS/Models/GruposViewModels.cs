using MDS.Dto;
using System.Collections.Generic;

namespace SistemasBIPS.Models
{
    public class GruposViewModels
    {
        public List<TablaPerfilesDto> listaPermisosPerfiles { get; set; }
        public IList<int> listaAnos { get; set; }
        public IList<MinisterioServicios> listaMinistServ { get; set; }
        public IList<TablaParametrosDto> tipos { get; set; }
        public IList<TablaParametrosDto> tiposGrupos { get; set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public GruposViewModels()
        {
            this.listaPermisosPerfiles = new List<TablaPerfilesDto>();
            this.listaMinistServ = new List<MinisterioServicios>();
            this.tipos = new List<TablaParametrosDto>();
            this.tiposGrupos = new List<TablaParametrosDto>();
        }
    }    
}
