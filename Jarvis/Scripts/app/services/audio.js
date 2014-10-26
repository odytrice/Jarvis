app.factory("_audio", function () {
    var buffer = null;

    // Fix up prefixing
    var win = window;
    var AudioContext = win.AudioContext || win.webkitAudioContext;
    var context = new AudioContext();

    var play = function (text) {
        function loadSound(url) {
            var request = new XMLHttpRequest();
            request.open('GET', url, true);
            request.responseType = 'arraybuffer';

            // Decode asynchronously
            request.onload = function () {
                context.decodeAudioData(request.response, function (buf) {
                    var source = context.createBufferSource();
                    source.buffer = buf; // tell the source which sound to play
                    source.connect(context.destination); // connect the source to the context's destination (the speakers)
                    source.start(0); // play the source now
                    // note: on older systems, may have to use deprecated noteOn(time);
                }, function (e) {
                    return console.log(e);
                });
            };
            request.send();
        }

        function playSound(buffer) {
        }

        loadSound("/audio/query?text=" + encodeURI(text));
    };
    var _audio = {
        playAudio: function (text) {
            play(text);
        }
    };

    return _audio;
});
