using System;

namespace MDS.Dto
{
    /// <summary>
    /// Clase representativa de la tabla CB_GRUPOS_FORMULARIOS
    /// </summary>
    public partial class TablaGruposFormulariosDto : ICloneable
    {
        #region campos privados
        private Nullable<Decimal> _idgrupoformulario;
        private String _nombre;
        private String _descripcion;
        private Nullable<Decimal> _estado;
        #endregion

        #region propiedades publicas        
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
        public TablaGruposFormulariosDto()
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
            TablaGruposFormulariosDto objDto = (TablaGruposFormulariosDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
