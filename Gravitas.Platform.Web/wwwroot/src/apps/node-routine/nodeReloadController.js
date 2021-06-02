function nodeReloadController($scope, $http, $compile) {
    $scope.reload = function () {
        $('#spinner').show();

        var url = window.location.href;
        var arr = url.split("/");
        var host = arr[0] + "//" + arr[2];

        var nodeId = document.getElementById('nodeId').value;
        
        if (nodeId) {
            $http.get(host + '/Node/RoutineSingle/' + nodeId + '?date='+$.now())
                .then(function (response) {
                    $('#body').html($compile(response.data)($scope));
                })
                .catch(function onReject(errorResponse) {
                    console.error('StatusCode: ' + errorResponse.status + ' Message: ' + errorResponse.message);
                })
                .finally(function() {
                    $('#spinner').hide();
                });
        }
    };
    
    $scope.updateProcessingMessage = function () {
        var url = window.location.href;
        var arr = url.split("/");
        var host = arr[0] + "//" + arr[2];

        $http.get(host + '/Node/NodeProcessingMessageItems?nodeId=' + document.getElementById('nodeId').value, { cache: false })
            .then(function (response) {
                $('#nodeProcessingMessage').html($compile(response.data)($scope));
            }).catch(function onReject(errorResponse) {
            console.error('StatusCode: ' + errorResponse.status + ' Message: ' + errorResponse.message);
        }).finally(function() {
        });
    };
}

nodeReloadController.$inject = ['$scope', '$http', '$compile'];
nodeRoutineApp.controller("nodeReloadController", nodeReloadController);