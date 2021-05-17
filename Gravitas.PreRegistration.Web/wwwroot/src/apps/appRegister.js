var registerApp = angular.module('registerApp', ['ui.bootstrap', 'toastr', 'ngSanitize', 'ui.select', 'ngMask', 'infinite-scroll'/*'ui.select', 'ngSanitize'*//*'ui.bootstrap', 'toastr', 'ui.select'*/]);
/*'ui.bootstrap', 'toastr', 'ngSanitize', 'ui.select', 'ngMask', 'infinite-scroll'*/
registerApp.config(function ($locationProvider, $provide) {
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