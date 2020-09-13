using System.Web.Optimization;

namespace MaverickSystem
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            //CSS
            bundles.Add(new StyleBundle("~/Content/jvectormap").Include(
                      "~/Assets/css/vendor/jquery-jvectormap-1.2.2.css"));

            bundles.Add(new StyleBundle("~/Content/icons").Include(
                     "~/Assets/css/icons.min.css"));

            bundles.Add(new StyleBundle("~/Content/app").Include(
                     "~/Assets/css/app.min.css"));

            bundles.Add(new StyleBundle("~/Content/userbootstrap").Include(
                     "~/UserAssets/css/bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/userlogin").Include(
                     "~/UserAssets/css/login.css"));

            bundles.Add(new StyleBundle("~/Content/maverickOrder").Include(
                    "~/UserAssets/css/maverickOrder.css"));

            bundles.Add(new StyleBundle("~/Content/pick").Include(
                    "~/UserAssets/css/pick-pcc.css"));

            bundles.Add(new StyleBundle("~/Content/pickuptime").Include(
                    "~/UserAssets/css/pickuptime.css"));

            bundles.Add(new StyleBundle("~/Content/themify").Include(
                    "~/UserAssets/css/themify-icons.css"));

            bundles.Add(new StyleBundle("~/Content/swiper").Include(
                    "~/UserAssets/css/swiper.min.css"));

            bundles.Add(new StyleBundle("~/Content/flaticon").Include(
                    "~/UserAssets/css/flaticon.css"));

            bundles.Add(new StyleBundle("~/Content/style").Include(
                    "~/UserAssets/css/style.css"));

            bundles.Add(new StyleBundle("~/Content/jqueryUi").Include(
                    "~/UserAssets/css/jquery-ui-slider-pips.min.css"));



            //JS
            bundles.Add(new ScriptBundle("~/bundles/appJS").Include(
                      "~/Assets/js/app.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jvectormapJS").Include(
                      "~/Assets/js/vendor/jquery-jvectormap-1.2.2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jvectormapWordJS").Include(
                      "~/Assets/js/vendor/jquery-jvectormap-world-mill-en.js"));

            bundles.Add(new ScriptBundle("~/bundles/dashboard").Include(
                      "~/Assets/js/pages/demo.dashboard.js"));

            bundles.Add(new ScriptBundle("~/bundles/userbootstrapJS").Include(
                      "~/UserAssets/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/userdrawCodeJS").Include(
                      "~/UserAssets/js/drawCode.js"));

            bundles.Add(new ScriptBundle("~/bundles/pickJS").Include(
                      "~/UserAssets/js/pick-pcc.js"));

            bundles.Add(new ScriptBundle("~/bundles/pickuptimeJS").Include(
                      "~/UserAssets/js/pickuptime.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/pluginCollectionJS").Include(
                      "~/UserAssets/js/jquery-plugin-collection.js"));

            bundles.Add(new ScriptBundle("~/bundles/scriptJS").Include(
                      "~/UserAssets/js/script.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryPlusUiJS").Include(
                      "~/UserAssets/js/jquery-plus-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryUiSliderJS").Include(
                     "~/UserAssets/js/jquery-ui-slider-pips.js"));

            bundles.Add(new ScriptBundle("~/bundles/datatableToolJS").Include(
                     "~/Assets/js/mavericks/datatable-tool.js"));
        }
    }
}
