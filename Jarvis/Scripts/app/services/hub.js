/// <reference path="../../typings/signalr/signalr.d.ts" />
'use strict';
app.service('_hub', [
    '$rootScope', "_audio", function ($rootScope, _audio) {
        //Create Representation of the Server's Move Shape
        var conn = $.connection;
        var hub = conn.clientHub;

        var self = $rootScope.$new();

        var state = {
            Messages: []
        };

        //Make Client Side Methods Available for Server
        $.extend(hub.client, {
            Initialize: function (messages) {
                $rootScope.$apply(function () {
                    state.Messages = messages;
                });
            },
            NewMessage: function (message) {
                $rootScope.$apply(function () {
                    state.Messages.push(message);
                });
            },
            PlayAudio: function (text) {
                _audio.playAudio(text);
            }
        });

        //Start Connection
        self.start = conn.hub.start();

        ///Bind Server Methods
        self.SendMessage = function (message) {
            hub.server.OnCommandCompleted(message);
        };

        self.GetMessages = function () {
            return state.Messages;
        };

        return self;
    }]);
