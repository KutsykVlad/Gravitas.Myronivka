using System.Web.Optimization;

namespace Gravitas.Platform.Web
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js",
						"~/Scripts/jquery-ui-1.12.1.min.js",
						"~/Scripts/jquery.unobtrusive-ajax.min.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*"));

			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
						"~/Scripts/bootstrap.bundle.js",
						"~/Scripts/respond.js",
						"~/Scripts/site.js"));

			bundles.Add(new StyleBundle("~/bundles/Content/css").Include(
				"~/Content/css/bootstrap/bootstrap.css",
				"~/Content/css/font-awesome/all.css",
				"~/Content/css/select2.css",
                "~/wwwroot/lib/angular-tree-control/css/tree-control.css",
                "~/wwwroot/lib/angular-tree-control/tree-control-attribute.css",
                "~/Content/css/site.css"));

			bundles.Add(new StyleBundle("~/bundles/Content/angular-css").Include(
				"~/wwwroot/lib/angular/angular-csp.css",
				"~/wwwroot/lib/angular-toastr/dist/angular-toastr.css",
				"~/wwwroot/lib/angular-ui-bootstrap/dist/ui-bootstrap-csp.css",
				"~/wwwroot/lib/angular-ui-select/select.css",
				"~/wwwroot/lib/angular-moment-picker/dist/angular-moment-picker.css",
				"~/wwwroot/lib/angular-xeditable/dist/css/xeditable.css",
                "~/wwwroot/lib/angular-bootstrap-nav-tree-master/dist/abn_tree.css",
				"~/wwwroot/lib/ng-sortable/dist/ng-sortable.css",
				"~/wwwroot/lib/ng-sortable/dist/ng-sortable.style.css",
                "~/wwwroot/src/css/node-routine.css"));

			bundles.Add(new ScriptBundle("~/bundles/angular").Include(
				"~/wwwroot/lib/angular/angular.js",
				"~/wwwroot/lib/angular-tree-control/angular-tree-control.js",
				"~/wwwroot/lib/angular-tree-control/context-menu.js",
				"~/wwwroot/lib/angular-i18n/angular-locale_uk-ua.js",
				"~/wwwroot/lib/angular-toastr/dist/angular-toastr.js",
				"~/wwwroot/lib/angular-toastr/dist/angular-toastr.tpls.js",
				"~/wwwroot/lib/angular-ui-bootstrap/dist/ui-bootstrap.js",
				"~/wwwroot/lib/angular-ui-bootstrap/dist/ui-bootstrap-tpls.js",
				"~/wwwroot/lib/angular-sanitize/angular-sanitize.js",
				"~/wwwroot/lib/angular-messages/angular-messages.js",
				"~/wwwroot/lib/angular-ui-select/select.js",
				"~/wwwroot/lib/angular-ui-router/release/angular-ui-router.js",
				"~/wwwroot/lib/jquery/dist/jquery.js",
				"~/wwwroot/lib/angular-datatables/dist/angular-datatables.js",
				"~/wwwroot/lib/angular-datatables/dist/plugins/bootstrap/angular-datatables.bootstrap.js"));

#if DEBUG
			BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
		}
	}
}
