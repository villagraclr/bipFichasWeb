using MDS.Dto;
using System.Collections.Generic;

namespace SistemasBIPS.Models
{
    public class ProgramasViewModels
    {             
        public IList<MinisterioServicios> listaMinServ { get; set; }
        public IList<int> listaAnos { get; set; }
        public IList<TablaLogFormulariosDto> listaFormulariosTomados { get; set; }
        public IList<TablaParametrosDto> listaRutasFichas { get; set; }
        public IList<TablaParametrosDto> listaTiposProgramas { get; set; }
        public IList<TablaParametrosDto> listaAnosFichas { get; set; }
        public bool permisoLibera { get; set; }
        public bool permisoInfoEval { get; set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ProgramasViewModels()
        {
            this.listaMinServ = new List<MinisterioServicios>();
            this.listaAnos = new List<int>();
            this.listaFormulariosTomados = new List<TablaLogFormulariosDto>();
            this.listaRutasFichas = new List<TablaParametrosDto>();
            this.listaTiposProgramas = new List<TablaParametrosDto>();
            this.listaAnosFichas = new List<TablaParametrosDto>();
        }
    }    
}
