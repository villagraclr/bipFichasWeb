using System;

namespace MDS.Dto
{
    public partial class TablaProgramasFiltroDto
    {
        #region campos privados
        private string _iduser;
        private Nullable<Decimal> _idplataforma;
        private Nullable<Decimal> _idgrupoformulario;
        private Nullable<Decimal> _tipogrupo;
        private Nullable<Decimal> _idexcepcion;
        #endregion

        #region propiedades publicas
        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public String IdUser
        {
            get
            {
                return _iduser;
            }
            set
            {
                _iduser = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdPlataforma
        {
            get
            {
                return _idplataforma;
            }
            set
            {
                _idplataforma = value;
            }
        }

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
        public Nullable<Decimal> TipoGrupo
        {
            get
            {
                return _tipogrupo;
            }
            set
            {
                _tipogrupo = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
        /// </summary>
        public Nullable<Decimal> IdExcepcion
        {
            get
            {
                return _idexcepcion;
            }
            set
            {
                _idexcepcion = value;
            }
        }
        #endregion

        #region constructores
        #endregion

        #region metodos publicos
        #endregion
    }
}