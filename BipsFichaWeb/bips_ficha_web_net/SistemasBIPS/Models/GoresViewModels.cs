using MDS.Dto;
using System;
using System.Collections.Generic;

namespace SistemasBIPS.Models
{
    public class GoresViewModels
    {
        public IList<TablaParametrosDto> listaGores { get; set; }
        public IList<int> listaAnos { get; set; }
        public IList<TablaLogFormulariosDto> listaFormulariosTomados { get; set; }
        public IList<TablaParametrosDto> listaRutasFichas { get; set; }
        public IList<TablaParametrosDto> listaTiposProgramas { get; set; }
        public IList<TablaParametrosDto> listaAnosFichas { get; set; }
        public bool permisoLibera { get; set; }
        public bool permisoInfoEval { get; set; }
        public TablaUsuariosDto usuarioGore { get; set; }
        public Nullable<Decimal> etapaPerfilCalificado { get; set; }
        public IList<MinisterioServicios> listaMinistServ { get; set; }
        public DatosUsuarios dataUsuario { get; set; }
        public IList<TablaPerfilesDto> listaPerfiles { get; set; }
        public UsuariosViewModels usuarioViewModel { get; set; }
        public IList<TablaExcepcionesPlantillasDto> plantillas { get; set; }
        public IList<TablaParametrosDto> tipos { get; set; }
        public IList<TablaUsuariosDto> listaEvaluadores { get; set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public GoresViewModels()
        {
            this.listaGores = new List<TablaParametrosDto>();
            this.listaAnos = new List<int>();
            this.listaFormulariosTomados = new List<TablaLogFormulariosDto>();
            this.listaRutasFichas = new List<TablaParametrosDto>();
            this.listaTiposProgramas = new List<TablaParametrosDto>();
            this.listaAnosFichas = new List<TablaParametrosDto>();
            this.usuarioGore = new TablaUsuariosDto();
            this.listaMinistServ = new List<MinisterioServicios>();
            this.dataUsuario = new DatosUsuarios();
            this.listaPerfiles = new List<TablaPerfilesDto>();
            this.usuarioViewModel = new UsuariosViewModels();
            this.plantillas = new List<TablaExcepcionesPlantillasDto>();
            this.listaEvaluadores = new List<TablaUsuariosDto>();
        }
    }
}