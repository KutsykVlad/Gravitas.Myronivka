routesEditorApp.factory('routesService', ['$http', '$location', function ($http, $location) {
	var baseApiUrl = $location.protocol() + '://' + $location.host() + ':' + $location.port() + '/api/';

	var getRoute = function (id) {
		var postData = {
			'id': id
		};

		var options = {
			url: baseApiUrl + 'Routes/GetRoute',
			method: 'GET',
			params: postData
		};

		return $http(options);
	};

	var getGroup = function (data) {
	
		var options = {
			url: baseApiUrl + 'Routes/GetRouteGroup',
			method: 'GET',
			params: data
		};

		return $http(options);
	};

	var getNode = function (data) {
		var options = {
			url: baseApiUrl + 'Routes/GetNode',
			method: 'GET',
			params: data
		};

		return $http(options);
	};

	var getAvailableNodeDots = function(data) {
		var options = {
			url: baseApiUrl + 'Routes/GetAvailableNodeDots',
			method: 'POST',
			data: data
		};

		return $http(options);
	};

	var addDotToNode = function (data) {
		var options = {
			url: baseApiUrl + 'Routes/AddDotToNode',
			method: 'POST',
			data: data
		};

		return $http(options);
	};

	var removeDotFromNode = function (data) {
		var options = {
			url: baseApiUrl + 'Routes/RemoveDotFromNode',
			method: 'POST',
			data: data
		};

		return $http(options);
	};

	var getNodes = function () {
		var options = {
			url: baseApiUrl + 'Routes/GetNodes',
			method: 'GET'
		};

		return $http(options);
	};

	var getRoutines = function () {
		var options = {
			url: baseApiUrl + 'Routes/GetRoutines',
			method: 'GET'
		};

		return $http(options);
	};

	var addNode = function (postData, success, error) {
		$http.defaults.headers.post["Content-Type"] = "application/json; charset=UTF-8";
		console.log(postData);
		var options = {
			url: baseApiUrl + 'Routes/AddNode',
			method: 'POST',
			data: postData
		};

		return $http(options).then(success, error);
	};

	var deleteNode = function (postData, success, error) {
		$http.defaults.headers.post["Content-Type"] = "application/json; charset=UTF-8";
		var options = {
			url: baseApiUrl + 'Routes/DeleteNode',
			method: 'POST',
			data: postData
		};

		return $http(options).then(success, error);
	};

	var updateNode = function (postData, success, error) {
		$http.defaults.headers.post["Content-Type"] = "application/json; charset=UTF-8";
		var options = {
			url: baseApiUrl + 'Routes/UpdateNode',
			method: 'POST',
			data: postData
		};

		return $http(options).then(success, error);
	};

	var updateNodesOrder = function (postData, success, error) {
		$http.defaults.headers.post["Content-Type"] = "application/json; charset=UTF-8";
		var options = {
			url: baseApiUrl + 'Routes/UpdateNodesOrder',
			method: 'POST',
			data: postData
		};

		return $http(options).then(success, error);
	};

	return {
		getRoute: getRoute,
		getNodes: getNodes,
		getRoutines: getRoutines,
		getGroup: getGroup,
		addNode: addNode,
		getNode: getNode,
		deleteNode: deleteNode,
		updateNode: updateNode,
		updateNodesOrder: updateNodesOrder,
		getAvailableNodeDots: getAvailableNodeDots,
		addDotToNode: addDotToNode,
		removeDotFromNode: removeDotFromNode
	};
}]);