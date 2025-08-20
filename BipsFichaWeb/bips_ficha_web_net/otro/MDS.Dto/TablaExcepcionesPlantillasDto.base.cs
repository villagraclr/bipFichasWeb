using System;
using System.Diagnostics;

namespace MDS.Dto
{
    /// <summary>
	///	Clase representativa de la tabla CB_EXCEPCIONES_PLANTILLAS
	/// </summary>
    public partial class TablaExcepcionesPlantillasDto : ICloneable
    {
        #region campos privados
        private Nullable<Decimal> _idexcepcionplantilla;
        private String _nombre;
        private String _descripcion;
        private Nullable<Decimal> _tipoformulario;
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
        public String Descripcion
        {
            get
            {
                return _descripcion;
            }
            set
            {
                _descripcion = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> TipoFormulario
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
        public TablaExcepcionesPlantillasDto()
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
            TablaExcepcionesPlantillasDto objDto = (TablaExcepcionesPlantillasDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
