


function validateVenue() {
    var msg = "";
    if ($('#txtvenuetype').val() == "") { msg = "Select Venue Type Name !!\n"; }
    if ($('#ddlvenueid').val() == "0") { msg = "Select Venue Name!!\n"; }
    return msg;
}

function Clear() {
    $('#id').val("0");
    $('#ddlvenueid').val("0");
    $('#txtvenuetype').val("");
}


function IUTypeVenue() {
    var id = $('#id').val();
    var venuid = $('#ddlvenueid').val();
    var NAME = $('#txtvenuetype').val();
    var ButtonValue = $('#btnsave').val();
    var fdata = new FormData();
    fdata.append("id", id);
    fdata.append("venuid", venuid);
    fdata.append("NAME", NAME);
    var msg = validateVenue();
    if (msg == "") {
        $.ajax({
            type: "POST",
            url: '/Admin/clubmaster/IUTypeVenue',
            dataType: "JSON",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: fdata,
            contentType: false,
            processData: false,
            success: function (result) {
                if (result.message == "This Type Venue is already exit") {
                    alert("This Type Venue is already exit");
                }
                else if (result.message == "Type Venue added") {
                    alert("Type Venue Added successfully.");
                }
                else if (result.message == "Type Venue not added") {
                    alert("Type Venue Added not successfully.");
                }
                else if (result.message == "Type Venue update") {
                    alert("Type Venue update successfully.");
                }
                else if (result.message == "Type Venue not update") {
                    alert("Type Venue update not successfully.");
                }
                ShowDataInTable();
                Clear();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    else {
        alert(msg);
    }
}

function IUTypeVenueUpdate() {
    var id = $('#id').val();
    var venuid = $('#ddlvenueid').val();
    var NAME = $('#txtvenuetype').val();
    var ButtonValue = $('#btnsave').val();
    var fdata = new FormData();
    fdata.append("id", id);
    fdata.append("venuid", venuid);
    fdata.append("NAME", NAME);
    $.ajax({
        type: "POST",
        url: '/Admin/clubmaster/IUTypeVenue',
        dataType: "JSON",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: fdata,
        contentType: false,
        processData: false,
        success: function (result) {
            $("#save").val(result).show();
            $("#update").val(result).hide();
            if (result.message == "This Type Venue is already exit") {
                alert("This Type Venue is already exit");
            }
            else if (result.message == "Type Venue added") {
                alert("Type Venue Added successfully.");
            }
            else if (result.message == "Type Venue not added") {
                alert("Type Venue Added not successfully.");
            }
            else if (result.message == "Type Venue update") {
                alert("Type Venue update successfully.");
            }
            else if (result.message == "Type Venue not update") {
                alert("Type Venue update not successfully.");
            }
            ShowDataInTable();
            Clear();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function ShowDataInTable() {
    $.ajax({
        url: '/Admin/clubmaster/ShowTypeVenue',
        dataType: 'JSON',
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        data: { venuid: $("#ddlvenueid").val() },
        success: function (data) {
            console.log(data);
            var html = '';
            var index = 1;
            $.each(data, function (key, item) {
                html += '<tr class="hover-primary">';
                html += '<td style="display:none">' + item.id + '</td>';
                html += '<td>' + item.venueName + '</td>';
                html += '<td>' + item.name + '</td>';
                html += '<td><a class="btn btn-sm" href="#" onclick="return Editbyid(' + item.id + ' )"><i class="fa fa-edit"></i></a></td>';
                html += '<td><a class="btn btn-sm" href="#" onclick="return DeletebyId(' + item.id + ')"><i class="fa fa-trash-o"></i></a></td>';
                html += '</tr>';
                index++;
            });
            $('.SubCategorybody').html(html);
        }
    });
}

function DeletebyId(id) {
    var checkstr = confirm('Are You Sure You Want To Delete This?');
    var PetObj = JSON.stringify({ id: id });
    if (checkstr == true) {
        $.ajax({
            url: '/Admin/clubmaster/DeleteTypeVenue',
            data: JSON.parse(PetObj),
            dataType: 'JSON',
            type: 'POST',
            success: function (result) {
                console.log(result);
                if (result.msg == "Delete Successfull!!") {
                    alert("Delete Success");
                    ShowDataInTable();
                }
            },

            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    else {
        return false;
    }
}

function Editbyid(id) {
    var PetObj = JSON.stringify({ id: id });

    $.ajax({
        url: '/Admin/clubmaster/EditTypeVenue',
        data: JSON.parse(PetObj),
        dataType: 'JSON',
        type: 'POST',
        success: function (result) {
            console.log(result);
            $("#save").val(result).hide();
            $("#update").val(result).show();
            $('#btnsave').html("UPDATE");
            $('#id').val(result.id);
            $('#ddlvenueid').val(result.venuid);
            $('#txtvenuetype').val(result.name);
        },
        error: function () {
            alert(errormessage.responseText);
        }
    });
}

