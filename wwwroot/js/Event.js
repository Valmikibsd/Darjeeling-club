
var filenameorginal = "";
$(document).ready(function () {
    $("#imgfile").change(function () {
        filenameorginal = $('#imgfile').val();
    })
})

function Clear() {
    $('#id').val("0");
    $('#txteventname').val("");
    $('#txteventdate').val("");
    $("#chkStatus").prop("checked", true);
    CKEDITOR.instances["Description"].setData("");
}

function validateEvent() {
    var msg = "";
    if ($('#txteventdate').val() == "") { msg = "Please Enter Event Date !!\n"; }
    if ($('#txteventname').val() == "") { msg = "Please Enter Event Name !!\n"; }
    return msg;
}

function IUEvent() {
    var EventId = $('#id').val();
    var EventName = $('#txteventname').val();
    var EventDate = $('#txteventdate').val();
    var display = $('#chkStatus').is(':checked') ? 1 : 0;
    var DESCRIPTION = CKEDITOR.instances["Description"].getData();
    var ButtonValue = $('#btnsave').val();
    var fdata = new FormData();
    var fileExtension = ['png', 'img', 'jpg', 'jpeg', 'PNG', 'IMG', 'JPG', 'JPEG',];
    fdata.append("eventId", EventId);
    fdata.append("eventName", EventName);
    fdata.append("eventDate", EventDate);
    fdata.append("display", display);
    fdata.append("filenmes", filenameorginal);
    fdata.append("description", DESCRIPTION);
    var msg = validateEvent();
    if (msg == "") {
        var filename = $('#imgfile').val();
        if (filenameorginal == filename) {
            if (filename.length == 0) {
                alert("Please select a file.");
                return false;
            }
            else {
                var extension = filename.replace(/^.*\./, '');
                if ($.inArray(extension, fileExtension) == -1) {
                    alert("Please select only PNG/IMG/JPG/DOCX/DOC/XLSX files.");
                    return false;
                }
            }
            fdata.append("flg", "okg");
            var fileUpload = $("#imgfile").get(0);
            var files = fileUpload.files;
            fdata.append(files[0].name, files[0]);
        }
        else {
            fdata.append("flg", "ok");

        }
        $.ajax({
            type: "POST",
            url: '/Admin/clubmaster/IUEvent',
            dataType: "JSON",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: fdata,
            contentType: false,
            processData: false,
            success: function (result) {
                if (result.message == "This Event is already exit") {
                    alert(" Event is already exit.");
                }
                else if (result.message == "Event added") {
                    alert(" Event Added successfully.");
                }
                else if (result.message == "Event not added") {
                    alert(" Event Added not successfully.");
                }
                else if (result.message == "Event update") {
                    alert(" Event update successfully.");
                }
                else if (result.message == "Event not update") {
                    alert(" Event update not successfully.");
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

function IUpdateEvent() {
    var EventId = $('#id').val();
    var EventName = $('#txteventname').val();
    var EventDate = $('#txteventdate').val();
    var display = $('#chkStatus').is(':checked') ? 1 : 0;
    var DESCRIPTION = CKEDITOR.instances["Description"].getData();
    var ButtonValue = $('#btnsave').val();
    var fdata = new FormData();
    var fileExtension = ['png', 'img', 'jpg', 'jpeg', 'PNG', 'IMG', 'JPG', 'JPEG',];
    fdata.append("eventId", EventId);
    fdata.append("eventName", EventName);
    fdata.append("eventDate", EventDate);
    fdata.append("display", display);
    fdata.append("filenmes", filenameorginal);
    fdata.append("description", DESCRIPTION);
    var filename = $('#imgfile').val();
    if (filenameorginal == filename) {
        if (filename.length == 0) {
            alert("Please select a file.");
            return false;
        }
        else {
            var extension = filename.replace(/^.*\./, '');
            if ($.inArray(extension, fileExtension) == -1) {
                alert("Please select only PNG/IMG/JPG/DOCX/DOC/XLSX files.");
                return false;
            }
        }
        fdata.append("flg", "okg");
        var fileUpload = $("#imgfile").get(0);
        var files = fileUpload.files;
        fdata.append(files[0].name, files[0]);
    }
    else {
        fdata.append("flg", "ok");

    }
    $.ajax({
        type: "POST",
        url: '/Admin/clubmaster/IUEvent',
        dataType: "JSON",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: fdata,
        contentType: false,
        processData: false,
        success: function (result) {
            $("#btnsv").val(result).show();
            $("#btnupdt").val(result).hide();
            if (result.message == "This Event is already exit") {
                alert(" Event is already exit.");
            }
            else if (result.message == "Event added") {
                alert(" Event Added successfully.");
            }
            else if (result.message == "Event not added") {
                alert(" Event Added not successfully.");
            }
            else if (result.message == "Event update") {
                alert(" Event update successfully.");
            }
            else if (result.message == "Event not update") {
                alert(" Event update not successfully.");
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
        url: '/Admin/clubmaster/ShowEvent',
        dataType: 'JSON',
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            console.log(data);
            var html = '';
            var index = 1;
            $.each(data, function (key, item) {
                html += '<tr class="hover-primary">';
                html += '<td style="display:none">' + item.eventId + '</td>';
                html += '<td>' + item.eventName + '</td>';
                html += '<td>' + item.eventDate + '</td>';
                html += '<td><img src="/Event/' + item.eventImage + '" alt="your image" width="75" height="80" /></td>';
                html += '<td>' + item.description + '</td>';
                if (item.display == "1") {
                    html += '<td class="active">Active</td>';
                }
                else {
                    html += '<td class="active">InActive</td>';
                }
                html += '<td><a class="btn btn-sm" href="#" onclick="return Editbyid(' + item.eventId + ' , ' + item.display + ')"><i class="fa fa-edit"></i></a></td>';
                html += '<td><a class="btn btn-sm" href="#" onclick="return DeletebyId(' + item.eventId + ')"><i class="fa fa-trash-o"></i></a></td>';
                html += '</tr>';
                index++;
            });
            $('.SubCategorybody').html(html);
        }
    });
}

function DeletebyId(EventId) {
    var checkstr = confirm('Are You Sure You Want To Delete This?');
    var PetObj = JSON.stringify({ EventId: EventId });
    if (checkstr == true) {
        $.ajax({
            url: '/Admin/clubmaster/DeleteEvent',
            data: JSON.parse(PetObj),
            dataType: 'JSON',
            type: 'POST',
            success: function (result) {
                if (result.msg == "Event Delete Successfull!!") {
                    alert("Event Delete Success");
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

function Editbyid(EventId, display) {
    var PetObj = JSON.stringify({ EventId: EventId });

    $.ajax({
        url: '/Admin/clubmaster/EditEvent',
        data: JSON.parse(PetObj),
        dataType: 'JSON',
        type: 'POST',
        success: function (result) {
            console.log(result);
            $("#btnsv").val(result).hide();
            $("#btnupdt").val(result).show();
            $('#btnsave').html("UPDATE");
            $('#id').val(result.eventId);
            $('#txteventname').val(result.eventName);
            $('#txteventdate').val(result.eventDate);
            CKEDITOR.instances["Description"].setData(result.description);
            if ((display) == "1") {
                $("#chkStatus").prop("checked", true);
            }
            else {
                $('#chkStatus').prop('checked', false);
            }
            $('#imgfile').attr('src', "/Event/" + result.eventImage + "");
            filenameorginal = result.eventImage;

        },
        error: function () {
            alert(errormessage.responseText);
        }
    });
}


