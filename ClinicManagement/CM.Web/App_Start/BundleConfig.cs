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
                        "~/assets/css/style.css",
                        "~/assets/vendors/mdi/css/materialdesignicons.min.css",
                        "~/assets/vendors/css/vendor.bundle.base.css"));
            bundles.Add(new ScriptBundle("~/assets/scripts").Include(
                "~/assets/vendors/js/vendor.bundle.base.js",
                "~/assets/vendors/chart.js/Chart.min.js",
                "~/assets/js/off-canvas.js",
                "~/assets/js/hoverable-collapse.js",
                "~/assets/js/misc.js",
                "~/assets/js/dashboard.js",
                "~/assets/js/todolist.js"
                ));
        }
    }
}
