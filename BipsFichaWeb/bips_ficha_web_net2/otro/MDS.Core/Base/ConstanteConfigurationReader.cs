using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace MDS.Core.Base
{
    /// <summary>
    /// clase de lectura de las constantes paramtericas del sistema
    /// </summary>
    internal class ConstanteConfigurationReader
    {
        #region campos privados estaticos
        private static DateTime dateModify = new DateTime();
        private static string pathFile;
        private static string nameFile = "Constantes.config";
        private static string nameFolder = "Config";
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        [DebuggerStepThrough]
        internal ConstanteConfigurationReader()
        {
            string pathDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nameFolder);
            pathFile = Path.Combine(pathDir, nameFile);
        }
        #endregion

        #region metodos estaticos privados
        /// <summary>
        /// metodo que permite determinar si el archivo de instancias existe
        /// </summary>
        /// <returns>valor indicando si existe el archivo de instancias</returns>
        private static bool ExistFile()
        {
            bool result = true;
            if (!File.Exists(pathFile))
                throw new ArgumentException("ARCHIVO_CONSTANTES_NO_ENCONTRADO", pathFile);
            return result;
        }
        #endregion

        #region metodos privados
        /// <summary>
        /// Verifica si elemento tiene información
        /// </summary>
        /// <param name="p_Element"></param>
        /// <returns></returns>
        private bool HasElements(XElement p_Element)
        {
            bool result = true;
            if (!p_Element.HasElements)
                throw new ArgumentException("NODO_CONSTANTES_SIN_ATRIBUTOS", "node " + p_Element.Name);
            return result;
        }

        /// <summary>
        /// Verifica si elemento tiene información
        /// </summary>
        /// <param name="p_Element"></param>
        /// <param name="p_ElementName"></param>
        /// <returns></returns>
        private bool HasElement(XElement p_Element, string p_ElementName)
        {
            bool result = true;
            XElement xElement = p_Element.Element(p_ElementName);
            if (xElement == null || xElement.Value.Trim().Length == 0)
                throw new ArgumentException("ATRIBUTO_CONSTANTES_SIN_INFORMACION", "node " + p_Element.Name + ", attribute " + p_ElementName);
            return result;
        }

        /// <summary>
        /// Chequea elementos del archivo xml
        /// </summary>
        /// <param name="p_Element"></param>
        /// <param name="p_ElementsName"></param>
        private void CheckElement(XElement p_Element, params string[] p_ElementsName)
        {
            HasElements(p_Element);
            foreach (var element in p_ElementsName)
            {
                HasElement(p_Element, element);
            }
        }
        #endregion

        #region metodos internos
        /// <summary>
        /// metodo que permite leer la configuracion de las constantes
        /// </summary>
        /// <param name="p_Constantes">diccionario que contiene la coleccion de constantes</param>
        internal void ReadConfiguration(Dictionary<String, String> p_Constantes)
        {
            if (ExistFile())
            {
                dateModify = (new FileInfo(pathFile)).LastWriteTime;
                try
                {
                    XDocument xmlDoc = XDocument.Load(pathFile);
                    XElement nodeRoot = xmlDoc.Element("constantes");
                    if (nodeRoot == null)
                        throw new ArgumentException("ARCHIVO_CONSTANTES_SIN_INFORMACION", "node constantes");
                    List<XElement> listChilds = nodeRoot.Elements("value").ToList();
                    if (listChilds.Count == 0)
                        throw new ArgumentException("ARCHIVO_CONSTANTES_SIN_INFORMACION", "nodes value");
                    foreach (XElement dtoChild in listChilds)
                    {
                        CheckElement(dtoChild, new string[] { "clave", "valor" });
                        string keyChild = dtoChild.Element("clave").Value;
                        if (p_Constantes.ContainsKey(keyChild))
                            throw new ArgumentException("ARCHIVO_CONSTANTES_INFORMACION_DUPLICADA", "node value, key " + keyChild);
                        p_Constantes.Add(keyChild, dtoChild.Element("valor").Value);
                    }
                }
                catch (XmlException ex)
                {
                    throw new XmlException("ARCHIVO_CONSTANTES_FORMATO_XML_INCORRECTO", ex, ex.LineNumber, ex.LinePosition);
                }
            }
        }
        #endregion

        #region metodos internos estaticos
        /// <summary>
        /// metodo que permite verificar si el archivo de configuracion se ha modificado desde el ultimo acceso realizado
        /// </summary>
        /// <returns>valor booleano indicado si se encuentra modificado</returns>
        internal static bool IsModify()
        {
            bool result = false;
            if (ExistFile())
            {
                FileInfo fileXml = new FileInfo(pathFile);
                if (!dateModify.Equals(fileXml.LastWriteTime))
                {
                    result = true;
                }
            }
            return result;
        }
        #endregion
    }
}
