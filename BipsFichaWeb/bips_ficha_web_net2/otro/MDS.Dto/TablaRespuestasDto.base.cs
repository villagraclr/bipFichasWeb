using System;

namespace MDS.Dto
{
    /// <summary>
    /// Clase representativa de la tabla CB_RESPUESTAS
    /// </summary>
    public partial class TablaRespuestasDto : ICloneable
    {
        #region campos privados
        private Nullable<Decimal> _idpregunta;
        private Nullable<Decimal> _idformulario;
        private Nullable<Decimal> _idtab;
        private Object _respuesta;
        private Nullable<Decimal> _tipopregunta;
        private Nullable<Decimal> _idcategoria;
        #endregion

        #region propiedades publicas        
        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdPregunta
        {
            get
            {
                return _idpregunta;
            }
            set
            {
                _idpregunta = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdFormulario
        {
            get
            {
                return _idformulario;
            }
            set
            {
                _idformulario = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdTab
        {
            get
            {
                return _idtab;
            }
            set
            {
                _idtab = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Object Respuesta
        {
            get
            {
                return _respuesta;
            }
            set
            {
                _respuesta = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> TipoPregunta
        {
            get
            {
                return _tipopregunta;
            }
            set
            {
                _tipopregunta = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdCategoria
        {
            get
            {
                return _idcategoria;
            }
            set
            {
                _idcategoria = value;
            }
        }
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public TablaRespuestasDto()
        {
        }
        #endregion

        #region ICloneable
        /// <summary>
        /// metodo que permite crear una copia de la actual instancia en memoria
        /// </summary>
        /// <returns>una copia del objeto existente en memoria</returns>
        public Object Clone()
        {
            TablaRespuestasDto objDto = (TablaRespuestasDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
