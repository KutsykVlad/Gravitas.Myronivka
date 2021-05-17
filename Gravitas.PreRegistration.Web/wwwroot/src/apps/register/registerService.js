registerApp.factory('registerService', ['$http', '$location', function ($http, $location) {
    var baseApiUrl = $location.protocol() + '://' + 'localhost:3142' + '/api/';

    var getFilteredPartnerItems = function (postData) {
        var options = {
            url: baseApiUrl + 'QueueApi/GetPartnerList',
            method: 'GET',
            header: {
                'Content-Type': 'application/json; charset=UTF-8',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Methods': 'GET',
                'Access-Control-Allow-Headers': 'Content-Type, Authorization'
            },
            params: {
                'page': postData.page,
                'filter': postData.filter
            }
        };

        return $http(options);
    };

    var getQueueItems = function (postData) {
        var options = {
            url: baseApiUrl + 'QueueApi/GetExternalQueueList',
            method: 'GET',
            header: {
                'Content-Type': 'application/json; charset=UTF-8',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Methods': 'GET',
                'Access-Control-Allow-Headers': 'Content-Type, Authorization'
            },
            params: {
                'productId': postData.productId
            }
        };

        return $http(options);
    };

    var getFilteredProductList = function () {
        var options = {
            url: baseApiUrl + 'QueueApi/GetFilteredProductList',
            method: 'GET',
            header: {
                'Content-Type': 'application/json; charset=UTF-8',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Methods': 'GET',
                'Access-Control-Allow-Headers': 'Content-Type, Authorization'
            }
        };

        return $http(options);
    };

    var getAvailableDateTime = function (postData) {
        var options = {
            url: baseApiUrl + 'QueueApi/GetAvailableDateTime',
            method: 'GET',
            header: {
                'Content-Type': 'application/json; charset=UTF-8',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Methods': 'GET',
                'Access-Control-Allow-Headers': 'Content-Type, Authorization'
            },
            params: {
                'routeId': postData.routeId
            }
        };

        return $http(options);
    };

    var orderQueue = function (postData) {
        var options = {
            url: baseApiUrl + 'QueueApi/OrderQueue',
            method: 'POST',
            data: postData
        };

        return $http(options);
    };

    var addTruck = function(postData) {
        var options = {
            url: $location.protocol() + '://' + $location.host() + ':' + $location.port() + '/api/Registry/AddTruck',
            method: 'POST',
            data: postData
        };

        return $http(options);
    }

    var getTruckList = function (postData) {
        var options = {
            url: $location.protocol() + '://' + $location.host() + ':' + $location.port() + '/api/Registry/GetTruckRecords',
            method: 'GET',
            params: {
                'partnerId': postData.partnerId
            }
        };

        return $http(options);
    }

    var deleteTruckRecord = function(postData) {
        var options = {
            url: baseApiUrl + 'QueueApi/DeleteQueueRecord',
            method: 'GET',
            params: {
                'phoneNo': postData.phoneNo
            }
        };

        return $http(options);
    }

    var postResult = function(postData) {
        var options = {
            url: $location.protocol() + '://' + $location.host() + ':' + $location.port() + '/api/UserTruckManage/PostEditedTrucks',
            method: 'POST',
            data: postData
        };

        return $http(options);
    }
    return {
        getFilteredPartnerItems: getFilteredPartnerItems,
        getQueueItems: getQueueItems,
        getFilteredProductList: getFilteredProductList,
        getAvailableDateTime: getAvailableDateTime,
        orderQueue: orderQueue,
        addTruck: addTruck,
        getTruckList: getTruckList,
        deleteTruckRecord: deleteTruckRecord,
        postResult: postResult
    };
}]);