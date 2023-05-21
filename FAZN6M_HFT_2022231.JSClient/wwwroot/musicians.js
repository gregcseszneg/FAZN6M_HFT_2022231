let musicians = [];
let connection = null;
getdata();
setupSignalR();

async function getdata() {
    await fetch('http://localhost:34694/musician')
        .then(x => x.json())
        .then(y => {
            musicians = y;
            //console.log(musicians);

            //if we did an update/delete the selected class is already removed so we need to set the input fields to null as well
            document.getElementById('musicianname').value = null;
            document.getElementById('dateofbirth').value = null;
            document.getElementById('age').value = null;
            document.getElementById('gender').value = null;
            document.getElementById('country').value = null;
            document.getElementById('hometown').value = null;
            document.getElementById('recordlabelid').value = null;
            display();
        });
}


function setupSignalR() {
        connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:34694/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("MusicianCreated", (user, message) => {
        getdata();
    });

    connection.on("MusicianDeleted", (user, message) => {
        getdata();
    });

    connection.on("MusicianUpdated", (user, message) => {
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

    musicians.forEach(t => {
        let dob = new Date(t.dateOfBirth).toLocaleDateString(); //convert the dateTime to not have hour, minutes and secs displayed

        document.getElementById('resultarea').innerHTML += //gave the data to the resultarea
            `<tr data-musician-id="${t.musicianId}"><td>` + t.musicianId + "</td><td>" //set the musician ID tp the row's data-id attribute
                + t.name + "</td><td>"
                + dob + "</td><td>"
                + t.age + "</td><td>"
                + t.gender + "</td><td>"
                + t.country + "</td><td>"
                + t.homeTown + "</td><td>"
                + t.recordLabelId + "</td></tr>"

        console.log(t.name);
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

            let id = parseInt(this.dataset.musicianId);
            let selectedMusician = musicians.find(musician => musician.musicianId === id);

            // Populate the input fields
            document.getElementById('musicianname').value = selectedMusician.name;
            document.getElementById('dateofbirth').value = new Date(selectedMusician.dateOfBirth).toISOString().split('T')[0];
            document.getElementById('age').value = selectedMusician.age;
            document.getElementById('gender').value = selectedMusician.gender;
            document.getElementById('country').value = selectedMusician.country;
            document.getElementById('hometown').value = selectedMusician.homeTown;
            document.getElementById('recordlabelid').value = selectedMusician.recordLabelId;
        });
    })
}

function remove() {

    let selectedRow = document.getElementById('resultarea').querySelector('.selected');

    if (selectedRow) {
        // Get the musician ID from the row's data-id attribute
        let id = parseInt(selectedRow.dataset.musicianId);
        console.log(id);

        fetch(`http://localhost:34694/musician/${id}`, {
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
    let musicianName = document.getElementById('musicianname').value;
    let dateOfBirth = new Date("0001-01-01");
    if (document.getElementById('dateofbirth').value !== "") {
        dateOfBirth = new Date(document.getElementById('dateofbirth').value);
    }
    console.log(dateOfBirth);
    let age = parseInt(document.getElementById('age').value);
    let gender = document.getElementById('gender').value;
    let country = document.getElementById('country').value;
    let homeTown = document.getElementById('hometown').value;
    let recordLabelId= 0;
    if (document.getElementById('recordlabelid').value!=="") {
        recordLabelId = parseInt(document.getElementById('recordlabelid').value);
    }

    fetch('http://localhost:34694/musician', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({
            name: musicianName,
            dateOfBirth: dateOfBirth.toISOString(),
            age: age,
            gender: gender,
            country: country,
            homeTown: homeTown,
            recordLabelId: recordLabelId
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

    if (selectedRow) {
        let id = parseInt(selectedRow.dataset.musicianId);

        let musicianName = document.getElementById('musicianname').value;
        let dateOfBirth = new Date("0001-01-01");
        if (document.getElementById('dateofbirth').value !== "") {
            dateOfBirth = new Date(document.getElementById('dateofbirth').value);
        }
        let age = parseInt(document.getElementById('age').value);
        let gender = document.getElementById('gender').value;
        let country = document.getElementById('country').value;
        let homeTown = document.getElementById('hometown').value;
        let recordLabelId = 0;
        if (document.getElementById('recordlabelid').value !== "") {
            recordLabelId = parseInt(document.getElementById('recordlabelid').value);
        }

        fetch(`http://localhost:34694/musician`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json', },
            body: JSON.stringify({
                musicianId: id,
                name: musicianName,
                dateOfBirth: dateOfBirth.toISOString(),
                age: age,
                gender: gender,
                country: country,
                homeTown: homeTown,
                recordLabelId: recordLabelId
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
