
var filenameorginal = "";
$(document).ready(function () {
    $("#imgfile").change(function () {
        filenameorginal = $('#imgfile').val();
    })
})

function Clear() {
    $('#id').val("0");
    $('#ddlname').val("0");
    $("#chkStatus").prop("checked", false);
}



function IUGallery() {
    var Id = $('#id').val();
    var mainid = $('#ddlname').val();
    var display = $('#chkStatus').is(':checked') ? 1 : 0;
    var ButtonValue = $('#btnsave').val();
    var fdata = new FormData();
    var fileExtension = ['png', 'img', 'jpg', 'jpeg', 'PNG', 'IMG', 'JPG', 'JPEG',];
    fdata.append("Id", Id);
    fdata.append("mainid", mainid);
    fdata.append("display", display);
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
        url: '/Admin/clubmaster/IUGallery',
        dataType: "JSON",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: fdata,
        contentType: false,
        processData: false,
        success: function (result) {
            if (result.message == "Image added") {
                alert(" Image Added successfully.");
            }
            else if (result.message == "Image not added") {
                alert(" Image Added not successfully.");
            }
            else if (result.message == "Image update") {
                alert(" Image update successfully.");
            }
            else if (result.message == "Image not update") {
                alert(" Image update not successfully.");
            }
            ShowDataInTable();
            Clear();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function IUpdateGallery() {
    var Id = $('#id').val();
    var mainid = $('#ddlname').val();
    var display = $('#chkStatus').is(':checked') ? 1 : 0;
    var ButtonValue = $('#btnsave').val();
    var fdata = new FormData();
    var fileExtension = ['png', 'img', 'jpg', 'jpeg', 'PNG', 'IMG', 'JPG', 'JPEG',];
    fdata.append("Id", Id);
    fdata.append("mainid", mainid);
    fdata.append("display", display);
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
        url: '/Admin/clubmaster/IUGallery',
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
            if (result.message == "Image added") {
                alert(" Image Added successfully.");
            }
            else if (result.message == "Image not added") {
                alert(" Image Added not successfully.");
            }
            else if (result.message == "Image update") {
                alert(" Image update successfully.");
            }
            else if (result.message == "Image not update") {
                alert(" Image update not successfully.");
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
        url: '/Admin/clubmaster/ShowGallery',
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
                html += '<td style="display:none">' + item.mainid + '</td>';
                html += '<td>' + item.name + '</td>';
                html += '<td><img src="/Gallery/' + item.gImage + '" alt="your image" width="75" height="80" /></td>';
                if (item.display == "1") {
                    html += '<td class="active">Active</td>';
                }
                else {
                    html += '<td class="active">InActive</td>';
                }
                html += '<td><a class="btn btn-sm" href="#" onclick="return Editbyid(' + item.id + ' , ' + item.display + ')"><i class="fa fa-edit"></i></a></td>';
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
            url: '/Admin/clubmaster/DeleteGallery',
            data: JSON.parse(PetObj),
            dataType: 'JSON',
            type: 'POST',
            success: function (result) {
                if (result.msg == "Image Delete Successfull!!") {
                    alert("Image Delete Success");
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

function Editbyid(Id, display) {
    var PetObj = JSON.stringify({ Id: Id });

    $.ajax({
        url: '/Admin/clubmaster/EditGallery',
        data: JSON.parse(PetObj),
        dataType: 'JSON',
        type: 'POST',
        success: function (result) {
            console.log(result);
            $("#btnsv").val(result).hide();
            $("#btnupdt").val(result).show();
            $('#btnsave').html("UPDATE");
            $('#id').val(result.id);
            $('#ddlname').val(result.mainid);
            if ((display) == "1") {
                $("#chkStatus").prop("checked", true);
            }
            else {
                $('#chkStatus').prop('checked', false);
            }
            $('#imgfile').attr('src='," /Gallery/" + result.gImage + "");
            filenameorginal = result.gImage;

        },
        error: function () {
            alert(errormessage.responseText);
        }
    });
}


