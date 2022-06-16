function ajaxCall(url, data) {
    OKButton = document.getElementById("modalOKButton");
    OKButton.disabled = true;
    $.ajax({
        type: "POST",
        url: url,
        //contentType: "application/json; charset=utf-8",
        data: data,
        dataType: "json",
        success: function (response, status) {
            textP = document.createElement("p");
            textP.style.textAlign = "center";
            textP.innerText = response;
            document.getElementById("modal-body").appendChild(textP);
            this.always();
        },
        error: function (error, status) { this.always(); },
        always: function () {
            OKButton = document.getElementById("modalOKButton");
            OKButton.disabled = false;
            OKbutton.onclick = function () {
                location = location;
                //hideModal();
            }
            CancelButton = document.getElementById("modalCancelButton");
            CancelButton.disabled = true;
        }
    });
}

function showInformationModal(text) {
    textP = document.createElement("p");
    textP.style.textAlign = "center";
    textP.innerText = text;
    document.getElementById("modal-body").appendChild(textP);
    $(".modal-footer").empty();
    OKbutton = document.createElement("button");
    OKbutton.innerText = "OK";
    OKbutton.id = "modalOKButton";
    OKbutton.classList.add("btn");
    OKbutton.classList.add("btn-primary");
    OKbutton.onclick = function () {
        location = location;
        //hideModal();
    }
    $(".modal-footer").append(OKbutton);
    document.getElementById("modal").style.display = "block";
}
//<claims>
function showConfirmationModalForClaims(url, data) {
    document.getElementById("modal-header").innerText = data.addOrDelete + " claim ?";
    questionP = document.createElement("p");
    questionP.style.textAlign = "center";
    questionP.innerText = data.addOrDelete + " claim ?";
    document.getElementById("modal-body").appendChild(questionP);

    OKbutton = document.createElement("button");
    OKbutton.innerText = "OK";
    OKbutton.id = "modalOKButton";
    OKbutton.classList.add("btn");
    OKbutton.classList.add("btn-primary");

    if (data.addOrDelete == 'Add') {
        claimTypeP = document.createElement("p");
        claimTypeP.style.textAlign = "center";
        claimTypeP.innerText = 'Claim Type';
        document.getElementById("modal-body").appendChild(claimTypeP);
        claimTypeTextArea = document.createElement("input");
        claimTypeP.appendChild(claimTypeTextArea);

        claimValueP = document.createElement("p");
        claimValueP.style.textAlign = "center";
        claimValueP.innerText = 'Claim Value';
        document.getElementById("modal-body").appendChild(claimValueP);
        claimValueTextArea = document.createElement("input");
        claimValueP.appendChild(claimValueTextArea);

        OKbutton.onclick = function () {
            data.claimtype = claimTypeTextArea.value;
            data.claimvalue = claimValueTextArea.value;
            claimTypeTextArea.disabled = true;
            claimValueTextArea.disabled = true;
            ajaxCall(url, data);
        }
    }
    else {
        OKbutton.onclick = function () {
            ajaxCall(url, data);
        }
    }
    $(".modal-footer").append(OKbutton);
    CancelButton = document.createElement("button");
    CancelButton.innerText = "Cancel";
    CancelButton.id = "modalCancelButton";
    CancelButton.classList.add("btn");
    CancelButton.classList.add("btn-danger");
    CancelButton.onclick = function () {
        hideModal();
    }
    $(".modal-footer").append(CancelButton);
    document.getElementById("modal").style.display = "block";
}
//</claims>

//<roles>
function showConfirmationModalForRoles(url, data) {
    console.log("Ciao");
    return
    document.getElementById("modal-header").innerText = data.addOrDelete + " role ?";
    questionP = document.createElement("p");
    questionP.style.textAlign = "center";
    questionP.innerText = data.addOrDelete + " role ?";
    document.getElementById("modal-body").appendChild(questionP);

    OKbutton = document.createElement("button");
    OKbutton.innerText = "OK";
    OKbutton.id = "modalOKButton";
    OKbutton.classList.add("btn");
    OKbutton.classList.add("btn-primary");

    if (data.addOrDelete == 'Add') {
        RoleNameP = document.createElement("p");
        RoleNameP.style.textAlign = "center";
        RoleNameP.innerText = 'Role Name';
        document.getElementById("modal-body").appendChild(RoleNameP);
        RoleNameTextArea = document.createElement("input");
        RoleNameP.appendChild(RoleNameTextArea);

        OKbutton.onclick = function () {
            data.roleName = RoleNameTextArea.value;
            RoleNameTextArea.disabled = true;
            ajaxCall(url, data);
        }
    }
    else {
        OKbutton.onclick = function () {
            ajaxCall(url, data);
        }
    }
    $(".modal-footer").append(OKbutton);
    CancelButton = document.createElement("button");
    CancelButton.innerText = "Cancel";
    CancelButton.id = "modalCancelButton";
    CancelButton.classList.add("btn");
    CancelButton.classList.add("btn-danger");
    CancelButton.onclick = function () {
        hideModal();
    }
    $(".modal-footer").append(CancelButton);
    document.getElementById("modal").style.display = "block";
}
//</roles>

//<users>
function showConfirmationModalForUsers(url, data) {
    document.getElementById("modal-header").innerText = data.addOrDelete + " user ?";
    questionP = document.createElement("p");
    questionP.style.textAlign = "center";
    questionP.innerText = data.addOrDelete + " user ?";
    document.getElementById("modal-body").appendChild(questionP);

    OKbutton = document.createElement("button");
    OKbutton.innerText = "OK";
    OKbutton.id = "modalOKButton";
    OKbutton.classList.add("btn");
    OKbutton.classList.add("btn-primary");

    if (data.addOrDelete == 'Add' || data.addOrDelete == 'Edit') {
        userNameP = document.createElement("p");
        userNameP.style.textAlign = "center";
        userNameP.innerText = 'Username';
        document.getElementById("modal-body").appendChild(userNameP);
        userNameTextArea = document.createElement("input");
        userNameP.appendChild(userNameTextArea);

        emailP = document.createElement("p");
        emailP.style.textAlign = "center";
        emailP.innerText = 'Email';
        document.getElementById("modal-body").appendChild(emailP);
        emailTextArea = document.createElement("input");
        emailP.appendChild(emailTextArea);

        passwordP = document.createElement("p");
        passwordP.style.textAlign = "center";
        passwordP.innerText = 'Password';
        document.getElementById("modal-body").appendChild(passwordP);
        passwordTextArea = document.createElement("input");
        passwordTextArea.setAttribute("type", "password");
        passwordP.appendChild(passwordTextArea);

        confirmPasswordP = document.createElement("p");
        confirmPasswordP.style.textAlign = "center";
        confirmPasswordP.innerText = 'Confirm Password';
        document.getElementById("modal-body").appendChild(confirmPasswordP);
        confirmPasswordTextArea = document.createElement("input");
        confirmPasswordTextArea.setAttribute("type", "password");
        confirmPasswordP.appendChild(confirmPasswordTextArea);

        OKbutton.onclick = function () {
            data.username = userNameTextArea.value;
            data.email = emailTextArea.value;
            data.password = passwordTextArea.value;
            data.confirmpassword = confirmPasswordTextArea.value;
            userNameTextArea.disabled = true;
            emailTextArea.disabled = true;
            passwordTextArea.disabled = true;
            confirmPasswordTextArea.disabled = true;
            ajaxCall(url, data);
        }
    }
    else {
        OKbutton.onclick = function () {
            ajaxCall(url, data);
        }
    }
    $(".modal-footer").append(OKbutton);
    CancelButton = document.createElement("button");
    CancelButton.innerText = "Cancel";
    CancelButton.id = "modalCancelButton";
    CancelButton.classList.add("btn");
    CancelButton.classList.add("btn-danger");
    CancelButton.onclick = function () {
        hideModal();
    }
    $(".modal-footer").append(CancelButton);
    document.getElementById("modal").style.display = "block";
}
//</users>

function hideModal() {
    document.getElementById("modal-header").innerText = "";
    $(".modal-body").empty();
    $(".modal-footer").empty();
    document.getElementById("modal").style.display = "none";
}


// EVENTI 
function ButtonClick(Ithis) {
    const dataDiv = $(Ithis).attr("data-div");
    const idUser = $("[data-idUser]").attr("data-idUser"); // trovare om prendere id utente da menu
    const userName = $("[data-idUser] a").text();
    const idVillage = $("#" + dataDiv + " #idVillage").val();
    const posti = $("#" + dataDiv + " #posti").val();
    const settimana = $("#" + dataDiv + " #settimana").val();

    if (posti == "" || posti < 1 || settimana == "") {
        alert("Non è possibile procedere con la prenotazione per mancanza di dati o dati non corretti nella registrazione.");
        return;
    };


    // alert("prenota");

    // creo il json con i dati
    const body = {};
    body.userName = userName;
    body.idUser = idUser;
    body.idVillage = idVillage;
    body.posti = posti;
    body.settimana = settimana;

    $.ajax({
        method: "POST",
        url: "/api/Prenota",
        // Prenotazione è il nome del controller
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(body), //  dati inviati
        dataType: "json", // response
        success: function (data, status) {
            console.log("body ", body); // dati inviati
            console.log("data ", data); // oggetto(key-value)  dati inviati

            console.log("status ", status); // response

            if (data.result === "Record inserito") {
                $("#posti").val("");
                $("#settimana").val("");

                alert(data.result);
            } else {
                alert("Errore: prenotazione non accetttata");
            };

            this.always();
        },
        error: function (error, status) {
            console.log("body ", body);
            console.log("error ", error);
            console.log("status ", status);

            //console.log("responseText ", error.responseText); // proprietà di obj error

            this.always();
        },
        always: function () { }
    });
}

const arrVillage = {
    "villaggio1": "Pacchetto Eden",
    "villaggio1_prezzo": 600,
    "villaggio2": "Pacchetto Acquavillage",
    "villaggio2_prezzo": 600,
    "villaggio3": "Pacchetto Dune",
    "villaggio3_prezzo": 600,
    "villaggio4": "Pacchetto Paradiso",
    "villaggio4_prezzo": 950,
    "villaggio5": "Pacchetto Privè",
    "villaggio5_prezzo": 950,
};

function getTabellaPrenoa() {
    const idUser = $("[data-idUser]").attr("data-idUser"); // trovare om prendere id utente da menu

    $.ajax({
        method: "GET",
        //url: "/api/Prenota/GetPrenotazioni?id=Annalisa",
        url: "/api/Prenota/GetPrenotazioni?id=" + idUser, // giusto
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, status) {
            console.log(data);

            let tableData = `
                             <table class="table table-striped table-bordered" style="background-color: azure;">
                                <thead>
                                    <tr>
                                        <th>Villaggio</th>
                                        <th>Posti</th>
                                        <th>Settimana</th>
                                        <th>Prezzo totale</th>
                                    </tr>
                                </thead>
                                <tbody>
                            `;

            for (var i = 0; i < data.length; i++) {
                let dateIta = getDateOfWeek(data[i].settimana);

                //$("#myPrenota").append("<br/><div>" + data[i].userName + ", " + data[i].idVillage + ", " + data[i].posti + ", " + dateIta + ", " + data[i] +  "</div>");
                tableData += `<tr>
                                    <td> ${arrVillage[data[i].idVillage]} </td>
                                    <td> ${data[i].posti} </td>
                                    <td> ${dateIta} </td>
                                    <td style="text-align: right;"> ${arrVillage[data[i].idVillage + "_prezzo"] * data[i].posti} €</td>
                              </tr>`;
            }

            tableData += `<tbody>`;

            $("#myPrenota").append(tableData);
            //$("#myPrenota").css("background-color: azure;"); // non va

            $("button").hide();

            this.always();
        },
        error: function (error, status) {
            console.log(error);
            console.log(status);
            this.always();
        },
        always: function () { }
    });
};

//function getDateOfWeek(w, y) {
function getDateOfWeek(week) {
    //debugger;

    const myArrayDate = week.split("-W");
    ////console.log(myArrayDate[1]); return;
    //var d = (1 + (myArrayDate[1] - 1) * 7); // 1st of January + 7 days for each week

    //return new Date(myArrayDate[0], 0, d);

    //let dateObj = new Date(dateWeek);
    //console.log(dateObj); return;


    // Create a date for 1 Jan in required year
    var d = new Date(myArrayDate[0], 0);
    // Get day of week number, sun = 0, mon = 1, etc.
    var dayNum = d.getDay();
    // Get days to add
    var requiredDate = --myArrayDate[1] * 7;

    // For ISO week numbering
    // If 1 Jan is Friday to Sunday, go to next week 
    if (dayNum != 0 || dayNum > 4) {
        requiredDate += 7;
    }

    // Add required number of days
    d.setDate(1 - d.getDay() + ++requiredDate);
    //console.log(d);
    return d.toLocaleDateString('it-US', 'DD/MM/yyyy');

}

function getAllTabellaPrenoa() {
    $.ajax({
        method: "GET",
        url: "/api/Prenota/GetAllPrenotazioni",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data, status) {
            console.log(data);

            let tableData = `
                             <table class="table table-striped table-bordered" style="background-color: azure;">
                                <thead>
                                    <tr>
                                        <th>Utente</th>
                                        <th>Villaggio</th>
                                        <th>Posti</th>
                                        <th>Settimana</th>
                                        <th>Prezzo totale</th>
                                    </tr>
                                </thead>
                                <tbody>
                            `;

            for (var i = 0; i < data.length; i++) {
                let dateIta = getDateOfWeek(data[i].settimana);

                //$("#myPrenotas").append("<br/><div>" + data[i].userName + ", " + data[i].idVillage + ", " + data[i].posti + ", " + dateIta + "</div>");

                tableData += `<tr>
                                    <td> ${data[i].userName} </td>
                                    <td> ${arrVillage[data[i].idVillage]} </td>
                                    <td> ${data[i].posti} </td>
                                    <td> ${dateIta} </td>
                                    <td style="text-align: right;"> ${arrVillage[data[i].idVillage + "_prezzo"] * data[i].posti} €</td>
                              </tr>`;
            }

            tableData += `<tbody>`;

            $("#myPrenotas").append(tableData);

            $("button").hide();

            this.always();
        },
        error: function (error, status) {
            console.log(error);
            console.log(status);
            this.always();
        },
        always: function () { }
    });
};

/*
 * <table class="table table-striped table-bordered">
          <thead>
            <tr>
              <th>Villaggio</th>
              <th>Posti</th>
              <th>Settimana</th>
              <th>Prezzo</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>Villaggio1</td>
              <td>20</td>
              <td>john@example.com</td>
              <td>1007</td>
            </tr>
            <tr>
              <td>Villaggio2</td>
              <td>15</td>
              <td>mary@example.com</td>
              <td>1</td>
            </tr>
            <tr>
              <td>Villaggio3</td>
              <td>27</td>
              <td>july@example.com</td>
              <td>5488849595959</td>
            </tr>

          </tbody>
        </table>
      </div>
      */

// TO DO
/*
 * - cambiare idVillaggio in nome pacchetto
 * -- contare quanto costo totale
 * -- cercare confirm prenotazione
 * -- gestire colore tabella pari
 */