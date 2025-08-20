using MDS.Core.Providers;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MDS.Core.Util
{
    /// <summary>
    /// clase de utilidad de arquitectura encargada de ejecutar tareas de comunes a los objetos de acceso a datos (DAO)
    /// </summary>
    public static class ExtensionDao
    {
        #region campos privados
        private static string nameDefaultFieldDataBase = "iProviderData";
        private static Type typeDefaultFieldDataBase = typeof(IProviderData);
        #endregion

        #region metodos publicos estaticos
        /// <summary>
        /// metodo que evalua el valor a registrar en el origen de datos dependiendo del valor contenido en el parametro
        /// </summary>
        /// <typeparam name="T">Tipo de negocio que realiza la llamada</typeparam>
        /// <param name="p_Caller">objeto de negocio de acceso a datos que realiza la llamada</param>
        /// <param name="p_Obj">valor a evaluar al momento de registrar en el origen de datos</param>
        /// <returns>retorna el valor a registrar en el origen de datos</returns>
        public static object GetValueToBD<T>(this T p_Caller, Object p_Obj)
        {
            object objReturn = DBNull.Value;
            if (p_Obj is System.String)
            {
                System.String objString = (System.String)p_Obj;
                if (!objString.Equals(""))
                {
                    objReturn = objString;
                }
            }
            else if (p_Obj is System.DateTime)
            {
                System.DateTime objDateTime = (System.DateTime)p_Obj;
                if (!objDateTime.Equals(new System.DateTime()))
                {
                    objReturn = objDateTime;
                }
            }
            else if (p_Obj is System.Decimal)
            {
                System.Decimal? objDecimal = (System.Decimal?)p_Obj;
                if (objDecimal != null)
                {
                    objReturn = objDecimal;
                }

            }
            else if (p_Obj is System.Int64)
            {
                System.Int64 objInt64 = (System.Int64)p_Obj;
                objReturn = objInt64;
            }
            else if (p_Obj is System.Int32)
            {
                System.Int32 objInt32 = (System.Int32)p_Obj;
                objReturn = objInt32;
            }
            else if (p_Obj is System.Int16)
            {
                System.Int16 objInt16 = (System.Int16)p_Obj;
                objReturn = objInt16;
            }
            else if (p_Obj is System.Byte || p_Obj is System.Byte[])
            {
                objReturn = p_Obj;
            }
            else if (p_Obj is System.Boolean)
            {
                objReturn = (bool)p_Obj;
            }
            else if (p_Obj is List<int>)
            {
                //retorna string separada por coma con la lista de trabajos realizados
                string result = "";
                foreach (int items in (List<int>)p_Obj)
                {
                    result += items + ",";
                }

                //remover ultima coma
                result = result.Remove(result.Length - 1);

                objReturn = result;
            }
            return objReturn;
        }

        /// <summary>
        /// metodo que evalua el valor a registrar en el origen de datos dependiendo del valor contenido en el parametro
        /// </summary>
        /// <typeparam name="T">Tipo de negocio que realiza la llamada</typeparam>
        /// <typeparam name="Y">tipo de objeto a evaluar</typeparam>
        /// <param name="p_Caller">objeto de negocio de acceso a datos que realiza la llamada</param>
        /// <param name="p_Obj">objeto que contiene la propiedad a evaluar</param>
        /// <param name="p_PropertyName">valor de propiedad a rescatar</param>
        /// <returns>retorna el valor a registrar en el origen de datos</returns>
        public static object GetValueToBD<T, Y>(this T p_Caller, Y p_Obj, string p_PropertyName)
        {
            object objReturn = DBNull.Value;
            if (p_Obj != null)
            {
                return p_Caller.GetValueToBD(GetProperty(p_Obj, p_PropertyName));
            }
            return objReturn;
        }

        /// <summary>
        /// metodo que evalua el valor a registrar en el origen de datos dependiendo del valor contenido en el parametro
        /// </summary>
        /// <typeparam name="T">Tipo de negocio que realiza la llamada</typeparam>
        /// <typeparam name="Y">tipo de objeto a evaluar</typeparam>
        /// <param name="p_Caller">objeto de negocio de acceso a datos que realiza la llamada</param>
        /// <param name="p_Obj">objeto que contiene la propiedad a evaluar</param>
        /// <param name="p_PropertyName">valor de propiedad a rescatar</param>
        /// <param name="p_DefaultValue">valor por defecto a asignar en caso de ser nulo el campo</param>
        /// <returns>retorna el valor a registrar en el origen de datos</returns>
        public static object GetValueToBD<T, Y>(this T p_Caller, Y p_Obj, string p_PropertyName, object p_DefaultValue)
        {
            object objReturn = p_DefaultValue;
            if (p_Obj != null)
            {
                return p_Caller.GetValueToBD(GetProperty(p_Obj, p_PropertyName));
            }
            return objReturn;
        }

        /// <summary>
        /// metodo que permite obtener el valor de una propiedad determinada de un objeto determinado
        /// </summary>
        /// <typeparam name="T">tipo de objeto</typeparam>
        /// <param name="p_Obj">objeto a evaluar</param>
        /// <param name="p_PropertyName">nombre de propiedad a evaluar</param>
        /// <returns>valor de la propiedad a evaluar</returns>
        private static object GetProperty<T>(T p_Obj, string p_PropertyName)
        {
            PropertyInfo propInfo = p_Obj.GetType().GetProperties().Where(prop => prop.Name == p_PropertyName).First();
            return propInfo.GetValue(p_Obj, null);
        }

        /// <summary>
        /// metodo que permite obtener el valor de una propiedad determinada de un objeto determinado
        /// </summary>
        /// <typeparam name="T">tipo de objeto</typeparam>
        /// <param name="p_Caller">objeto a evaluar</param>
        /// <param name="p_NameDB">nombre de base de datos a obtener</param>
        /// <returns>Instancia de objeto de base de datos</returns>
        public static Database GetConexion<T>(this T p_Caller, string p_NameDB)
        {
            Database db = null;
            FieldInfo fieldDataBaseProvider = typeof(T).GetField(ExtensionDao.nameDefaultFieldDataBase);
            if (fieldDataBaseProvider != null && fieldDataBaseProvider.FieldType == typeDefaultFieldDataBase)
            {
                try
                {
                    db = (Database)typeDefaultFieldDataBase.InvokeMember("GetConexion", BindingFlags.InvokeMethod, null, (IProviderData)fieldDataBaseProvider.GetValue(p_Caller), new Object[1] { p_NameDB });
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return db;
        }
        #endregion
    }
}
