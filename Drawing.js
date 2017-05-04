//Event handlers
//document.addEventListener("click", printMousePos);
document.addEventListener("mousedown", handleClick);

//Get new dots at regular intervals
window.setInterval(asyncLoadDots, 500);

//Handle when the mouse is clicked
function handleClick(event) {
    switch (event.button) {
        case 0:
            //Offset to center the dot on the point clicked
            drawDot(event.clientX - 3, event.clientY - 3);
            asyncStoreDot(event.clientX - 3, event.clientY - 3);
            break;

        case 1:
            asyncClearDots();
            break;
    }
}

//Loads the dots from the server
function asyncLoadDots() {
    var xhttp = new XMLHttpRequest();

    xhttp.onreadystatechange = function() {
        if (xhttp.readyState == 4 && xhttp.status == 200) {
            if (xhttp.responseText == "")
                document.getElementById("dots").innerHTML = "";
            else
                addDotsWithoutDuplicates(xhttp.responseText.split("\n"));
        }
    };
    xhttp.open("GET", "dots.aspx", true);
    xhttp.send();
}

//Inserts dots into the database
function asyncStoreDot(x, y) {
    var xhttp = new XMLHttpRequest();

    xhttp.open("GET", "dots.aspx?x=" + x + "&y=" + y, true);
    xhttp.send();
}

//Deletes dots from the database
function asyncClearDots() {
    var xhttp = new XMLHttpRequest();

    xhttp.open("GET", "dots.aspx?clear=1", true);
    xhttp.send();
}

//Draws a dot at the point specified if it does not already exist
function drawDot(x, y) {
    if (document.getElementById("x" + x + "y" + y) === null)
        document.getElementById("dots").innerHTML += makeDot(x, y);
}

//Generates the HTML for a dot at the point specified
function makeDot(x, y) {
    return "<div id='x" + x + "y" + y + "' class='dot' style='left: " + x + "px; top: " + y + "px;'></div>";
}

//Draw dots from the database that have not already been drawn
function addDotsWithoutDuplicates(dots) {
    for (i = 0; i < dots.length; i++) {
        var coords = dots[i].split(" ");
        drawDot(coords[0], coords[1]);
    }
}
