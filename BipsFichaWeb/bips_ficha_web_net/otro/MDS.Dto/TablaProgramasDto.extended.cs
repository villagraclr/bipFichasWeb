using System;

namespace MDS.Dto
{
    public partial class TablaProgramasDto
    {
        #region campos privados
        private string _ministerio;
        private string _servicio;
        private string _tipo;
        private string _estadodesc;
        private string _etapadesc;
        private string _idencriptado;
        private Nullable<Decimal> _idtipoformulario;
        private string _iduser;
        private Nullable<Decimal> _idplataforma;
        private Nullable<Decimal> _acceso;
        private Nullable<Decimal> _tipogrupo;
        private string _desctipogrupo;
        private Nullable<Decimal> _idgrupoformulario;
        private string _nombregrupo;
        private string _descgrupoformulario;
        private Nullable<Decimal> _idexcepcion;
        private string _tipoexcepcion;
        private string _descexcepcion;
        private Nullable<Decimal> _idperfil;
        private Nullable<Decimal> _totalusuarios;
        private Nullable<Decimal> _totalgrupos;
        private Nullable<Decimal> _idetapa;
        private Nullable<Decimal> _idestado;
        private String _calificacion;
        private String _calificacionprograma;
        private String _idevaluador1;
        private String _idevaluador2;
        private String _version;
        private Nullable<Decimal> _tomado;
        private String _fecSolicitudEval;
        private String _fecAsigEval1;
        private String _fecAsigEval2;
        private String _fecEnvioComentSect;
        private String _fecEvalFinalizada;
        private String _fecCorreccion;
        private String _fecComentMonitoreo;
        private String _fecComentEstudios;
        private String _fecEnvioSect;
        private Nullable<Decimal> _totaDiasIteracion;
        private String _nombreEvaluador1;
        private String _nombreEvaluador2;
        private String _puntajeFinal;
        private Nullable<Decimal> _tipogeneral;
        private String _gore;
        #endregion

        #region propiedades publicas
        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Ministerio
        {
            get
            {
                return _ministerio;
            }
            set
            {
                _ministerio = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Servicio
        {
            get
            {
                return _servicio;
            }
            set
            {
                _servicio = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Tipo
        {
            get
            {
                return _tipo;
            }
            set
            {
                _tipo = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String EstadoDesc
        {
            get
            {
                return _estadodesc;
            }
            set
            {
                _estadodesc = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String EtapaDesc
        {
            get
            {
                return _etapadesc;
            }
            set
            {
                _etapadesc = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String IdEncriptado
        {
            get
            {
                return _idencriptado;
            }
            set
            {
                _idencriptado = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdTipoFormulario
        {
            get
            {
                return _idtipoformulario;
            }
            set
            {
                _idtipoformulario = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String IdUser
        {
            get
            {
                return _iduser;
            }
            set
            {
                _iduser = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdPlataforma
        {
            get
            {
                return _idplataforma;
            }
            set
            {
                _idplataforma = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> Acceso
        {
            get
            {
                return _acceso;
            }
            set
            {
                _acceso = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> TipoGrupo
        {
            get
            {
                return _tipogrupo;
            }
            set
            {
                _tipogrupo = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdGrupoFormulario
        {
            get
            {
                return _idgrupoformulario;
            }
            set
            {
                _idgrupoformulario = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String DescTipoGrupo
        {
            get
            {
                return _desctipogrupo;
            }
            set
            {
                _desctipogrupo = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String DescGrupoFormulario
        {
            get
            {
                return _descgrupoformulario;
            }
            set
            {
                _descgrupoformulario = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdExcepcion
        {
            get
            {
                return _idexcepcion;
            }
            set
            {
                _idexcepcion = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String TipoExcepcion
        {
            get
            {
                return _tipoexcepcion;
            }
            set
            {
                _tipoexcepcion = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String DescExcepcion
        {
            get
            {
                return _descexcepcion;
            }
            set
            {
                _descexcepcion = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdPerfil
        {
            get
            {
                return _idperfil;
            }
            set
            {
                _idperfil = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> TotalUsuarios
        {
            get
            {
                return _totalusuarios;
            }
            set
            {
                _totalusuarios = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> TotalGrupos
        {
            get
            {
                return _totalgrupos;
            }
            set
            {
                _totalgrupos = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String NombreGrupo
        {
            get
            {
                return _nombregrupo;
            }
            set
            {
                _nombregrupo = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdEtapa
        {
            get
            {
                return _idetapa;
            }
            set
            {
                _idetapa = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdEstado
        {
            get
            {
                return _idestado;
            }
            set
            {
                _idestado = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String Calificacion
        {
            get
            {
                return _calificacion;
            }
            set
            {
                _calificacion = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String CalificacionPrograma
        {
            get
            {
                return _calificacionprograma;
            }
            set
            {
                _calificacionprograma = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String IdEvaluador1
        {
            get
            {
                return _idevaluador1;
            }
            set
            {
                _idevaluador1 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String IdEvaluador2
        {
            get
            {
                return _idevaluador2;
            }
            set
            {
                _idevaluador2 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String Version
        {
            get
            {
                return _version;
            }
            set
            {
                _version = value;
            }
        }
        
        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public Nullable<Decimal> Tomado
        {
            get
            {
                return _tomado;
            }
            set
            {
                _tomado = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String FecSolicitudEval
        {
            get
            {
                return _fecSolicitudEval;
            }
            set
            {
                _fecSolicitudEval = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String FecAsigEval1
        {
            get
            {
                return _fecAsigEval1;
            }
            set
            {
                _fecAsigEval1 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String FecAsigEval2
        {
            get
            {
                return _fecAsigEval2;
            }
            set
            {
                _fecAsigEval2 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String FecEvalFinalizada
        {
            get
            {
                return _fecEvalFinalizada;
            }
            set
            {
                _fecEvalFinalizada = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String FecEnvioComentSect
        {
            get
            {
                return _fecEnvioComentSect;
            }
            set
            {
                _fecEnvioComentSect = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String FecCorreccion
        {
            get
            {
                return _fecCorreccion;
            }
            set
            {
                _fecCorreccion = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String FecComentMonitoreo
        {
            get
            {
                return _fecComentMonitoreo;
            }
            set
            {
                _fecComentMonitoreo = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String FecComentEstudios
        {
            get
            {
                return _fecComentEstudios;
            }
            set
            {
                _fecComentEstudios = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String FecEnvioSect
        {
            get
            {
                return _fecEnvioSect;
            }
            set
            {
                _fecEnvioSect = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public Nullable<Decimal> TotalDiasIteracion
        {
            get
            {
                return _totaDiasIteracion;
            }
            set
            {
                _totaDiasIteracion = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String NombreEvaluador1
        {
            get
            {
                return _nombreEvaluador1;
            }
            set
            {
                _nombreEvaluador1 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String NombreEvaluador2
        {
            get
            {
                return _nombreEvaluador2;
            }
            set
            {
                _nombreEvaluador2 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String PuntajeFinal
        {
            get
            {
                return _puntajeFinal;
            }
            set
            {
                _puntajeFinal = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public Nullable<Decimal> TipoGeneral
        {
            get
            {
                return _tipogeneral;
            }
            set
            {
                _tipogeneral = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String Gore
        {
            get
            {
                return _gore;
            }
            set
            {
                _gore = value;
            }
        }
        #endregion

        #region constructores
        #endregion

        #region metodos publicos
        #endregion
    }
}