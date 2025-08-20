$(document).ready(function () {
    $("#menuMantenedores").addClass('in');
    $("#aMenuPermisos").dropdown('toggle');
    $('#tblGrupos').dataTable({
        "autoWidth": false,
        "processing": true,
        "ajax": {
            "url": ROOT + "Mantenedores/GetGruposFormularios",
            "dataSrc": "",
            "type": "POST"
        },
        "columns": [
            { "data": 'Nombre', "width": "50%" },
            { "data": 'Descripcion', "width": "50%" },
            {
                "data": null, "orderable": false,
                "render": function (data, type, full, meta) {
                    return '<button type="button" class="btn btn-primary btn-xs" data-toggle="tooltip" data-placement="left" title="Click para asignar permiso" onclick="modalPermisos(\'' + data.IdGrupoFormulario + '\')"><i class="fa fa-check-square-o" aria-hidden="true"></i></button>';
                }
            }
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {
            $('#tblGrupos_spFiltro').append('<button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>');
            this.css('width', '100%');
            $("[data-toggle=tooltip]").tooltip({ html: true });
        },
        "drawCallback": function (settings) { $("[data-toggle=tooltip]").tooltip({ html: true }); }
    });
    $('#tblFormularios').dataTable({
        "autoWidth": false,
        "processing": true,
        "ajax": {
            "url": ROOT + "Mantenedores/GetFormularios",
            "dataSrc": "",
            "type": "POST"
        },
        "columns": [
            { "data": 'IdBips', "width": "5%" },
            { "data": 'Ano', "width": "5%" },
            { "data": 'Ministerio', "width": "20%" },
            { "data": 'Servicio', "width": "20%" },
            { "data": 'Tipo', "width": "10%" },
            { "data": 'Nombre', "width": "40%" },
            {
                "data": null, "orderable": false,
                "render": function (data, type, full, meta) {
                    return '<button type="button" class="btn btn-primary btn-xs" data-toggle="tooltip" data-placement="left" title="Click para asignar permiso" onclick="modalPermisos(\'' + data.IdPrograma + '\')"><i class="fa fa-check-square-o" aria-hidden="true"></i></button>';
                }
            }
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {
            $('#tblFormularios_spFiltro').append('<button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>');
            this.css('width', '100%');
            $("[data-toggle=tooltip]").tooltip({ html: true });
        },
        "drawCallback": function (settings) { $("[data-toggle=tooltip]").tooltip({ html: true }); }
    });
    $('#tblUsuarios').dataTable({
        "autoWidth": false,
        "processing": true,
        "ajax": {
            "url": ROOT + "Mantenedores/GetUsuarios",
            "dataSrc": "",
            "type": "POST"
        },
        "columns": [
            { "data": 'UserName', "width": "10%" },
            { "data": 'Nombre', "width": "30%" },
            { "data": 'Ministerio', "width": "25%" },
            { "data": 'Servicio', "width": "25%" },            
            { "data": 'Perfil', "width": "10%" },
            {   "data": null, "orderable": false,
                "render": function (data, type, full, meta) { 
                    return '<button type="button" class="btn btn-primary btn-xs" data-toggle="tooltip" data-placement="left" title="Click para asignar permiso" onclick="modalPermisos(\'' + data.Id + '\')"><i class="fa fa-check-square-o" aria-hidden="true"></i></button>';
                }
            },
            { "data": 'EmailConfirmed', "visible": false },
            { "data": 'Estado', "visible": false }
        ],
        "order": [[0, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" },
        "initComplete": function (settings, json) {
            $('#tblUsuarios_spFiltro').append('<button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>');
            this.css('width', '100%');
        },
        "drawCallback": function (settings) { $("[data-toggle=tooltip]").tooltip({ html: true }); }
    });
    $("#cmbMinisterio").change(function () {
        $("#cmbServicio").empty();
        $("#cmbServicio").append("<option value='-1'>Seleccione</option>");
        var idMinist = $(this).val();
        $.each(aMinsServ, function (i, item) {
            if (idMinist == item.IdParametro) {
                $.each(item.Servicios, function (i2, item2) {
                    $("#cmbServicio").append("<option value='" + item2.IdParametro + "'>" + item2.Descripcion + "</option>");
                });
            }
        });
    });
});
function modalPermisos(id) {
    $('#modalPermisos').modal('show');
}