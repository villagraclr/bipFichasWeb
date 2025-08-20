using MDS.Dto;
using System;
using System.Collections.Generic;

namespace SistemasBIPS.Models
{
    public class EvaluacionExAnteViewModels
    {
        public IList<TablaUsuariosDto> listaEvaluadores { get; set; }
        public IList<TablaParametrosDto> listaCalificaciones { get; set; }
        public PreguntaEvaluaciones listaPregEvaluaciones { get; set; }
        public string linkInformeEvalExAnte { get; set; }
        public string linkInformeDetalleExAnte { get; set; }
        public Nullable<Decimal> etapaCierre { get; set; }
        public Nullable<Decimal> etapaPerfilEnConsulta { get; set; }
        public Nullable<Decimal> etapaCierrePerfilGore { get; set; }
        public Nullable<Decimal> etapaRevisionJefe { get; set; }
        public Nullable<Decimal> etapaProgramaCalificado { get; set; }
        public Nullable<Decimal> perfilEvaluacion { get; set; }
        public Nullable<Decimal> perfilCoordinadorEval { get; set; }
        public Nullable<Decimal> perfilSoloLecturaEval { get; set; }
        public Nullable<Decimal> perfilUsuario { get; set; }
        public List<Nullable<Decimal>> perfilesNuevaIteracion { get; set; }
        public Nullable<Decimal> estadoIteracion { get; set; }
        public List<Nullable<Decimal>> idTipoNuevos { get; set; }
        public List<Nullable<Decimal>> idTipoReformulados { get; set; }
        public Nullable<Decimal> idTipoValorFormulario { get; set; }
        public bool revisionJefaturas { get; set; }
        public int tipoJefatura { get; set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public EvaluacionExAnteViewModels()
        {
            listaEvaluadores = new List<TablaUsuariosDto>();
            listaCalificaciones = new List<TablaParametrosDto>();
            listaPregEvaluaciones = new PreguntaEvaluaciones();
            this.perfilesNuevaIteracion = new List<decimal?>();
            this.idTipoNuevos = new List<decimal?>();
            this.idTipoReformulados = new List<decimal?>();
        }
    }

    public class PreguntaEvaluaciones
    {
        public Nullable<Decimal> idPrograma { get; set; }
        public Nullable<Decimal> calificacion { get; set; }
        public string comentSegpres { get; set; }
        public string comentGeneral { get; set; }
        public string atingencia { get; set; }
        public string coherencia { get; set; }
        public string consistencia { get; set; }
        public string antecPrograma { get; set; }
        public string diagPrograma { get; set; }
        public string objPoblPrograma { get; set; }
        public string estrategiaPrograma { get; set; }
        public string indicadoresPrograma { get; set; }
        public string gastosPrograma { get; set; }
    }

    public class ComentariosJefaturas
    {
        public Nullable<Decimal> idPrograma { get; set; }
        public string tieneComentMonitoreo { get; set; }
        public string comentMonitoreo { get; set; }
        public string tieneComentEstudios { get; set; }
        public string comentEstudios { get; set; }
        public string nombrePDF { get; set; }        
    }
}
