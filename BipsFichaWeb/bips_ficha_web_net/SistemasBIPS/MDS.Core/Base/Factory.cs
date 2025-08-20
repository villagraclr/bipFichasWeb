namespace MDS.Core.Base
{
    /// <summary>
    /// clase de factoria de arquitectura
    /// </summary>
    internal class Factory
    {
        #region propiedades estaticas publicas
        /// <summary>
        /// factoria encargada de la administracion de instances del sistema
        /// </summary>
        internal static FactoryInstance Instances
        {
            get
            {
                return FactoryInstance.GetInstance();
            }
        }

        /// <summary>
        /// factoria encargada de la administracion de peticiones de logs
        /// </summary>
        internal static FactoryLog Logs
        {
            get
            {
                return FactoryLog.GetInstance();
            }
        }

        /// <summary>
        /// factoria encargada de la administracion de excepciones
        /// </summary>
        internal static FactoryException Exceptiones
        {
            get
            {
                return FactoryException.GetInstance();
            }
        }

        /// <summary>
        /// factoria encargada de la administracion de errores del sistema
        /// </summary>
        internal static FactoryError Errores
        {
            get
            {
                return FactoryError.GetInstance();
            }
        }

        /// <summary>
        /// factoria encargada de la administracion de constantes del sistema
        /// </summary>
        internal static FactoryConstante Constantes
        {
            get
            {
                return FactoryConstante.GetInstance();
            }
        }

        /// <summary>
        /// factoria encargada de la administracion de base de datos
        /// </summary>
        internal static FactoryDataBase DataBase
        {
            get
            {
                return FactoryDataBase.GetInstance();
            }
        }
        #endregion

        #region constructor
        /// <summary>
        /// constructor privado por defecto del objeto, permite leer, configurar y cargar las instancias en memoria
        /// </summary>
        static Factory()
        {
        }
        #endregion
    }
}
