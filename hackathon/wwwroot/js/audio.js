    var Permission = ""
    var MediaRecorder = ""
    var RecordingStatus = ""
    var Stream = ""
    var AudioChunks = ""
    var Audio = ""
    var MimeType = ""
    var RecordedAudio = ""
    function getMicrophonePermission() {
        if ("MediaRecorder" in window) {
            try {
                const streamData = navigator.mediaDevices.getUserMedia({
                    audio: true,
                    video: false,
                });
                Permission = true;
                Stream = streamData;
            } catch (err) {
                alert(err.message);
            }
        } else {
            alert("The MediaRecorder API is not supported in your browser.");
        }
    };

    function startRecording() {
        //setRecordingStatus("recording");
        console.log("startRecording");
        MediaRecorder.start();
        //mediaRecorder.current = media;
        //mediaRecorder.current.start();
        
        //let localAudioChunks = [];
        //mediaRecorder.current.ondataavailable = (event) => {
        //    if (typeof event.data === "undefined") return;
        //    if (event.data.size === 0) return;
        //    localAudioChunks.push(event.data);
        //};
        //AudioChunks = localAudioChunks;
        
    };

function endRecording() {
    RecordingStatus = "inactive";
    //stops the recording instance
    console.log(MediaRecorder.current);
    MediaRecorder.onstop = (e) => {
        
        const audioUrl = URL.createObjectURL(audioBlob);
        Audio = audioUrl;
        AudioChunks = [];
        
        const reader = new FileReader();
        reader.readAsDataURL(audio);
        reader.onloadend = function () {
            const base64data = reader.result;
            console.log(base64data);
        };
    };
    MediaRecorder.stop();
    
}

//w³¹czenie
function TurnOn() {
    console.log("TurnOn");
    getMicrophonePermission();
    
}