var nonStandartDataApp = angular.module('nonStandartDataApp',
    ['ui.bootstrap', 'toastr', 'ngSanitize', 'ui.select']);

nonStandartDataApp.config(function ($locationProvider, $provide) {
    
    //Exception handling
    $provide.decorator('$exceptionHandler', ['$delegate', function ($delegate) {
        return function (exception, cause) {
            $delegate(exception, cause);
            //debugger;
        };
    }]);
});