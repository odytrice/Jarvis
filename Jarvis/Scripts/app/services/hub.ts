/// <reference path="../../typings/signalr/signalr.d.ts" />
'use strict';

app.service('_hub', ['$rootScope', "_audio", function ($rootScope: ng.IRootScopeService, _audio: IAudioService) {
    //Create Representation of the Server's Move Shape
    var conn: any = $.connection;
    var hub = conn.clientHub;

    var self: IHub = <IHub><any>$rootScope.$new();

    var state = {
        Messages: []
    }

    //Make Client Side Methods Available for Server
    $.extend(hub.client, {
        Initialize: function (messages: Message[]) {
            $rootScope.$apply(function () {
                state.Messages = messages;
            });
        },
        NewMessage: function (message: Message) {
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
        hub.server.receive(message);
    }

    self.GetMessages = function () {
        return state.Messages;
    }

    return self;
}]);


interface IHub {
    /**
    * Asynchronously Starts the Connection to the Hub
    */
    start: JQueryPromise<any>;

    /**
    * Says Hello
    */
    SendMessage: (message: Message) => void;
    GetMessages: () => Message[];

    $emit(name: string, ...args: any[]): ng.IAngularEvent;
    $on(name: string, listener: (event: ng.IAngularEvent, args: any) => any): Function;
}

interface IState {
    Messages: Message[]
}

interface Message {
    Sender: string;
    Body: string;
}