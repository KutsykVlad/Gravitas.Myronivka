queueApp.factory('queueService', ['$http', function ($http) {
    var url = window.location.href;
    var arr = url.split("/");
    var host = arr[0] + "//" + arr[2];

    var baseApiUrl = host + '/api/';

    var getFilteredProductItems = function (postData) {
        var options = {
            url: baseApiUrl + 'SingleWindowApi/FilteredProductItems',
            method: 'GET',
            header: { 'Content-Type': 'application/json; charset=UTF-8' },
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
            header: { 'Content-Type': 'application/json; charset=UTF-8' },
            params: {
                'productId': postData.productId
            }
        };

        return $http(options);
    };

    return {
        getFilteredProductItems: getFilteredProductItems,
        getQueueItems: getQueueItems
    };
}]);