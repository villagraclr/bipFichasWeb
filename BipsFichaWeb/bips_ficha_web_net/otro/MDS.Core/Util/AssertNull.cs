using System;

namespace MDS.Core.Util
{
    /// <summary>
    /// clase de utilidad de arquitectura encargada de verificar la nulidad de los objetos
    /// </summary>
    public class AssertNull
    {
        #region metodos publicos estaticos
        /// <summary>
        /// Metodo estatico que permite validar que un objeto no sea null o vacio
        /// </summary>
        /// <param name="pObj">Objeto que se desea validar</param>
        public static void NotNullOrEmpty(Object pObj)
        {
            if (pObj == null)
                throw new Exception("PARAMETERS_NULL_OR_EMPTY");

            // Si el objeto es del tipo String
            if (pObj is String)
            {
                String objString = (String)pObj;
                if (objString.Equals(""))
                    throw new Exception("PARAMETERS_NULL_OR_EMPTY");

            }

            // Si el objeto es del tipo DateTime
            if (pObj is DateTime)
            {
                DateTime objDateTime = (DateTime)pObj;
                if (objDateTime.Equals(new DateTime()))
                    throw new Exception("PARAMETERS_NULL_OR_EMPTY");

            }
            //TODO: SE DEBE VALIDAR EL USO DE CERO EN TIPOS NUMERICOS.
            //// Si el objeto es del tipo Int16
            //if (pObj is Int16)
            //{
            //    Int16 objInt16 = (Int16)pObj;
            //    if (objInt16 == 0)
            //        throw new Exception("PARAMETERS_NULL_OR_EMPTY");

            //}

            //// Si el objeto es del tipo Int32
            //if (pObj is Int32)
            //{
            //    Int32 objInt32 = (Int32)pObj;
            //    if (objInt32 == 0)
            //        throw new Exception("PARAMETERS_NULL_OR_EMPTY");

            //}

            //// Si el objeto es del tipo Int64
            //if (pObj is Int64)
            //{
            //    Int64 objInt64 = (Int64)pObj;
            //    if (objInt64 == 0)
            //        throw new Exception("PARAMETERS_NULL_OR_EMPTY");

            //}

            //// Si el objeto es del tipo Decimal
            //if (pObj is Decimal)
            //{
            //    Decimal objDecimal = (Decimal)pObj;
            //    if (objDecimal == 0)
            //        throw new Exception("PARAMETERS_NULL_OR_EMPTY");
            //}
        }
        #endregion
    }
}
