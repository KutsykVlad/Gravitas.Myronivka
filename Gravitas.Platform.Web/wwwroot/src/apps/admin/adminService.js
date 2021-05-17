adminApp.factory('adminService', ['$http', function ($http) {

    var url = window.location.href;
    var arr = url.split("/");
    var host = arr[0] + "//" + arr[2];

    var baseApiUrl = host + '/api/';

    var getPartnerItem = function (postData) {
        var options = {
            url: baseApiUrl + 'SingleWindowApi/PartnerItem',
            method: 'GET',
            header: { 'Content-Type': 'application/json; charset=UTF-8' },
            params: { 'carrierCode': postData.carrierCode }
        };

        return $http(options);
    }

    var getFilteredPartnerPageItems = function (postData) {
        var options = {
            url: baseApiUrl + 'SingleWindowApi/FilteredPartnerItemsPage',
            method: 'GET',
            header: { 'Content-Type': 'application/json; charset=UTF-8' },
            params: {
                'page': postData.page,
                'filter': postData.filter
            }
        };

        return $http(options);
    };

    var getQueueCategories = function () {
        var options = {
            url: baseApiUrl + 'AdminApi/GetQueueCategories',
            method: 'GET',
            header: { 'Content-Type': 'application/json; charset=UTF-8' }
        };

        return $http(options);
    };

    var getQueuePriorities = function () {
        var options = {
            url: baseApiUrl + 'AdminApi/GetQueuePriorities',
            method: 'GET',
            header: { 'Content-Type': 'application/json; charset=UTF-8' }
        };

        return $http(options);
    };

    var getQueuePatternItems = function () {
        var options = {
            url: baseApiUrl + 'AdminApi/GetQueuePatternItems',
            method: 'GET',
            header: { 'Content-Type': 'application/json; charset=UTF-8' }
        };

        return $http(options);
    };

    var postQueuePatternItems = function(result) {
        var options = {
            url: baseApiUrl + 'AdminApi/PostQueuePatternItems',
            method: 'POST',
            data: result 
        };

        return $http(options);
    }

    var deleteQueuePatternItem = function (postData) {
        var options = {
            url: baseApiUrl + 'AdminApi/DeleteQueuePatternItem',
            method: 'GET',
            header: { 'Content-Type': 'application/json; charset=UTF-8' },
            params: { 'id': postData.Id }
        };

        return $http(options);
    };

    var addQueuePatternItem = function () {
        var options = {
            url: baseApiUrl + 'AdminApi/AddQueuePatternItem',
            method: 'GET',
            header: { 'Content-Type': 'application/json; charset=UTF-8' }
        };

        return $http(options);
    };

    return {
        getPartnerItem: getPartnerItem,
        getFilteredPartnerPageItems: getFilteredPartnerPageItems,
        getQueueCategories: getQueueCategories,
        getQueuePriorities: getQueuePriorities,
        getQueuePatternItems: getQueuePatternItems,
        postQueuePatternItems: postQueuePatternItems,
        deleteQueuePatternItem: deleteQueuePatternItem,
        addQueuePatternItem: addQueuePatternItem
    };
}]);