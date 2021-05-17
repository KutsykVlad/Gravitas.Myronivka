var weighbridgeProcessTimer;

function startScaleTimer(redirectTo, nodeId, isTruckWeighting, operationDescription, scaleOpTypeName, isSecondWeighting, isTrailerAvailable) {

    var truck = (isTruckWeighting === "True");
    var trailer = (isTruckWeighting === "False");

    weighbridgeProcessTimer = setInterval(function () {

        $.ajax({
            url: redirectTo,
            type: 'get',
            cache: false,
            async: false,
            data: {
                nodeId: nodeId
            },
            success: function (result) {
                $('#scaleData').html(result);
                $('#operationDescription').html(operationDescription);
                if (truck) {
                    truckWeightProcess((scaleOpTypeName === "Тара"), (isSecondWeighting === "True" && isTrailerAvailable === "False"));
                }
                if (trailer) {
                    trailerWeightProcess((scaleOpTypeName === "Тара"), (isSecondWeighting === "True"));
                }
            }
        });
    }, 2000);
}

function stopScaleCountdownTimer() {
    clearInterval(weighbridgeProcessTimer);
}

function truckWeightProcess(isTare, showDelta) {

    function refreshCurrentValue() {

        var scaleValue = document.getElementById("scaleValueRuntime");

        if (scaleValue != null) {
            var currentWeight = scaleValue.innerHTML;
            if (isTare) {
                document.getElementById("TareValue").value = Number(currentWeight);
            } else {
                document.getElementById("GrossValue").value = Number(currentWeight);
            }
            if (showDelta) {
                document.getElementById("CurrentDocNetValue").value = Math.abs(
                    Number(document.getElementById("TareValue").value) -
                    Number(document.getElementById("GrossValue").value));
                document.getElementById("WeightingDelta").value =
                    Number(document.getElementById("CurrentDocNetValue").value) -
                    Number(document.getElementById("DocNetValue").value);
            }
        }
    }

    refreshCurrentValue();
}

function trailerWeightProcess(isTare, isSecondWeighting) {
    function refreshCurrentValue() {

        var scaleValue = document.getElementById("scaleValueRuntime");
        if (scaleValue != null) {
            var currentWeight = scaleValue.innerHTML;

            if (isTare) {
                document.getElementById("TrailerTareValue").value = Number(currentWeight);
            } else {
                document.getElementById("TrailerGrossValue").value = Number(currentWeight);
            }
            if (isSecondWeighting) {
                var sumTare =
                    Number(document.getElementById("TareValue").value) +
                        Number(document.getElementById("TrailerTareValue").value);
                var sumGross = Number(document.getElementById("GrossValue").value) +
                    Number(document.getElementById("TrailerGrossValue").value);

                document.getElementById("CurrentDocNetValue").value = Math.abs(
                    Number(sumGross) -
                    Number(sumTare));
                document.getElementById("WeightingDelta").value =
                    Number(document.getElementById("CurrentDocNetValue").value) -
                    Number(document.getElementById("DocNetValue").value);
            }
        }
    }

    refreshCurrentValue();

}