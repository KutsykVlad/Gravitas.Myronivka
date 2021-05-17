nodeRoutineApp.factory('nodeRoutineService', ['$http', function ($http) {
    var url = window.location.href;
    var arr = url.split("/");
    var host = arr[0] + "//" + arr[2];

    var baseApiUrl = host + '/api/';

	var getRoutineData = function (data) {
		var options = {
			url: baseApiUrl + 'SingleWindowApi/GetRoutineData',
			method: 'GET',
			header: { 'Content-Type': 'application/json; charset=UTF-8' },
			params: { 'nodeId': data.nodeId }
		};

		return $http(options);
    };

    var getNodeName = function (data) {
        var options = {
            url: baseApiUrl + 'SingleWindowApi/GetNodeName',
            method: 'GET',
            header: { 'Content-Type': 'application/json; charset=UTF-8' },
            params: { 'nodeId': data.nodeId }
        };

        return $http(options);
    };

    var getBudgetName = function(data) {
        var options = {
            url: baseApiUrl + 'SingleWindowApi/GetBudgetName',
            method: 'GET',
            header: { 'Content-Type': 'application/json; charset=UTF-8' },
            params: { 'budgetId': data.budgetId }
        };

        return $http(options);
    };
    
    var getBudgetChildren = function (postData) {
        var options = {
            url: baseApiUrl + 'SingleWindowApi/GetBudgetData',
            method: 'GET',
            header: { 'Content-Type': 'application/json; charset=UTF-8' },
            params: { 'parentId': postData.parentId }
        };

        return $http(options);
    };

    var getPartnerChildren = function (postData) {
        var options = {
            url: baseApiUrl + 'SingleWindowApi/GetPartnerData',
            method: 'GET',
            header: { 'Content-Type': 'application/json; charset=UTF-8' },
            params: { 'parentId': postData.parentId }
        };

        return $http(options);
    };
    
    var getProductChildren = function (postData) {
        var options = {
            url: baseApiUrl + 'SingleWindowApi/GetProductData',
            method: 'GET',
            header: { 'Content-Type': 'application/json; charset=UTF-8' },
            params: { 'parentId': postData.parentId }
        };

        return $http(options);
    };

    var getProductName = function (postData) {
        var options = {
            url: baseApiUrl + 'SingleWindowApi/GetProductName',
            method: 'GET',
            header: { 'Content-Type': 'application/json; charset=UTF-8' },
            params: { 'id': postData.id }
        };

        return $http(options);
    };

    var getEmployeeChildren = function (postData) {
        var options = {
            url: baseApiUrl + 'SingleWindowApi/GetEmployeeData',
            method: 'GET',
            header: { 'Content-Type': 'application/json; charset=UTF-8' },
            params: { 'parentId': postData.parentId }
        };

        return $http(options);
    };


   
    // new Stock

    var getFilteredStockItems = function (postData) {
        var options = {
            url: baseApiUrl + 'SingleWindowApi/GetStockItems',
            method: 'GET',
            header: { 'Content-Type': 'application/json; charset=UTF-8' },
            params: {
                'id': postData.id
            }
        };

        return $http(options);
    };


    var getStockItem = function (postData) {
        var options = {
            url: baseApiUrl + 'SingleWindowApi/GetStockItem',
            method: 'GET',
            header: { 'Content-Type': 'application/json; charset=UTF-8' },
            params: {
                'id': postData.id
            }
        };

        return $http(options);
    };

    var getFilteredTrailerItems = function (postData) {
        var options = {
            url: baseApiUrl + 'SingleWindowApi/FilteredTrailerItems',
            method: 'GET',
            header: { 'Content-Type': 'application/json; charset=UTF-8' },
            params: {
                'page': postData.page,
                'filter': postData.filter
            }
        };

        return $http(options);
    };

    var getTrailerItem = function (postData) {
        var options = {
            url: baseApiUrl + 'SingleWindowApi/TrailerItem',
            method: 'GET',
            header: { 'Content-Type': 'application/json; charset=UTF-8' },
            params: {
                'id': postData.id
            }
        };

        return $http(options);
    };

    var getFilteredDriverItems = function (postData) {
        var options = {
            url: baseApiUrl + 'SingleWindowApi/FilteredDriverItems',
            method: 'GET',
            header: { 'Content-Type': 'application/json; charset=UTF-8' },
            params: {
                'page': postData.page,
                'filter': postData.filter
            }
        };

        return $http(options);
    };

    var getEmployeeItem = function (postData) {
        var options = {
            url: baseApiUrl + 'SingleWindowApi/EmployeeItem',
            method: 'GET',
            header: { 'Content-Type': 'application/json; charset=UTF-8' },
            params: { 'id': postData.id }
        };

        return $http(options);
    }

    var getFilteredAssetItems = function (postData) {
        var options = {
            url: baseApiUrl + 'SingleWindowApi/FilteredAssetItems',
            method: 'GET',
            header: { 'Content-Type': 'application/json; charset=UTF-8' },
            params: {
                'page': postData.page,
                'filter': postData.filter
            }
        };

        return $http(options);
    };

    var getAssetItem = function (postData) {
        var options = {
            url: baseApiUrl + 'SingleWindowApi/AssetItem',
            method: 'GET',
            header: { 'Content-Type': 'application/json; charset=UTF-8' },
            params: { 'id': postData.id }
        };

        return $http(options);
    }

    var getPartnerItem = function (postData) {
        var options = {
            url: baseApiUrl + 'SingleWindowApi/PartnerItem',
            method: 'GET',
            header: { 'Content-Type': 'application/json; charset=UTF-8' },
            params: { 'carrierCode': postData.carrierCode }
        };

        return $http(options);
    };

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

	return {
		getRoutineData: getRoutineData,
		getBudgetChildren: getBudgetChildren,
        getPartnerChildren: getPartnerChildren,
        getEmployeeChildren: getEmployeeChildren,
        getStockItem: getStockItem,
		getNodeName: getNodeName,
		getBudgetName: getBudgetName,
        getFilteredStockItems: getFilteredStockItems,
		getFilteredAssetItems: getFilteredAssetItems,
        getAssetItem: getAssetItem,
		getPartnerItem: getPartnerItem,
        getFilteredPartnerPageItems: getFilteredPartnerPageItems,
        getEmployeeItem: getEmployeeItem,
        getFilteredDriverItems: getFilteredDriverItems,
        getTrailerItem: getTrailerItem,
        getFilteredTrailerItems: getFilteredTrailerItems,
        getProductChildren: getProductChildren,
        getProductName: getProductName,
        getFilteredProductItems: getFilteredProductItems

	};
}]);