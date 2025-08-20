using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using MDS.Core.Dto;
using MDS.Core.Enum;

namespace MDS.Core.Base
{
    /// <summary>
    /// clase de lectura de los mensajes de errores paramtericos del sistema
    /// </summary>
    internal class ErrorConfigurationReader
    {
        #region campos privados estaticos
        /// <summary>
        /// campo publico que permite almacenar la fecha de modificacion del archivo de configuracion
        /// </summary>
        private static DateTime dateModify = new DateTime();
        private static string pathFile;
        private static string nameFolder = "config";
        private static string nameFile = "errores.config";
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        [DebuggerStepThrough]
        internal ErrorConfigurationReader()
        {
            string pathDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nameFolder);
            pathFile = System.IO.Path.Combine(pathDir, nameFile);
        }
        #endregion

        #region metodos estaticos privados
        /// <summary>
        /// metodo que permite determinar si el archivo de errores existe
        /// </summary>
        /// <returns>valor indicando si existe el archivo de instancias</returns>
        private static bool ExistFile()
        {
            bool result = true;
            if (!System.IO.File.Exists(pathFile))
                throw new ArgumentException("FILE_ERRORES_NOT_FOUND", pathFile);
            return result;
        }
        #endregion

        #region metodos privados
        private bool HasElements(XElement p_Element)
        {
            bool result = true;
            if (!p_Element.HasElements)
                throw new ArgumentException("FILE_ERRORES_NODE_WITHOUT_ELEMENTS", "node " + p_Element.Name);
            return result;
        }

        private bool HasElement(XElement p_Element, string p_ElementName)
        {
            bool result = true;
            XElement xElement = p_Element.Element(p_ElementName);
            if (xElement == null || xElement.Value.Trim().Length == 0)
                throw new ArgumentException("FILE_ERRORES_ELEMENT_WITHOUT_INFO", "node " + p_Element.Name + ", attribute " + p_ElementName);
            return result;
        }

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
        /// metodo que permite leer la configuracion de las instancias
        /// </summary>
        /// <param name="p_Dict">diccionario que contiene la coleccion de errores</param>
        internal void ReadConfiguration(Dictionary<String, ErrorDto> p_Dict)
        {
            if (ExistFile())
            {
                dateModify = (new FileInfo(pathFile)).LastWriteTime;
                try
                {
                    XDocument xmlDoc = XDocument.Load(pathFile);
                    XElement nodeRoot = xmlDoc.Element("errores");
                    if (nodeRoot == null)
                        throw new ArgumentException("FILE_ERRORES_WITHOUT_INFO", "node errores");
                    List<XElement> listChilds = nodeRoot.Elements("error").ToList();
                    if (listChilds.Count == 0)
                        throw new ArgumentException("FILE_ERRORES_WITHOUT_INFO", "nodes error");
                    foreach (XElement dtoChild in listChilds)
                    {
                        CheckElement(dtoChild, new string[] { "codigo", "severidad", "descripcion" });
                        string keyChild = dtoChild.Element("codigo").Value;
                        if (p_Dict.ContainsKey(keyChild))
                            throw new ArgumentException("FILE_ERRORES_DUPLICATE_INFO", "node error, key " + keyChild);
                        string textSeveridadChild = dtoChild.Element("severidad").Value;
                        EnumSeveridad severidadChild;
                        try
                        {
                            severidadChild = (EnumSeveridad)System.Enum.Parse(typeof(EnumSeveridad), textSeveridadChild, true);
                        }
                        catch
                        {
                            throw new ArgumentException("FILE_ERRORES_SEVERITY_WRONG", "node error, key " + keyChild);
                        }
                        ErrorDto dtoError = new ErrorDto()
                        {
                            Codigo = keyChild,
                            Severidad = severidadChild,
                            Mensaje = dtoChild.Element("descripcion").Value
                        };
                        p_Dict.Add(keyChild, dtoError);
                    }
                }
                catch (XmlException ex)
                {
                    throw new XmlException("FILE_ERRORES_BAD_XML_FORMAT", ex, ex.LineNumber, ex.LinePosition);
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