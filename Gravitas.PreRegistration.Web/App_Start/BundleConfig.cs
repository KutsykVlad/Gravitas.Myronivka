using System.Web;
using System.Web.Optimization;
using System.Web.WebSockets;

namespace Gravitas.PreRegistration.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/bundles/Content/angular-css").Include(
                "~/wwwroot/lib/old/angular-toastr/dist/angular-toastr.css",
                "~/wwwroot/lib/old/angular-ui-select/select.css",
                "~/wwwroot/lib/old/angular-xeditable/dist/css/xeditable.css",
                "~/wwwroot/lib/old/ng-sortable/dist/ng-sortable.css",
                "~/wwwroot/lib/old/ng-sortable/dist/ng-sortable.style.css"
                ));


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-1.12.1.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.bundle.js",
                "~/Scripts/respond.js",
                "~/Scripts/site.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/wwwroot/lib/angular/angular.js",
                "~/wwwroot/lib/old/angular-i18n/angular-locale_uk-ua.js",
                "~/wwwroot/lib/old/angular-toastr/dist/angular-toastr.js",
                "~/wwwroot/lib/old/angular-toastr/dist/angular-toastr.tpls.js",
                "~/wwwroot/lib/old/angular-ui-bootstrap/dist/ui-bootstrap.js",
                "~/wwwroot/lib/old/angular-ui-bootstrap/dist/ui-bootstrap-tpls.js",
                "~/wwwroot/lib/old/angular-sanitize/angular-sanitize.js",
                "~/wwwroot/lib/old/angular-route/angular-route.js",
                "~/wwwroot/lib/old/angular-animate/angular-animate.js",
                "~/wwwroot/lib/old/angular-aria/angular-aria.js",
                "~/wwwroot/lib/old/angular-messages/angular-messages.js",
                "~/wwwroot/lib/old/angular-drag-and-drop-lists/angular-drag-and-drop-lists.js",
                "~/wwwroot/lib/old/ng-sortable/dist/ng-sortable.js",
                "~/wwwroot/lib/old/angular-xeditable/dist/js/xeditable.js",
                "~/wwwroot/lib/old/angular-ui-select/select.js",
                "~/wwwroot/lib/old/angular-ui-router/release/angular-ui-router.js",
                "~/wwwroot/lib/old/ng-mask/dist/ngMask.js",
                "~/wwwroot/lib/old/infinite-scrolling/infinite-scrolling.js",
                "~/wwwroot/lib/old/underscore/underscore.js",
                "~/wwwroot/lib/old/jquery/dist/jquery.js",
                "~/wwwroot/lib/old/moment/moment.js",
                "~/wwwroot/lib/old/angular-moment-picker/dist/angular-moment-picker.js").IncludeDirectory(
                "~/wwwroot/lib/old/moment/locale", "*.js").IncludeDirectory("~/wwwroot/src/apps/shared/_common", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/angularapp").Include(
                "~/wwwroot/src/apps/appRegister.js",
                "~/wwwroot/src/apps/register/registerService.js",
                "~/wwwroot/src/apps/register/registerController.js",
                "~/wwwroot/src/apps/truck-register/truckRegistrationController.js",
                "~/wwwroot/src/apps/manage/userManageController.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/Content/css").Include(
                "~/Content/css/bootstrap.css",
                "~/Content/css/all.css",
                 "~/Content/css/select2.css",
                "~/Content/css/site.css"));
        }
    }
}
