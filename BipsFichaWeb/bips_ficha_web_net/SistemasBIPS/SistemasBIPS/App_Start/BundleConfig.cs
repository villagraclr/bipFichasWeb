using System.Web.Optimization;

namespace SistemasBIPS
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {                                              
            //CSS
            bundles.Add(new StyleBundle("~/Content/General").Include("~/Content/Css/FrontEnd/Sitio.css"));
            bundles.Add(new StyleBundle("~/Content/Bootstrap_min").Include("~/Content/bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/Content/Fonts_min").Include("~/Content/font-awesome.min.css"));
            bundles.Add(new StyleBundle("~/Content/MetisMenu_min").Include("~/Content/metisMenu.min.css"));
            bundles.Add(new StyleBundle("~/Content/APP").Include("~/Content/admin.css"));
            bundles.Add(new StyleBundle("~/Content/DataTable").Include(
                "~/Content/dataTables.bootstrap.min.css",
                "~/Content/responsive.bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/Content/Sitio").Include("~/Content/Sitio.css"));
            bundles.Add(new StyleBundle("~/Content/Formularios").Include("~/Content/Css/FrontEnd/Formularios.css"));
            bundles.Add(new StyleBundle("~/Content/HomeFormularios").Include("~/Content/Css/FrontEnd/HomeFormularios.css"));
            bundles.Add(new StyleBundle("~/Content/OfertaSocial").Include("~/Content/Css/FrontEnd/OfertaSocial.css"));
            bundles.Add(new StyleBundle("~/Content/DataTable2").Include(
                "~/Content/dataTables.bootstrap2.min.css",
                "~/Content/buttons.dataTables.min.css"));
            bundles.Add(new StyleBundle("~/Content/Dashboard").Include("~/Content/Css/FrontEnd/Dashboard.css"));
            //JS
            bundles.Add(new ScriptBundle("~/bundles/Jquery_2_1_4").Include("~/Scripts/jquery-2.2.0.*"));
            bundles.Add(new ScriptBundle("~/bundles/Jquery").Include("~/Scripts/jquery-3.7.0.*"));
            bundles.Add(new ScriptBundle("~/bundles/Boostrap").Include("~/Scripts/bootstrap.*"));
            bundles.Add(new ScriptBundle("~/bundles/MetisMenu").Include("~/Scripts/metisMenu.*"));
            bundles.Add(new ScriptBundle("~/bundles/SbAdmin").Include("~/Scripts/sb-admin-2.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/Login-3").Include("~/Scripts/login-3.0.js"));
            bundles.Add(new ScriptBundle("~/bundles/Angular").Include("~/Scripts/angular.*"));
            bundles.Add(new ScriptBundle("~/bundles/AngularRoute").Include("~/Scripts/angular-route.js"));
            /*bundles.Add(new ScriptBundle("~/bundles/DataTable").Include("~/Scripts/DataTable").Include(
                "~/Scripts/jquery.dataTables.min.js",
                "~/Scripts/dataTables.bootstrap5.min.js",
                "~/Scripts/dataTables.buttons.min.js",
                "~/Scripts/jszip.min.js",
                "~/Scripts/pdfmake.min.js",
                "~/Scripts/vfs_fonts.js",
                "~/Scripts/buttons.html5.min.js"));*/
            bundles.Add(new ScriptBundle("~/bundles/DataTable").Include(
                "~/Scripts/jquery.dataTables.min.js",
                "~/Scripts/dataTables.bootstrap.min.js",
                "~/Scripts/dataTables.responsive.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryUnobtrusive").Include("~/Scripts/jquery.unobtrusive*"));
            //bundles.Add(new ScriptBundle("~/bundles/Countdown").Include("~/Scripts/jquery.countdown.min.js"));
            //bundles.Add(new ScriptBundle("~/bundles/TimerSesion").Include("~/Scripts/relojSesion.js"));
            bundles.Add(new ScriptBundle("~/bundles/Users").Include("~/Scripts/users.js"));
            bundles.Add(new ScriptBundle("~/bundles/Grupos").Include("~/Scripts/grupos.js"));
            bundles.Add(new ScriptBundle("~/bundles/Permisos").Include("~/Scripts/permisos.js"));
            bundles.Add(new ScriptBundle("~/bundles/FormMantenedor").Include("~/Scripts/formulariosMantenedor.js"));
            bundles.Add(new ScriptBundle("~/bundles/HomeFormularios").Include("~/Scripts/formulariosHome.js"));
            bundles.Add(new ScriptBundle("~/bundles/Formularios").Include("~/Scripts/formularios.js"));
            bundles.Add(new ScriptBundle("~/bundles/EvalExAnte").Include("~/Scripts/evaluacionExAnte.js"));
            bundles.Add(new ScriptBundle("~/bundles/OfertaSocial").Include("~/Scripts/ofertaSocial.js"));
            bundles.Add(new ScriptBundle("~/bundles/Moment").Include("~/Scripts/moment.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/Biblioteca").Include("~/Scripts/biblioteca.js"));
            bundles.Add(new ScriptBundle("~/bundles/Funciones").Include("~/Scripts/funciones.js"));
            bundles.Add(new ScriptBundle("~/bundles/Formularios2").Include("~/Scripts/formularios2.js"));
            bundles.Add(new ScriptBundle("~/bundles/ArmaFormulario").Include("~/Scripts/armaFormulario.js"));
            bundles.Add(new ScriptBundle("~/bundles/SerializeArray").Include("~/Scripts/jquery.serializeArray.js"));
            bundles.Add(new ScriptBundle("~/bundles/Validaciones").Include("~/Scripts/validaciones.js"));
            bundles.Add(new ScriptBundle("~/bundles/ValidacionesPerfilGore").Include("~/Scripts/validacionesPerfilGore.js"));
            bundles.Add(new ScriptBundle("~/bundles/ValidacionesProgramaGore").Include("~/Scripts/validacionesProgramaGore.js"));
            bundles.Add(new ScriptBundle("~/bundles/PanelExAnte").Include("~/Scripts/panelExAnte.js"));
            bundles.Add(new ScriptBundle("~/bundles/DataTable2").Include(
                "~/Scripts/jquery.dataTables2.min.js",
                "~/Scripts/dataTables.bootstrap.min.js",
                "~/Scripts/dataTables.buttons.min.js",
                "~/Scripts/jszip.min.js",
                "~/Scripts/pdfmake.min.js",
                "~/Scripts/vfs_fonts.js",
                "~/Scripts/buttons.html5.min.js"));
            //bundles.Add(new ScriptBundle("~/bundles/Popper").Include("~/Scripts/popper.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/Jquery_ui").Include("~/Scripts/jquery-ui-1.11.4.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/CargaRIS").Include("~/Scripts/cargaRIS.js"));
            bundles.Add(new ScriptBundle("~/bundles/Dashboard").Include("~/Scripts/dashboard.js"));
            bundles.Add(new Bundle("~/bundles/HighCharts").Include(
                "~/Scripts/highcharts.js",
                "~/Scripts/highcharts-more.js",
                "~/Scripts/solid-gauge.js",
                "~/Scripts/exporting.js",
                "~/Scripts/export-data.js",
                "~/Scripts/accessibility.js"));
            bundles.Add(new ScriptBundle("~/bundles/HomeGores").Include("~/Scripts/formulariosGores.js"));
            bundles.Add(new ScriptBundle("~/bundles/HomeProgramasGores").Include("~/Scripts/formulariosProgramasGores.js"));
            bundles.Add(new ScriptBundle("~/bundles/EvalExAntePerfil").Include("~/Scripts/evaluadorPerfil.js"));
            bundles.Add(new ScriptBundle("~/bundles/EvalExAntePrograma").Include("~/Scripts/evaluadorPrograma.js"));
            bundles.Add(new Bundle("~/bundles/EditorTexto").Include("~/Scripts/ckeditor.js"));
            bundles.Add(new ScriptBundle("~/bundles/Repositorio").Include("~/Scripts/repositorio.js"));

            BundleTable.EnableOptimizations = false;
        }
    }
}
