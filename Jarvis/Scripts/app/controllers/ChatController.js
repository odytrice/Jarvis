app.controller("ChatController", function ($scope) {
    var models = {
        Message: {
            sender: 'You',
            body: 'Hi Jarvis'
        }
    };

    var sendMessage = function () {
        console.log('good');
    };

    angular.extend($scope, {
        Message: models.Message,
        SendMessage: sendMessage
    });
});
//# sourceMappingURL=ChatController.js.map
