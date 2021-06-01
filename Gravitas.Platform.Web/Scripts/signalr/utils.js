$(function () {
    var chat = $.connection.MainHub;
    chat.client.reload = function (groupId) {
        document.getElementById('reloadPage_' + groupId).click();
    };

    chat.client.stopSpinner = function () {
        loading(false);
    };

    chat.client.startSpinner = function () {
        loading(true);
    };
    
    chat.client.updateProcessingMessage = function (groupId) {
        var modal = $('#exampleModal');
        var m = modal.data('bs.modal');
        var isShown = m && modal.data('bs.modal')._isShown;
        if (!m || !isShown) {
            modal.modal();
        }
        
        // document.getElementById('updateProcessingMessage_' + groupId).click();
    };

    ConnectToHub();

    $.connection.hub.disconnected(function() {
        setTimeout(function() {
            ConnectToHub();
        }, 5000); // Restart connection after 5 seconds.
    });

    function ConnectToHub() {
        $.connection.hub.start().done(
            function() {
                console.log('Now connected, connection ID=' + $.connection.hub.id);
                chat.server.addClient($('#nodeId').val());
                chat.server.addClient($('#workstationId').val());
            }
        );
    }
});