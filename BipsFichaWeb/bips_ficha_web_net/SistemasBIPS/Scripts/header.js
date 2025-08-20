$(document).ready(function () {  
    $("#txtBuscarProgramas").autocomplete({
        minLength: 3,
        source: getData,
        autoFill: true,
        select: function (event, ui) {
            var parms = {
                data: [ui.item.id, ui.item.ano]
            };
            $.ajax({
                async: true,
                type: "POST",
                traditional: true,
                url: ROOT + "Programa/validaProgAbierto",
                async: false,
                data: parms,
                dataType: "json",
                success: function (data) {
                    var ruta;
                    if (ui.item.tipoProgEr == 1) {
                        ruta = ROOT + "FormularioNuevo?id=" + ui.item.id + '&ano=' + ui.item.ano;
                    } else if (ui.item.tipoProgEr == 2) {
                        ruta = ROOT + "FormularioReformulado?listadoProgramasReformulados=&idPadre=0&ano=" + ui.item.ano + '&idPrograma=' + ui.item.id;
                    } else if (ui.item.tipoProgEr == 3) {
                        ruta = ROOT + "FormularioVigente2?id=" + ui.item.id + '&ano=' + ui.item.ano;
                    } else if (ui.item.tipoProgEr == 4) {
                        ruta = ROOT + "FormularioIniciativa?id=" + ui.item.id + '&ano=' + ui.item.ano;
                    }
                    if (data.estado == true && data.usuario != data.usuarioLogeado) {
                        if (confirm("¿El programa esta siendo editado por el usuario: " + data.usuario + ", desea abrirlo de igual manera en modo solo lectura?")) {
                            var f = document.forms[0];
                            f.action = ruta;
                            f.filtroMas.value = $("#txtBuscarProgramas").val();
                            f.submit();
                        }
                    } else {
                        var f = document.forms[0];
                        f.action = ruta;
                        f.filtroMas.value = $("#txtBuscarProgramas").val();
                        f.submit();
                    }
                },
                error: function (data) {
                    alert("Se ha producido un error al intentar abrir el programa");
                }
            });
        }
    });
});
function getData(request, response) {
    fecha = new Date();
    var anoActual = fecha.getFullYear();
    var anoBusqueda = pagina == "1" ? $("ul.tabs li.active").find("a").attr("id") : (pagina == "2" || pagina == "3" || pagina == "4" || pagina == "14") ? queryString('ano') : anoActual;
    $.ajax({
        url: ROOT + "Programa/retornarProgramasAjax",
        type: 'GET',
        dataType: 'json',
        data: { words: request.term, ano: anoBusqueda },
        success: function (data) {
            response(data);
        }
    });
}
function cierraSession() {
    //$(window).off('beforeunload');
    var f = document.forms[0];
    f.action = ROOT + "Programa/cierraSession"; 
    f.submit();
}
function queryString(valor)
{
    valor = valor.replace(/[\[]/,"\\\[").replace(/[\]]/,"\\\]");
    var regexS = "[\\?&]"+valor+"=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.href);
    if(results == null)
        return "";
    else
        return results[1];
}