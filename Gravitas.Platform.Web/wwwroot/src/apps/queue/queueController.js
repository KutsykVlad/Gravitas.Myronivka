function queueController($scope, queueService) {

    $scope.Queue = [];

    $scope.ProductsPage = 1;
    $scope.ProductsItems = [];
    $scope.ProductsItems.selected = undefined;
    $scope.filter = null;

    $scope.ProductsAddMoreItems = function ($select) {
        $scope.ProductsPage++;
        $scope.refreshResults($select);

    };

    $scope.refreshResults = function ($select) {
        if ($select.search !== $scope.filter) {
            $scope.filter = $select.search;
            $scope.ProductsPage = 1;
            var selected = $scope.ProductsItems.selected;
            $scope.ProductsItems = [];
            $scope.ProductsItems.selected = selected;
        }

        var pageData = {
            "page": $scope.ProductsPage,
            "filter": $scope.filter
        };

        queueService.getFilteredProductItems(pageData).then(function (filterProductResponse) {
            $scope.isLoading = false;
            if (filterProductResponse.data.Items !== null && filterProductResponse.data.Items.length > 0) {
                var selected = $scope.ProductsItems.selected;
                $scope.ProductsItems = $scope.ProductsItems.concat(filterProductResponse.data.Items);
                $scope.ProductsItems.selected = selected;
            } 
            

        },
            function (errResponse) {
                toastr.error('Помилка завантаження Продуктів!', 'Помилка');
                $scope.loading = false;
                console.log(errResponse);
            });

       
    }
    var postData = {
        productId: ""
    }
    queueService.getQueueItems(postData).then(function(response) {
        $scope.isLoading = false;
        if (response.data.Items !== null) {
            $scope.Queue = response.data.Items;
        }
    });

    $scope.refreshQueue = function () {
        var postData = {
            productId: $scope.ProductsItems.selected.Id
        }
        queueService.getQueueItems(postData).then(function (response) {
            $scope.isLoading = false;
            if (response.data.Items !== null) {
                $scope.Queue = response.data.Items;
            }
        });
    }
}

queueController.$inject = ['$scope', 'queueService'];
queueApp.controller("queueController", queueController);