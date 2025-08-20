using System;

namespace MDS.Dto
{
    /// <summary>
    /// Clase representativa de la tabla CB_PROGRAMAS
    /// </summary>
    public partial class TablaProgramasDto : ICloneable
    {
        #region campos privados
        private Nullable<Decimal> _idprograma;
        private Nullable<Decimal> _idbips;
        private Nullable<Decimal> _ano;
        private TablaParametrosDto _tipoformulario;
        private String _nombre;
        private TablaParametrosDto _idministerio;
        private TablaParametrosDto _idservicio;
        private TablaParametrosDto _etapa;
        private TablaParametrosDto _estado;
        private Nullable<Decimal> _versionActual;
        #endregion

        #region propiedades publicas
        /// <summary>
        /// propiedad publica que almacena la clave primaria del objeto
        /// </summary>
        /// <remarks>miembro de la clave primaria de la tabla B_PROGRAMA</remarks>
        public Nullable<Decimal> IdPrograma
        {
            get
            {
                return _idprograma;
            }
            set
            {
                _idprograma = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdBips
        {
            get
            {
                return _idbips;
            }
            set
            {
                _idbips = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> Ano
        {
            get
            {
                return _ano;
            }
            set
            {
                _ano = value;
            }
        }

        /// <summary>
        /// propiedad publica de tipo Dto que mantiene relacion con el objeto b_parametros
        /// </summary>
        /// <remarks>miembro relacional que representa la propiedad id_parametro del objeto b_parametros</remarks>
        public TablaParametrosDto TipoFormulario
        {
            get
            {
                return _tipoformulario;
            }
            set
            {
                _tipoformulario = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Nombre
        {
            get
            {
                return _nombre;
            }
            set
            {
                _nombre = value;
            }
        }

        /// <summary>
        /// propiedad publica de tipo Dto que mantiene relacion con el objeto b_parametros
        /// </summary>
        /// <remarks>miembro relacional que representa la propiedad id_parametro del objeto b_parametros</remarks>
        public TablaParametrosDto IdMinisterio
        {
            get
            {
                return _idministerio;
            }
            set
            {
                _idministerio = value;
            }
        }

        /// <summary>
        /// propiedad publica de tipo Dto que mantiene relacion con el objeto b_parametros
        /// </summary>
        /// <remarks>miembro relacional que representa la propiedad id_parametro del objeto b_parametros</remarks>
        public TablaParametrosDto IdServicio
        {
            get
            {
                return _idservicio;
            }
            set
            {
                _idservicio = value;
            }
        }

        /// <summary>
        /// propiedad publica de tipo Dto que mantiene relacion con el objeto b_parametros
        /// </summary>
        /// <remarks>miembro relacional que representa la propiedad id_parametro del objeto b_parametros</remarks>
        public TablaParametrosDto Etapa
        {
            get
            {
                return _etapa;
            }
            set
            {
                _etapa = value;
            }
        }

        /// <summary>
        /// propiedad publica de tipo Dto que mantiene relacion con el objeto b_parametros
        /// </summary>
        /// <remarks>miembro relacional que representa la propiedad id_parametro del objeto b_parametros</remarks>
        public TablaParametrosDto Estado
        {
            get
            {
                return _estado;
            }
            set
            {
                _estado = value;
            }
        }

        /// <summary>
        /// propiedad publica de tipo Dto que mantiene relacion con el objeto b_parametros
        /// </summary>
        /// <remarks>miembro relacional que representa la propiedad id_parametro del objeto b_parametros</remarks>
        public Nullable<Decimal> VersionActual
        {
            get
            {
                return _versionActual;
            }
            set
            {
                _versionActual = value;
            }
        }
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public TablaProgramasDto()
        {
            _tipoformulario = (TablaParametrosDto)Activator.CreateInstance(typeof(TablaParametrosDto));
            _idministerio = (TablaParametrosDto)Activator.CreateInstance(typeof(TablaParametrosDto));
            _idservicio = (TablaParametrosDto)Activator.CreateInstance(typeof(TablaParametrosDto));
            _etapa = (TablaParametrosDto)Activator.CreateInstance(typeof(TablaParametrosDto));
            _estado = (TablaParametrosDto)Activator.CreateInstance(typeof(TablaParametrosDto));
        }
        #endregion

        #region ICloneable
        /// <summary>
        /// metodo que permite crear una copia de la actual instancia en memoria
        /// </summary>
        /// <returns>una copia del objeto existente en memoria</returns>
        public Object Clone()
        {
            TablaProgramasDto objDto = (TablaProgramasDto)this.MemberwiseClone();
            objDto.TipoFormulario = this.TipoFormulario == null ? null : (TablaParametrosDto)this.TipoFormulario.Clone();
            objDto.IdMinisterio = this.IdMinisterio == null ? null : (TablaParametrosDto)this.IdMinisterio.Clone();
            objDto.IdServicio = this.IdServicio == null ? null : (TablaParametrosDto)this.IdServicio.Clone();
            objDto.Etapa = this.Etapa == null ? null : (TablaParametrosDto)this.Etapa.Clone();
            objDto.Estado = this.Estado == null ? null : (TablaParametrosDto)this.Estado.Clone();
            return objDto;
        }
        #endregion
    }
}
