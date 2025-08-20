using System;

namespace MDS.Dto
{
    /// <summary>
    /// Clase representativa de la tabla CB_CONSULTAS
    /// </summary>
    public partial class TablaConsultasDto : ICloneable
    {
        #region campos privados
        private Nullable<Decimal> _idconsulta;
        private Nullable<Decimal> _idtema;
        private Nullable<Decimal> _idtipo;
        private String _tipo;
        private String _idusuario;
        private String _emailusuario;
        private String _nombreusuario;
        private Nullable<Decimal> _idprograma;
        private String _idprogramastring;
        private Nullable<Decimal> _idmenu;
        private String _tema;
        private String _consulta;
        private Nullable<DateTime> _fecha;
        private Nullable<Decimal> _estado;
        private Nullable<Decimal> _idpregunta;
        private Nullable<Decimal> _idtab;
        private Nullable<Decimal> _idmenuhijo;
        private String _menupadre;
        private String _menuhijo;
        private String _pregunta;
        #endregion

        #region propiedades publicas        
        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdConsulta
        {
            get
            {
                return _idconsulta;
            }
            set
            {
                _idconsulta = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdTema
        {
            get
            {
                return _idtema;
            }
            set
            {
                _idtema = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdTipo
        {
            get
            {
                return _idtipo;
            }
            set
            {
                _idtipo = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Tipo
        {
            get
            {
                return _tipo;
            }
            set
            {
                _tipo = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String IdUsuario
        {
            get
            {
                return _idusuario;
            }
            set
            {
                _idusuario = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String EmailUsuario
        {
            get
            {
                return _emailusuario;
            }
            set
            {
                _emailusuario = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String NombreUsuario
        {
            get
            {
                return _nombreusuario;
            }
            set
            {
                _nombreusuario = value;
            }
        }

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
        public String IdProgramaString
        {
            get
            {
                return _idprogramastring;
            }
            set
            {
                _idprogramastring = value;
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
        public String Tema
        {
            get
            {
                return _tema;
            }
            set
            {
                _tema = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String Consulta
        {
            get
            {
                return _consulta;
            }
            set
            {
                _consulta = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<DateTime> Fecha
        {
            get
            {
                return _fecha;
            }
            set
            {
                _fecha = value;
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
        public Nullable<Decimal> IdMenuHijo
        {
            get
            {
                return _idmenuhijo;
            }
            set
            {
                _idmenuhijo = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String MenuPadre
        {
            get
            {
                return _menupadre;
            }
            set
            {
                _menupadre = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String MenuHijo
        {
            get
            {
                return _menuhijo;
            }
            set
            {
                _menuhijo = value;
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
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        public TablaConsultasDto()
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
            TablaConsultasDto objDto = (TablaConsultasDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
