function userManageController(
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
    //$scope.begin = function () {
        $scope.Trucks = [];
       
    $scope.begin = function (params) {
        $scope.model = params;
        $scope.Trucks = $scope.model.Items;
    }
        $scope.deleteRecord = function (record) {
            var postData = {
                'phoneNo': record.PhoneNo
            };
            registerService.deleteTruckRecord(postData).then(function (response) {
                    var index = $scope.Trucks.indexOf(record);
                    $scope.Trucks.splice(index, 1);
                    $scope.postData();
                },
                function(errResponse) {
                    toastr.error('Помилка видалення запису!', 'Помилка');
                            $scope.loading = false;
                            console.log(errResponse);
                });
        }

        $scope.postData = function() {
            $scope.model.Items = $scope.Trucks;
            var postData = {
                'Items': $scope.Trucks,
                'PartnerId': $scope.model.PartnerId
            };
            registerService.postResult(postData).then(function (response) {
                toastr.success('Запис успішно видалено!', 'Успіх!');
                },
                function (errResponse) {
                    toastr.error('Помилка видалення запису!', 'Помилка');
                    $scope.loading = false;
                    console.log(errResponse);
                });
        }
    //}
}

userManageController.$inject = ['$scope', '$uibModal', '$q', '$timeout', '$location', '$window', 'toastr', '$interval', '$http', 'registerService' ];
registerApp.controller("userManageController", userManageController);