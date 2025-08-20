using MDS.Core.Dto;
using MDS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MDS.Dao
{
    public partial class TablaPreguntasFormulariosDao
    {
        #region campos privados
        #endregion

        #region campos publicos
        #endregion

        #region constructores
        #endregion

        #region metodos privados
        private static Dictionary<string, object> getElementosPreguntas(object obj, MemberTypes tipoElemento)
        {
            try
            {
                Dictionary<string, object> elementos = new Dictionary<string, object>();
                var dato = from i in obj.GetType().GetMembers()
                           where ((i.MemberType == tipoElemento) && (i != null))
                           select new KeyValuePair<string, object>(((PropertyInfo)i).Name, ((PropertyInfo)i).GetValue(obj, null));
                foreach (KeyValuePair<string, object> kvp in dato)
                    elementos.Add(kvp.Key, kvp.Value);

                return elementos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private PreguntasFormulariosDto agregaPregunta(Object pregunta, IList<PreguntasFormulariosDto> preguntasGrupos, IList<PreguntasFormulariosDto> preguntasTablas, IList<TablaExcepcionesPreguntasDto> excepPreguntas)
        {
            PreguntasFormulariosDto data = new PreguntasFormulariosDto();
            TablaParametrosDao parametros = new TablaParametrosDao();
            try
            {
                Dictionary<string, object> elementos = new Dictionary<string, object>();
                elementos = getElementosPreguntas(pregunta, MemberTypes.Property);

                ViewDto<TablaParametrosDto> valores = new ViewDto<TablaParametrosDto>();
                ViewDto<TablaParametrosDto> valores_funcion = new ViewDto<TablaParametrosDto>();
                var valor = elementos.SingleOrDefault(p => p.Key == "Valores").Value;
                if (valor != null)
                {
                    TablaParametrosFiltroDto datos = new TablaParametrosFiltroDto();
                    datos.Estado = decimal.Parse(constantes.GetValue("Activo"));
                    if (valor.ToString() == constantes.GetValue("Tab") || valor.ToString() == constantes.GetValue("Tabla"))
                        datos.IdParametro = decimal.Parse(valor.ToString());
                    else
                        datos.IdCategoria = decimal.Parse(valor.ToString());
                    valores = parametros.Buscar(new ContextoDto(), datos);
                }
                var valFuncion = elementos.SingleOrDefault(p => p.Key == "ValorFuncion").Value;
                if (valFuncion != null)
                {
                    var idFunc = elementos.SingleOrDefault(p => p.Key == "IdFuncion").Value;
                    if (idFunc.ToString() == constantes.GetValue("LargoTexto"))
                        valores_funcion = new ViewDto<TablaParametrosDto>(new TablaParametrosDto() { IdParametro = decimal.Parse(idFunc.ToString()), Valor = decimal.Parse(valFuncion.ToString()) });
                    else
                        valores_funcion = parametros.Buscar(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(valFuncion.ToString()), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                }
                var funcValor = elementos.SingleOrDefault(p => p.Key == "FuncionValor").Value;
                var funcValor2 = elementos.SingleOrDefault(p => p.Key == "FuncionValor2").Value;
                if (funcValor != null && funcValor2 != null)
                    valores_funcion = new ViewDto<TablaParametrosDto>(new TablaParametrosDto() { IdParametro = decimal.Parse(elementos.SingleOrDefault(p => p.Key == "IdFuncion").Value.ToString()), Valor = decimal.Parse(funcValor.ToString()), Valor2 = decimal.Parse(funcValor2.ToString()) });

                if (elementos.ContainsKey("IdPreguntaFormulario"))
                    data.id = decimal.Parse(elementos.SingleOrDefault(p => p.Key == "IdPreguntaFormulario").Value.ToString());
                else if (elementos.ContainsKey("IdPreguntaTabla"))
                    data.id = decimal.Parse(elementos.SingleOrDefault(p => p.Key == "IdPregunta").Value.ToString());

                if (elementos.ContainsKey("IdPreguntaGrupo"))
                    data.menuGrupo = decimal.Parse(elementos.SingleOrDefault(p => p.Key == "IdMenu").Value.ToString());

                data.IdTabla = elementos.ContainsKey("IdTabla") ? decimal.Parse(elementos.SingleOrDefault(p => p.Key == "IdTabla").Value.ToString()) : (decimal?)null;
                data.idPregunta = elementos.ContainsKey("IdPregunta") ? decimal.Parse(elementos.SingleOrDefault(p => p.Key == "IdPregunta").Value.ToString()) : (decimal?)null;
                data.pregunta = elementos.ContainsKey("Pregunta") ? elementos.SingleOrDefault(p => p.Key == "Pregunta").Value.ToString() : string.Empty;
                data.fila = elementos.ContainsKey("Fila") ? decimal.Parse(elementos.SingleOrDefault(p => p.Key == "Fila").Value.ToString()) : (decimal?)null;
                data.columna = elementos.ContainsKey("Columna") ? decimal.Parse(elementos.SingleOrDefault(p => p.Key == "Columna").Value.ToString()) : (decimal?)null;
                data.menu = elementos.ContainsKey("TipoMenu") ? decimal.Parse(elementos.SingleOrDefault(p => p.Key == "TipoMenu").Value.ToString()) : (decimal?)null;
                data.tipoFormulario = new TablaParametrosDto()
                {
                    IdParametro = elementos.ContainsKey("IdTipoFormulario") ? decimal.Parse(elementos.SingleOrDefault(p => p.Key == "IdTipoFormulario").Value.ToString()) : (decimal?)null,
                    Descripcion = elementos.ContainsKey("TipoFormulario") ? elementos.SingleOrDefault(p => p.Key == "TipoFormulario").Value.ToString() : string.Empty
                };
                data.tipoPregunta = new TablaParametrosDto()
                {
                    IdParametro = elementos.ContainsKey("IdTipoPregunta") ? decimal.Parse(elementos.SingleOrDefault(p => p.Key == "IdTipoPregunta").Value.ToString()) : (decimal?)null,
                    Descripcion = elementos.ContainsKey("TipoPregunta") ? elementos.SingleOrDefault(p => p.Key == "TipoPregunta").Value.ToString() : string.Empty,
                    IdCategoria = elementos.ContainsKey("IdCategoriaPregunta") ? decimal.Parse(elementos.SingleOrDefault(p => p.Key == "IdCategoriaPregunta").Value.ToString()) : (decimal?)null,
                    Valor = elementos.ContainsKey("TipoPreguntaValor") ? (elementos.SingleOrDefault(p => p.Key == "TipoPreguntaValor").Value != null ? decimal.Parse(elementos.SingleOrDefault(p => p.Key == "TipoPreguntaValor").Value.ToString()) : (decimal?)null) : (decimal?)null,
                    Valor2 = elementos.ContainsKey("TipoPreguntaValor2") ? (elementos.SingleOrDefault(p => p.Key == "TipoPreguntaValor2").Value != null ? decimal.Parse(elementos.SingleOrDefault(p => p.Key == "TipoPreguntaValor2").Value.ToString()) : (decimal?)null) : (decimal?)null
                };
                data.valores = (valores.HasElements() ? valores.Dtos.OrderBy(p => p.Orden).ToList() : new List<TablaParametrosDto>());
                data.funcion = new TablaParametrosDto()
                {
                    IdParametro = elementos.ContainsKey("IdFuncion") ? decimal.Parse(elementos.SingleOrDefault(p => p.Key == "IdFuncion").Value.ToString()) : (decimal?)null,
                    Descripcion = elementos.ContainsKey("Funcion") ? elementos.SingleOrDefault(p => p.Key == "Funcion").Value.ToString() : string.Empty,
                    Valor = elementos.ContainsKey("FuncionValor") ? (elementos.SingleOrDefault(p => p.Key == "FuncionValor").Value != null ? decimal.Parse(elementos.SingleOrDefault(p => p.Key == "FuncionValor").Value.ToString()) : (decimal?)null) : (decimal?)null,
                    Valor2 = elementos.ContainsKey("FuncionValor2") ? (elementos.SingleOrDefault(p => p.Key == "FuncionValor2").Value != null ? decimal.Parse(elementos.SingleOrDefault(p => p.Key == "FuncionValor2").Value.ToString()) : (decimal?)null) : (decimal?)null
                };
                data.valor_funcion = (valores_funcion.HasElements() ? valores_funcion.Dtos.OrderBy(p => p.Orden).ToList() : new List<TablaParametrosDto>());
                data.preguntasGrupos = (preguntasGrupos.Count > 0 ? preguntasGrupos : new List<PreguntasFormulariosDto>());
                data.preguntasTablas = (preguntasTablas.Count > 0 ? preguntasTablas : new List<PreguntasFormulariosDto>());
                data.soloLectura = (excepPreguntas.Count(p => p.IdPregunta == data.idPregunta) > 0 ? true : false);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return data;
        }        
        #endregion

        #region metodos publicos
        #endregion
    }
}
