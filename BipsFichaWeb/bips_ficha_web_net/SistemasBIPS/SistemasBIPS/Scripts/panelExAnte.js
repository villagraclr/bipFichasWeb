$(document).ready(function () {
    $("#menuExAnte").addClass('in');
    $("#aMenuPanelExAnte").dropdown('toggle');
    iniciaTablaPanel();
});
function iniciaTablaPanel() {
    var tablaPanelExAnte = $('#tblPanelExAnte').DataTable();
    tablaPanelExAnte.destroy();
    $('#tblPanelExAnte').dataTable({
        dom: 'Bfrtip',
        buttons: [{
            extend: 'excel',
            text: 'Descargar planilla',
            filename: moment().format("YYYY/MM/DD") + '_planilla_ex_ante',
            title: ''
        }],
        "processing": true,
        "ajax": {
            "url": ROOT + "EvaluacionExAnte/GetPanelExAnte",
            "dataSrc": "",
            "type": "POST"
        },
        "columns": [
            { "data": 'IdBips', "width": "4%" },
            { "data": 'Ministerio', "width": "20%" },
            { "data": 'Servicio', "width": "20%" },
            {
                "data": 'Nombre', "width": "30%",
                "render": function (data, type, full, meta) {
                    var link = ROOT + "Formulario/Index" + "?_id=" + encodeURIComponent(full.IdEncriptado);
                    return '<a href="' + link + '" data-toggle="tooltip" data-placement="right" title="Click para abrir programa" target="_blank">' + full.Nombre + '</ña>';
                }
            },
            { "data": 'Tipo', "width": "2%" },
            { "data": 'EtapaDesc', "width": "2%" },
            { "data": 'Version', "width": "2%" },            
            { "data": 'NombreEvaluador1', "width": "2%" },
            { "data": 'NombreEvaluador2', "width": "2%" },
            { "data": 'FecSolicitudEval', "width": "5%" },
            { "data": 'FecAsigEval1', "width": "5%" },
            { "data": 'FecAsigEval2', "width": "5%" },
            {
                "data": 'FecEnvioSect', "width": "5%",
                "render": function (data, type, full, meta) {
                    let fecSolEval = (full.FecSolicitudEval != null && full.FecSolicitudEval != "" ? moment(full.FecSolicitudEval.split(" ")[0], 'DD-MM-YYYY') : "");
                    let fecEnvioSect = (full.FecEnvioSect != null && full.FecEnvioSect != "" ? moment(full.FecEnvioSect.split(" ")[0], 'DD-MM-YYYY') : "");
                    let fecFinal = full.FecEnvioSect;
                    if (fecSolEval != null && fecSolEval != "") {
                        if (fecSolEval.diff(fecEnvioSect, 'days') > 0) {
                            fecFinal = "";
                        }
                    } else {
                        fecFinal = "";
                    }
                    return fecFinal;
                }
            },
            {
                "data": 'FecEnvioComentSect', "width": "5%",
                "render": function (data, type, full, meta) {
                    let fecSolEval = (full.FecSolicitudEval != null && full.FecSolicitudEval != "" ? moment(full.FecSolicitudEval.split(" ")[0], 'DD-MM-YYYY') : "");
                    let fecEnvioSect = (full.FecEnvioComentSect != null && full.FecEnvioComentSect != "" ? moment(full.FecEnvioComentSect.split(" ")[0], 'DD-MM-YYYY') : "");
                    let fecFinal = full.FecEnvioComentSect;
                    if (fecSolEval != null && fecSolEval != "") {
                        if (fecSolEval.diff(fecEnvioSect, 'days') > 0) {
                            fecFinal = "";
                        }
                    } else {
                        fecFinal = "";
                    }
                    return fecFinal;
                }
            },
            {
                "data": 'FecCorreccion', "width": "5%",
                "render": function (data, type, full, meta) {
                    let fecSolEval = (full.FecSolicitudEval != null && full.FecSolicitudEval != "" ? moment(full.FecSolicitudEval.split(" ")[0], 'DD-MM-YYYY') : "");
                    let fecEnvioSect = (full.FecCorreccion != null && full.FecCorreccion != "" ? moment(full.FecCorreccion.split(" ")[0], 'DD-MM-YYYY') : "");
                    let fecFinal = full.FecCorreccion;
                    if (fecSolEval != null && fecSolEval != "") {
                        if (fecSolEval.diff(fecEnvioSect, 'days') > 0) {
                            fecFinal = "";
                        }
                    } else {
                        fecFinal = "";
                    }
                    return fecFinal;
                }
            },
            {
                "data": 'FecComentMonitoreo', "width": "5%",
                "render": function (data, type, full, meta) {
                    let fecSolEval = (full.FecSolicitudEval != null && full.FecSolicitudEval != "" ? moment(full.FecSolicitudEval.split(" ")[0], 'DD-MM-YYYY') : "");
                    let fecEnvioSect = (full.FecComentMonitoreo != null && full.FecComentMonitoreo != "" ? moment(full.FecComentMonitoreo.split(" ")[0], 'DD-MM-YYYY') : "");
                    let fecFinal = full.FecComentMonitoreo;
                    if (fecSolEval != null && fecSolEval != "") {
                        if (fecSolEval.diff(fecEnvioSect, 'days') > 0) {
                            fecFinal = "";
                        }
                    } else {
                        fecFinal = "";
                    }
                    return fecFinal;
                }
            },
            {
                "data": 'FecComentEstudios', "width": "5%",
                "render": function (data, type, full, meta) {
                    let fecSolEval = (full.FecSolicitudEval != null && full.FecSolicitudEval != "" ? moment(full.FecSolicitudEval.split(" ")[0], 'DD-MM-YYYY') : "");
                    let fecEnvioSect = (full.FecComentEstudios != null && full.FecComentEstudios != "" ? moment(full.FecComentEstudios.split(" ")[0], 'DD-MM-YYYY') : "");
                    let fecFinal = full.FecComentEstudios;
                    if (fecSolEval != null && fecSolEval != "") {
                        if (fecSolEval.diff(fecEnvioSect, 'days') > 0) {
                            fecFinal = "";
                        }
                    } else {
                        fecFinal = "";
                    }
                    return fecFinal;
                }
            },
            {
                "data": 'FecEvalFinalizada', "width": "5%",
                "render": function (data, type, full, meta) {
                    let fecSolEval = (full.FecSolicitudEval != null && full.FecSolicitudEval != "" ? moment(full.FecSolicitudEval.split(" ")[0], 'DD-MM-YYYY') : "");
                    let fecEnvioSect = (full.FecEvalFinalizada != null && full.FecEvalFinalizada != "" ? moment(full.FecEvalFinalizada.split(" ")[0], 'DD-MM-YYYY') : "");
                    let fecFinal = full.FecEvalFinalizada;
                    if (fecSolEval != null && fecSolEval != "") {
                        if (fecSolEval.diff(fecEnvioSect, 'days') > 0) {
                            fecFinal = "";
                        }
                    } else {
                        fecFinal = "";
                    }
                    return fecFinal;
                }
            },
            { "data": 'PuntajeFinal', "width": "2%" },
            { "data": 'Calificacion', "width": "5%" },
            {
                "data": 'TotalDiasIteracion', "width": "5%",
                "render": function (data, type, full, meta) {
                    let fecSolEval = (full.FecSolicitudEval != null && full.FecSolicitudEval != "" ? moment(full.FecSolicitudEval.split(" ")[0], 'DD-MM-YYYY') : "");
                    let fecEnvioSect = (full.FecEvalFinalizada != null && full.FecEvalFinalizada != "" ? moment(full.FecEvalFinalizada.split(" ")[0], 'DD-MM-YYYY') : "");
                    let fecFinal = "";//full.FecEvalFinalizada;
                    if (fecSolEval != null && fecSolEval != "" && fecEnvioSect != null && fecEnvioSect != "") {
                        if (fecEnvioSect.diff(fecSolEval, 'days') > 0) {
                            fecFinal = fecEnvioSect.diff(fecSolEval, 'days');
                        }
                    }
                    return fecFinal;
                }
            },
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {            
            this.css('width', '100%');
            $("[data-toggle=tooltip]").tooltip({ html: true });
            this.api()
            .columns()
            .every(function () {
                let column = this;

                // Create select element
                let select = document.createElement('select');
                select.add(new Option(''));
                column.footer().replaceChildren(select);

                // Apply listener for user change in value
                select.addEventListener('change', function () {
                    var val = DataTable.util.escapeRegex(select.value);

                    column
                        .search(val ? '^' + val + '$' : '', true, false)
                        .draw();
                });

                // Add list of options
                column
                    .data()
                    .unique()
                    .sort()
                    .each(function (d, j) {
                        select.add(new Option(d));
                    });
            });
        },
        "drawCallback": function (settings) { $("[data-toggle=tooltip]").tooltip({ html: true }); }        
    });
}