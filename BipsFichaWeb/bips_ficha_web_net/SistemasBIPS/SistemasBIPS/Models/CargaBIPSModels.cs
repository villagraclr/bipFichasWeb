using log4net;
using MDS.Core.Providers;
using MDS.Svc;
using System;

namespace SistemasBIPS.Models
{
    public class CargaBIPSModels
    {
        private static ISistemasBIPSSvc bips = null;

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public CargaBIPSModels()
        {
            bips = (SistemasBIPSSvc)Activator.CreateInstance(typeof(SistemasBIPSSvc));
        }
    }
}