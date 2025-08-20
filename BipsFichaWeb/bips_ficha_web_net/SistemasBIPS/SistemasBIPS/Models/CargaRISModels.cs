using log4net;
using MDS.Core.Dto;
using MDS.Core.Enum;
using MDS.Core.Providers;
using MDS.Dto;
using MDS.Svc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SistemasBIPS.Models
{
    public class CargaRISModels
    {
        private static ISistemasBIPSSvc bips = null;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IProviderConstante constantes = null;

        #region Constructor
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public CargaRISModels()
        {
            bips = (ISistemasBIPSSvc)Activator.CreateInstance(typeof(SistemasBIPSSvc));
            constantes = (IProviderConstante)Activator.CreateInstance(typeof(ProviderConstante));
        }
        #endregion

        public Task<List<TablaProgramasDto>> getPanelCargaRIS(TablaProgramasFiltroDto programasRIS, string usuario)
        {
            List<TablaProgramasDto> data = new List<TablaProgramasDto>();
            try
            {
                ViewDto<TablaProgramasDto> buscarProgramas = new ViewDto<TablaProgramasDto>();
                buscarProgramas = bips.BuscarProgramas(new ContextoDto(), programasRIS, EnumAccionRealizar.BuscarPanelCargaRIS);
                if (buscarProgramas.HasElements())
                {
                    //Busca datos usuario conectado
                    ViewDto<TablaUsuariosDto> usuarioConectado = new ViewDto<TablaUsuariosDto>();
                    usuarioConectado = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = usuario });
                    if (usuarioConectado.HasElements())
                    {
                        data = buscarProgramas.Dtos;
                        if (usuarioConectado.Dtos.FirstOrDefault().IdPerfil != decimal.Parse(constantes.GetValue("PerfilAdmin")) && usuarioConectado.Dtos.FirstOrDefault().IdPerfil != decimal.Parse(constantes.GetValue("RolAnalistaMonitoreo")))
                        {
                            data.RemoveAll(TablaProgramasDto => TablaProgramasDto.IdServicio.IdParametro != usuarioConectado.Dtos.FirstOrDefault().IdServicio);
                        }
                    }
                }                    
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
                throw ex;
            }
            return Task.FromResult(data);
        }

        public async Task<string> envioBeneficiariosRIS(int idPrograma, string idUsuario, string rutaArchivos, HttpPostedFileBase beneficiarios, String justificacion, String tieneBeneficiarios, String tipoJustificacion, string usuario)
        {
            string registro = "ok";
            try
            {                
                //Envio archivo de beneficiarios                
                string rutaArchivo = string.Empty;
                //Busco datos del programa
                FormulariosModels formulario = new FormulariosModels();
                ViewDto<TablaProgramasDto> programa = new ViewDto<TablaProgramasDto>();
                programa = bips.BuscarProgramas(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idPrograma, Estado = decimal.Parse(constantes.GetValue("Activo")) }, EnumAccionRealizar.Buscar);
                string mailJefaturas = string.Empty;
                string nombre = string.Empty;
                decimal tamanoArchivo = 0;
                string nombreEncode = string.Empty;
                Random random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                if (tieneBeneficiarios == "Si")
                {
                    if (beneficiarios != null)
                    {
                        if (beneficiarios.ContentLength > 0)
                        {
                            tamanoArchivo = beneficiarios.ContentLength;
                            nombre = beneficiarios.FileName.Split('.')[0];
                            nombreEncode = new string(Enumerable.Repeat(chars, 10).Select(s => s[random.Next(s.Length)]).ToArray()) + "_" + DateTime.Now + "_" + programa.Dtos.SingleOrDefault().IdPrograma;//Convert.ToBase64String(Encoding.UTF8.GetBytes(nombre));
                            rutaArchivo = Path.Combine(rutaArchivos, Path.GetFileName(nombreEncode.Replace(":","-") + ".zip"));
                            if (File.Exists(rutaArchivo))
                                File.Delete(rutaArchivo);
                            beneficiarios.SaveAs(rutaArchivo);                            
                        }
                    }
                }
                //borrado
                ViewDto<TablaBenefiariosRisDto> borrado = new ViewDto<TablaBenefiariosRisDto>();
                borrado = bips.RegistrarBeneficiariosRis(new ContextoDto(), new TablaBenefiariosRisDto() { IdPrograma = programa.Dtos.SingleOrDefault().IdPrograma }, EnumAccionRealizar.Eliminar);
                if (!borrado.Sucess())
                    throw new Exception("error guardado (borrado)");
                //guardado
                ViewDto<TablaBenefiariosRisDto> guardado = new ViewDto<TablaBenefiariosRisDto>();
                guardado = bips.RegistrarBeneficiariosRis(new ContextoDto(), new TablaBenefiariosRisDto() { IdPrograma = programa.Dtos.SingleOrDefault().IdPrograma, NombreArchivo = nombre, TamanoArchivo = tamanoArchivo, CargaBeneficiarios = tieneBeneficiarios, Justificacion = decimal.Parse(tipoJustificacion), TextoJustificacion = justificacion, UsuarioCarga = usuario, FechaCarga = DateTime.Now, NombreEncode = nombreEncode }, EnumAccionRealizar.Insertar);
                if (!guardado.Sucess())
                    throw new Exception("error guardado");

                //Busca datos usuario conectado
                ViewDto<TablaUsuariosDto> usuarioConectado = new ViewDto<TablaUsuariosDto>();
                usuarioConectado = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = idUsuario });
                //Correo por defecto
                mailJefaturas = constantes.GetValue("CorreoBIPSRIS") + ",";
                //Busco correos sectorialistas
                ViewDto<TablaParametrosDto> correoSectorialistas = new ViewDto<TablaParametrosDto>();
                correoSectorialistas = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("CorreoSectorialistas")), Estado = decimal.Parse(constantes.GetValue("Activo")) });
                if (correoSectorialistas.HasElements())
                {
                    foreach (var item in correoSectorialistas.Dtos)
                    {
                        if (item.Valor == programa.Dtos.FirstOrDefault().IdServicio.IdParametro)
                        {
                            mailJefaturas += item.Descripcion + ",";
                        }
                    }
                }
                //Busco lista de correos DAIS
                ViewDto<TablaParametrosDto> correosDAIS = new ViewDto<TablaParametrosDto>();
                correosDAIS = bips.BuscarParametros(new ContextoDto(), new TablaParametrosFiltroDto() { IdCategoria = decimal.Parse(constantes.GetValue("CorreosDAIS")), Estado = decimal.Parse(constantes.GetValue("Activo")) });                
                if (correosDAIS.HasElements())
                {
                    foreach(var correo in correosDAIS.Dtos.Where(p=>p.IdParametro != p.IdCategoria))
                    {
                        mailJefaturas += correo.Descripcion + ",";
                    }
                }
                //Busco correos asociados al permiso del programa
                ViewDto<TablaProgramasDto> permisos = new ViewDto<TablaProgramasDto>();
                permisos = bips.BuscarPermisosFormularios(new ContextoDto(), new TablaProgramasFiltroDto() { IdPrograma = idPrograma, Estado = decimal.Parse(constantes.GetValue("Activo")) });
                if (permisos.HasElements())
                {
                    ViewDto<TablaUsuariosDto> permisoUser = new ViewDto<TablaUsuariosDto>();
                    List<String> listaCorreos = !String.IsNullOrEmpty(mailJefaturas) ? mailJefaturas.Split(',').ToList() : new List<string>();
                    foreach (var user in permisos.Dtos)
                    {
                        permisoUser = new ViewDto<TablaUsuariosDto>();
                        permisoUser = bips.BuscarUsuarios(new ContextoDto(), new TablaUsuariosFiltroDto() { Id = user.IdUser, IdEstado = decimal.Parse(constantes.GetValue("Activo")) });
                        if (permisoUser.HasElements())
                        {
                            if (permisoUser.Dtos.FirstOrDefault().Email != usuarioConectado.Dtos.SingleOrDefault().Email && listaCorreos.Count(p => p == permisoUser.Dtos.FirstOrDefault().Email) == 0 && permisoUser.Dtos.FirstOrDefault().IdPerfil == decimal.Parse(constantes.GetValue("PerfilContraparte")))
                            {
                                mailJefaturas += permisoUser.Dtos.FirstOrDefault().Email + ",";
                            }
                        }
                    }
                }
                //Se define mensaje a enviar
                string msj = string.Empty;
                //Envio de mail                
                DatosEmail dataMail = new DatosEmail();
                dataMail.de = usuarioConectado.Dtos.SingleOrDefault().Email;
                dataMail.para = mailJefaturas + usuarioConectado.Dtos.SingleOrDefault().Email;
                dataMail.asunto = "Sistema de Reporte de Monitoreo SES-DIPRES - Carga de personas beneficiarias";
                dataMail.imagen = true;
                dataMail.imagenDipres = false;
                string nombreProg = programa.Dtos.SingleOrDefault().Nombre;
                FormulariosModels prog = new FormulariosModels();
                string version = await prog.getRespuestasEvaluacion(int.Parse(constantes.GetValue("PreguntaVersionProgramas")), idPrograma);
                string msj1 = string.Format("Estimada(o) {0}<br/><br/>Se informa que ha realizado una carga del registro de personas beneficiarias del programa {1} en el Sistema de Reporte de Monitoreo SES-DIPRES.<br/><br/>ID BIPS: {2}<br/>Fecha de carga: {3}<br/>Iteración: {4}<br/><br/>Esta carga será revisada para determinar si cumple con el estándar de calidad del Gobierno de Datos del Ministerio de Desarrollo Social y Familia. En caso contrario, se le solicitará una nueva carga.<br/><br/>En caso de dudas o consultas, por favor contactar a asistencia.datos.bips.ris@desarrollosocial.gob.cl.", usuarioConectado.Dtos.FirstOrDefault().Nombre, nombreProg, programa.Dtos.FirstOrDefault().IdBips, DateTime.Now, version);
                string msj2 = "Saluda atentamente,<br/>Subsecretaría de Evaluación Social<br/>Ministerio de Desarrollo Social y Familia";
                dataMail.mensaje = string.Format(new CuerpoEmail().emailHtmlSeguimiento, msj1, msj2);                                
                EvaluacionExAnteModels envioEmail = new EvaluacionExAnteModels();
                Boolean estadoUsuario = true;//envioEmail.enviaMail(dataMail);
                if (!estadoUsuario)
                    throw new Exception("error envio mail de carga de beneficiarios bips-ris");
            }
            catch (Exception ex)
            {
                registro = ex.Message;
                log.Error(ex.Message, ex);
            }
            return (registro);
        }
    }
}