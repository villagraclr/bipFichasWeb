using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using MDS.Core.Dto;

namespace MDS.Core.Base
{
    /// <summary>
    /// clase de lectura de la configuracion de las instancias de arquitectura
    /// </summary>
    internal class InstanceConfigurationReader
    {
        #region campos privados estaticos
        /// <summary>
        /// campo publico que permite almacenar la fecha de modificacion del archivo de configuracion
        /// </summary>
        private static DateTime dateModify = new DateTime();
        private static string pathFile;
        #endregion

        #region constructores
        /// <summary>
        /// constructor por defecto del objeto
        /// </summary>
        [DebuggerStepThrough]
        internal InstanceConfigurationReader()
        {
            string pathDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config");
            pathFile = System.IO.Path.Combine(pathDir, "instance.config");
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
            if (!System.IO.File.Exists(pathFile))
                throw new ArgumentException("FILE_INSTANCES_NOT_FOUND", pathFile);
            return result;
        }
        #endregion

        #region metodos privados
        private bool HasAttributes(XElement p_Element)
        {
            bool result = true;
            if (!p_Element.HasAttributes)
                throw new ArgumentException("FILE_INSTANCES_NODE_WITHOUT_ATTRIBUTES", "node " + p_Element.Name);
            return result;
        }

        private bool HasAttribute(XElement p_Element, string p_AttName)
        {
            bool result = true;
            XAttribute xAtt = p_Element.Attribute(p_AttName);
            if (xAtt == null || xAtt.Value.Trim().Length == 0)
                throw new ArgumentException("FILE_INSTANCES_ATTRIBUTE_WITHOUT_INFO", "node " + p_Element.Name + ", attribute " + p_AttName);
            return result;
        }

        private void CheckElement(XElement p_Element, params string[] p_AttName)
        {
            HasAttributes(p_Element);
            foreach (var attr in p_AttName)
            {
                HasAttribute(p_Element, attr);
            }
        }
        #endregion

        #region metodos internos
        /// <summary>
        /// metodo que permite leer la configuracion de las instancias
        /// </summary>
        /// <param name="p_InstanceConfigurations">diccionario que contiene la coleccion de instancias</param>
        internal void ReadConfiguration(Dictionary<String, InstanceConfigurationDto> p_InstanceConfigurations)
        {
            string errorKey = "";
            if (ExistFile())
            {
                dateModify = (new FileInfo(pathFile)).LastWriteTime;
                try
                {
                    XDocument xmlDoc = XDocument.Load(pathFile);
                    XElement nodeInstance = xmlDoc.Element("instances");
                    if (nodeInstance == null)
                        throw new ArgumentException("FILE_INSTANCES_WITHOUT_INFO", "node instances");
                    List<XElement> listInstances = nodeInstance.Elements("instance").ToList();
                    if (listInstances.Count == 0)
                        throw new ArgumentException("FILE_INSTANCES_WITHOUT_INFO", "nodes instance");
                    foreach (XElement dtoInstance in listInstances)
                    {
                        CheckElement(dtoInstance, new string[] { "name", "type", "assembly" });
                        string keyInstance = dtoInstance.Attribute("name").Value;
                        errorKey = keyInstance;
                        if (p_InstanceConfigurations.ContainsKey(keyInstance))
                            throw new ArgumentException("FILE_INSTANCES_DUPLICATE_INFO", "node instance, key " + keyInstance);
                        InstanceConfigurationDto dtoInstanceConfig = new InstanceConfigurationDto()
                        {
                            Name = dtoInstance.Attribute("name").Value,
                            Type = dtoInstance.Attribute("type").Value,
                            Assembly = dtoInstance.Attribute("assembly").Value
                        };
                        foreach (XElement dtoParam in dtoInstance.Elements("parameter"))
                        {
                            CheckElement(dtoParam, new string[] { "name", "value" });
                            string keyParam = dtoParam.Attribute("name").Value;
                            if (dtoInstanceConfig.Parameters.Where(dto => dto.Name == keyParam).Count() > 0)
                                throw new ArgumentException("FILE_INSTANCES_DUPLICATE_INFO", "node instance " + keyInstance + ", param " + keyParam);
                            dtoInstanceConfig.Parameters.Add(new ParameterConfigurationDto()
                            {
                                Name = dtoParam.Attribute("name").Value,
                                Value = dtoParam.Attribute("value").Value
                            });
                        }
                        p_InstanceConfigurations.Add(dtoInstanceConfig.Name, dtoInstanceConfig);
                    }
                }
                catch (XmlException ex)
                {
                    throw new XmlException("FILE_INSTANCES_BAD_XML_FORMAT", ex, ex.LineNumber, ex.LinePosition);
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
