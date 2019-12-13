using System.Web;
using System.Web.Optimization;

namespace CM.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region styles

            bundles.Add(new StyleBundle("~/assets/materialDesignIconsCss").Include(
                        "~/assets/vendors/mdi/css/materialdesignicons.min.css"));

            bundles.Add(new StyleBundle("~/assets/vendorsCss").Include(
                        "~/assets/vendors/css/vendor.bundle.base.css"));

            bundles.Add(new StyleBundle("~/assets/styles").Include(
                        "~/assets/css/style.css"));

            bundles.Add(new StyleBundle("~/assets/bootstrapCss").Include(
                        "~/Content/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/assets/jqueryDatatablesCss").Include(
                        "~/assets/css/jquery.dataTables.min.css",
                        "~/assets/css/dataTables.bootstrap4.min.css"));

            bundles.Add(new StyleBundle("~/assets/dateTimePickerCss").Include(
                       "~/assets/css/datetimepicker.min.css"));

            bundles.Add(new StyleBundle("~/assets/siteCss").Include(
                        "~/assets/css/Site.css"));

            #endregion

            #region scripts

            bundles.Add(new ScriptBundle("~/assets/jqueryJs").Include(
                "~/Scripts/jquery-{version}.js",
                "~/assets/js/moment.min.js"));

            bundles.Add(new ScriptBundle("~/assets/bootstrapJs").Include(
                "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/assets/validateClientSideScripts").Include(
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/assets/datatablesJs").Include(
                "~/assets/js/jquery.dataTables.min.js",
                "~/assets/js/dataTables.bootstrap4.min.js"));

            bundles.Add(new ScriptBundle("~/assets/notifyJs").Include(
               "~/assets/js/notify.min.js"));

            bundles.Add(new ScriptBundle("~/assets/dateTimePickerJs").Include(
               "~/assets/js/datetimepicker.js"));

            bundles.Add(new ScriptBundle("~/assets/scripts").Include(
                "~/assets/js/off-canvas.js",
                "~/assets/js/hoverable-collapse.js",
                "~/assets/js/misc.js",
                "~/assets/js/dashboard.js",
                "~/assets/js/todolist.js"
                ));

            #endregion
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
                            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}
