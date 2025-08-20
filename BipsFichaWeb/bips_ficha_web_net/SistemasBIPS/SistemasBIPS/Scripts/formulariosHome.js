$(document).ready(function () {
    //$('#popOverEtapas').popover();
    $('ul.nav a').each(function () { if ($(this).hasClass('active')) { $(this).removeClass('active'); } });
    $("#liNivelCentral").addClass('active');
    $('#aMenuProgramas').addClass('active');
    $("#menuNivelCentral").addClass('in');
    //Tabla programas
    tablaProgramas(null);
    //Filtro todos los formularios
    $('#todosFormularios').change(function () {
        if (this.checked) {
            $('.formularios').prop("checked", true);
        } else {
            $('.formularios').prop("checked", false);
        }
        aplicaFiltros();
    });
    //Filtro mis formularios
    $('.formularios').change(function () {
        $('#todosFormularios').prop("checked", false);
        aplicaFiltros();
    });
    //Filtro todos los años
    $('#todosAnosFiltro').change(function () {
        //$('#filtroAnos').collapse('show');
        if (this.checked) {
            $('.anosFiltro').prop("checked", true);
        } else {
            $('.anosFiltro').prop("checked", false);
        }
        aplicaFiltros();
    });
    //Filtro años
    $('.anosFiltro').change(function () {
        if (!this.checked) {
            $('#todosAnosFiltro').prop("checked", false);
        } else {
            var todos = 1;
            $('.anosFiltro').each(function () {
                if (!this.checked) {
                    todos = 0;
                }
            });
            if (todos == 1) {
                $('#todosAnosFiltro').prop("checked", true);
            }
        }
        aplicaFiltros();
    });
    //Filtro todos los ministerios
    $('#todosMinisterios').change(function () {
        //$('#filtroMiniServ').collapse('show');
        if (this.checked) {
            $('.ministeriosFiltro').prop("checked", true);
            $('.serviciosFiltro').prop("checked", true);
        } else {
            $('.ministeriosFiltro').prop("checked", false);
            $('.serviciosFiltro').prop("checked", false);
        }
        aplicaFiltros();
    });
    //Filtro ministerios
    $('.ministeriosFiltro').change(function () {
        $('#filtroMinisterio' + this.id.substring(1, this.id.length)).collapse('show');
        if (!this.checked) {
            $('#todosMinisterios').prop("checked", false);
            $('input[name="s' + this.id.substring(1, this.id.length) + '"]').prop("checked", false);
        } else {
            var todos = 1;
            $('.ministeriosFiltro').each(function () {
                if (!this.checked) {
                    todos = 0;
                }
            });
            if (todos == 1) {
                $('#todosMinisterios').prop("checked", true);
            }
            $('input[name="s' + this.id.substring(1, this.id.length) + '"]').prop("checked", true);
        }
        aplicaFiltros();
    });
    //Filtro servicios
    $('.serviciosFiltro').change(function () {
        if (!this.checked) {
            $('#m' + this.name.substring(1, this.name.length)).prop("checked", false);
            $('#todosMinisterios').prop("checked", false);
        } else {
            var todos = 1;
            $('input[name="' + this.name + '"]').each(function () {
                if (!this.checked) {
                    todos = 0;
                }
            });
            if (todos == 1) {
                $('#m' + this.name.substring(1, this.name.length)).prop("checked", true);
                todos = 1;
                $('.ministeriosFiltro').each(function () {
                    if (!this.checked) {
                        todos = 0;
                    }
                });
                if (todos == 1) {
                    $('#todosMinisterios').prop("checked", true);
                }
            }
        }             
        aplicaFiltros();
    });
    //Aplicar filtros
    function aplicaFiltros() {
        tablaProgramas({ filtroAnos: JSON.stringify($('input[name="anosFiltro"]').serializeObject()), filtroMinisterios: JSON.stringify($('input[class="serviciosFiltro"]').serializeObject()), filtroFormularios: JSON.stringify($('input[name="formularios"]').serializeObject()) });
    }
    $.fn.serializeObject = function () {
        var o = {};
        var a = this.serializeArray();
        $.each(a, function () {
            if (o[this.name] !== undefined) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    };
});
function tablaProgramas(filtros) {
    if (filtros != null) {
        var tablaProgramas = $('#tblProgramas').DataTable();
        tablaProgramas.destroy();
    }    
    $('#tblProgramas').dataTable({
        "processing": true,
        "ajax": {
            "url": MyApp.rootPath + "Programa/GetProgramas",
            "dataSrc": "",
            "data": filtros,
            "type": "POST"
        },
        "columns": [
            { "data": 'IdBips' },
            { "data": 'Ano' },
            { "data": 'Ministerio' },
            { "data": 'Servicio' },
            {
                "data": 'Tipo',
                "render": function (data, type, full, meta) {
                    var tipo = full.Tipo;
                    if (full.IdTipoFormulario == 336) {
                        tipo = "Social";
                    } else if (full.IdTipoFormulario == 337) {
                        tipo = "No social";
                    }
                    return tipo;
                }
            },
            {
                "data": 'Nombre',
                "render": function (data, type, full, meta) {
                    if (full.Acceso > 0) {
                        var link = ROOT + "Formulario/Index" + "?_id=" + encodeURIComponent(full.IdEncriptado);
                        var title = "Acceso de sólo lectura";
                        var color = "d9534f";                        
                        for (var i = 0; i < arrayFT.length; i++) {                                    
                            if (full.IdPrograma == arrayFT[i]) {
                                title = "Programa en uso";
                                color = "00DC21";
                                break;
                            }                                    
                        }
                        return '<a href="' + link + '" onclick="muestraLoad();" data-toggle="tooltip" title="' + title + '" style="color:#' + color + '">' + data + '</a>';
                    } else {
                        var link = ROOT + "Formulario/Index" + "?_id=" + encodeURIComponent(full.IdEncriptado);
                        return '<a href="' + link + '" onclick="muestraLoad();" data-toggle="tooltip" title="Click para abrir formulario">' + data + '</a>';
                    }
                }
            },
            {
                "data": 'EtapaDesc', "orderable": false,
                "render": function (data, type, full, meta) {
                    return '<label data-toggle="tooltip" title="En esta etapa..." style="font-weight: normal">' + data + '</label>';
                }
            },
            {
                "data": null, "orderable": false, "width": "2%",
                "render": function (data, type, full, meta) {
                    if (anosFichas.indexOf(full.Ano) >= 0) {
                            var seguimiento = "#";
                            var detalle = "#";
                            $.each(tiposProgramas, function (i, item) {
                                if (item.Id == full.IdTipoFormulario) {
                                    seguimiento = item.rutaSeguimiento + full.IdPrograma + '/' + full.Ano + "/" + (item.IdPadre == "302" ? "" : item.IdPadre);
                                    detalle = item.rutaDetalle + full.IdPrograma + '/' + full.Ano + "/" + (item.IdPadre == "302" ? "" : item.IdPadre);
                                }
                            });
                            fichas = "";
                            if (permisoInfoEval == "True") {
                                fichas = '<a href="' + seguimiento + '" data-toggle="tooltip" data-placement="left" title="Informe de seguimiento" target="_blank"><i class="fa fa-file-pdf-o" aria-hidden="true"></i></a>';
                            }
                            fichas += '<a href="' + detalle + '" data-toggle="tooltip" data-placement="left" title="Informe de detalle" target="_blank" style="margin-left: 7px;"><i class="fa fa-file-pdf-o" aria-hidden="true"></i></a>';
                    } else {
                        fichas = '<span class="glyphicon glyphicon-remove" aria-hidden="true" data-toggle="tooltip" data-placement="left" title="Sin informe"></span>';
                    }               
                    return fichas;
                }
            },
            {
                "data": null, "orderable": false, "width": "2%", "visible": (permisoLibera === "True"),
                "render": function (data, type, full, meta) {                    
                    var botar = '';
                    if (permisoLibera == "True") {
                        if (full.Tomado > 0) {
                            botar = '<button id="btnLiberaPrograma" class="btn btn-primary btn-xs" type="button" data-toggle="tooltip" data-placement="left" title="Liberar programa" onclick="liberaPrograma(' + full.IdPrograma + ');"><i class="fa fa-sign-out"></i></button>';
                        }
                    }                    
                    return botar;
                }
            }
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {
            $('#tblProgramas_spFiltro').append('<button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>');
            this.css('width', '100%');
            $("[data-toggle=tooltip]").tooltip({ html: true });
        },
		"drawCallback": function(settings) {
			$("[data-toggle=tooltip]").tooltip({ html: true });
		}
    });        
}
function muestraLoad(){
    $("#cargaPagina").fadeIn();
}
function liberaPrograma(id) {
    $(document).ready(function () {
        var opciones = {
            url: ROOT + "Formulario/Post",
            data: { _id: id },
            type: "post",
            datatype: "json",
            beforeSend: function () { $("#cargaPagina").fadeIn(); },
            success: function (result, xhr, status) {
                if (result == "ok") {
                    $("#btnLiberaPrograma").prop('disabled', true);
                    $("#txtMensajesFormulario").empty();
                    $("#txtMensajesFormulario").html("<p>Programa liberado con éxito.</p>");
                    $('#modalMensajesFormularios').modal('show');
                }
            },
            error: function (xhr, status, error) {
                console.log(error);
            },
            complete: function () { $("#cargaPagina").fadeOut(); }
        };
        $.ajax(opciones);
        /*
        debugger;        
        //Boton botar programa
        var notificacion = $.connection.notificacionesHub;
        $.connection.hub.start().done(function () {
            notificacion.server.notificacion(id);
        });*/
    });    
}