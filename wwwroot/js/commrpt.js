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

function getmemdate() {
    var msg = "";
    
    $.ajax({
        type: "GET",
        url: '/Admin/reports/getreminderreport',
        data: { cat: $("#ddlcatecory").val(), mem: $("#ddlmember").val() },
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