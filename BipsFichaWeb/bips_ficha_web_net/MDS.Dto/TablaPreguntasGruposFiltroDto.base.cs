using System;
using System.Diagnostics;

namespace MDS.Dto
{
    /// <summary>
	///	Clase representativa de los filtros aplicados a las busquedas de CB_PREGUNTAS_GRUPOS
	/// </summary>
    public partial class TablaPreguntasGruposFiltroDto
    {
        #region campos privados
        private Nullable<Decimal> _idpreguntagrupo;
        private Nullable<Decimal> _idpregunta;
        private Nullable<Decimal> _idgrupo;
        private Nullable<Decimal> _idtipoformulario;        
        private Nullable<Decimal> _idmenu;
        private Nullable<Decimal> _orden;
        private Nullable<Decimal> _idestado;
        #endregion

        #region propiedades publicas
        /// <summary>
        /// propiedad publica que almacena la clave primaria del objeto
        /// </summary>
        /// <remarks>miembro de la clave primaria de la tabla CB_PREGUNTAS_GRUPOS</remarks>
        public Nullable<Decimal> IdPreguntaGrupo
        {
            get
            {
                return _idpreguntagrupo;
            }
            set
            {
                _idpreguntagrupo = value;
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
        public Nullable<Decimal> IdGrupo
        {
            get
            {
                return _idgrupo;
            }
            set
            {
                _idgrupo = value;
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
        public Nullable<Decimal> IdMenu
        {
            get
            {
                return _idmenu;
            }
            set
            {
                _idmenu = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> Orden
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
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        [DebuggerStepThrough]
        public TablaPreguntasGruposFiltroDto()
        {
        }
        #endregion
    }
}
