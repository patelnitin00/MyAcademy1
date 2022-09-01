//we will need url and ID paramater to send true action with true ID

let URL = "";
let ID = "";

function AskQuestion(url, id) {
    //now we will open model message 
    $("#modalmessage").modal();

    //now set parameters
    URL = url;
    ID = id;
}

/*function AskQuestion() {
    $("#modalmessage").modal();
}*/


function Delete() {
    //now we will use ajax
    $.ajax({
        url : URL + ID,
        type: "POST",
        success: function (result) {
            //now we need to delete record in table also
            // for this we will use a_itemID parameter
            $("#a_" + ID).fadeOut();
            $("#modalmessage").modal("hide");
        }
    })
   /* alert("URL: ",URL);
    alert("ID: ",ID);*/
   
}

//now we will add this script to each list page