function preRegistrationController($scope, nodeRoutineService, toastr) {
    $scope.productsPage = 1;
    $scope.productsItems = [];

    $scope.productsAddMoreItems = function ($select) {
        $scope.productsPage++;
        $scope.refreshResults($select);
    }; 
    
    $scope.onProductSelect = function () {
        document.getElementById('ProductId').value = $scope.productsItems.selected.Id;
    };

    $scope.refreshResults = function ($select) {
        if ($select.search !== $scope.filter) {
            $scope.filter = $select.search;
            $scope.productsPage = 1;
            var selected = $scope.productsItems.selected;
            $scope.productsItems = [];
            $scope.productsItems.selected = selected;
        }

        var pageData = {
            "page": $scope.productsPage,
            "filter": $scope.filter
        };

        nodeRoutineService.getFilteredProductItems(pageData).then(function (r) {
                $scope.isLoading = false;
                if (r.data.Items !== null && r.data.Items.length > 0) {
                    var selected = $scope.productsItems.selected;
                    $scope.productsItems = $scope.productsItems.concat(r.data.Items);
                    $scope.productsItems.selected = selected;
                }
            },
            function (errResponse) {
                toastr.error('Помилка завантаження Продуктів!', 'Помилка');
                $scope.loading = false;
                console.log(errResponse);
            });
    }
}

preRegistrationController.$inject = ['$scope', 'nodeRoutineService', 'toastr'];
nodeRoutineApp.controller("preRegistrationController", preRegistrationController);