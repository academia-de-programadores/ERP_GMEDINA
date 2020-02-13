﻿using System.Web.Optimization;

namespace ERP_GMEDINA
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Plantilla Inventario y facturación

            // jQuery Validation
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            "~/Scripts/jquery.validate.min.js"));

            // jQuery plugins
            bundles.Add(new ScriptBundle("~/plugins/Menu").Include(
                      "~/Scripts/plugins/metisMenu/metisMenu.min.js"));

            bundles.Add(new ScriptBundle("~/plugins/pace").Include(
                      "~/Scripts/plugins/pace/pace.min.js"));

            bundles.Add(new StyleBundle("~/Content/DataTabla")
                .Include("~/Content/DataTables/css/dataTables.bootstrap.css"));

            bundles.Add(new ScriptBundle("~/Scripts/DataTabla")
          .Include(
                "~/Scripts/DataTables/jquery.dataTables.min.js",
                "~/Scripts/DataTables/dataTables.responsive.min.js",
                "~/Scripts/DataTables/dataTables.bootstrap.min.js"
    ));

            //Date picker
            bundles.Add(new StyleBundle("~/Content/picker").Include(
                        "~/Content/themes/base/jquery-ui.min.css"
            ));
            // Font Awesome icons
            bundles.Add(new StyleBundle("~/Content/font-Style").Include(
                      "~/fonts/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new ScriptBundle("~/Scripts/picker").Include(
    "~/Scripts/jquery-ui-1.12.1.min.js"
    ));

            #endregion

            #region Plantilla
            // CSS style (bootstrap/inspinia)
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/animate.css",
                      "~/Content/style.css",
                      "~/Content/BTN/BTN-HOME.css"
                      ));

            // Font Awesome icons
            bundles.Add(new StyleBundle("~/font-awesome/css").Include(
                      "~/fonts/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            bundles.Add(new StyleBundle("~/Content/background").Include(
                     "~/Content/background.css", new CssRewriteUrlTransform()));

            // jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.4.1.min.js",
                        "~/Scripts/app/Menu/Menu.js"));

            // jQueryUI CSS
            bundles.Add(new ScriptBundle("~/Scripts/plugins/jquery-ui/jqueryuiStyles").Include(
                        "~/Scripts/plugins/jquery-ui/jquery-ui.css"));

            // jQueryUI
            bundles.Add(new StyleBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/plugins/jquery-ui/jquery-ui.min.js"));

            // Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            // Inspinia script
            bundles.Add(new ScriptBundle("~/bundles/inspinia").Include(
                      "~/Scripts/plugins/metisMenu/metisMenu.min.js",
                      //"~/Scripts/plugins/pace/pace.min.js",
                      "~/Scripts/app/inspinia.js"));

            // Inspinia skin config script
            bundles.Add(new ScriptBundle("~/bundles/skinConfig").Include(
                      "~/Scripts/app/skin.config.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/HistorialAmonestaciones/Admin").Include(
           "~/Scripts/app/General/HistorialAmonestaciones/Admin.js"));


            bundles.Add(new ScriptBundle("~/Scripts/app/General/HistorialIncapacidades/Admin").Include(
          "~/Scripts/app/General/HistorialIncapacidades/Admin.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/HistorialIncapacidades/IndexHistorialIncapacidades").Include(
            "~/Scripts/app/General/HistorialIncapacidades/IndexHistorialIncapacidades.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/HistorialIncapacidades/CreateIncapacidades").Include(
             "~/Scripts/app/General/HistorialIncapacidades/CreateIncapacidades.js"));

            // SlimScroll
            bundles.Add(new ScriptBundle("~/plugins/slimScroll").Include(
                      "~/Scripts/plugins/slimscroll/jquery.slimscroll.min.js"));

            // Peity
            bundles.Add(new ScriptBundle("~/plugins/peity").Include(
                      "~/Scripts/plugins/peity/jquery.peity.min.js"));

            // Video responsible
            bundles.Add(new ScriptBundle("~/plugins/videoResponsible").Include(
                      "~/Scripts/plugins/video/responsible-video.js"));

            // Lightbox gallery css styles
            bundles.Add(new StyleBundle("~/Content/plugins/blueimp/css/").Include(
                      "~/Content/plugins/blueimp/css/blueimp-gallery.min.css"));

            // Lightbox gallery
            bundles.Add(new ScriptBundle("~/plugins/lightboxGallery").Include(
                      "~/Scripts/plugins/blueimp/jquery.blueimp-gallery.min.js"));

            // Sparkline
            bundles.Add(new ScriptBundle("~/plugins/sparkline").Include(
                      "~/Scripts/plugins/sparkline/jquery.sparkline.min.js"));

            // Morriss chart css styles
            bundles.Add(new StyleBundle("~/plugins/morrisStyles").Include(
                      "~/Content/plugins/morris/morris-0.4.3.min.css"));

            // Morriss chart
            bundles.Add(new ScriptBundle("~/plugins/morris").Include(
                      "~/Scripts/plugins/morris/raphael-2.1.0.min.js",
                      "~/Scripts/plugins/morris/morris.js"));

            // Flot chart
            bundles.Add(new ScriptBundle("~/plugins/flot").Include(
                      "~/Scripts/plugins/flot/jquery.flot.js",
                      "~/Scripts/plugins/flot/jquery.flot.tooltip.min.js",
                      "~/Scripts/plugins/flot/jquery.flot.resize.js",
                      "~/Scripts/plugins/flot/jquery.flot.pie.js",
                      "~/Scripts/plugins/flot/jquery.flot.time.js",
                      "~/Scripts/plugins/flot/jquery.flot.spline.js"));

            // Rickshaw chart
            bundles.Add(new ScriptBundle("~/plugins/rickshaw").Include(
                      "~/Scripts/plugins/rickshaw/vendor/d3.v3.js",
                      "~/Scripts/plugins/rickshaw/rickshaw.min.js"));

            // ChartJS chart
            bundles.Add(new ScriptBundle("~/plugins/chartJs").Include(
                      "~/Scripts/plugins/chartjs/Chart.min.js"));

            // iCheck css styles
            bundles.Add(new StyleBundle("~/Content/plugins/iCheck/iCheckStyles").Include(
                      "~/Content/plugins/iCheck/custom.css"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/CreateSucursal").Include(
            "~/Scripts/app/General/Sucursales/CreateSucursales.js"));


            bundles.Add(new ScriptBundle("~/Scripts/app/general/SucursalesEdit").Include(
            "~/Scripts/app/General/Sucursales/EditSucursales.js"));



            bundles.Add(new ScriptBundle("~/Scripts/app/general/Sucursales").Include(
            "~/Scripts/app/General/Sucursales/IndexSucursales.js"));

            // iCheck
            bundles.Add(new ScriptBundle("~/plugins/iCheck").Include(
                      "~/Scripts/plugins/iCheck/icheck.min.js"));

            // dataTables css styles
            bundles.Add(new StyleBundle("~/Content/plugins/dataTables/dataTablesStyles").Include(
                      "~/Content/plugins/dataTables/datatables.min.css"));

            // dataTables
            bundles.Add(new ScriptBundle("~/plugins/dataTables").Include(
                      "~/Scripts/plugins/dataTables/datatables.min.js",
                      "~/Scripts/plugins/dataTables/DatatablesConfigurations.js"));
            // dataTables personalizado
            bundles.Add(new ScriptBundle("~/plugins/customdataTables").Include(
                     "~/Scripts/plugins/dataTables/datatables.min.js"));

            // dataTables RPT
            bundles.Add(new ScriptBundle("~/plugins/dataTablesRPT").Include(
                      "~/Scripts/plugins/dataTables/datatables.min.js",
                      "~/Scripts/plugins/dataTables/DatatablesConfigurationsRPT.js"));

            // jeditable
            bundles.Add(new ScriptBundle("~/plugins/jeditable").Include(
                       "~/Scripts/plugins/jeditable/jquery.jeditable.js"));

            // jqGrid styles
            bundles.Add(new StyleBundle("~/Content/plugins/jqGrid/jqGridStyles").Include(
                      "~/Content/plugins/jqGrid/ui.jqgrid.css"));

            // jqGrid
            bundles.Add(new ScriptBundle("~/plugins/jqGrid").Include(
                      "~/Scripts/plugins/jqGrid/i18n/grid.locale-en.js",
                      "~/Scripts/plugins/jqGrid/jquery.jqGrid.min.js"));

            // codeEditor styles
            bundles.Add(new StyleBundle("~/plugins/codeEditorStyles").Include(
                      "~/Content/plugins/codemirror/codemirror.css",
                      "~/Content/plugins/codemirror/ambiance.css"));

            // codeEditor
            bundles.Add(new ScriptBundle("~/plugins/codeEditor").Include(
                      "~/Scripts/plugins/codemirror/codemirror.js",
                      "~/Scripts/plugins/codemirror/mode/javascript/javascript.js"));

            // codeEditor
            bundles.Add(new ScriptBundle("~/plugins/nestable").Include(
                      "~/Scripts/plugins/nestable/jquery.nestable.js"));

            // validate
            bundles.Add(new ScriptBundle("~/plugins/validate").Include(
                      "~/Scripts/plugins/validate/jquery.validate.min.js"));

            // fullCalendar styles
            bundles.Add(new StyleBundle("~/plugins/fullCalendarStyles").Include(
                      "~/Content/plugins/fullcalendar/fullcalendar.css"));

            // fullCalendar
            bundles.Add(new ScriptBundle("~/plugins/fullCalendar").Include(
                      "~/Scripts/plugins/fullcalendar/moment.min.js",
                      "~/Scripts/plugins/fullcalendar/fullcalendar.min.js"));

            // vectorMap
            bundles.Add(new ScriptBundle("~/plugins/vectorMap").Include(
                      "~/Scripts/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                      "~/Scripts/plugins/jvectormap/jquery-jvectormap-world-mill-en.js"));

            // ionRange styles
            bundles.Add(new StyleBundle("~/Content/plugins/ionRangeSlider/ionRangeStyles").Include(
                      "~/Content/plugins/ionRangeSlider/ion.rangeSlider.css",
                      "~/Content/plugins/ionRangeSlider/ion.rangeSlider.skinFlat.css"));

            // ionRange
            bundles.Add(new ScriptBundle("~/plugins/ionRange").Include(
                      "~/Scripts/plugins/ionRangeSlider/ion.rangeSlider.min.js"));

            // dataPicker styles
            bundles.Add(new StyleBundle("~/plugins/dataPickerStyles").Include(
                      "~/Content/plugins/datapicker/datepicker3.css"));

            // dataPicker
            bundles.Add(new ScriptBundle("~/plugins/dataPicker").Include(
                      "~/Scripts/plugins/datapicker/bootstrap-datepicker.js"));

            // nouiSlider styles
            bundles.Add(new StyleBundle("~/plugins/nouiSliderStyles").Include(
                      "~/Content/plugins/nouslider/jquery.nouislider.css"));

            // nouiSlider
            bundles.Add(new ScriptBundle("~/plugins/nouiSlider").Include(
                      "~/Scripts/plugins/nouslider/jquery.nouislider.min.js"));

            // jasnyBootstrap styles
            bundles.Add(new StyleBundle("~/plugins/jasnyBootstrapStyles").Include(
                      "~/Content/plugins/jasny/jasny-bootstrap.min.css"));

            // jasnyBootstrap
            bundles.Add(new ScriptBundle("~/plugins/jasnyBootstrap").Include(
                      "~/Scripts/plugins/jasny/jasny-bootstrap.min.js"));

            // switchery styles
            bundles.Add(new StyleBundle("~/plugins/switcheryStyles").Include(
                      "~/Content/plugins/switchery/switchery.css"));

            // switchery
            bundles.Add(new ScriptBundle("~/plugins/switchery").Include(
                      "~/Scripts/plugins/switchery/switchery.js"));

            // chosen styles
            bundles.Add(new StyleBundle("~/Content/plugins/chosen/chosenStyles").Include(
                      "~/Content/plugins/chosen/bootstrap-chosen.css"));

            // chosen
            bundles.Add(new ScriptBundle("~/plugins/chosen").Include(
                      "~/Scripts/plugins/chosen/chosen.jquery.js"));

            // knob
            bundles.Add(new ScriptBundle("~/plugins/knob").Include(
                      "~/Scripts/plugins/jsKnob/jquery.knob.js"));

            // wizardSteps styles
            bundles.Add(new StyleBundle("~/plugins/wizardStepsStyles").Include(
                      "~/Content/plugins/steps/jquery.steps.css"));

            // wizardSteps
            bundles.Add(new ScriptBundle("~/plugins/wizardSteps").Include(
                      "~/Scripts/plugins/staps/jquery.steps.min.js"));

            // dropZone styles
            bundles.Add(new StyleBundle("~/Content/plugins/dropzone/dropZoneStyles").Include(
                      "~/Content/plugins/dropzone/basic.css",
                      "~/Content/plugins/dropzone/dropzone.css"));

            // dropZone
            bundles.Add(new ScriptBundle("~/plugins/dropZone").Include(
                      "~/Scripts/plugins/dropzone/dropzone.js"));

            // summernote styles
            bundles.Add(new StyleBundle("~/plugins/summernoteStyles").Include(
                      "~/Content/plugins/summernote/summernote.css",
                      "~/Content/plugins/summernote/summernote-bs3.css"));

            // summernote
            bundles.Add(new ScriptBundle("~/plugins/summernote").Include(
                      "~/Scripts/plugins/summernote/summernote.min.js"));

            // toastr notification
            bundles.Add(new ScriptBundle("~/plugins/toastr").Include(
                      "~/Scripts/plugins/toastr/toastr.min.js"));

            // toastr notification styles
            bundles.Add(new StyleBundle("~/plugins/toastrStyles").Include(
                      "~/Content/plugins/toastr/toastr.min.css"));

            // color picker
            bundles.Add(new ScriptBundle("~/plugins/colorpicker").Include(
                      "~/Scripts/plugins/colorpicker/bootstrap-colorpicker.min.js"));

            // color picker styles
            bundles.Add(new StyleBundle("~/Content/plugins/colorpicker/colorpickerStyles").Include(
                      "~/Content/plugins/colorpicker/bootstrap-colorpicker.min.css"));

            // image cropper
            bundles.Add(new ScriptBundle("~/plugins/imagecropper").Include(
                      "~/Scripts/plugins/cropper/cropper.min.js"));

            // image cropper styles
            bundles.Add(new StyleBundle("~/plugins/imagecropperStyles").Include(
                      "~/Content/plugins/cropper/cropper.min.css"));

            // jsTree
            bundles.Add(new ScriptBundle("~/plugins/jsTree").Include(
                      "~/Scripts/plugins/jsTree/jstree.min.js"));

            // jsTree styles
            bundles.Add(new StyleBundle("~/Content/plugins/jsTree").Include(
                      "~/Content/plugins/jsTree/style.css"));

            // Diff
            bundles.Add(new ScriptBundle("~/plugins/diff").Include(
                      "~/Scripts/plugins/diff_match_patch/javascript/diff_match_patch.js",
                      "~/Scripts/plugins/preetyTextDiff/jquery.pretty-text-diff.min.js"));

            // Idle timer
            bundles.Add(new ScriptBundle("~/plugins/idletimer").Include(
                      "~/Scripts/plugins/idle-timer/idle-timer.min.js"));

            // Tinycon
            bundles.Add(new ScriptBundle("~/plugins/tinycon").Include(
                      "~/Scripts/plugins/tinycon/tinycon.min.js"));

            // Chartist
            bundles.Add(new StyleBundle("~/plugins/chartistStyles").Include(
                      "~/Content/plugins/chartist/chartist.min.css"));

            // jsTree styles
            bundles.Add(new ScriptBundle("~/plugins/chartist").Include(
                      "~/Scripts/plugins/chartist/chartist.min.js"));

            // Awesome bootstrap checkbox
            bundles.Add(new StyleBundle("~/plugins/awesomeCheckboxStyles").Include(
                      "~/Content/plugins/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css"));

            // Clockpicker styles
            bundles.Add(new StyleBundle("~/plugins/clockpickerStyles").Include(
                      "~/Content/plugins/clockpicker/clockpicker.css"));

            // Clockpicker
            bundles.Add(new ScriptBundle("~/plugins/clockpicker").Include(
                      "~/Scripts/plugins/clockpicker/clockpicker.js"));

            // Date range picker Styless
            bundles.Add(new StyleBundle("~/plugins/dateRangeStyles").Include(
                      "~/Content/plugins/daterangepicker/daterangepicker-bs3.css"));

            // Date range picker
            bundles.Add(new ScriptBundle("~/plugins/dateRange").Include(
                      // Date range use moment.js same as full calendar plugin
                      "~/Scripts/plugins/fullcalendar/moment.min.js",
                      "~/Scripts/plugins/daterangepicker/daterangepicker.js"));

            // Sweet alert Styless
            bundles.Add(new StyleBundle("~/plugins/sweetAlertStyles").Include(
                      "~/Content/plugins/sweetalert/sweetalert.css"));

            // Sweet alert
            bundles.Add(new ScriptBundle("~/plugins/sweetAlert").Include(
                      "~/Scripts/plugins/sweetalert/sweetalert.min.js"));

            // Footable Styless
            bundles.Add(new StyleBundle("~/plugins/footableStyles").Include(
                      "~/Content/plugins/footable/footable.core.css", new CssRewriteUrlTransform()));

            // Footable alert
            bundles.Add(new ScriptBundle("~/plugins/footable").Include(
                      "~/Scripts/plugins/footable/footable.all.min.js"));

            // Select2 Styless
            bundles.Add(new StyleBundle("~/plugins/select2Styles").Include(
                      "~/Content/plugins/select2/select2.min.css"));

            // Select2
            bundles.Add(new ScriptBundle("~/plugins/select2").Include(
                      "~/Scripts/plugins/select2/select2.full.min.js"));

            // Masonry
            bundles.Add(new ScriptBundle("~/plugins/masonry").Include(
                      "~/Scripts/plugins/masonary/masonry.pkgd.min.js"));

            // Slick carousel Styless
            bundles.Add(new StyleBundle("~/plugins/slickStyles").Include(
                      "~/Content/plugins/slick/slick.css", new CssRewriteUrlTransform()));

            // Slick carousel theme Styless
            bundles.Add(new StyleBundle("~/plugins/slickThemeStyles").Include(
                      "~/Content/plugins/slick/slick-theme.css", new CssRewriteUrlTransform()));

            // Slick carousel
            bundles.Add(new ScriptBundle("~/plugins/slick").Include(
                      "~/Scripts/plugins/slick/slick.min.js"));

            // Ladda buttons Styless
            bundles.Add(new StyleBundle("~/plugins/laddaStyles").Include(
                      "~/Content/plugins/ladda/ladda-themeless.min.css"));

            // Ladda buttons
            bundles.Add(new ScriptBundle("~/plugins/ladda").Include(
                      "~/Scripts/plugins/ladda/spin.min.js",
                      "~/Scripts/plugins/ladda/ladda.min.js",
                      "~/Scripts/plugins/ladda/ladda.jquery.min.js"));

            // Dotdotdot buttons
            bundles.Add(new ScriptBundle("~/plugins/truncate").Include(
                      "~/Scripts/plugins/dotdotdot/jquery.dotdotdot.min.js"));

            // Touch Spin Styless
            bundles.Add(new StyleBundle("~/plugins/touchSpinStyles").Include(
                      "~/Content/plugins/touchspin/jquery.bootstrap-touchspin.min.css"));

            // Touch Spin
            bundles.Add(new ScriptBundle("~/plugins/touchSpin").Include(
                      "~/Scripts/plugins/touchspin/jquery.bootstrap-touchspin.min.js"));

            // Tour Styless
            bundles.Add(new StyleBundle("~/plugins/tourStyles").Include(
                      "~/Content/plugins/bootstrapTour/bootstrap-tour.min.css"));

            // Tour Spin
            bundles.Add(new ScriptBundle("~/plugins/tour").Include(
                      "~/Scripts/plugins/bootstrapTour/bootstrap-tour.min.js"));

            // i18next Spin
            bundles.Add(new ScriptBundle("~/plugins/i18next").Include(
                      "~/Scripts/plugins/i18next/i18next.min.js"));

            // Clipboard Spin
            bundles.Add(new ScriptBundle("~/plugins/clipboard").Include(
                      "~/Scripts/plugins/clipboard/clipboard.min.js"));

            // c3 Styless
            bundles.Add(new StyleBundle("~/plugins/c3Styles").Include(
                      "~/Content/plugins/c3/c3.min.css"));

            // c3 Charts
            bundles.Add(new ScriptBundle("~/plugins/c3").Include(
                      "~/Scripts/plugins/c3/c3.min.js"));

            // D3
            bundles.Add(new ScriptBundle("~/plugins/d3").Include(
                      "~/Scripts/plugins/d3/d3.min.js"));

            // Markdown Styless
            bundles.Add(new StyleBundle("~/plugins/markdownStyles").Include(
                      "~/Content/plugins/bootstrap-markdown/bootstrap-markdown.min.css"));

            // Markdown
            bundles.Add(new ScriptBundle("~/plugins/markdown").Include(
                      "~/Scripts/plugins/bootstrap-markdown/bootstrap-markdown.js",
                      "~/Scripts/plugins/bootstrap-markdown/markdown.js"));

            // Datamaps
            bundles.Add(new ScriptBundle("~/plugins/datamaps").Include(
                      "~/Scripts/plugins/topojson/topojson.js",
                      "~/Scripts/plugins/datamaps/datamaps.all.min.js"));

            // Taginputs Styless
            bundles.Add(new StyleBundle("~/plugins/tagInputsStyles").Include(
                      "~/Content/plugins/bootstrap-tagsinput/bootstrap-tagsinput.css"));

            // Taginputs
            bundles.Add(new ScriptBundle("~/plugins/tagInputs").Include(
                      "~/Scripts/plugins/bootstrap-tagsinput/bootstrap-tagsinput.js"));

            // Duallist Styless
            bundles.Add(new StyleBundle("~/plugins/duallistStyles").Include(
                      "~/Content/plugins/dualListbox/bootstrap-duallistbox.min.css"));

            // Duallist
            bundles.Add(new ScriptBundle("~/plugins/duallist").Include(
                      "~/Scripts/plugins/dualListbox/jquery.bootstrap-duallistbox.js"));

            // SocialButtons styles
            bundles.Add(new StyleBundle("~/plugins/socialButtonsStyles").Include(
                      "~/Content/plugins/bootstrapSocial/bootstrap-social.css"));

            // Typehead
            bundles.Add(new ScriptBundle("~/plugins/typehead").Include(
                      "~/Scripts/plugins/typehead/bootstrap3-typeahead.min.js"));

            // Pdfjs
            bundles.Add(new ScriptBundle("~/plugins/pdfjs").Include(
                      "~/Scripts/plugins/pdfjs/pdf.js"));

            // iziToast css
            bundles.Add(new StyleBundle("~/Content/plugins/izitoast/iziToast").Include(
                      "~/Content/plugins/izitoast/iziToast.css"));

            bundles.Add(new StyleBundle("~/Content/plugins/izitoast/iziToast.min").Include(
                      "~/Content/plugins/izitoast/iziToast.min.css"));
            #endregion

            #region Planillas

            #region ReportesPlanilla

            bundles.Add(new ScriptBundle("~/Scripts/app/General/ReportesPlanilla").Include(
                "~/Scripts/app/General/ReportesPlanilla.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/ReportesDeducciones").Include(
                "~/Scripts/app/General/ReportesDeducciones.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/ReportesDecimoTercerMes").Include(
                "~/Scripts/app/General/ReportesDecimoTercerMes.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/ReportesIngresos").Include(
                "~/Scripts/app/General/ReportesIngresos.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/ReportesDecimoCuarto").Include(
                "~/Scripts/app/General/ReportesDecimoCuarto.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/ReportesInstitucionesFinancieras").Include(
                "~/Scripts/app/General/ReportesInstitucionesFinancieras.js"));



            #endregion


            #region Equipo Flamenco
            bundles.Add(new ScriptBundle("~/Scripts/app/general/DeduccionesExtraordinarias").Include(
                "~/Scripts/app/General/DeduccionesExtraordinarias.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/DeduccionAFP").Include(
                "~/Scripts/app/General/DeduccionAFP.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/AFP").Include(
                "~/Scripts/app/General/AFP.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/DeduccionesIndividuales").Include(
                "~/Scripts/app/General/DeduccionesIndividuales.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/IngresosIndividuales").Include(
                "~/Scripts/app/General/IngresosIndividuales.js"));

            #endregion

            #region Equipo Willian
            //bundles APP/GENERAL
            bundles.Add(new ScriptBundle("~/Scripts/app/general/Planilla").Include(
                "~/Scripts/app/General/Planilla.js",
                "~/Scripts/app/General/excelexportjs.js",
                "~/Scripts/plugins/switchery/switchery.js",
                "~/Scripts/plugins/dualListbox/jquery.bootstrap-duallistbox.js",
                "~/Scripts/app/FileSaver.min.js"));

            //planilla
            bundles.Add(new StyleBundle("~/Panilla/css").Include(
                      "~/Content/plugins/dualListbox/bootstrap-duallistbox.min.css",
                      "~/Content/plugins/iCheck/custom.css"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/ISR").Include(
                "~/Scripts/app/General/ISR.js"));

            //bundles APP/GENERAL
            bundles.Add(new ScriptBundle("~/Scripts/app/general/CatalogoDeIngresos").Include(
                "~/Scripts/app/General/CatalogoDeIngresos.js"));

            //bundles APP/GENERAL
            bundles.Add(new ScriptBundle("~/Scripts/app/general/TechosDeducciones").Include(
                "~/Scripts/app/General/TechosDeducciones.js"));
            //bundles APP/GENERAL
            bundles.Add(new ScriptBundle("~/Scripts/app/general/AcumuladosISR").Include(
                "~/Scripts/app/General/AcumuladosISR.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/CatalogoPlanillas").Include(
                "~/Scripts/app/General/CatalogoPlanillas.min.js"));

            // SCRIPT GENERAL
            bundles.Add(new ScriptBundle("~/Scripts/app/general").Include(
             "~/Scripts/app/general/general.js"));

            bundles.Add(new ScriptBundle("~/Scripts/plugins/iziToast/iziToast").Include(
             "~/Scripts/plugins/iziToast/iziToast.js"));

            bundles.Add(new ScriptBundle("~/Scripts/plugins/iziToast/iziToast.min").Include(
             "~/Scripts/plugins/iziToast/iziToast.min.js"));



            //bundles APP/GENERAL
            bundles.Add(new ScriptBundle("~/Scripts/app/general/Planilla").Include(
                "~/Scripts/app/General/Planilla.js"));

            //bundles APP/GENERAL
            bundles.Add(new ScriptBundle("~/Scripts/app/general/CatalogoDeIngresos").Include(
                "~/Scripts/app/General/CatalogoDeIngresos.js"));

            //bundles APP/GENERAL
            bundles.Add(new ScriptBundle("~/Scripts/app/general/TechosDeducciones").Include(
                "~/Scripts/app/General/TechosDeducciones.js"));

            // SELVIN

            bundles.Add(new ScriptBundle("~/plugins/dataTablesSelvin").Include(
                 "~/Scripts/plugins/dataTables/datatables.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/IndexHistorialIncapacidades").Include(
             "~/Scripts/app/General/HistorialIncapacidades/IndexHistorialIncapacidades.js"));

            //dataTables.buttons.min.js

            bundles.Add(new StyleBundle("~/Content/app/General").Include(
                 "~/Content/app/General/catalogoPlanillas.css"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/Liquidacion").Include(
                "~/Scripts/app/General/Liquidacion.js"));
            #endregion

            #region Equipo Malcom

            //bundles ScripBase/Validate DataAnnotations
            bundles.Add(new ScriptBundle("~/Scripts/Scripts_Base/Jquery-Validate-DataAnnotations").Include(
                "~/Scripts/Scripts_Base/jquery.validate.js",
                "~/Scripts/Scripts_Base/jquery.validate.min.js",
                "~/Scripts/Scripts_Base/jquery.validate-vsdoc.js",
                "~/Scripts/Scripts_Base/jquery.validate.unobtrusive.js",
                "~/Scripts/Scripts_Base/jquery.validate.unobtrusive.min.js"));

            //==========================EQUIPO MALCOM ===========================
            bundles.Add(new ScriptBundle("~/Scripts/app/General/TipoDeducciones").Include(
                "~/Scripts/app/General/TipoDeducciones.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/EmpleadoBonos").Include(
                "~/Scripts/app/General/EmpleadoBonos.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/FormaPago").Include(
                "~/Scripts/app/General/FormaPago.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/DecimoCuartoMes").Include(
                "~/Scripts/app/General/DecimoCuartoMes.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/AdelantoSueldo").Include(
                "~/Scripts/app/General/AdelantoSueldo.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/Period").Include(
                 "~/Scripts/app/General/Periodos.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/Preaviso").Include(
                "~/Scripts/app/General/Preaviso.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/InstitucionesFinancierasIndex").Include(
                "~/Scripts/app/General/InstitucionesFinancierasIndex.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/PagoCesantia").Include(
                "~/Scripts/app/General/PagoDeCesantia.js",
                "~/Scripts/app/General/excelexportjs.js",
                "~/Scripts/plugins/switchery/switchery.js",
                "~/Scripts/plugins/dualListbox/jquery.bootstrap-duallistbox.js",
                "~/Scripts/app/FileSaver.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/Planilla_ImpuestoVecinal").Include(
                "~/Scripts/app/General/Planilla_ImpuestoVecinal.js",
                "~/Scripts/app/General/excelexportjs.js",
                "~/Scripts/plugins/switchery/switchery.js",
                "~/Scripts/plugins/dualListbox/jquery.bootstrap-duallistbox.js",
                "~/Scripts/app/FileSaver.min.js"));

            #endregion

            #region Otros
            bundles.Add(new ScriptBundle("~/Scripts/app/General/FechaPlanilla").Include(
                "~/Scripts/app/General/FechaPlanilla.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/Menu").Include(
                "~/Scripts/app/General/Menu.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/IV").Include(
                "~/Scripts/app/General/IV.js"));

            //mascaras de entrada
            bundles.Add(new ScriptBundle("~/plugins/mascarasDeEntrada").Include(
                 "~/Scripts/plugins/mascarasDeEntrada/js/jquery.inputmask.bundle.js"));

            //DataAnnotations
            bundles.Add(new ScriptBundle("~/Scripts/Scripts_Base/Jquery-Validate-DataAnnotations").Include(
                "~/Scripts/Scripts_Base/jquery.validate.js",
                "~/Scripts/Scripts_Base/jquery.validate.min.js",
                "~/Scripts/Scripts_Base/jquery.validate-vsdoc.js",
                "~/Scripts/Scripts_Base/jquery.validate.unobtrusive.js",
                "~/Scripts/Scripts_Base/jquery.validate.unobtrusive.min.js"));
            #endregion
            #endregion

            #region Recursos Humanos
            bundles.Add(new ScriptBundle("~/Scripts/app/datatableEdit/").Include(
            "~/Scripts/app/General/datatableEdit.js"));
            // SCRIPT HABILIDADES ADMIN
            bundles.Add(new ScriptBundle("~/Scripts/app/general/Habilidades/Admin").Include(
            "~/Scripts/app/general/Habilidades/Admin.js"));


            //SCRIPT TIPOHORAS ADMIN
            bundles.Add(new ScriptBundle("~/Scripts/app/general/TipoHOras/Admin").Include(
            "~/Scripts/app/general/TipoHOras/Admin.js"));

            //SCRIPT HISTORIALPERMISOS ADMIN
            bundles.Add(new ScriptBundle("~/Scripts/app/general/HistorialPermisos/Admin").Include(
            "~/Scripts/app/general/HistorialPermisos/Admin.js"));

            // SCRIPT TIPOSALIDAS ADMIN
            bundles.Add(new ScriptBundle("~/Scripts/app/general/TipoSalidas/Admin").Include(
            "~/Scripts/app/general/TipoSalidas/Admin.js"));


            //SCRIPT CARGOS ADMIN
            bundles.Add(new ScriptBundle("~/Scripts/app/general/Cargos/Admin").Include(
          "~/Scripts/app/general/Cargos/Admin.js"));

            //SCRIPT HABILIDADES
            bundles.Add(new ScriptBundle("~/Scripts/app/general/Habilidades").Include(
            "~/Scripts/app/general/Habilidades/Habilidades.js"));

            //SCRIPT TIPOSALIDAS
            bundles.Add(new ScriptBundle("~/Scripts/app/general/TipoSalidas").Include(
            "~/Scripts/app/general/TipoSalidas/TipoSalidas.js"));

            //SCRIPT IDIOMAS
            bundles.Add(new ScriptBundle("~/Scripts/app/general/Idiomas").Include(
                "~/Scripts/app/general/Idiomas/Idiomas.js"));

            //SCRIPT TITULOS
            bundles.Add(new ScriptBundle("~/Scripts/app/General/Titulos").Include(
                "~/Scripts/app/General/Titulos/Titulos.js"));

            //SCRIPT TIPOINCAPACIDADES
            bundles.Add(new ScriptBundle("~/Scripts/app/General/TipoIncapacidades").Include(
               "~/Scripts/app/General/TipoIncapacidades/TipoIncapacidades.js"));

            //SCRIPT COMPETENCIAS
            bundles.Add(new ScriptBundle("~/Scripts/app/Competencias/Competencias").Include(
                  "~/Scripts/app/general/Competencias/Competencias.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/Competencias/Admin").Include(
            "~/Scripts/app/General/Competencias/Admin.js"));

            //bundles APP/TipoHoras/TipoHoras
            bundles.Add(new ScriptBundle("~/Scripts/app/General/TipoHoras").Include(
                "~/Scripts/app/General/TipoHoras/TipoHoras.js"));

            //SCRIPT FASES RECLUTAMIENTO
            bundles.Add(new ScriptBundle("~/Scripts/app/general/FasesReclutaiento").Include(
                "~/Scripts/app/General/FasesReclutamiento/FasesReclutamiento.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/FasesReclutaiento/Admin").Include(
                    "~/Scripts/app/General/FasesReclutamiento/Admin.js"));

            //SCRIPT EMPRESAS
            bundles.Add(new ScriptBundle("~/Scripts/app/general/Empresas").Include(
                "~/Scripts/app/general/Empresas/Empresas.js"));



            bundles.Add(new ScriptBundle("~/Scripts/app/general/Admin/Empresas").Include(
                          "~/Scripts/app/general/Empresas/Admin.js"));


            //SCRIPT TIPOMONEDAS
            bundles.Add(new ScriptBundle("~/Scripts/app/general/TipoMonedas").Include(
                "~/Scripts/app/general/TipoMonedas/TipoMonedas.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/TipoMonedas/Admin").Include(
                "~/Scripts/app/general/TipoMonedas/Admin.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/Idiomas/Idiomas").Include(
                               "~/Scripts/app/general/Idiomas/Idiomas.js"));


            //SCRIPT EMPLEADOS
            bundles.Add(new ScriptBundle("~/Scripts/app/general/Empleados").Include(
                              "~/Scripts/app/general/Empleados/IndexEmpleados.js",
                              "~/Scripts/app/general/Empleados/AgregarEmpleado.js",
                              "~/Scripts/app/general/Empleados/DocumentoEmpleado.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/ExpedienteEmpleado").Include(
                              "~/Scripts/app/general/Empleados/ExpedienteEmpleado.js"));
            //SCRIPT CARGOS
            bundles.Add(new ScriptBundle("~/Scripts/app/general/Cargos").Include(
                             "~/Scripts/app/general/Cargos/Cargos.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/TipoAmonestaciones").Include(
                  "~/Scripts/app/General/TipoAmonestaciones/TipoAmonestaciones.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/TipoAmonestaciones/Admin").Include(
                  "~/Scripts/app/General/TipoAmonestaciones/Admin.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/Requisiciones").Include(
                "~/Scripts/app/General/Requisiciones/IndexRequisiciones.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/Requisiciones/Admin").Include(
                "~/Scripts/app/General/Requisiciones/Admin.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/CreateRequisiciones").Include(
                "~/Scripts/app/General/Requisiciones/CreateRequisiciones.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/EditRequisiciones").Include(
                "~/Scripts/app/General/Requisiciones/EditRequisiciones.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/RazonSalidas").Include(
              "~/Scripts/app/general/RazonSalidas/RazonSalidas.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/RazonSalidas/Admin").Include("~/Scripts/app/General/RazonSalidas/Admin.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/Nacionalidades").Include(
            "~/Scripts/app/General/Nacionalidades/Nacionalidades.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/Nacionalidades/Admin").Include(
            "~/Scripts/app/General/Nacionalidades/Admin.js"));


            bundles.Add(new ScriptBundle("~/Scripts/app/general/Habilidades").Include(
            "~/Scripts/app/general/Habilidades/Habilidades.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/TipoSalidas").Include(
            "~/Scripts/app/general/TipoSalidas/TipoSalidas.js"));
            //script de areas
            bundles.Add(new ScriptBundle("~/Scripts/app/general/IndexArea").Include(
            "~/Scripts/app/general/Areas/IndexAreas.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/CreateArea").Include(
                        "~/Scripts/app/general/Areas/CreateArea.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/EditArea").Include(
                        "~/Scripts/app/general/Areas/EditArea.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/TipoIncapacidades/Admin").Include(
             "~/Scripts/app/general/TipoIncapacidades/Admin.js"));


            bundles.Add(new ScriptBundle("~/Scripts/app/General/Titulos/Admin").Include(
           "~/Scripts/app/General/Titulos/Admin.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/HistorialAmonestaciones/AdminHistorialAmonestaciones").Include(
                       "~/Scripts/app/General/HistorialAmonestaciones/Admin.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/Idiomas/Admin").Include(
              "~/Scripts/app/general/Idiomas/Admin.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/Admin").Include(
                "~/Scripts/app/general/Areas/Admin.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/IndexHistorialAmonestaciones").Include(
                         "~/Scripts/app/General/HistorialAmonestaciones/IndexHistorialAmonestaciones.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/HistorialCargos").Include(
                         "~/Scripts/app/General/HistorialCargos/HistorialCargos.js"));
            bundles.Add(new ScriptBundle("~/Scripts/app/General/PromoverHistorialCargos").Include(
                 "~/Scripts/app/General/HistorialCargos/PromoverHistorialCargos.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/HistorialHorasTrabajadas").Include(
                "~/Scripts/app/General/HistorialHorasTrabajadas/HistorialHorasTrabajadas.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/HistorialContrataciones").Include(
                "~/Scripts/app/General/HistorialContrataciones/HistorialContrataciones.js"));
            bundles.Add(new ScriptBundle("~/Scripts/app/General/HistorialAmonestaciones").Include(
                "~/Scripts/app/General/CreateHistorialAmonestaciones.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/IndexHistorialAmonestaciones").Include(
               "~/Scripts/app/General/HistorialAmonestaciones/IndexHistorialAmonestaciones.js"));
            //App/Jornadas
            bundles.Add(new ScriptBundle("~/Scripts/app/General/Jornadas").Include(
                "~/Scripts/app/General/Jornadas/IndexJornadas.js"));

            //App/Admin Jornadas
            bundles.Add(new ScriptBundle("~/Scripts/app/General/AdminJornadas").Include(
                "~/Scripts/app/General/Jornadas/Admin.js"));


            //App/Jornadas
            bundles.Add(new ScriptBundle("~/Scripts/app/General/EquipoEmpleados").Include(
                "~/Scripts/app/General/EquipoEmpleados/EquipoEmpleados.js"));

            //~/Scripts/app/general/Requisiciones
            bundles.Add(new ScriptBundle("~/Scripts/app/general/RequerimientosEspeciales").Include(
                "~/Scripts/app/General/RequerimientosEspeciales/RequerimientosEspeciales.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/RequerimientosEspeciales/Admin").Include(
                "~/Scripts/app/General/RequerimientosEspeciales/Admin.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/Requisiciones").Include(
                "~/Scripts/app/General/Requisiciones/IndexRequisiciones.js"));


            bundles.Add(new ScriptBundle("~/Scripts/app/general/CreateRequisiciones").Include(
                "~/Scripts/app/General/Requisiciones/CreateRequisiciones.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/IndexHistorialAudienciaDescargos").Include(
              "~/Scripts/app/General/HistorialAudiencias/IndexHistorialAudienciaDescargos.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/CreateHistorialAudienciaDescargos").Include(
               "~/Scripts/app/General/HistorialIncapacidades/CreateIncapacidades.js"));


            bundles.Add(new ScriptBundle("~/Scripts/app/General/HistorialAudienciaDescargos/Admin").Include(
             "~/Scripts/app/General/HistorialAudiencias/Admin.js"));


            bundles.Add(new ScriptBundle("~/Scripts/app/general/CreatePersonas").Include(
                "~/Scripts/app/General/Personas/CreatePersonas.js"));
            //Inputmask
            bundles.Add(new ScriptBundle("~/Content/InputMask").Include(
            "~/Content/plugins/jasny/jasny-bootstrap.min.css"));

            bundles.Add(new ScriptBundle("~/Scripts/plugins/jasny").Include(
                "~/Scripts/plugins/jasny/jasny-bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/EditPersonas").Include(
                "~/Scripts/app/General/Personas/EditPersonas.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/Personas/Admin").Include(
                "~/Scripts/app/General/Personas/Admin.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/TipoHoras").Include(
                "~/Scripts/app/General/TipoHoras/TipoHoras.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/IndexHistorialSalidas").Include(
            "~/Scripts/app/general/HistorialSalidas/IndexHistorialSalidas.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/CreateHistorialSalidas").Include(
            "~/Scripts/app/general/HistorialSalidas/CreateHistorialSalidas.js"));

            // SCRIPT Hsal
            bundles.Add(new ScriptBundle("~/Scripts/app/general/HistorialSalidas/Admin").Include(
            "~/Scripts/app/general/HistorialSalidas/Admin.js"));

            // SCRIPT tper
            bundles.Add(new ScriptBundle("~/Scripts/app/general/TipoPermisos/Admin").Include(
            "~/Scripts/app/general/TipoPermisos/Admin.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/HistorialVacaciones/Admin").Include(
                "~/Scripts/app/General/HistorialVacaciones/Admin.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/HistorialVacaciones").Include(
                "~/Scripts/app/General/CreateHistorialVacaciones.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/IndexHistorialVacaciones").Include(
   "~/Scripts/app/General/HistorialVacaciones/IndexHistorialVacaciones.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/HistorialVacaciones/DateDiff").Include(
   "~/Scripts/app/General/HistorialVacaciones/DateDiff.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/IndexSueldos").Include(
                         "~/Scripts/app/general/Sueldos/IndexSueldos.js"));


            //SELECCION CANDIDATOS
            bundles.Add(new ScriptBundle("~/Scripts/app/General/SeleccionCandidatos").Include(
             "~/Scripts/app/General/SeleccionCandidatos/IndexSeleccionCandidatos.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/IndexHistorialAudienciaDescargos").Include(
               "~/Scripts/app/General/HistorialAudiencias/IndexHistorialAudienciaDescargos.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/SeleccionCandidatos/Admin").Include(
                  "~/Scripts/app/General/SeleccionCandidatos/Admin.js"));


            bundles.Add(new ScriptBundle("~/Scripts/app/General/ContratarCandidato").Include(
                    "~/Scripts/app/General/SeleccionCandidatos/ContratarCandidato.js"));

            // dataTables
            bundles.Add(new ScriptBundle("~/plugins/dataTablesSeleccionCandidatos").Include(
                      "~/Scripts/plugins/dataTables/datatables.min.js"));

            bundles.Add(new ScriptBundle("~/plugins/dataTablesSeleccionCandidatos2").Include(
          "~/Scripts/plugins/dataTables/Datatables_SeleccionCandidatos.js"));


            bundles.Add(new ScriptBundle("~/Scripts/app/TipoPermisos").Include(
                 "~/Scripts/app/general/TipoPermisos/TipoPermisos.js"));

            bundles.Add(new ScriptBundle("~/Scripts/plugins/dataTablesMatutina/datatables.min").Include(
                      "~/Scripts/plugins/dataTablesMatutina/datatables.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/plugins/dataTablesMatutina/DatatablesConfigurations").Include(
            "~/Scripts/plugins/dataTablesMatutina/DatatablesConfigurations.js"));

            //App/HistorialPermisos
            bundles.Add(new ScriptBundle("~/Scripts/app/General/HistorialPermisos").Include(
                "~/Scripts/app/General/HistorialPermisos/IndexHistorialPermisos.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/CreateHistorialPermisos").Include(
                 "~/Scripts/app/general/HistorialPermisos/CreateHistorialPermisos.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/EquipoTrabajo").Include(
                 "~/Scripts/app/general/EquipoTrabajo/EquipoTrabajo.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/Competencias/Admin").Include(
                 "~/Scripts/app/General/Competencias/Admin.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/General/HistorialVacaciones/Admin").Include(
                 "~/Scripts/app/General/HistorialVacaciones/Admin.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general/EquipoTrabajo/Admin").Include(
                "~/Scripts/app/general/EquipoTrabajo/Admin.js"));

            bundles.Add(new ScriptBundle("~/Scripts/app/general_Fechas").Include(
             "~/Scripts/app/general/general_Fechas.js"));

            #endregion

            #region Optimizaciones
            //Allow any type of Content Delivery Network
            bundles.UseCdn = true;

            //Execute the Optimization at Bundles in runtime
            BundleTable.EnableOptimizations = true;

#if DEBUG
            BundleTable.EnableOptimizations = false;
#endif
            #endregion
        }
    }
}
