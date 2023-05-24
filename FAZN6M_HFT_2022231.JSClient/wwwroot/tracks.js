let tracks = [];
let connection = null;
getdata();
setupSignalR();

async function getdata() {
    await fetch('http://localhost:34694/track')
        .then(x => x.json())
        .then(y => {
            tracks = y;
            console.log(tracks);

            //if we did an update/delete the selected class is already removed so we need to set the input fields to null as well
            document.getElementById('trackname').value = null;
            document.getElementById('length').value = null;
            document.getElementById('musicianid').value = null;
            document.getElementById('albumid').value = null;
            display();
        });
}


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:34694/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("TrackCreated", (user, message) => {
        getdata();
    });

    connection.on("TrackDeleted", (user, message) => {
        getdata();
    });

    connection.on("TrackUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    }
    catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
}

function display() {
    document.getElementById('resultarea').innerHTML = '';

    tracks.forEach(t => {

        document.getElementById('resultarea').innerHTML += //gave the data to the resultarea
            `<tr data-track-id="${t.trackId}"><td>` + t.trackId + "</td><td>" //set the track ID to the row's data-id attribute
            + t.name + "</td><td>"
            + t.length + "</td><td>"
            + t.musicianId + "</td><td>"
        + t.albumId + "</td></tr>"

        //console.log(t.name);
    });
    // Add click event listeners to the rows
    let rows = document.getElementById('resultarea').querySelectorAll('tr');
    rows.forEach(row => {
        row.addEventListener('click', function () {
            // Remove the "selected" class from any other row
            rows.forEach(row => {
                row.classList.remove('selected');
            });
            // Add the "selected" class to this row
            this.classList.add('selected');

            let id = parseInt(this.dataset.trackId);
            let selectedTrack = tracks.find(track => track.trackId === id);

            // Populate the input fields
            document.getElementById('trackname').value = selectedTrack.name;
            document.getElementById('length').value = selectedTrack.length;
            document.getElementById('musicianid').value = selectedTrack.musicianId;
            document.getElementById('albumid').value = selectedTrack.albumId;
        });
    })
}

function remove() {

    let selectedRow = document.getElementById('resultarea').querySelector('.selected');

    if (selectedRow) {
        // Get the musician ID from the row's data-id attribute
        let id = parseInt(selectedRow.dataset.trackId);
        console.log(id);

        fetch(`http://localhost:34694/track/${id}`, {
            method: 'DELETE',
            headers: { 'Content-Type': 'application/json', },
        })
            .then(response => { //check the response
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response;
            })
            .then(data => {
                console.log('Success:', data);
                getdata(); //refresh the document
            })
            .catch((error) => {
                console.error('Error:', error);

            });
    } else {
        console.error('No row selected'); //give an error message if no row is selected
    }

}

function create() { //get the given values from the inputs and parse Them
    let trackName;
    if (document.getElementById('trackname').value !="") {

        trackName = document.getElementById('trackname').value;
    }
    else {
        alert("Every track has a name, please fill out the name!")
        return;
    }
    let length;
    if (document.getElementById('length').value !== "") {
        length = parseInt(document.getElementById('length').value);
    }
    else {
        alert("Every track has a length, please eneter an intiger!")
        return;
    }
    let musicianId;
    if (document.getElementById('musicianid').value !== "") {
        musicianId = parseInt(document.getElementById('musicianid').value);
    }
    else {
        alert("Every track has a musician, please eneter an id!")
        return;
    }
    let albumId = 0;
    if (document.getElementById('albumid').value !== "") {
        albumId = parseInt(document.getElementById('albumid').value);
    }

    fetch('http://localhost:34694/track', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({
            name: trackName,
            length: length,
            musicianId: musicianId,
            albumId: albumId
        }),
    })
        .then(response => { //check the response
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response;
        })
        .then(data => {
            console.log('Success:', data);
            getdata(); //refresh the document
        })
        .catch((error) => {
            console.error('Error:', error);

        });
}

function update() {
    let selectedRow = document.getElementById('resultarea').querySelector('.selected');
    let id = parseInt(selectedRow.dataset.trackId);

    if (selectedRow) {
        let trackName;
        if (document.getElementById('trackname').value != "") {

            trackName = document.getElementById('trackname').value;
        }
        else {
            alert("Every track has a name, please fill out the name!")
            return;
        }
        let length;
        if (document.getElementById('length').value !== "") {
            length = parseInt(document.getElementById('length').value);
        }
        else {
            alert("Every track has a length, please eneter an intiger!")
            return;
        }
        let musicianId;
        if (document.getElementById('musicianid').value !== "") {
            musicianId = parseInt(document.getElementById('musicianid').value);
        }
        else {
            alert("Every track has a musician, please eneter an id!")
            return;
        }
        let albumId = 0;
        if (document.getElementById('albumid').value !== "") {
            albumId = parseInt(document.getElementById('albumid').value);
        }

        fetch('http://localhost:34694/track', {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json', },
            body: JSON.stringify({
                trackId: id,
                name: trackName,
                length: length,
                musicianId: musicianId,
                albumId: albumId
            }),
        })
            .then(response => { //check the response
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response;
            })
            .then(data => {
                console.log('Success:', data);
                getdata(); //refresh the document
            })
            .catch((error) => {
                console.error('Error:', error);

            });
    } else {
        console.error('No row selected');
    }
}
