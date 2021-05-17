function adminQueuePatternController($scope, toastr, adminService) {

    adminService.getQueuePatternItems().then(function(queuePartnerResponse) {

        $scope.QueuePatterns = queuePartnerResponse.data.Items;

        $scope.fixedPartnerPage = 1;
        $scope.fixedPartnerItems = [];
        $scope.categories = [];
        $scope.priorities = [];
        $scope.filter = null;

        $scope.fixedPartnerAddMoreItems = function ($select) {
            $scope.fixedPartnerPage++;
            $scope.refreshResults($select);

        };

        $scope.refreshResults = function ($select) {
            if ($select.search !== $scope.filter) {
                $scope.filter = $select.search;
                $scope.fixedPartnerPage = 1;
                $scope.fixedPartnerItems = [];
            }

            var pageData = {
                "page": $scope.fixedPartnerPage,
                "filter": $scope.filter
            };

            adminService.getFilteredPartnerPageItems(pageData).then(function (filterPartnerResponse) {
                    $scope.isLoading = false;
                    if (filterPartnerResponse.data.Items !== null && filterPartnerResponse.data.Items.length > 0) {
                        $scope.fixedPartnerItems = $scope.fixedPartnerItems.concat(filterPartnerResponse.data.Items);
                    }
                },
                function (errResponse) {
                    toastr.error('Помилка завантаження партнерів!', 'Помилка');
                    $scope.loading = false;
                    console.log(errResponse);
                });

        }

        $scope.onSelected = function($item, queueItem) {
            queueItem.ReceiverId = $item.Id;
            queueItem.ReceiverName = $item.ShortName;
        }

        $scope.onSelectedPriority = function($item, queueItem) {
            queueItem.Priority = $item.Id;
            queueItem.PriorityDescription = $item.Description;
        }

        $scope.onSelectedCategory = function ($item, queueItem) {
            queueItem.Category = $item.Id;
            queueItem.CategoryDescription = $item.Description;
        }

        adminService.getQueueCategories().then(function(response) {
            $scope.categories = response.data.Items;
        });

        adminService.getQueuePriorities().then(function(response) {
            $scope.priorities = response.data.Items;
        });

        $scope.postResult = function () {
            var postData = {
                'Items': $scope.QueuePatterns,
                'Count': $scope.QueuePatterns.length
            };
            adminService.postQueuePatternItems(postData).then(function(response) {
                $scope.QueuePatterns = response.data.Items;
                toastr.success('Дані успішно змінено!', 'Успіх');
            }, function (errResponse) {
                toastr.error('Помилка зміни шаблону!', 'Помилка');
                $scope.loading = false;
                console.log(errResponse);
            });
        }

        $scope.deleteResult = function(id) {
            var postData = {
                'Id': id
            };
            adminService.deleteQueuePatternItem(postData).then(function(response) {
                $scope.QueuePatterns = response.data.Items;
                toastr.success('Запис успішно видалено!', 'Успіх');
            }, function (errResponse) {
                toastr.error('Помилка видалення запису!', 'Помилка');
                $scope.loading = false;
                console.log(errResponse);
            });
        }

        $scope.addRecord = function() {
            adminService.addQueuePatternItem().then(function (response) {
                $scope.QueuePatterns = response.data.Items;
                toastr.success('Запис успішно додано!', 'Успіх');
            }, function (errResponse) {
                toastr.error('Помилка додавання запису!', 'Помилка');
                $scope.loading = false;
                console.log(errResponse);
            });
        }
        
    });
   
}

adminQueuePatternController.$inject = ['$scope', 'toastr', 'adminService'];
adminApp.controller("adminQueuePatternController", adminQueuePatternController);