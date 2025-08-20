using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace SistemasBIPS
{
    public class RouteValuesEncriptedValueProviderFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            return new RouteValuesEncriptedValueProvider(controllerContext);
        }
    }

    class RouteValuesEncriptedValueProvider : DictionaryValueProvider<string>
    {
        public RouteValuesEncriptedValueProvider(ControllerContext controllerContext) : base (GetRouteValueDictionary(controllerContext), Thread.CurrentThread.CurrentCulture)
        {
        }
        private static IDictionary<string, string> GetRouteValueDictionary(ControllerContext controllerContext)
        {
            var dict = new Dictionary<string, string>();
            foreach (var key in controllerContext.RouteData.Values.Keys.Where(x => x.First() == '_'))
            {
                var value = controllerContext.RouteData.GetRequiredString(key);
                var decripted = Models.EncriptaDatos.RijndaelSimple.Desencriptar(value);
                dict.Add(key.Substring(1), decripted);
            }
            return dict;
        }
    }
}