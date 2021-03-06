function mixedFeedController($scope, nodeRoutineService, toastr) {
    $scope.mixedFeedItems = [];
    $scope.fetchMixedFeedItems = function () {
        var postData = {
            "parentId": "3c682f24-9770-11db-89fe-0030482f22a6"
        };
        nodeRoutineService.getProductChildren(postData).then(function (response) {

            $scope.mixedFeedItems = response.data.Items;
        });
    };
    $scope.fetchMixedFeedItems();

    $scope.mixedFeedItemsOptions = {
        nodeChildren: "children",
        dirSelectable: false,
        isLeaf: function isLeafFn(node) {
            return !node.IsFolder;
        }
    };

    $scope.fetchChildNodes = function fetchChildNodes(node, expanded) {
        var postData = {
            "parentId": node.Id
        };
        node.children = [{ ShortName: 'Завантаження ...' }];
        node.isEmpty = "";
        nodeRoutineService.getProductChildren(postData).then(function (response) {
                node.children = response.data.Items;
                if (node.children.length < 1) {
                    node.isEmpty = "(пусто)";
                }
            },
            function (errResponse) {
                toastr.error('Помилка завантаження!', 'Помилка');
                $scope.loading = false;
                console.log(errResponse);
            });

    };

    $scope.mixedFeedFiltering = "";

    $scope.selectMixedFeedItem = function (node) {
        if (node.ShortName !== 'Завантаження ...') {
            document.getElementById('ProductId').value = node.Id;
            $scope.selectedMixedFeedItemName = node.ShortName;
        }
    };

    angular.element(document).ready(function () {
        if (document.getElementById('ProductId').value != null) {
            var postData = {
                "id": document.getElementById('ProductId').value
            };
            nodeRoutineService.getProductName(postData).then(function (nResponse) {
                $scope.selectedMixedFeedItemName = nResponse.data;
            }, function (errResponse) {
                $scope.loading = false;
                console.log(errResponse);
            });
        }
    });
    
}

mixedFeedController.$inject = ['$scope', 'nodeRoutineService', 'toastr'];
nodeRoutineApp.controller("mixedFeedController", mixedFeedController);