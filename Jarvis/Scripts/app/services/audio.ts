app.factory("_audio", function ($q: ng.IQService) {
    var buffer = null;
    // Fix up prefixing
    var win = <any>window;
    var AudioContext = win.AudioContext || win.webkitAudioContext;
    var context = new AudioContext();

    var play = function (text: string) {
        function loadSound(url) {
            var request = new XMLHttpRequest();
            request.open('GET', url, true);
            request.responseType = 'arraybuffer';

            // Decode asynchronously
            request.onload = function () {
                context.decodeAudioData(request.response, function (buf) {
                    var source = context.createBufferSource(); // creates a sound source
                    source.buffer = buf;                    // tell the source which sound to play
                    source.connect(context.destination);       // connect the source to the context's destination (the speakers)
                    source.start(0);                           // play the source now
                    // note: on older systems, may have to use deprecated noteOn(time);
                }, e => console.log(e));
            }
            request.send();
        }

        loadSound("/audio/query?text=" + encodeURI(text));
    }
    var _audio: IAudioService = {
        playAudio: function (text) {
            play(text);
        },
        listen: function (call) {
            var defer = $q.defer<string>();

            var wnd = <any>window;
            var recognition = new wnd.webkitSpeechRecognition();
            recognition.onresult = function (event) {
                var text = event.results[0][0].transcript;

                if (event.results[0][0].confidence > 0.2) {
                    call(text);
                    console.log(text);
                }
            }
            recognition.start();
            return defer.promise;
        }
    }

    return _audio;
});

interface IAudioService {
    playAudio: (text: string) => void;
    listen: (call: (st: string) => void) => void;
}