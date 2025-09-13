function getmemdate() {
    var msg = "";
    //if ($("#ddlype").val() == 0) {
    //    //alert("Please select Reminder Type..!");
    //    msg = "reminder type Require..!\n"

    //}
    //if ($("#ddlmonth").val() == 0) {
    //    msg += "month require..!"
    //}
    //if (msg != "") {
    //    alert(msg);
    //    return false;
    //}
    $.ajax({
        type: "GET",
        url: '/Admin/reports/getreminderreport',
        data: { mon: $("#ddlmonth").val(), type: $("#ddlype").val(), cat: $("#ddlcatecory").val(), mem: $("#ddlmember").val(), amount: 0 },
        async: false,
        success: function (json) {
            console.log(json);
            CreateTableFromArray(JSON.parse(json), 'printdiv');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}




function getmember() {
    $.ajax({
        type: "GET",
        url: '/Admin/clubmaster/getMember',
        data: { id: $("#ddlcatecory").val() },
        async: false,
        success: function (json, result) {
            $("#ddlmember").empty();
            json = json || {};
            $("#ddlmember").append('<option value="0">Select</option>');
            for (var i = 0; i < json.length; i++) {
                $("#ddlmember").append('<option value="' + json[i].value + '">' + json[i].text + '</option>');
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}