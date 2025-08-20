using System;

namespace MDS.Dto
{
    /// <summary>
    /// Clase representativa de la tabla CB_PREGUNTAS_GRUPOS
    /// </summary>
    public partial class TablaPreguntasGruposDto : ICloneable
    {
        #region campos privados
        private Nullable<Decimal> _idpreguntagrupo;
        private Nullable<Decimal> _idpregunta;
        private String _pregunta;
        private Nullable<Decimal> _idgrupo;
        private Nullable<Decimal> _idtipoformulario;
        private String _tipoformulario;        
        private Nullable<Decimal> _idtipopregunta;
        private String _tipopregunta;
        private Nullable<Decimal> _idcategoriapregunta;
        private Nullable<Decimal> _tipopreguntavalor;
        private Nullable<Decimal> _tipopreguntavalor2;
        private Nullable<Decimal> _valores;
        private Nullable<Decimal> _idfuncion;
        private String _funcion;
        private Nullable<Decimal> _funcionvalor;
        private Nullable<Decimal> _funcionvalor2;
        private Nullable<Decimal> _valorfuncion;
        private Nullable<Decimal> _idmenu;
        private Nullable<Decimal> _tipomenu;
        private Nullable<Decimal> _orden;
        private Nullable<Decimal> _idestado;
        private String _estado;
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
        public String Pregunta
        {
            get
            {
                return _pregunta;
            }
            set
            {
                _pregunta = value;
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
        public Nullable<Decimal> IdTipoPregunta
        {
            get
            {
                return _idtipopregunta;
            }
            set
            {
                _idtipopregunta = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String TipoPregunta
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
        public Nullable<Decimal> IdCategoriaPregunta
        {
            get
            {
                return _idcategoriapregunta;
            }
            set
            {
                _idcategoriapregunta = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> TipoPreguntaValor
        {
            get
            {
                return _tipopreguntavalor;
            }
            set
            {
                _tipopreguntavalor = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> TipoPreguntaValor2
        {
            get
            {
                return _tipopreguntavalor2;
            }
            set
            {
                _tipopreguntavalor2 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> Valores
        {
            get
            {
                return _valores;
            }
            set
            {
                _valores = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdFuncion
        {
            get
            {
                return _idfuncion;
            }
            set
            {
                _idfuncion = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Funcion
        {
            get
            {
                return _funcion;
            }
            set
            {
                _funcion = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> FuncionValor
        {
            get
            {
                return _funcionvalor;
            }
            set
            {
                _funcionvalor = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> FuncionValor2
        {
            get
            {
                return _funcionvalor2;
            }
            set
            {
                _funcionvalor2 = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> ValorFuncion
        {
            get
            {
                return _valorfuncion;
            }
            set
            {
                _valorfuncion = value;
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
        public Nullable<Decimal> TipoMenu
        {
            get
            {
                return _tipomenu;
            }
            set
            {
                _tipomenu = value;
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

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Estado
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
        public TablaPreguntasGruposDto()
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
            TablaPreguntasGruposDto objDto = (TablaPreguntasGruposDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
