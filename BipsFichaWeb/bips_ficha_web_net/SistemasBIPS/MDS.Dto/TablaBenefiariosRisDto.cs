using System;

namespace MDS.Dto
{
    /// <summary>
    /// Clase representativa de la tabla CB_BENEFICIARIOS_RIS
    /// </summary>
    public partial class TablaBenefiariosRisDto : ICloneable     
    {
        #region campos privados
        private Nullable<Decimal> _idprograma;
        private String _nombrearchivo;
        private Nullable<Decimal> _tamanoarchivo;
        private String _cargabeneficiarios;
        private Nullable<Decimal> _justificacion;
        private String _textojustificacion;
        private String _usuariocarga;
        private Nullable<DateTime> _fechacarga;
        private String _nombreencode;
        #endregion

        #region propiedades publicas        
        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
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
        public String NombreArchivo
        {
            get
            {
                return _nombrearchivo;
            }
            set
            {
                _nombrearchivo = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> TamanoArchivo
        {
            get
            {
                return _tamanoarchivo;
            }
            set
            {
                _tamanoarchivo = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String CargaBeneficiarios
        {
            get
            {
                return _cargabeneficiarios;
            }
            set
            {
                _cargabeneficiarios = value;
            }
        }               

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> Justificacion
        {
            get
            {
                return _justificacion;
            }
            set
            {
                _justificacion = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String TextoJustificacion
        {
            get
            {
                return _textojustificacion;
            }
            set
            {
                _textojustificacion = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String UsuarioCarga
        {
            get
            {
                return _usuariocarga;
            }
            set
            {
                _usuariocarga = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<DateTime> FechaCarga
        {
            get
            {
                return _fechacarga;
            }
            set
            {
                _fechacarga = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String NombreEncode
        {
            get
            {
                return _nombreencode;
            }
            set
            {
                _nombreencode = value;
            }
        }
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public TablaBenefiariosRisDto()
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
            TablaBenefiariosRisDto objDto = (TablaBenefiariosRisDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
