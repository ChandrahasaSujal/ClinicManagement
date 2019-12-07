using System.Web;
using System.Web.Optimization;

namespace CM.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/assets/styles").Include(
                        "~/Content/bootstrap.min.css",
                        "~/assets/css/jquery-ui.min.css",
                        "~/assets/css/style.css",
                        "~/assets/css/jquery.dataTables.min.css",
                        "~/assets/css/dataTables.bootstrap4.min.css",
                        "~/assets/vendors/mdi/css/materialdesignicons.min.css",
                        "~/assets/vendors/css/vendor.bundle.base.css"));
            bundles.Add(new ScriptBundle("~/assets/scripts").Include(
                "~/Scripts/jquery-{version}.js",
                "~/assets/js/jquery-ui.min.js",
                "~/assets/vendors/js/vendor.bundle.base.js",
                "~/assets/js/jquery.dataTables.min.js",
                "~/assets/js/dataTables.bootstrap4.min.js",
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
                #if DEBUG
                            BundleTable.EnableOptimizations = false;
                #else
                            BundleTable.EnableOptimizations = true;
                #endif
        }
    }
}
