using System;

namespace MDS.Dto
{
    public partial class TablaFormulariosGruposDto
    {
        #region campos privados
        private Nullable<Decimal> _idbips;
        private Nullable<Decimal> _ano;
        private String _tipoformulario;
        private String _nombre;
        private String _ministerio;
        private String _servicio;
        private Nullable<Decimal> _idtipoformulario;
        private Nullable<Decimal> _idministerio;
        private Nullable<Decimal> _idservicio;
        #endregion

        #region propiedades publicas
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
        /// propiedad publica del objeto
        /// </summary>
        public String TipoFormulario
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
        public Nullable<Decimal> IdMinisterio
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
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdServicio
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
        #endregion

        #region constructores
        #endregion

        #region metodos publicos
        #endregion
    }
}
