using System;
using System.Diagnostics;

namespace MDS.Dto
{
    /// <summary>
	///	Clase representativa de la tabla CB_USERS
    public partial class TablaUsuariosDto : ICloneable
    {
        #region campos privados
        private String _id;
        private String _email;
        private Int16 _emailconfirmed;
        private Object _passwordhash;
        private Object _securitystamp;
        private Object _phonenumber;
        private Nullable<Decimal> _phonenumberconfirmed;
        private Nullable<Decimal> _twofactorenabled;
        private Nullable<DateTime> _lockoutenddateutc;
        private Nullable<Decimal> _lockoutenabled;
        private Nullable<Decimal> _accessfailedcount;
        private String _username;
        private Nullable<Decimal> _idministerio;
        private Nullable<Decimal> _idservicio;
        private Nullable<Decimal> _idestado;
        private Nullable<Decimal> _idperfil;
        private String _nombre;
        private Nullable<Decimal> _idgore;
        private String _gore;
        private Nullable<Decimal> _idperfilgore;
        #endregion

        #region propiedades publicas
        /// <summary>
        /// propiedad publica que almacena la clave primaria del objeto
        /// </summary>
        /// <remarks>miembro de la clave primaria de la tabla CB_USERS</remarks>
        public String Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public Int16 EmailConfirmed
        {
            get
            {
                return _emailconfirmed;
            }
            set
            {
                _emailconfirmed = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public Object PasswordHash
        {
            get
            {
                return _passwordhash;
            }
            set
            {
                _passwordhash = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public Object SecurityStamp
        {
            get
            {
                return _securitystamp;
            }
            set
            {
                _securitystamp = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public Object PhoneNumber
        {
            get
            {
                return _phonenumber;
            }
            set
            {
                _phonenumber = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public Nullable<Decimal> PhoneNumberConfirmed
        {
            get
            {
                return _phonenumberconfirmed;
            }
            set
            {
                _phonenumberconfirmed = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public Nullable<Decimal> TwoFactorEnabled
        {
            get
            {
                return _twofactorenabled;
            }
            set
            {
                _twofactorenabled = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public Nullable<DateTime> LockOutEndDateUtc
        {
            get
            {
                return _lockoutenddateutc;
            }
            set
            {
                _lockoutenddateutc = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public Nullable<Decimal> LockOutEnabled
        {
            get
            {
                return _lockoutenabled;
            }
            set
            {
                _lockoutenabled = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public Nullable<Decimal> AccessFailedCount
        {
            get
            {
                return _accessfailedcount;
            }
            set
            {
                _accessfailedcount = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String UserName
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public Nullable<Decimal> IdMinisterio
        {
            get
            {
                return _idministerio;
            }
            set
            {
                _idministerio = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public Nullable<Decimal> IdServicio
        {
            get
            {
                return _idservicio;
            }
            set
            {
                _idservicio = value;
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
        public Nullable<Decimal> IdPerfil
        {
            get
            {
                return _idperfil;
            }
            set
            {
                _idperfil = value;
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
        public Nullable<Decimal> IdGore
        {
            get
            {
                return _idgore;
            }
            set
            {
                _idgore = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public String Gore
        {
            get
            {
                return _gore;
            }
            set
            {
                _gore = value;
            }
        }

        /// <summary>
        /// propiedad publica del objeto
		/// </summary>
        public Nullable<Decimal> IdPerfilGore
        {
            get
            {
                return _idperfilgore;
            }
            set
            {
                _idperfilgore = value;
            }
        }
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        [DebuggerStepThrough]
        public TablaUsuariosDto()
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
            TablaUsuariosDto objDto = (TablaUsuariosDto)this.MemberwiseClone();
            return objDto;
        }
        #endregion
    }
}
