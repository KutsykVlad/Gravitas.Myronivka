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
