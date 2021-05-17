function nonStandartDataController($scope) {

    $scope.init = function(beginDate, endDate) {
        $scope.beginDate = new Date(beginDate);
        $scope.endDate = new Date(endDate);
    }

    $scope.BeginDateTimePicker = {
        opened: false
    };
    $scope.EndDateTimePicker = {
        opened: false
    };


    $scope.BeginDateTimePickerOpen = function () {
        $scope.BeginDateTimePicker.opened = true;
    };

    $scope.EndDateTimePickerOpen = function () {
        $scope.EndDateTimePicker.opened = true;
    };


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
        maxDate: new Date(2020, 5, 22)
        //  minDate: new Date(),
       
    };
}

nonStandartDataController.$inject = ['$scope'];
nonStandartDataApp.controller("nonStandartDataController", nonStandartDataController);