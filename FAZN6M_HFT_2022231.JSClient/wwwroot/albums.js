let albums = [];
let connection = null;
getdata();
setupSignalR();

async function getdata() {
    await fetch('http://localhost:34694/album')
        .then(x => x.json())
        .then(y => {
            albums = y;
            //console.log(albums);

            //if we did an update/delete the selected class is already removed so we need to set the input fields to null as well
            document.getElementById('albumname').value = null;
            document.getElementById('musicianid').value = null;
            document.getElementById('yearofrelease').value = null;
            document.getElementById('numberoftracks').value = null;
            display();
        });
}


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:34694/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("AlbumCreated", (user, message) => {
        getdata();
    });

    connection.on("AlbumDeleted", (user, message) => {
        getdata();
    });

    connection.on("AlbumUpdated", (user, message) => {
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

    albums.forEach(t => {

        document.getElementById('resultarea').innerHTML += //gave the data to the resultarea
            `<tr data-album-id="${t.albumId}"><td>` + t.albumId + "</td><td>" //set the track ID to the row's data-id attribute
        + t.name + "</td><td>"
        + t.musicianId + "</td><td>"
            + t.yearOfRelease + "</td><td>"
        + t.numberOfTracks + "</td></tr>"

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

            let id = parseInt(this.dataset.albumId);
            let selectedAlbum = albums.find(album => album.albumId === id);

            // Populate the input fields
            document.getElementById('albumname').value = selectedAlbum.name;
            document.getElementById('musicianid').value = selectedAlbum.musicianId;
            document.getElementById('yearofrelease').value = selectedAlbum.yearOfRelease
            document.getElementById('numberoftracks').value = selectedAlbum.numberOfTracks;
        });
    })
}

function remove() {

    let selectedRow = document.getElementById('resultarea').querySelector('.selected');

    if (selectedRow) {
        // Get the musician ID from the row's data-id attribute
        let id = parseInt(selectedRow.dataset.albumId);
        console.log(id);

        fetch(`http://localhost:34694/album/${id}`, {
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
    let albumName;
    if (document.getElementById('albumname').value != "") {
        albumName = document.getElementById('albumname').value;
    }
    else {
        alert("Every album has a name, please fill out the name field!")
        return;
    }
    let musicianId;
    if (document.getElementById('musicianid').value !== "") {
        musicianId = parseInt(document.getElementById('musicianid').value);
    }
    else {
        alert("Every album has been made by someone, please add an ID!");
        return;
    }
    let yearOfRelease = 0;
    if (document.getElementById('yearofrelease').value !== "") {
        yearOfRelease = parseInt(document.getElementById('yearofrelease').value);
    }
    let numberOfTracks = 0;
    if (document.getElementById('numberoftracks').value !== "") {
        numberOfTracks = parseInt(document.getElementById('numberoftracks').value);
    }

    fetch('http://localhost:34694/album', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({
            name: albumName,
            musicianId: musicianId,
            numberOfTracks: numberOfTracks,
            yearOfRelease: yearOfRelease
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
    let id = parseInt(selectedRow.dataset.albumId);

    if (selectedRow) {
        let albumName;
        if (document.getElementById('albumname').value != "") {
            albumName = document.getElementById('albumname').value;
        }
        else {
            alert("Every album has a name, please fill out the name field!")
            return;
        }
        let musicianId;
        if (document.getElementById('musicianid').value !== "") {
            musicianId = parseInt(document.getElementById('musicianid').value);
        }
        else {
            alert("Every album has been made by someone, please add an ID!");
            return;
        }
        let yearOfRelease = 0;
        if (document.getElementById('yearofrelease').value !== "") {
            yearOfRelease = parseInt(document.getElementById('yearofrelease').value);
        }
        let numberOfTracks = 0;
        if (document.getElementById('numberoftracks').value !== "") {
            numberOfTracks = parseInt(document.getElementById('numberoftracks').value);
        }

        fetch('http://localhost:34694/album', {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json', },
            body: JSON.stringify({
                albumId: id,
                name: albumName,
                musicianId: musicianId,
                numberOfTracks: numberOfTracks,
                yearOfRelease: yearOfRelease
            }),
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }
                return response;
            })
            .then(data => {
                console.log('Success:', data);
                getdata();
            })
            .catch((error) => {
                console.error('Error:', error);
            });
    } else {
        console.error('No row selected');
    }
}
