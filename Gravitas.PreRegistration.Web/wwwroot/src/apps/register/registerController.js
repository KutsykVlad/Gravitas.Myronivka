function registerController(
    $scope,
    $uibModal,
    $q,
    $timeout,
    $location,
    $window,
    toastr,
    $interval,
    $http,
    registerService) {

    $scope.PartnerPage = 1;
    $scope.PartnerItems = [];
    $scope.PartnerItems.selected = undefined;
    $scope.filter = null;

    $scope.PartnerAddMoreItems = function ($select) {
        $scope.PartnerPage++;
        $scope.refreshResults($select);
    };

    $scope.refreshResults = function ($select) {
        if ($select.search !== $scope.filter) {
            $scope.filter = $select.search;
            $scope.PartnerPage = 1;
            var selected = $scope.PartnerItems.selected;
            $scope.PartnerItems = [];
            $scope.PartnerItems.selected = selected;
        }

        var pageData = {
            "page": $scope.PartnerPage,
            "filter": $scope.filter
        };

        registerService.getFilteredPartnerItems(pageData).then(function (filterProductResponse) {
            $scope.isLoading = false;
            if (filterProductResponse.data.Items !== null && filterProductResponse.data.Items.length > 0) {
                var selected = $scope.PartnerItems.selected;
                $scope.PartnerItems = $scope.PartnerItems.concat(filterProductResponse.data.Items);
                $scope.PartnerItems.selected = selected;
            }

        },
            function (errResponse) {
                toastr.error('Помилка завантаження Партнерів!', 'Помилка');
                $scope.loading = false;
                console.log(errResponse);
            });


    }
   
}

registerController.$inject = ['$scope', '$uibModal', '$q', '$timeout', '$location', '$window', 'toastr', '$interval', '$http', 'registerService' ];
registerApp.controller("registerController", registerController);