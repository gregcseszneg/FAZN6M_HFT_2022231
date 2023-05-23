let recordlabels = [];
let connection = null;
getdata();
setupSignalR();

async function getdata() {
    await fetch('http://localhost:34694/recordlabel')
        .then(x => x.json())
        .then(y => {
            recordlabels = y;
            console.log(recordlabels);

            //if we did an update/delete the selected class is already removed so we need to set the input fields to null as well
            document.getElementById('recordlabelname').value = null;
            document.getElementById('yearoffoundation').value = null;
            document.getElementById('country').value = null;
            document.getElementById('headquarters').value = null;
            display();
        });
}


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:34694/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("RecordLabelCreated", (user, message) => {
        getdata();
    });

    connection.on("RecordLabelDeleted", (user, message) => {
        getdata();
    });

    connection.on("RecordLabelUpdated", (user, message) => {
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

    recordlabels.forEach(t => {

        document.getElementById('resultarea').innerHTML += //gave the data to the resultarea
            `<tr data-recordlabel-id="${t.recordLabelId}"><td>` + t.recordLabelId + "</td><td>" //set the track ID to the row's data-id attribute
            + t.name + "</td><td>"
            + t.yearOfFoundation + "</td><td>"
            + t.country + "</td><td>"
        + t.headquarters + "</td></tr>"

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

            let id = parseInt(this.dataset.recordlabelId);
            let selectedRecordLabel = recordlabels.find(recordlabel => recordlabel.recordLabelId === id);

            // Populate the input fields
            document.getElementById('recordlabelname').value = selectedRecordLabel.name;
            document.getElementById('yearoffoundation').value = selectedRecordLabel.yearOfFoundation;
            document.getElementById('country').value = selectedRecordLabel.country;
            document.getElementById('headquarters').value = selectedRecordLabel.headquarters;
        });
    })
}

function remove() {

    let selectedRow = document.getElementById('resultarea').querySelector('.selected');

    if (selectedRow) {
        // Get the musician ID from the row's data-id attribute
        let id = parseInt(selectedRow.dataset.recordlabelId);
        console.log(id);

        fetch(`http://localhost:34694/recordlabel/${id}`, {
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
    let recordLabelName = document.getElementById('recordlabelname').value;
    let yearOfFoundation = 0;
    if (document.getElementById('yearoffoundation').value !== "") {
        yearOfFoundation = parseInt(document.getElementById('yearoffoundation').value);
    }
    let country = document.getElementById('country').value;
    let headquarters = document.getElementById('headquarters').value;

    fetch('http://localhost:34694/recordlabel', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify({
            name: recordLabelName,
            yearOfFoundation: yearOfFoundation,
            country: country,
            headquarters: headquarters
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
    let id = parseInt(selectedRow.dataset.recordlabelId);

    if (selectedRow) {
        let recordLabelName = document.getElementById('recordlabelname').value;
        let yearOfFoundation = 0;
        if (document.getElementById('yearoffoundation').value !== "") {
            yearOfFoundation = parseInt(document.getElementById('yearoffoundation').value);
        }
        let country = document.getElementById('country').value;
        let headquarters = document.getElementById('headquarters').value;
        

        fetch('http://localhost:34694/recordlabel', {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json', },
            body: JSON.stringify({
                recordLabelId: id,
                name: recordLabelName,
                yearOfFoundation: yearOfFoundation,
                country: country,
                headquarters: headquarters
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
