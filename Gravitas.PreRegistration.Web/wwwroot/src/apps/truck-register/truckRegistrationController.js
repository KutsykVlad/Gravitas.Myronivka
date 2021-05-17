function truckRegistrationController(
    $scope,
    $uibModal,
    $q,
    $timeout,
    $location,
    $window,
    toastr,
    $interval,
    $http,
    $filter,
    registerService) {
    registerService.getFilteredProductList().then(function (productsResponse) {
        $scope.ProductItems = productsResponse.data;

        $scope.ProductItems.selected = undefined;

        $scope.formValidation = false;
        $scope.postItem = {};

        $scope.selectProduct = function () {
            console.log($scope.ProductItems);
            if ($scope.ProductItems.selected === undefined) {
                toastr.error('Вам необхідно обрати номенклатуру!', 'Помилка');
                return;
            }
            var postData = {
                'routeId': $scope.ProductItems.selected.Value
            }
            registerService.getAvailableDateTime(postData).then(function (timeResponse) {
                $scope.availableTime = $filter('date')(timeResponse.data, 'yyyy-MM-dd HH:mm');
                $scope.nonFilteredTime = timeResponse.data;
                $scope.direction = 1;
                $scope.postItem.RouteId = $scope.ProductItems.selected.Value;
                $scope.stage = 'stage2';
            }, function (errResponse) {
                toastr.error('Помилка завантаження доступного часу!', 'Помилка');
                $scope.loading = false;
                console.log(errResponse);
            });
        }

        $scope.stage = "";
        $scope.formValidation = false;
        $scope.toggleJSONView = false;
        $scope.toggleFormErrorsView = false;

        // Navigation functions
        $scope.next = function (stage) {
            //$scope.direction = 1;
            //$scope.stage = stage;

            $scope.formValidation = true;

            if ($scope.truckForm.$valid) {
                $scope.direction = 1;
                $scope.stage = stage;
                $scope.formValidation = false;
            }
        };

        $scope.back = function (stage) {
            $scope.direction = 0;
            $scope.stage = stage;
        };
       
        $scope.submitForm = function () {
            var item = {
                TruckNo: $scope.postItem.TruckNo,
                TrailerNo: $scope.postItem.TrailerNo,
                PhoneNo: $scope.postItem.PhoneNo.toString(),
                RouteId: parseInt($scope.postItem.RouteId, 10)
            }
            registerService.orderQueue(item).then(function (postResponse) {
                console.log(postResponse);
                item.PartnerId = $scope.partnerId;
                var truck = {
                    TruckNo: $scope.postItem.TruckNo,
                    TrailerNo: $scope.postItem.TrailerNo,
                    PhoneNo: $scope.postItem.PhoneNo.toString(),
                    RouteId: parseInt($scope.postItem.RouteId, 10),
                    PartnerId: $scope.partnerId,
                    EntranceTime: $scope.nonFilteredTime,
                    PartnerName: $scope.partnerName
                }
                registerService.addTruck(truck).then(function(response) {
                            $scope.stage = "success";
                        },
                        function(errResponse) {
                            toastr.error('Помилка надсилання даних!', 'Помилка');
                            $scope.stage = "error";
                            console.log(errResponse);
                        });
                },
                function(errResponse) {
                    toastr.error('Помилка надсилання даних!', 'Помилка');
                    $scope.stage = "error";
                    console.log(errResponse);
                });
        
        };

        $scope.reset = function () {
            // Clean up scope before destorying
            $scope.formParams = {};
            $scope.stage = "";
        }
    });

    
}

truckRegistrationController.$inject = ['$scope', '$uibModal', '$q', '$timeout', '$location', '$window', 'toastr', '$interval', '$http','$filter', 'registerService'];
registerApp.controller("truckRegistrationController", truckRegistrationController);