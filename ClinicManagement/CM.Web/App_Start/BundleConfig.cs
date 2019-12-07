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

            bundles.Add(new StyleBundle("~/assets/bootstrapCss").Include(
                        "~/Content/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/assets/jqueryUICss").Include(
                        "~/assets/css/jquery-ui.min.css"));

            bundles.Add(new StyleBundle("~/assets/jqueryDatatablesCss").Include(
                        "~/assets/css/jquery.dataTables.min.css",
                        "~/assets/css/dataTables.bootstrap4.min.css"));

            bundles.Add(new StyleBundle("~/assets/styles").Include(
                        "~/assets/css/style.css",
                        "~/assets/vendors/mdi/css/materialdesignicons.min.css",
                        "~/assets/vendors/css/vendor.bundle.base.css"));

            #endregion

            #region scripts

            bundles.Add(new ScriptBundle("~/assets/jqueryJs").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/assets/bootstrapJs").Include(
                "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/assets/jqueryUIJs").Include(
               "~/assets/js/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/assets/datatablesJs").Include(
                "~/assets/vendors/js/vendor.bundle.base.js"));

            bundles.Add(new ScriptBundle("~/assets/vendorJs").Include(
                "~/assets/js/jquery.dataTables.min.js"));

            bundles.Add(new ScriptBundle("~/assets/datatablesJs").Include(
                "~/assets/js/jquery.dataTables.min.js",
                "~/assets/js/dataTables.bootstrap4.min.js"));

            bundles.Add(new ScriptBundle("~/assets/scripts").Include(
                "~/assets/js/off-canvas.js",
                "~/assets/js/hoverable-collapse.js",
                "~/assets/js/misc.js",
                "~/assets/js/dashboard.js",
                "~/assets/js/todolist.js"
                ));

            bundles.Add(new StyleBundle("~/assets/datePickerStyles").Include(
                "~/assets/css/jquery-ui.css"
                ));
            bundles.Add(new ScriptBundle("~/assets/datePickerScripts").Include(
                "~/assets/js/jquery-ui.js"
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
