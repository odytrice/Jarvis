/// <reference path="../services/hub.ts" />
app.controller("ChatController", function ($scope, _hub: IHub) {

    var model = {
        Message: {
            Sender: '',
            Body: ''
        },
        GetMessages: function () {
            return _hub.GetMessages();
        }
    };


    _hub.start.done(function () {
        $scope.SendMessage = function (content: Message) {
            _hub.SendMessage(content);
            model.Message.Body = "";
        }
    });

    _hub.start.fail(function (reason) {
        console.error(reason);
    });



    angular.extend($scope, {
        model: model
    });

})

