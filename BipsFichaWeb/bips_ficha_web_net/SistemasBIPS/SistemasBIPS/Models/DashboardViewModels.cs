using MDS.Dto;
using System;
using System.Collections.Generic;

namespace SistemasBIPS.Models
{
    /// <summary>
    /// Contiene objetos necesarios para armar dashboard
    /// </summary>
    public class DashboardViewModels
    {
        public IList<TablaParametrosDto> ministerios { get; set; }
        public IList<TablaParametrosDto> servicios { get; set; }
        public IList<TablaProgramasDto> programas { get; set; }
        public IList<Int16> iteraciones { get; set; }
        public IList<TablaUsuariosDto> evaluadores { get; set; }
        public TablaProgramasDto indicadores { get; set; }
        public IList<TablaProgramasDto> tipoProgramas { get; set; }
        public IList<TablaProgramasDto> califProgramas { get; set; }
        public IList<TablaProgramasDto> estadoProgramas { get; set; }
        public IList<TablaProgramasDto> programasXiteracion { get; set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public DashboardViewModels()
        {
            this.ministerios = new List<TablaParametrosDto>();
            this.servicios = new List<TablaParametrosDto>();
            this.programas = new List<TablaProgramasDto>();
            this.iteraciones = new List<Int16>();
            this.evaluadores = new List<TablaUsuariosDto>();
            this.indicadores = new TablaProgramasDto();
            this.tipoProgramas = new List<TablaProgramasDto>();
            this.califProgramas = new List<TablaProgramasDto>();
            this.estadoProgramas = new List<TablaProgramasDto>();
            this.programasXiteracion = new List<TablaProgramasDto>();
        }
    }    
}