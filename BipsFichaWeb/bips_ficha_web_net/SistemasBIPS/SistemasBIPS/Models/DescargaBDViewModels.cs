using MDS.Dto;
using System.Collections.Generic;

namespace SistemasBIPS.Models
{
    /// <summary>
    /// Contiene objetos necesarios para vista descarga BD
    /// </summary>
    public class DescargaBDViewModels
    {
        public IList<TablaParametrosDto> categorias { get; set; }
        public IList<TablaParametrosDto> formularios { get; set; }
        public IList<TablaParametrosDto> anos { get; set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public DescargaBDViewModels()
        {
            this.categorias = new List<TablaParametrosDto>();
            this.formularios = new List<TablaParametrosDto>();
            this.anos = new List<TablaParametrosDto>();
        }
    }
}