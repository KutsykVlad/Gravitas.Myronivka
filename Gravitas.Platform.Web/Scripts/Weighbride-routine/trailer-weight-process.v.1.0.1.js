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