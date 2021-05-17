
function checkForUpdate(redirectTo, nodeId, stateIndex) {
        var url = window.location.href;
        var arr = url.split("/");
        var host = arr[0] + "//" + arr[2];
    $.ajax({
        url: host + '/api/' + redirectTo,
        type: 'get',
        cache: false,
        data: {
            "nodeId": nodeId,
            "stateIndex": stateIndex
        },
        success: function (result) {
            if (result === true) {
                console.log("Page has actual data");
            } else {
                location.reload();
            }
        }
    });
}

function initializeCheck(url, nodeId) {

    function refreshPartial() {
        var stateIndex = Number(document.getElementById("currentStatusIndex").innerHTML);

        setTimeout(function() {
                checkForUpdate(url, nodeId, stateIndex);
                refreshPartial();
            },
            5000);
    };

    refreshPartial();
};
