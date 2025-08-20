using MDS.Core.Enum;
using MDS.Core.Providers;
using System;
using System.Reflection;

namespace MDS.Core.Util
{
    /// <summary>
    /// clase de utilidad de arquitectura encargada de ejecutar tareas comunes sobre tipos de datos string
    /// </summary>
    public static class ExtensionString
    {
        #region metodos string
        /// <summary>
        /// metodo que retorna el contenido de una cadena sin sus espacios iniciales y finales, en caso de ser nulo retorna un valor nulo
        /// </summary>
        /// <param name="p_Cadena">cadena a evaluar</param>
        /// <returns>cadena sin espacios al comienzo y final</returns>
        public static string GetTrim(this string p_Cadena)
        {
            string retorno = null;
            if (!string.IsNullOrEmpty(p_Cadena) && !string.IsNullOrEmpty(p_Cadena.Trim()))
            {
                retorno = p_Cadena.Trim();
            }
            return retorno;
        }

        /// <summary>
        /// metodo que retorna el contenido de una cadena con sus letras en minusculas, en caso de ser nulo retorna un valor nulo
        /// </summary>
        /// <param name="p_Cadena">cadena a evaluar</param>
        /// <returns>cadena en minuscula y sin espacios al inicio y fin</returns>
        public static string GetLower(this string p_Cadena)
        {
            string retorno = null;
            if (!string.IsNullOrEmpty(p_Cadena))
            {
                retorno = p_Cadena.ToLower();
            }
            return retorno;
        }

        /// <summary>
        /// metodo que retorna el contenido de una cadena con sus letras en mayuscula, en caso de ser nulo retorna un valor nulo
        /// </summary>
        /// <param name="p_Cadena">cadena a evaluar</param>
        /// <returns>cadena en mayuscula y sin espacios al inicio y fin</returns>
        public static string GetUpper(this string p_Cadena)
        {
            string retorno = null;
            if (!string.IsNullOrEmpty(p_Cadena))
            {
                retorno = p_Cadena.ToUpper();
            }
            return retorno;
        }

        /// <summary>
        /// metodo que retorna el contenido de una cadena con sus letras en minusculas y eliminando los espacios existentes al inicio y final de la cadena, en caso de ser nulo retorna un valor nulo
        /// </summary>
        /// <param name="p_Cadena">cadena a evaluar</param>
        /// <returns>cadena en minuscula y sin espacios al inicio y fin</returns>
        public static string GetTrimAndLower(this string p_Cadena)
        {
            return p_Cadena.GetTrim().GetLower();
        }

        /// <summary>
        /// metodo que retorna el contenido de una cadena con sus letras en mayuscula y eliminando los espacios existentes al inicio y final de la cadena, en caso de ser nulo retorna un valor nulo
        /// </summary>
        /// <param name="p_Cadena">cadena a evaluar</param>
        /// <returns>cadena en minuscula y sin espacios al inicio y fin</returns>
        public static string GetTrimAndUpper(this string p_Cadena)
        {
            return p_Cadena.GetTrim().GetUpper();
        }

        /// <summary>
        /// metodo que retorna la cantidad de caracteres especificados de una cadena entregada
        /// </summary>
        /// <param name="p_Cadena">cadena a evaluar</param>
        /// <param name="p_Largo">cantidad de caracteres a obtener</param>
        /// <returns>cadena con la cantidad de caracteres indicados</returns>
        public static string GetSubString(this string p_Cadena, int p_Largo)
        {
            string retorno = p_Cadena.GetTrim();
            if (!string.IsNullOrEmpty(retorno) && retorno.Length > p_Largo)
            {
                retorno = retorno.Substring(0, p_Largo);
            }
            return retorno;
        }
        #endregion
    }

    /// <summary>
    /// clase de utilidad de arquitectura encargada de ejecutar tareas comunes de logging
    /// </summary>
    public static class ExtensionLog
    {
        #region campos privados
        private static string nameDefaultFieldLogger = "iProviderLog";
        private static Type typeDefaultFieldLogger = typeof(IProviderLog);
        #endregion

        #region metodos log
        /// <summary>
        /// metodo que permite registrar en log un mensaje de ejecucion por defecto
        /// </summary>
        /// <typeparam name="T">Tipo de negocio que realiza la llamada, debe contener un campo de tipo IBaseControl</typeparam>
        /// <param name="p_Caller">objeto de negocio que realiza la llamada</param>
        /// <param name="p_MethodNameFromLog">metodo desde donde se realiza el llamado</param>
        public static void LogEntry<T>(this T p_Caller, string p_MethodNameFromLog)
        {
            p_Caller.LogEntry(p_MethodNameFromLog, EnumMessagesEntry.Ejecucion_Ok.ToString());
        }

        /// <summary>
        /// metodo que permite registrar en log un mensaje de ejecucion por defecto
        /// </summary>
        /// <typeparam name="T">Tipo de negocio que realiza la llamada, debe contener un campo de tipo IBaseControl</typeparam>
        /// <param name="p_Caller">objeto de negocio que realiza la llamada</param>
        /// <param name="p_MethodNameFromLog">metodo desde donde se realiza el llamado</param>
        /// <param name="p_MessageEntry">enumeracion que especifica el mensaje a registrar en log</param>
        public static void LogEntry<T>(this T p_Caller, string p_MethodNameFromLog, EnumMessagesEntry p_MessageEntry)
        {
            p_Caller.LogEntry(p_MethodNameFromLog, p_MessageEntry.ToString());
        }

        /// <summary>
        /// metodo que permite registrar en log un mensaje de ejecucion por defecto
        /// </summary>
        /// <typeparam name="T">Tipo de negocio que realiza la llamada, debe contener un campo de tipo IBaseControl</typeparam>
        /// <param name="p_Caller">objeto de negocio que realiza la llamada</param>
        /// <param name="p_MethodNameFromLog">metodo desde donde se realiza el llamado</param>
        /// <param name="messageEntry">mensaje a registrar en log</param>
        public static void LogEntry<T>(this T p_Caller, string p_MethodNameFromLog, string messageEntry)
        {
            FieldInfo fieldLogger = typeof(T).GetField(ExtensionLog.nameDefaultFieldLogger);
            if (fieldLogger != null && fieldLogger.FieldType == typeDefaultFieldLogger)
            {
                string messageReg = p_Caller.GetType().FullName + " " + p_MethodNameFromLog + messageEntry;
                typeDefaultFieldLogger.InvokeMember("LogInfo", BindingFlags.InvokeMethod, null, (IProviderLog)fieldLogger.GetValue(p_Caller), new Object[1] { messageReg });
            }
        }
        #endregion
    }
}
