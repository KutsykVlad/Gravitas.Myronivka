var routesEditorApp = angular.module('routesEditorApp',
	['ui.bootstrap',
		'toastr',
		'ngSanitize',
		'ui.select',
		'ngMask',
		'infinite-scroll',
		'ngAnimate',
		'as.sortable',
		'xeditable',
		'dndLists',
		'datatables',
		'datatables.bootstrap',
		'SmartAdmin','ui.router']);

routesEditorApp.config(function ($locationProvider, $provide) {
  $locationProvider.html5Mode({
    enabled: true,
    requireBase: false
  });
  
  //Exception handling
  $provide.decorator('$exceptionHandler', ['$delegate', function ($delegate) {
    return function (exception, cause) {
      $delegate(exception, cause);
      //debugger;
    };
  }]);
});

routesEditorApp.filter('nodeStatus', function () {
	return function (item) {
		if (item) {
			return "<span class='label label-success'>Active</span>";
		}
		return "<span class='label label-default'>Disabled</span>";
	};
});

routesEditorApp.filter('nodeType', function () {
	return function (item) {
		if (item.IsStart) {
			return "first_page";
		}
		if (item.IsFinish) {
			return "last_page";
		}
		return "pages";
	};
});