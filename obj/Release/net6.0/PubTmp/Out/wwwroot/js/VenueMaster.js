
var filenameorginal = "";
$(document).ready(function () {
    $("#imgfile").change(function () {
        filenameorginal = $('#imgfile').val();
    })
})

function validateVenue() {
    var msg = "";
    if (CKEDITOR.instances["Description"].getData() == "") { msg = "Enter Description!!\n"; }
    if ($('#txtrateformember').val() == "") { msg = "Enter Rate For Member!!\n"; }
    if ($('#txtshortname').val() == "") { msg = "Enter Short Name !!\n"; }
    if ($('#txtvenuename').val() == "") { msg = "Enter Venue Name!!\n"; }
    return msg;
}

function Clear() {
    $('#id').val("0");
    $('#txtvenuename').val("");
    $('#txtshortname').val("");
    $('#txtrateformember').val("");
    CKEDITOR.instances["Description"].setData("");
}

function IUIVenueMaster() {
    var ID = $('#id').val();
    var VenueName = $('#txtvenuename').val();
    var ShortName = $('#txtshortname').val();
    var RateForMember = $('#txtrateformember').val();
    var Description = CKEDITOR.instances["Description"].getData();
    var ButtonValue = $('#btnsave').val();
    var fdata = new FormData();
    var fileExtension = ['png', 'img', 'jpg', 'jpeg', 'PNG', 'IMG', 'JPG', 'JPEG',];
    fdata.append("ID", ID);
    fdata.append("VenueName", VenueName);
    fdata.append("ShortName", ShortName);
    fdata.append("RateForMember", RateForMember);
    fdata.append("Description", Description);
    fdata.append("filenmes", filenameorginal);
    var msg = validateVenue();
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
            url: '/Admin/clubmaster/IUIVenueMaster',
            dataType: "JSON",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: fdata,
            contentType: false,
            processData: false,
            success: function (result) {
                if (result.message == "This Venue Master is already exit") {
                    alert("This Venue Master is already exit");
                }
                else if (result.message == "Venue Master added") {
                    alert("Venue Master Added successfully.");
                }
                else if (result.message == "Venue Master not added") {
                    alert("Venue Master Added not successfully.");
                }
                else if (result.message == "Venue Master update") {
                    alert("Venue Master update successfully.");
                }
                else if (result.message == "Venue Master not update") {
                    alert("Venue Master update not successfully.");
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

function IUIVenueMasterUpdate() {
    var ID = $('#id').val();
    var VenueName = $('#txtvenuename').val();
    var ShortName = $('#txtshortname').val();
    var RateForMember = $('#txtrateformember').val();
    var Description = CKEDITOR.instances["Description"].getData();
    var ButtonValue = $('#btnsave').val();
    var fdata = new FormData();
    var fileExtension = ['png', 'img', 'jpg', 'jpeg', 'PNG', 'IMG', 'JPG', 'JPEG',];
    fdata.append("ID", ID);
    fdata.append("VenueName", VenueName);
    fdata.append("ShortName", ShortName);
    fdata.append("RateForMember", RateForMember);
    fdata.append("Description", Description);
    fdata.append("filenmes", filenameorginal);
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
        url: '/Admin/clubmaster/IUIVenueMaster',
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
            if (result.message == "This Venue Master is already exit") {
                alert("This Venue Master is already exit");
            }
            else if (result.message == "Venue Master added") {
                alert("Venue Master Added successfully.");
            }
            else if (result.message == "Venue Master not added") {
                alert("Venue Master Added not successfully.");
            }
            else if (result.message == "Venue Master update") {
                alert("Venue Master update successfully.");
            }
            else if (result.message == "Venue Master not update") {
                alert("Venue Master update not successfully.");
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
        url: '/Admin/clubmaster/ShowVenueMaster',
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
                html += '<td>' + item.venueName + '</td>';
                html += '<td>' + item.shortName + '</td>';
                html += '<td>' + item.rateForMember + '</td>';
                html += '<td><img src="/VenueMaster/' + item.photo + '" alt="your image" width="75" height="80" /></td>';
                html += '<td>' + item.description + '</td>';
                html += '<td><a class="btn btn-sm" href="#" onclick="return Editbyid(' + item.id + ' )"><i class="fa fa-edit"></i></a></td>';
                html += '<td><a class="btn btn-sm" href="#" onclick="return DeletebyId(' + item.id + ')"><i class="fa fa-trash-o"></i></a></td>';
                html += '</tr>';
                index++;
            });
            $('.SubCategorybody').html(html);
        }
    });
}

function DeletebyId(ID) {
    var checkstr = confirm('Are You Sure You Want To Delete This?');
    var PetObj = JSON.stringify({ ID: ID });
    if (checkstr == true) {
        $.ajax({
            url: '/Admin/clubmaster/DeleteVenueMaster',
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

function Editbyid(ID, venueid) {
    var PetObj = JSON.stringify({ ID: ID });

    $.ajax({
        url: '/Admin/clubmaster/EditVenueMaster',
        data: JSON.parse(PetObj),
        dataType: 'JSON',
        type: 'POST',
        success: function (result) {
            console.log(result);
            $("#save").val(result).hide();
            $("#update").val(result).show();
            $('#btnsave').html("UPDATE");
            $('#id').val(result.id);
            $('#txtvenuename').val(result.venueName);
            $('#txtshortname').val(result.shortName);
            $('#txtrateformember').val(result.rateForMember);
            $('#imgfile').attr('src', "/VenueMaster/" + result.photo + "");
            filenameorginal = result.photo;
            CKEDITOR.instances["Description"].setData(result.description);
        },
        error: function () {
            alert(errormessage.responseText);
        }
    });
}

