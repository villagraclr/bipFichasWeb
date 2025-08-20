using System;
using System.Diagnostics;

namespace MDS.Dto
{
    /// <summary>
	///	Clase representativa de la tabla CB_EXCEPCIONES_PLANTILLAS_FORM
	/// </summary>
    public partial class TablaExcepcionesPlantillasFormDto : ICloneable
    {
        #region campos privados
        private Nullable<Decimal> _idexcepcionplantilla;
        private Nullable<Decimal> _idpregunta;
        private Nullable<Decimal> _tipoexcepcion;
        private Nullable<Decimal> _estado;
        #endregion

        #region propiedades publicas        
        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdExcepcionPlantilla
        {
            get
            {
                return _idexcepcionplantilla;
            }
            set
            {
                _idexcepcionplantilla = value;
            }
        }

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
        public Nullable<Decimal> TipoExcepcion
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
        public Nullable<Decimal> Estado
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
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        [DebuggerStepThrough]
        public TablaExcepcionesPlantillasFormDto()
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
            TablaExcepcionesPlantillasFormDto objDto = (TablaExcepcionesPlantillasFormDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
