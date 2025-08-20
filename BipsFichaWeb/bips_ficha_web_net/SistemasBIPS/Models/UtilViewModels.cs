using MDS.Dto;
using System.Collections.Generic;

namespace SistemasBIPS.Models
{
    /// <summary>
    /// Clase que arma un ministerio y todos sus servicios asociados
    /// </summary>
    public class MinisterioServicios
    {
        public TablaParametrosDto Ministerios { get; set; }
        public IList<TablaParametrosDto> Servicios { get; set; }
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public MinisterioServicios()
        {
            this.Ministerios = new TablaParametrosDto();
            this.Servicios = new List<TablaParametrosDto>();
        }
    }

    /// <summary>
    /// Clase que arma vista de acceso
    /// </summary>
    public class AccesoRestringido
    {
        public string Vista { get; set; }
    }

    public class CuerpoEmail
    {
        private string _emailHtml = "<body bgcolor='#D8D8D8' lang=ES-CL link=blue vlink=purple>" + 
                                    "<div align='center'>" + 
                                    "<table style='background:white;width:600px;border: solid 1px #989898;box-shadow: 0px 0px 3px #999;-webkit-box-shadow: 0px 0px 3px #999;-moz-box-shadow: 0px 0px 3px #999;filter: shadow(color=#999999, direction=0, strength=3);border-radius: 6px;-webkit-border-radius: 6px;-moz-border-radius: 6px;'>" +
                                    "<tr><td>" + 
                                    "<table style='margin-left:15px;margin-right:15px;'><tr><td style='height:10px;'>" +
                                    "</td></tr>" +
                                    "<tr><td style='font-family: Arial,sans-serif;font-size: 12px;'>{0}</td></tr>" +
                                    "<tr><td style='height:5px;'></td></tr>" +
                                    "<tr><td style='font-family: Arial,sans-serif;font-size: 12px;'>{1}</td></tr><tr><td style='height:5px;'></td></tr>" +
                                    "<tr><td style='font-family: Arial,sans-serif;font-size: 12px;'>{2}</td></tr>" +
                                    "<tr><td style='height:15px;'></td></tr>" +
                                    "<tr><td style='font-family: Arial,sans-serif;font-size: 12px;margin-top:10px;'>Equipo de Monitoreo de Programas de la Oferta Pública</td></tr>" +
                                    "<tr><td style='height:10px;'></td></tr>" +
                                    "<tr><td><img src=cid:logoMDS id='imgMDS' alt='Ministerio de Desarrollo Social y Familia' width='161px' height='146px'></td></tr>" +
                                    "<tr><td style='height:10px;border-bottom: solid 2px #989898;'></td></tr>" +
                                    "<tr><td style='height:10px;'></td></tr>" +
                                    "<tr><td><span lang='ES' style='font-size:10.0pt;font-family:Arial,sans-serif;color:navy'>Subsecretaría de Evaluación Social - Dirección de Presupuestos - Gobierno de Chile</span></td></tr>" +
                                    "<tr><td style='height:10px;'></td></tr>" +
                                    "<tr><td><span lang='ES' style='font-size:7.0pt;font-family:Arial,sans-serif;color:#1F497D'>Este documento y sus adjuntos pueden contener información privilegiada y/o confidencial. Si usted no es el destinatario del mismo o si por error lo recibió, por favor remítalo a esta misma dirección y bórrelo permanentemente de su sistema. La correspondencia y la información privilegiada están protegidas por Tratados Internacionales, la Constitución y la Ley. La manipulación indebida está penada por la Ley. <br>Muchas Gracias.</span></td></tr>" +
                                    "<tr><td style='height:10px;'></td></tr>" +
                                    "<tr><td><span lang='EN-US' style='font-size:7.0pt;font-family:Arial,sans-serif;color:#1F497D'>This document and its attachments may contain privileged or classified information. If you are not the addressee or you misperceive this e-mail, please resend it to the sender´s e-mail and proceed to delete it permanently from your computer. Private letters and confidential information are protected by International Treaties, the National Constitution and the Law. Tampering is severely punished by law. <br>Thank you</span></td></tr>" +
                                    "</table>" + 
                                    "</td></tr>" +
                                    "<tr><td style='height:10px;'></td></tr>" +
                                    "</table>" +
                                    "</div>" +
                                    "</body>";
        public string emailHtml { get { return _emailHtml; } }

        private string _emailHtmlEvaluacion = "<body bgcolor='#D8D8D8' lang=ES-CL link=blue vlink=purple>" +
                                    "<div align='center'>" +
                                    "<table style='background:white;width:600px;border: solid 1px #989898;box-shadow: 0px 0px 3px #999;-webkit-box-shadow: 0px 0px 3px #999;-moz-box-shadow: 0px 0px 3px #999;filter: shadow(color=#999999, direction=0, strength=3);border-radius: 6px;-webkit-border-radius: 6px;-moz-border-radius: 6px;'>" +
                                    "<tr><td>" +
                                    "<table style='margin-left:15px;margin-right:15px;'><tr><td style='height:10px;'>" +
                                    "</td></tr>" +
                                    "<tr><td style='font-family: Arial,sans-serif;font-size: 12px;'>{0}</td></tr>" +
                                    "<tr><td style='height:5px;'></td></tr>" +
                                    "<tr><td style='font-family: Arial,sans-serif;font-size: 12px;'>{1}</td></tr><tr><td style='height:5px;'></td></tr>" +
                                    "<tr><td style='height:10px;'></td></tr>" +                                                                        
                                    "<tr><td><img src=cid:logoMDS id='imgMDS' alt='Ministerio de Desarrollo Social y Familia' width='161px' height='146px'></td></tr>" +
                                    "<tr><td style='height:10px;border-bottom: solid 2px #989898;'></td></tr>" +
                                    "<tr><td style='height:10px;'></td></tr>" +
                                    "<tr><td><span lang='ES' style='font-size:10.0pt;font-family:Arial,sans-serif;color:navy'>Subsecretaría de Evaluación Social - Dirección de Presupuestos - Gobierno de Chile</span></td></tr>" +
                                    "<tr><td style='height:10px;'></td></tr>" +
                                    "<tr><td><span lang='ES' style='font-size:7.0pt;font-family:Arial,sans-serif;color:#1F497D'>Este documento y sus adjuntos pueden contener información privilegiada y/o confidencial. Si usted no es el destinatario del mismo o si por error lo recibió, por favor remítalo a esta misma dirección y bórrelo permanentemente de su sistema. La correspondencia y la información privilegiada están protegidas por Tratados Internacionales, la Constitución y la Ley. La manipulación indebida está penada por la Ley. <br>Muchas Gracias.</span></td></tr>" +
                                    "<tr><td style='height:10px;'></td></tr>" +
                                    "<tr><td><span lang='EN-US' style='font-size:7.0pt;font-family:Arial,sans-serif;color:#1F497D'>This document and its attachments may contain privileged or classified information. If you are not the addressee or you misperceive this e-mail, please resend it to the sender´s e-mail and proceed to delete it permanently from your computer. Private letters and confidential information are protected by International Treaties, the National Constitution and the Law. Tampering is severely punished by law. <br>Thank you</span></td></tr>" +
                                    "</table>" +
                                    "</td></tr>" +
                                    "<tr><td style='height:10px;'></td></tr>" +
                                    "</table>" +
                                    "</div>" +
                                    "</body>";

        public string emailHtmlEvaluacion { get { return _emailHtmlEvaluacion; } }

        private string _emailHtmlSeguimiento = "<body bgcolor='#D8D8D8' lang=ES-CL link=blue vlink=purple>" +
                                    "<div align='center'>" +
                                    "<table style='background:white;width:600px;border: solid 1px #989898;box-shadow: 0px 0px 3px #999;-webkit-box-shadow: 0px 0px 3px #999;-moz-box-shadow: 0px 0px 3px #999;filter: shadow(color=#999999, direction=0, strength=3);border-radius: 6px;-webkit-border-radius: 6px;-moz-border-radius: 6px;'>" +
                                    "<tr><td>" +
                                    "<table style='margin-left:15px;margin-right:15px;'><tr><td colspan='2' style='height:10px;'></td></tr>" +
                                    "<tr><td colspan='2' style='font-family: Arial,sans-serif;font-size: 12px;'>{0}</td></tr>" +
                                    "<tr><td colspan='2' style ='height:5px;'></td></tr>" +
                                    "<tr><td colspan='2' style='font-family: Arial,sans-serif;font-size: 12px;'>{1}</td></tr><tr><td colspan='2' style='height:5px;'></td></tr>" +
                                    "<tr><td colspan='2' style='height:10px;'></td></tr>" +
                                    "<tr><td style='width:35%'><img src=cid:logoSES id='imgMDS' alt='Ministerio de Desarrollo Social y Familia' width='161px' height='146px'></td><td><img src=cid:logo_DIPRES id='imgDPS' alt='Dirección de Presupuestos' width='161px' height='146px'></td></tr>" +
                                    "<tr><td colspan='2' style='height:10px;border-bottom: solid 2px #989898;'></td></tr>" +
                                    "<tr><td colspan='2' style='height:10px;'></td></tr>" +
                                    "<tr><td colspan='2'><span lang='ES' style='font-size:10.0pt;font-family:Arial,sans-serif;color:navy'>Subsecretaría de Evaluación Social - Dirección de Presupuestos - Gobierno de Chile</span></td></tr>" +
                                    "<tr><td colspan='2' style='height:10px;'></td></tr>" +
                                    "<tr><td colspan='2'><span lang='ES' style='font-size:7.0pt;font-family:Arial,sans-serif;color:#1F497D'>Este documento y sus adjuntos pueden contener información privilegiada y/o confidencial.Si usted no es el destinatario del mismo o si por error lo recibió, por favor remítalo a esta misma dirección y bórrelo permanentemente de su sistema.La correspondencia y la información privilegiada están protegidas por Tratados Internacionales, la Constitución y la Ley.La manipulación indebida está penada por la Ley. <br>Muchas Gracias.</span></td></tr>" +
                                    "<tr><td colspan='2' style='height:10px;'></td></tr>" +
                                    "<tr><td colspan='2'><span lang='EN-US' style='font-size:7.0pt;font-family:Arial,sans-serif;color:#1F497D'> This document and its attachments may contain privileged or classified information. If you are not the addressee or you misperceive this e-mail, please resend it to the sender´s e-mail and proceed to delete it permanently from your computer. Private letters and confidential information are protected by International Treaties, the National Constitution and the Law.Tampering is severely punished by law. <br>Thank you</span></td></tr>" +
                                    "</table>" +
                                    "</td></tr>" +
                                    "<tr><td style='height:10px;'></td></tr></table></div></body>";

        public string emailHtmlSeguimiento { get { return _emailHtmlSeguimiento; } }

        private string _emailHtmlSES = "<body bgcolor='#D8D8D8' lang=ES-CL link=blue vlink=purple>" +
                                    "<div align='center'>" +
                                    "<table style='background:white;width:600px;border: solid 1px #989898;box-shadow: 0px 0px 3px #999;-webkit-box-shadow: 0px 0px 3px #999;-moz-box-shadow: 0px 0px 3px #999;filter: shadow(color=#999999, direction=0, strength=3);border-radius: 6px;-webkit-border-radius: 6px;-moz-border-radius: 6px;'>" +
                                    "<tr><td>" +
                                    "<table style='margin-left:15px;margin-right:15px;'><tr><td colspan='2' style='height:10px;'></td></tr>" +
                                    "<tr><td colspan='2' style='font-family: Arial,sans-serif;font-size: 12px;'>{0}</td></tr>" +
                                    "<tr><td colspan='2' style ='height:5px;'></td></tr>" +
                                    "<tr><td colspan='2' style='font-family: Arial,sans-serif;font-size: 12px;'>{1}</td></tr><tr><td colspan='2' style='height:5px;'></td></tr>" +
                                    "<tr><td colspan='2' style='height:10px;'></td></tr>" +
                                    "<tr><td style='width:35%'><img src=cid:logoMDS id='imgMDS' alt='Ministerio de Desarrollo Social y Familia' width='161px' height='146px'></td></tr>" +
                                    "<tr><td colspan='2' style='height:10px;border-bottom: solid 2px #989898;'></td></tr>" +
                                    "<tr><td colspan='2' style='height:10px;'></td></tr>" +
                                    "<tr><td colspan='2'><span lang='ES' style='font-size:10.0pt;font-family:Arial,sans-serif;color:navy'>Subsecretaría de Evaluación Social - Gobierno de Chile</span></td></tr>" +
                                    "<tr><td colspan='2' style='height:10px;'></td></tr>" +
                                    "<tr><td colspan='2'><span lang='ES' style='font-size:7.0pt;font-family:Arial,sans-serif;color:#1F497D'>Este documento y sus adjuntos pueden contener información privilegiada y/o confidencial.Si usted no es el destinatario del mismo o si por error lo recibió, por favor remítalo a esta misma dirección y bórrelo permanentemente de su sistema.La correspondencia y la información privilegiada están protegidas por Tratados Internacionales, la Constitución y la Ley.La manipulación indebida está penada por la Ley. <br>Muchas Gracias.</span></td></tr>" +
                                    "<tr><td colspan='2' style='height:10px;'></td></tr>" +
                                    "<tr><td colspan='2'><span lang='EN-US' style='font-size:7.0pt;font-family:Arial,sans-serif;color:#1F497D'> This document and its attachments may contain privileged or classified information. If you are not the addressee or you misperceive this e-mail, please resend it to the sender´s e-mail and proceed to delete it permanently from your computer. Private letters and confidential information are protected by International Treaties, the National Constitution and the Law.Tampering is severely punished by law. <br>Thank you</span></td></tr>" +
                                    "</table>" +
                                    "</td></tr>" +
                                    "<tr><td style='height:10px;'></td></tr></table></div></body>";

        public string emailHtmlSES { get { return _emailHtmlSES; } }
    }

    /// <summary>
    /// Clase que contiene datos necesarios para el envio de mail
    /// </summary>
    public class DatosEmail
    {
        public string de { get; set; }
        public string para { get; set; }
        public string cc { get; set; }
        public string asunto { get; set; }
        public string mensaje { get; set; }
        public bool imagen { get; set; }
        public bool imagenDipres { get; set; }
        public string adjunto { get; set; }
        public string cco { get; set; }
    }
}
