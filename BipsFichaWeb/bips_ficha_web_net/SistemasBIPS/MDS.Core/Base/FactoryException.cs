using System;

namespace MDS.Core.Base
{
    /// <summary>
    /// clase encargada de gestionar las excepciones del sistema
    /// </summary>
    internal class FactoryException
    {
        #region campo estaticos privados
        private static FactoryException _instance = null;
        #endregion

        #region campos privados
        private const string policyName = "Policy";
        #endregion

        #region constructor
        private FactoryException()
        {
        }
        #endregion

        #region singleton
        /// <summary>
        /// metodo que permite obtener una instancia del objeto en memoria
        /// </summary>
        /// <returns></returns>
        internal static FactoryException GetInstance()
        {
            if (_instance == null)
                _instance = new FactoryException();
            return _instance;
        }
        #endregion

        #region metodos internos
        /// <summary>
        /// metodo que permite manejar la excepcion
        /// </summary>
        /// <param name="p_Ex">Excepcion a manejar</param>
        /// <returns>valor que indica si la excepcion debe ser relanzada o no</returns>
        internal bool HandleException(Exception p_Ex)
        {
            //bool reThrow = ExceptionPolicy.HandleException(p_Ex, policyName);
            return true;
        }

        /// <summary>
        /// metodo que permite manejar la excepcion
        /// </summary>
        /// <param name="p_Ex">excepcion a manejar</param>
        /// <param name="p_MsgWrapper">mensaje a contener por excepcion que envolvera la excepcion de origen</param>
        /// <returns>valor que indica si la excepcion debe ser relanzada o no</returns>
        internal Exception HandleException(Exception p_Ex, string p_MsgWrapper)
        {
            Exception reThrow;
            try
            {
                throw new Exception(p_MsgWrapper, p_Ex);
            }
            catch (Exception ex)
            {
                HandleException(ex);
                reThrow = ex;
            }
            return reThrow;
        }
        #endregion
    }
}
