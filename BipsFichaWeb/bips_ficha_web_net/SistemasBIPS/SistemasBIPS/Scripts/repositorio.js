$(document).ready(function () {
    $("#liNivelCentral").addClass('active');
    $("#liBiblioteca").addClass('active');
    $("#menuNivelCentral").addClass('in');
    $("#menuBiblioteca").addClass('in');
    $("#aMenuRepositorio").dropdown('toggle');
});
function iniciaTablaRepositorio() {
    var tablaRepositorio = $('#tblRepositorio').DataTable();
    tablaRepositorio.destroy();
    $('#tblRepositorio').dataTable({
        "autoWidth": false,
        "processing": true,
        "ajax": {
            "url": ROOT + "Biblioteca/GetRepositorio",
            "type": "POST",
            "dataSrc": function (response) {
                response.categorias.shift();
                response.categorias.forEach(obj => {
                    let partes = obj.Descripcion.split("%");
                    let formulario = response.formularios.filter(element => element.IdParametro == obj.Valor);
                    obj.Valor = formulario[0].Descripcion;
                    obj.Nombre = partes[0];
                    obj.Url = partes[1];
                });
                return response.categorias;
            }
        },
        "columns": [
            {
                "data": 'Nombre', "width": "45%",
            },
            {
                "data": 'Valor', "width": "30%",
            },
            {
                "data": 'Valor2', "width": "20%",
            },
            {
                "data": null, "width": "5%",
                "render": function (data, type, full, meta) {
                    return '<a href="' + full.Url + '" target="_blank"><i class="fa fa-download" aria-hidden="true"></i></a>';
                }
            }
        ],
        "order": [[0, "asc"], [2, "asc"]],
        "language": { "url": ROOT + "Scripts/dataTables.esp.js" }
    });
}