/// <reference path="../services/hub.ts" />
app.controller("ChatController", function ($scope, _hub: IHub, _audio: IAudioService) {

    var model = {
        Message: {
            Sender: 'You',
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
        $scope.startSpeech = function () {
            _audio.listen(function (result) {
                $scope.$apply(function () {
                    model.Message.Body = result;
                    _hub.SendMessage(model.Message);
                });
            });
        }
    });

    _hub.start.fail(function (reason) {
        console.error(reason);
    });

    



    angular.extend($scope, {
        model: model
    });

})

