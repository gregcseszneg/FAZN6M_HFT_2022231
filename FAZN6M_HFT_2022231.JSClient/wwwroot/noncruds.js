let musiciansFromRecordLabel = [];
let musiciansHaveLongerSong = [];
let avgAgeInRecordLabels = [];
let tracks = [];
let sumOfMusicLength = [];

function integerInputOnly(event) {
    let char = String.fromCharCode(event.which);
    if (!(/[0-9]/.test(char))) {
        event.preventDefault();
        return false;
    }
}

function validateInput(input) {
    input.value = input.value.replace(/[^0-9]/g, ''); //if the input value is not a digit it will replace it to ""
    //the g flag makes it replace all occurrences in the string
}

function displayMusicians(functionName) {
    let source;
    let result;
    if (functionName == "hasLongerSongsThan") {
        source = musiciansHaveLongerSong;
        result = 'haslongersongresult';
    }
    else if (functionName == "fromRecordLabel") {
        source = musiciansFromRecordLabel;
        result = 'fromrecordlabelresult';
    }
    document.getElementById(result).innerHTML = '';
    source.forEach(m => {
        
        document.getElementById(result).innerHTML +=
        "<tr><td>" + m.musicianId + "</td><td>" //set the musician ID tp the row's data-id attribute
            + m.name + "</td></tr>"
    });
}

function displayTracks() {
    document.getElementById('bornafterresult').innerHTML = '';

    tracks.forEach(t => {
        document.getElementById('bornafterresult').innerHTML +=
            "<tr><td>" + t.trackId + "</td><td>"
        + t.name + "</td><td>"
        + t.musicianId + "</td></tr>"
    })
}
function displayAvgAge() {
    document.getElementById('avgageresult').innerHTML = '';
    avgAgeInRecordLabels.forEach(m => {
        document.getElementById('avgageresult').innerHTML +=
            "<tr><td>" + m.recordLabel + "</td><td>" 
        + m.avgAge + "</td></tr>"
    });
}

function displaySumOfLength() {
    document.getElementById('sumoflengthresult').innerHTML = '';
    sumOfMusicLength.forEach(s => {
        document.getElementById('sumoflengthresult').innerHTML +=
            "<tr><td>" + s.name + "</td><td>"
        +s.length + "</td></tr>"
    });
}

function fromRecordLabel() {

    var recordLabelName = document.getElementById("recordlabelname").value;
    if (recordLabelName !== "") {
    fetch(`http://localhost:34694/query/musiciansfromrecordlabel/${recordLabelName}/`)
        .then(response => response.json())
        .then(data => {
            if (data.length == 0) {
                alert("No item found");
            }
            else{
                if (data !== musiciansFromRecordLabel) {
                    musiciansFromRecordLabel = data;
                    displayMusicians(fromRecordLabel.name);

                }
            }
        })
        .catch(error => {

            console.error(error);
        });
    } else {

        alert("Please enter a record label name to run the query!");
       
    }
}

function hasLongerSongsThan() {
    var length = document.getElementById("length").value;

    if (length !== "") {
        fetch(`http://localhost:34694/query/MusiciansWhoHasLongerSongThan/${length}/`)
            .then(response => response.json())
            .then(data => {
                if (data.length == 0) {
                    alert("No item found");
                }
                else {
                    if (data.length !== musiciansHaveLongerSong.length &&
                        data.every((value, index) => value !== musiciansHaveLongerSong[index])) {
                        musiciansHaveLongerSong = data;
                        displayMusicians(hasLongerSongsThan.name);

                    }
                }
            })
            .catch(error => {

                console.error(error);
            });
    } else {

        alert("Please enter a music length in seconds to run the query!");
    }
}

function avgAge() {

    fetch(`http://localhost:34694/query/musicianaverageageintherecordlabels/`)
        .then(response => { //check the response
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            if (data.length == 0) {
                alert("No item found");
            }
            avgAgeInRecordLabels = data;
            displayAvgAge();
        })
        .catch(error => {
            console.error(error);
        });
}
function tracksFromMusicians() {

    let year = document.getElementById('yearofbirth').value;
    if (year !== "") {
        fetch(`http://localhost:34694/query/TracksFromMusicianBornAfter/${year}/`)
            .then(response => { //check the response
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response.json();
            })
            .then(data => {
                if (data.length == 0) {
                    alert("No item found");
                }
                else {
                    if (data.length !== tracks.length &&
                        data.every((value, index) => value !== tracks[index])) {
                        tracks = data;
                        displayTracks();
                    }
                }
            })
            .catch(error => {
                console.error(error);
            });
    } else {

    alert("Please enter a year to run this query!");
    }
}

function sumOfMusic() {
    fetch(`http://localhost:34694/query/SumOfMusicLengthPerMusician/`)
        .then(response => { //check the response
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            if (data.length == 0) {
                alert("No item found");
            }
            else {
                if (data.length !== sumOfMusicLength.length &&
                    data.every((value, index) => value !== sumOfMusicLength[index])) {
                    sumOfMusicLength = data;
                    displaySumOfLength();
                }
            }
        })
        .catch(error => {
            console.error(error);
        });
}