using System;
using System.Diagnostics;

namespace MDS.Dto
{
    /// <summary>
	///	Clase representativa de la tabla CB_PLANTILLAS_TRASPASO
	/// </summary>
    public partial class TablaPlantillasTraspasoDto : ICloneable
    {
        #region campos privados
        private Nullable<Decimal> _idplantillatraspaso;
        private String _nombre;
        private String _descripcion;
        private Nullable<Decimal> _tipoformularioorigen;
        private Nullable<Decimal> _tipoformulariodestino;
        private Nullable<Decimal> _estado;
        #endregion

        #region propiedades publicas        
        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdPlantillaTraspaso
        {
            get
            {
                return _idplantillatraspaso;
            }
            set
            {
                _idplantillatraspaso = value;
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
        public Nullable<Decimal> TipoFormularioOrigen
        {
            get
            {
                return _tipoformularioorigen;
            }
            set
            {
                _tipoformularioorigen = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> TipoFormularioDestino
        {
            get
            {
                return _tipoformulariodestino;
            }
            set
            {
                _tipoformulariodestino = value;
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
        public TablaPlantillasTraspasoDto()
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
            TablaPlantillasTraspasoDto objDto = (TablaPlantillasTraspasoDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
