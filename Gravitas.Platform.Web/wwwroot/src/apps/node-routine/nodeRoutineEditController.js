
function nodeRoutineEditController($scope, $uibModal, $q, $timeout, nodeRoutineService, $window, toastr) {
    $scope.nodeId = document.getElementById('nodeId').value;

    var postData = {
        "nodeId": $scope.nodeId
    };

    nodeRoutineService.getRoutineData(postData).then(function (response) {

        $scope.routineData = response.data;

        //Stock
        $scope.stockPage = 1;
        $scope.stockItems = [];
        $scope.stockItems.selected = undefined;
        $scope.stockFilter = null;

        if ($scope.routineData.ReceiverDepotId !== undefined) {
            var stockPost = {
                "id": $scope.routineData.ReceiverDepotId
            };
            nodeRoutineService.getStockItem(stockPost).then(function (stockItemResponse) {
                if (stockItemResponse.data !== null) {
                    $scope.stockItems.unshift(stockItemResponse.data);
                    $scope.stockItems.selected = stockItemResponse.data;
                }
            });
        }
        $scope.stockAddMoreItems = function ($select) {
            // $scope.stockPage++;
            // $scope.refreshStockResults($select);
        };

        $scope.refreshStockResults = function($select) {
            if ($select.search !== $scope.stockFilter) {
                $scope.stockFilter = $select.search;
                $scope.stockPage = 1;
                var selected = $scope.stockItems.selected;
                $scope.stockItems = [];
                $scope.stockItems.selected = selected;
            }

            var stockData = {
                "id": $scope.routineData.ReceiverId
            };

            nodeRoutineService.getFilteredStockItems(stockData).then(function(stockFilteredResponse) {
                    $scope.isLoading = false;
                    if (stockFilteredResponse.data !== undefined && stockFilteredResponse.data.length > 0) {
                        var selected = $scope.stockItems.selected;
                        $scope.stockItems = $scope.stockItems.concat(stockFilteredResponse.data);
                        $scope.stockItems.selected = selected;
                    }

                },
                function(errResponse) {
                    toastr.error('Помилка завантаження Контрагента!', 'Помилка');
                    $scope.loading = false;
                    console.log(errResponse);
                });
        };

        // assetItems
        $scope.fixedAssetPage = 1;
        $scope.fixedAssetItems = [];
        $scope.fixedAssetItems.selected = undefined;
        $scope.assetFilter = null;

        if ($scope.routineData.TransportId !== null && $scope.routineData.TransportId !== "") {
            var transportPost = {
                "id": $scope.routineData.TransportId
            };
            nodeRoutineService.getAssetItem(transportPost).then(function (assetItemResponse) {
                if (assetItemResponse.data !== null) {
                    $scope.fixedAssetItems.unshift(assetItemResponse.data);
                    $scope.fixedAssetItems.selected = assetItemResponse.data;
                }
            });
        }

        $scope.fixedAssetAddMoreItems = function ($select) {
            $scope.fixedAssetPage++;
            $scope.refreshAssetResults($select);
        };

        $scope.refreshAssetResults = function ($select) {
            if ($select.search !== $scope.assetFilter) {
                $scope.assetFilter = $select.search;
                $scope.fixedAssetPage = 1;
                var selected = $scope.fixedAssetItems.selected;
                $scope.fixedAssetItems = [];
                $scope.fixedAssetItems.selected = selected;
            }

            var pageData = {
                "page": $scope.fixedAssetPage,
                "filter": $scope.assetFilter
            };

            nodeRoutineService.getFilteredAssetItems(pageData).then(function (filterAssetResponse) {
                    $scope.isLoading = false;
                    if (filterAssetResponse.data.Items !== null && filterAssetResponse.data.Items.length > 0) {
                        var selected = $scope.fixedAssetItems.selected;
                        $scope.fixedAssetItems = $scope.fixedAssetItems.concat(filterAssetResponse.data.Items);
                        $scope.fixedAssetItems.selected = selected;
                    }

                },
                function (errResponse) {
                    toastr.error('Помилка завантаження Трейлерів!', 'Помилка');
                    $scope.loading = false;
                    console.log(errResponse);
                });

        }

        //partners

        $scope.fixedPartnerPage = 1;
        $scope.fixedPartnerItems = [];
        $scope.fixedPartnerItems.selected = undefined;
        $scope.filter = null;


        if ($scope.routineData.CarrierCode !== null && $scope.routineData.CarrierCode.length > 0) {
            var partnerCode = {
                "carrierCode": $scope.routineData.CarrierCode
            };
            nodeRoutineService.getPartnerItem(partnerCode).then(function (partnerResponse) {
                if (partnerResponse.data !== null) {
                    $scope.fixedPartnerItems.unshift(partnerResponse.data);
                    $scope.fixedPartnerItems.selected = partnerResponse.data;
                }
            }, function (errResponse) {
                $scope.loading = false;
                console.log('No partner' + $scope.routineData.CarrierCode + ' provided');
            });
        }

        $scope.fixedPartnerAddMoreItems = function ($select) {
            $scope.fixedPartnerPage++;
            $scope.refreshResults($select);

        };

        $scope.refreshResults = function ($select) {
            if ($select.search !== $scope.filter) {
                $scope.filter = $select.search;
                $scope.fixedPartnerPage = 1;
                var selected = $scope.fixedPartnerItems.selected;
                $scope.fixedPartnerItems = [];
                $scope.fixedPartnerItems.selected = selected;
            }

            var pageData = {
                "page": $scope.fixedPartnerPage,
                "filter": $scope.filter
            };

            nodeRoutineService.getFilteredPartnerPageItems(pageData).then(function (filterPartnerResponse) {
                    $scope.isLoading = false;
                    if (filterPartnerResponse.data.Items !== null && filterPartnerResponse.data.Items.length > 0) {
                        var selected = $scope.fixedPartnerItems.selected;
                        $scope.fixedPartnerItems = $scope.fixedPartnerItems.concat(filterPartnerResponse.data.Items);
                        $scope.fixedPartnerItems.selected = selected;
                    } else {
                        var temporary = {
                            Code: $select.search,
                            ShortName: $select.search,
                            FullName: $select.search,
                            Address: "",
                            IsFolder: false,
                            Id: null
                        };
                        $scope.fixedPartnerItems[0] = temporary;
                    }

                },
                function (errResponse) {
                    toastr.error('Помилка завантаження партнерів!', 'Помилка');
                    $scope.loading = false;
                    console.log(errResponse);
                });

        }



        // Trailers
        $scope.fixedTrailerPage = 1;
        $scope.fixedTrailerItems = [];
        $scope.fixedTrailerItems.selected = undefined;
        $scope.trailerFilter = null;

        if ($scope.routineData.TrailerId !== null) {
            var trailerPost = {
                "id": $scope.routineData.TrailerId
            };
            nodeRoutineService.getTrailerItem(trailerPost).then(function (trailerResponse) {
                if (trailerResponse.data !== null) {
                    $scope.fixedTrailerItems.unshift(trailerResponse.data);
                    $scope.fixedTrailerItems.selected = trailerResponse.data;

                }
            });
        }

        $scope.fixedTrailersAddMoreItems = function ($select) {
            $scope.fixedTrailerPage++;
            $scope.refreshTrailerResults($select);
        };

        $scope.refreshTrailerResults = function ($select) {
            if ($select.search !== $scope.trailerFilter) {
                $scope.trailerFilter = $select.search;
                $scope.fixedTrailerPage = 1;
                var selected = $scope.fixedTrailerItems.selected;
                $scope.fixedTrailerItems = [];
                $scope.fixedTrailerItems.selected = selected;
            }

            var trailerPageData = {
                "page": $scope.fixedTrailerPage,
                "filter": $scope.trailerFilter
            };

            nodeRoutineService.getFilteredTrailerItems(trailerPageData).then(function (filterTrailerResponse) {
                    $scope.isLoading = false;
                    if (filterTrailerResponse.data.Items !== null && filterTrailerResponse.data.Items.length > 0) {
                        var selected = $scope.fixedTrailerItems.selected;
                        $scope.fixedTrailerItems = $scope.fixedTrailerItems.concat(filterTrailerResponse.data.Items);
                        $scope.fixedTrailerItems.selected = selected;
                    }

                },
                function (errResponse) {
                    toastr.error('Помилка завантаження Трейлерів!', 'Помилка');
                    $scope.loading = false;
                    console.log(errResponse);
                });

        }

        // Drivers 1

        $scope.driver1Page = 1;
        $scope.driver1Items = [];
        $scope.driver1Items.selected = undefined;
        $scope.driver1Filter = null;

        if ($scope.routineData.DriverOneId !== null) {
            var driver1Post = {
                "id": $scope.routineData.DriverOneId
            };
            nodeRoutineService.getEmployeeItem(driver1Post).then(function (driver1Response) {
                if (driver1Response.data !== null) {
                    $scope.driver1Items.unshift(driver1Response.data);
                    $scope.driver1Items.selected = driver1Response.data;
                }
            });
        }

        $scope.driver1AddMoreItems = function ($select) {
            $scope.driver1Page++;
            $scope.refreshDriver1Results($select);
        };

        $scope.refreshDriver1Results = function($select) {
            if ($select.search !== $scope.driver1Filter) {
                $scope.driver1Filter = $select.search;
                $scope.driver1Page = 1;
                var selected = $scope.driver1Items.selected;
                $scope.driver1Items = [];
                $scope.driver1Items.selected = selected;
            }

            var driver1PageData = {
                "page": $scope.driver1Page,
                "filter": $scope.driver1Filter
            };

            nodeRoutineService.getFilteredDriverItems(driver1PageData).then(function(filterDriver1Response) {
                    $scope.isLoading = false;
                    if (filterDriver1Response.data.Items !== null && filterDriver1Response.data.Items.length > 0) {
                        var selected = $scope.driver1Items.selected;
                        $scope.driver1Items = $scope.driver1Items.concat(filterDriver1Response.data.Items);
                        $scope.driver1Items.selected = selected;
                    }

                },
                function(errResponse) {
                    toastr.error('Помилка завантаження Водіїв!', 'Помилка');
                    $scope.loading = false;
                    console.log(errResponse);
                });

        }

        // Drivers 2

        $scope.driver2Page = 1;
        $scope.driver2Items = [];
        $scope.driver2Items.selected = undefined;
        $scope.driver2Filter = null;

        if ($scope.routineData.DriverTwoId !== null) {
            var driver2Post = {
                "id": $scope.routineData.DriverTwoId
            };
            nodeRoutineService.getEmployeeItem(driver2Post).then(function (driver2Response) {
                if (driver2Response.data !== null) {
                    $scope.driver2Items.unshift(driver2Response.data);
                    $scope.driver2Items.selected = driver2Response.data;
                }
            });
        }

        $scope.driver2AddMoreItems = function ($select) {
            $scope.driver2Page++;
            $scope.refreshDriver2Results($select);
        };

        $scope.refreshDriver2Results = function ($select) {
            if ($select.search !== $scope.driver2Filter) {
                $scope.driver2Filter = $select.search;
                $scope.driver2Page = 1;
                var selected = $scope.driver2Items.selected;
                $scope.driver2Items = [];
                $scope.driver2Items.selected = selected;
            }

            var driver2PageData = {
                "page": $scope.driver2Page,
                "filter": $scope.driver2Filter
            };

            nodeRoutineService.getFilteredDriverItems(driver2PageData).then(function (filterDriver2Response) {
                    $scope.isLoading = false;
                    if (filterDriver2Response.data.Items !== null && filterDriver2Response.data.Items.length > 0) {
                        var selected = $scope.driver2Items.selected;
                        $scope.driver2Items = $scope.driver2Items.concat(filterDriver2Response.data.Items);
                        $scope.driver2Items.selected = selected;
                    }

                },
                function (errResponse) {
                    toastr.error('Помилка завантаження Водіїв!', 'Помилка');
                    $scope.loading = false;
                    console.log(errResponse);
                });

        }

        // Budget 1
        $scope.budget1Page = 1;
        $scope.budget1Items = [];
        $scope.budget1TreeItems = [];
        $scope.budget2TreeItems = [];

        $scope.Budget1TreeOptions = {
            nodeChildren: "children",
            dirSelectable: false,
            isLeaf: function isLeafFn(node) {
                return !node.IsFolder;
            }
        }

        $scope.Budget2TreeOptions = {
            nodeChildren: "children",
            dirSelectable: false,
            isLeaf: function isLeafFn(node) {
                return !node.IsFolder;
            }
        }

        $scope.fetchBudget1TreeItems = function () {
            var postData = {
                "parentId": "00000000-0000-0000-0000-000000000000"
            };
            nodeRoutineService.getBudgetChildren(postData).then(function (response) {

                $scope.budget1TreeItems = response.data.Items;


            });
        }
        $scope.fetchBudget2TreeItems = function () {
            var postData = {
                "parentId": "00000000-0000-0000-0000-000000000000"
            };
            nodeRoutineService.getBudgetChildren(postData).then(function (response) {


                $scope.budget2TreeItems = response.data.Items;

            });
        }

        $scope.buyBudgetFiltering = "";


        $scope.selectedBuyBudgetId;
        $scope.selectedSellBudgetId;
        $scope.selectedBuyBudgetName;
        $scope.selectedSellBudgetName;

        if ($scope.routineData.BuyBudgetId != null && $scope.routineData.BuyBudgetId.length > 0) {
            $scope.selectedBuyBudgetId = $scope.routineData.BuyBudgetId;
            var buyBudgetIdPost = {
                "budgetId": $scope.routineData.BuyBudgetId
            };
            nodeRoutineService.getBudgetName(buyBudgetIdPost).then(function (nResponse) {
                $scope.selectedBuyBudgetName = nResponse.data;
            }, function (errResponse) {
                $scope.loading = false;
                console.log(errResponse);
            });
        }

        if ($scope.routineData.SellBudgetId != null && $scope.routineData.SellBudgetId.length > 0) {
            $scope.selectedSellBudgetId = $scope.routineData.SellBudgetId;
            var sellBudgetIdPost = {
                "budgetId": $scope.routineData.SellBudgetId
            };
            nodeRoutineService.getBudgetName(sellBudgetIdPost).then(function (nResponse) {
                $scope.selectedSellBudgetName = nResponse.data;
            }, function (errResponse) {
                toastr.error('Помилка завантаження статті продажу!', 'Помилка');
                $scope.loading = false;
                console.log(errResponse);
            });
        }



        $scope.selectBudgetChildNode = function (node) {
            if (node.Name !== 'Loading ...') {
                $scope.selectedBuyBudgetId = node.Id;
                $scope.selectedBuyBudgetName = node.Name;
            }
        }

        $scope.selectSellBudgetChildNode = function (node) {
            if (node.Name !== 'Loading ...') {
                $scope.selectedSellBudgetId = node.Id;
                $scope.selectedSellBudgetName = node.Name;
            }
        }

        $scope.fetchChild2Nodes = function fetchChild2Nodes(node, expanded) {
            var postData = {
                "parentId": node.Id
            };
            node.children = [{ Name: 'Loading ...' }];
            node.isEmpty = "";
            nodeRoutineService.getBudgetChildren(postData).then(function (response) {
                    node.children = response.data.Items;
                    if (node.children.length <= 1) {
                        node.isEmpty = "(пусто)";
                    }
                },
                function (errResponse) {
                    toastr.error('Помилка завантаження дітей!', 'Помилка');
                    $scope.loading = false;
                    console.log(errResponse);
                });
        };

        $scope.fetchChildNodes = function fetchChildNodes(node, expanded) {
            var postData = {
                "parentId": node.Id
            };
            node.children = [{ Name: 'Loading ...' }];
            node.isEmpty = "";
            nodeRoutineService.getBudgetChildren(postData).then(function (response) {
                    node.children = response.data.Items;
                    if (node.children.length < 1) {
                        node.isEmpty = "(пусто)";
                    }
                },
                function (errResponse) {
                    toastr.error('Помилка завантаження дітей!', 'Помилка');
                    $scope.loading = false;
                    console.log(errResponse);
                });

        };

        if ($scope.routineData.ScaleInNumber !== null && $scope.routineData.ScaleInNumber !== 0) {
            var scaleInPost = {
                "nodeId": $scope.routineData.ScaleInNumber
            };
            nodeRoutineService.getNodeName(scaleInPost).then(function (nResponse) {
                $scope.scaleInName = nResponse.data;
            });
        }

        if ($scope.routineData.ScaleOutNumber !== null && $scope.routineData.ScaleOutNumber !== 0) {
            var scaleOutPost = {
                "nodeId": $scope.routineData.ScaleOutNumber
            }
            nodeRoutineService.getNodeName(scaleOutPost).then(function (nResponse) {
                $scope.scaleOutName = nResponse.data;
            });
        }

        $scope.fetchBudget1TreeItems();
        $scope.fetchBudget2TreeItems();

        $scope.disabled = undefined;

        //netto etc
        $scope.incomeDocGrossValue = 0;
        $scope.incomeDocTareValue = 0;
        $scope.docNetValue = 0;
        $scope.netValue = 0;
        $scope.weightDeltaValue = 0;



        if ($scope.routineData.IncomeDocGrossValue !== null) {
            $scope.incomeDocGrossValue = $scope.routineData.IncomeDocGrossValue;
        }

        if ($scope.routineData.IncomeDocTareValue !== null) {
            $scope.incomeDocTareValue = $scope.routineData.IncomeDocTareValue;
        }

        if ($scope.routineData.NetValue != null) {
            $scope.netValue = $scope.routineData.NetValue;
        }

        $scope.docNetValue = $scope.incomeDocGrossValue - $scope.incomeDocTareValue;
        $scope.weightDeltaValue = $scope.netValue - $scope.docNetValue;
        $scope.weightsChanged = function () {
            $scope.docNetValue = $scope.incomeDocGrossValue - $scope.incomeDocTareValue;
            $scope.weightDeltaValue = $scope.netValue - $scope.docNetValue;
        };

        // datetimes
        $scope.incomeDocDateTimePicker = {
            opened: false
        };
        $scope.docNetDateTimePicker = {
            opened: false
        };
        $scope.tripTicketDateTimePicker = {
            opened: false
        };
        $scope.createDateTimePicker = {
            opened: false
        };
        $scope.warrantDateTimePicker = {
            opened: false
        };

        $scope.incomeDocDateTimePickerOpen = function () {
            $scope.incomeDocDateTimePicker.opened = true;
        };

        $scope.createDateTimePickerOpen = function () {
            $scope.createDateTimePicker.opened = true;
        };

        $scope.docNetDateTimePickerOpen = function () {
            $scope.docNetDateTimePicker.opened = true;
        };

        $scope.tripTicketDateTimePickerOpen = function () {
            $scope.tripTicketDateTimePicker.opened = true;
        };

        $scope.warrantDateTimePickerOpen = function () {
            $scope.warrantDateTimePicker.opened = true;
        };

        $scope.todayIncome = function () {
            $scope.incomeDt = new Date();
        };

        $scope.todayIncome();


        $scope.today = function () {
            $scope.createDatedt = new Date();
        };
        $scope.today();

        $scope.clear = function () {
            $scope.createDatedt = null;
        };


        $scope.setDate = function (year, month, day) {
            $scope.createDatedt = new Date(year, month, day);
        };

        // $scope.docNetDt = new Date($scope.routineData.DocNetDateTime);
        // $scope.incomeDt = new Date($scope.routineData.IncomeDocDateTime);
        // $scope.tripTicketDt = new Date($scope.routineData.TripTicketDateTime);
        // $scope.warrantDt = new Date($scope.routineData.WarrantDatetime);
        if ($scope.routineData.DocNetDateTime !== null) {
            $scope.docNetDt = new Date($scope.routineData.DocNetDateTime);
        }

        if ($scope.routineData.IncomeDocDateTime !== null) {
            $scope.incomeDt = new Date($scope.routineData.IncomeDocDateTime);
        }

        if ($scope.routineData.CreateDate !== null) {
            $scope.createDt = new Date($scope.routineData.CreateDate);
        }

        if ($scope.routineData.TripTicketDateTime !== null) {
            $scope.tripTicketDt = new Date($scope.routineData.TripTicketDateTime);
        }
        if ($scope.routineData.WarrantDatetime !== null) {
            $scope.warrantDt = new Date($scope.routineData.WarrantDatetime);
        }



        $scope.clearIncome = function () {
            $scope.incomeDt = null;
        };

        $scope.format = 'dd.MM.yyyy';

        // Disable weekend selection
        function disabled(data) {
            //var date = data.date,
            //  mode = data.mode;
            //return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
            return false;
        }

        $scope.dateOptions = {
            dateDisabled: disabled,
            formatYear: 'yy',
            maxDate: new Date(2099, 5, 22),
            //  minDate: new Date(),
            startingDay: 1
        };
    });










};


nodeRoutineEditController.$inject = ['$scope', '$uibModal', '$q', '$timeout', 'nodeRoutineService', '$window', 'toastr'];
nodeRoutineApp.controller("nodeRoutineEditController", nodeRoutineEditController);
