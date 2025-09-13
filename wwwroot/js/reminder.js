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
function CheckUncheckAll(chkAll) {
    //Fetch all rows of the Table.
    var rows = document.getElementById("data-table").rows;

    //Execute loop on all rows excluding the Header row.
    for (var i = 1; i < rows.length; i++) {
        rows[i].getElementsByTagName("INPUT")[0].checked = chkAll.checked;
    }
};
function validateInteger(event) {
    // Allow only digits (0-9), backspace, and enter
    const key = event.key;
    if (!/^[0-9]$/.test(key) && key !== "Backspace" && key !== "Enter") {
        event.preventDefault();
        return false;
    }
    return true;
}
$("#btnshow").click(function () {
    getmemdate()

   /* if ($("#ddlype").val() == 0) {
        alert("Please select Reminder Type..!");
        return false;
    }
    $.ajax({
        type: "GET",
        url: '/Admin/clubmaster/getMemberdata',
        data: { mon: $("#ddlmonth").val(), type: $("#ddlype").val(), cat: $("#ddlcatecory").val(), mem: $("#ddlmember").val(), amount: $("#ddlamount").val() },
        async: false,
        success: function (json) {
            console.log(json);
            CreateTableFromArray(JSON.parse(json), 'printdiv');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });*/
});

function getmemdate() {
    var msg = "";
    if ($("#ddlype").val() == 0) {
        //alert("Please select Reminder Type..!");
        msg = "reminder type Require..!\n"

    }
    if ($("#ddlmonth").val() == 0) {
        msg+="month require..!"
    }
    if (msg != "") {
        alert(msg);
        return false;
    }
    $.ajax({
        type: "GET",
        url: '/Admin/clubmaster/getMemberdata',
        data: { mon: $("#ddlmonth").val(), type: $("#ddlype").val(), cat: $("#ddlcatecory").val(), mem: $("#ddlmember").val(), amount: $("#ddlamount").val() },
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


//--$(function () {
    $("#memberSearch").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Admin/clubmaster/GetMembers', // change YourController
                data: {
                    id: $("#ddlcatecory").val(),              // pass category id
                    term: request.term  // what user typed
                },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            label: item.text,  // shown in dropdown
                            value: item.value  // value returned when selected
                        };
                    }));
                }
            });
        },
        minLength: 2, // start searching after 2 characters
        select: function (event, ui) {
            console.log("Selected ID:", ui.item.value);
        }
    });
//--});

function Submit() {
    if ($("#ddlype").val() == 0) {
        alert("Please select Reminder Type..!");
        return false;
    }
    jsonObj = [];
    
    $("#data-table tbody tr").each(function () {
        var row = $(this);
        var item = {}
        var check = parseInt(row.find('input.chk').is(':checked') ? 1 : 0);
        if (check == 1) {
            item.memid = row.find('.MemberId').html();
            item.amount = row.find('.Amount').html();
            item.name = row.find('.Name').html();
            item.address = row.find('.Address').html();
            item.contactno = row.find('.ContactNo').html();
            item.FirstRDate = row.find('.Hid_FirstRDate').html();
            item.SecondRdate = row.find('.Hid_SecondRDate').html();
            item.stipulated = row.find('.Hid_SecondRDate').html();
            item.thirddate = row.find('.Hid_thirsDate').html();
            item.lastrefno = row.find('.Hid_refno').html();
            item.email = row.find('.Email').html();
            item.type = $("#ddlype").val();
            item.remindertype = $("#ddlype").find("option:selected").text().replace(" ","");
            item.month = $("#ddlmonth").val();
            item.cat = $("#ddlcatecory").val();
            //item.cat = $("#ddlype").val();
            jsonObj.push(item);
        }
        
    })
    $.ajax({
        type: "POST",
        url: '/Admin/clubmaster/Submitreminder',
        data: { jdata: JSON.stringify(jsonObj) },
        async: false,
        success: function (json) {
            console.log(json);
            alert(json)
            getmemdate();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

