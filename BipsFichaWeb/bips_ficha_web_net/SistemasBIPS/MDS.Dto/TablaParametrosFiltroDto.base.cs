using System;
using System.Diagnostics;

namespace MDS.Dto
{
    /// <summary>
    ///	Clase representativa de los filtros aplicados a las busquedas de CB_PARAMETROS
    /// </summary>
    public partial class TablaParametrosFiltroDto
    {
        #region campos privados
        private Nullable<Decimal> _idparametro;
        private String _descripcion;
        private Nullable<Decimal> _idcategoria;
        private Nullable<Decimal> _valor;
        private Nullable<Decimal> _valor2;
        private Nullable<Decimal> _orden;
        private Nullable<Decimal> _estado;
        #endregion

        #region propiedades publicas
        /// <summary>
        /// propiedad publica que almacena la clave primaria del objeto
        /// </summary>
        public Nullable<System.Decimal> IdParametro
        {
            get
            {
                return _idparametro;
            }
            set
            {
                _idparametro = value;
            }
        }
        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public System.String Descripcion
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
        public Nullable<System.Decimal> IdCategoria
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
        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<System.Decimal> Valor
        {
            get
            {
                return _valor;
            }
            set
            {
                _valor = value;
            }
        }
        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<System.Decimal> Valor2
        {
            get
            {
                return _valor2;
            }
            set
            {
                _valor2 = value;
            }
        }
        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<System.Decimal> Orden
        {
            get
            {
                return _orden;
            }
            set
            {
                _orden = value;
            }
        }
        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<System.Decimal> Estado
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
        public TablaParametrosFiltroDto()
        {

        }
        #endregion
    }
}
