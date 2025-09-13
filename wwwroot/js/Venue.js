
var filenameorginal = "";
$(document).ready(function () {
    $("#imgfile").change(function () {
        filenameorginal = $('#imgfile').val();
    })
})

function validateVenue() {
    var msg = "";
    if ($('#txtdate').val() == "") { msg = "Select Block Date !!\n"; }
    if ($('#ddlvenuetypeid').val() == 0) { msg = "Select Venue Type !!\n"; }
    if ($('#ddlvenueid').val() == 0) { msg = "Select Venue Name!!\n"; }
    return msg;
}

function Clear() {
    $('#id').val("0");
    $('#txtdate').val("");
    $('#ddlvenueid').val("0");
    $('#ddlvenuetypeid').val("0");
}


function Bindvenuetypeid() {
    $.ajax({
        type: "GET",
        url: '/Admin/clubmaster/VenuetypeBind',
        data: { id: $("#ddlvenueid").val() },
        async: false,
        success: function (json, result) {
            $("#ddlvenuetypeid").empty();
            json = json || {};
            $("#ddlvenuetypeid").append('<option value="0">Select SubItemGroup</option>');
            for (var i = 0; i < json.length; i++) {
                $("#ddlvenuetypeid").append('<option value="' + json[i].value + '">' + json[i].text + '</option>');
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function IUIVenue() {
    var Id = $('#id').val();
    var venueid = $('#ddlvenueid').val();
    var venuetypeid = $('#ddlvenuetypeid').val();
    var BlockDate = $('#txtdate').val();
    var ButtonValue = $('#btnsave').val();
    var fdata = new FormData();
    fdata.append("Id", Id);
    fdata.append("venueid", venueid);
    fdata.append("venuetypeid", venuetypeid);
    fdata.append("BlockDate", BlockDate);
    var msg = validateVenue();
    if (msg == "") {
        $.ajax({
            type: "POST",
            url: '/Admin/clubmaster/IUIVenue',
            dataType: "JSON",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: fdata,
            contentType: false,
            processData: false,
            success: function (result) {
                if (result.message == "This Venue is already exit") {
                    alert(" This Venue is already exit");
                }
                else if (result.message == "Venue added") {
                    alert(" Venue Added successfully.");
                }
                else if (result.message == "Venue not added") {
                    alert(" Venue Added not successfully.");
                }
                else if (result.message == "Venue update") {
                    alert(" Venue update successfully.");
                }
                else if (result.message == "Venue not update") {
                    alert(" Venue update not successfully.");
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

function IUIVenueUpdate() {
    var Id = $('#id').val();
    var venueid = $('#ddlvenueid').val();
    var venuetypeid = $('#ddlvenuetypeid').val();
    var BlockDate = $('#txtdate').val();
    var ButtonValue = $('#btnsave').val();
    var fdata = new FormData();
    fdata.append("Id", Id);
    fdata.append("venueid", venueid);
    fdata.append("venuetypeid", venuetypeid);
    fdata.append("BlockDate", BlockDate);
    $.ajax({
        type: "POST",
        url: '/Admin/clubmaster/IUIVenue',
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
            if (result.message == "This Venue is already exit") {
                alert(" This Venue is already exit");
            }
            else if (result.message == "Venue added") {
                alert(" Venue Added successfully.");
            }
            else if (result.message == "Venue not added") {
                alert(" Venue Added not successfully.");
            }
            else if (result.message == "Venue update") {
                alert(" Venue update successfully.");
            }
            else if (result.message == "Venue not update") {
                alert(" Venue update not successfully.");
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
        url: '/Admin/clubmaster/ShowVenue',
        dataType: 'JSON',
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            console.log(data);
            var html = '';
            var index = 1;
            $.each(data, function (key, item) {
                html += '<tr class="hover-primary">';
                html += '<td style="display:none">' + item.id + '</td>';
                html += '<td style="display:none">' + item.venueid + '</td>';
                html += '<td style="display:none">' + item.venuetypeid + '</td>';
                html += '<td>' + item.venueName + '</td>';
                html += '<td>' + item.name + '</td>';
                html += '<td>' + item.blockDate + '</td>';
                html += '<td><a class="btn btn-sm" href="#" onclick="return Editbyid(' + item.id + ' , ' + item.id + ')"><i class="fa fa-edit"></i></a></td>';
                html += '<td><a class="btn btn-sm" href="#" onclick="return DeletebyId(' + item.id + ')"><i class="fa fa-trash-o"></i></a></td>';
                html += '</tr>';
                index++;
            });
            $('.SubCategorybody').html(html);
        }
    });
}

function DeletebyId(Id) {
    var checkstr = confirm('Are You Sure You Want To Delete This?');
    var PetObj = JSON.stringify({ Id: Id });
    if (checkstr == true) {
        $.ajax({
            url: '/Admin/clubmaster/DeleteVenue',
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

function Editbyid(Id, venueid) {
    var PetObj = JSON.stringify({ Id: Id });

    $.ajax({
        url: '/Admin/clubmaster/EditVenue',
        data: JSON.parse(PetObj),
        dataType: 'JSON',
        type: 'POST',
        success: function (result) {
            console.log(result);
            $("#save").val(result).hide();
            $("#update").val(result).show();
            $('#btnsave').html("UPDATE");
            $('#id').val(result.id);
            $('#ddlvenueid').val(result.venueid);
            Bindvenuetypeid();
            $('#ddlvenuetypeid').val(result.venuetypeid);
            $('#txtdate').val(result.blockDate);
        },
        error: function () {
            alert(errormessage.responseText);
        }
    });
}

