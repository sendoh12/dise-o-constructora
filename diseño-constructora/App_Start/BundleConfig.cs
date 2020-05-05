using System.Web;
using System.Web.Optimization;

namespace diseño_constructora
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundle)
        {
            bundle.Add(new StyleBundle("~/bundles/css")
                .Include(
                "~/app-assets/fonts/feather/style.min.css",
                "~/app-assets/fonts/simple-line-icons/style.css",
                "~/app-assets/fonts/font-awesome/css/font-awesome.min.css",
                "~/app-assets/vendors/css/perfect-scrollbar.min.css",
                "~/app-assets/vendors/css/prism.min.css",
                "~/app-assets/vendors/css/chartist.min.css",
                "~/app-assets/css/app.css"

                ));
            bundle.Add(new ScriptBundle("~/bundles/js")
                .Include(
                "~/app-assets/vendors/js/core/jquery-3.2.1.min.js",
                "~/app-assets/vendors/js/core/popper.min.js",
                "~/app-assets/vendors/js/core/bootstrap.min.js",
                "~/app-assets/vendors/js/perfect-scrollbar.jquery.min.js",
                "~/app-assets/vendors/js/prism.min.js",
                "~/app-assets/vendors/js/jquery.matchHeight-min.js",
                "~/app-assets/vendors/js/screenfull.min.js",
                "~/app-assets/vendors/js/pace/pace.min.js",
                "~/app-assets/vendors/js/chartist.min.js",
                "~/app-assets/js/app-sidebar.js",
                "~/app-assets/js/notification-sidebar.js",
                "~/app-assets/js/customizer.js",
                "~/app-assets/js/dashboard1.js"

                ));
        }
    }
}
