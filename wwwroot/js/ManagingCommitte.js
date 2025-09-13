
var filenameorginal = "";
$(document).ready(function () {
    $("#imgfile").change(function () {
        filenameorginal = $('#imgfile').val();
    })
})

function Clear() {
    $('#id').val("0");
    $('#txtname').val("");
    $('#txtdesignation').val("");
    $("#txtaddress").val("");
    $("#txtmobile").val("");
    $("#txtorderby").val("");
    CKEDITOR.instances["Description"].setData("");
}

function validateManagingCommitte() {
    var msg = "";   
    if ($('#txtdescription').val() == "") { msg = "Please Enter Description !!\n"; }
    if ($('#txtorderby').val() == "") { msg = "Please Enter Order Number !!\n"; }
    if ($('#txtmobile').val() == "") { msg = "Please Enter Mobile Number !!\n"; }
    if ($('#txtaddress').val() == "") { msg = "Please Enter Address !!\n"; }
    if ($('#txtdesignation').val() == "") { msg = "Please Enter Designation !!\n"; }
    if ($('#txtname').val() == "") { msg = "Please Enter Name !!\n"; }
    return msg;
}

function IUManagingCommitte() {
    var ManagingId = $('#id').val();
    var Name = $('#txtname').val();
    var Designation = $('#txtdesignation').val();
    var Address = $('#txtaddress').val();
    var Mobile = $('#txtmobile').val();
    var DESCRIPTION = CKEDITOR.instances["Description"].getData();
    var OrderBy = $('#txtorderby').val();
    var ButtonValue = $('#btnsave').val();
    var fdata = new FormData();
    var fileExtension = ['png', 'img', 'jpg', 'jpeg', 'PNG', 'IMG', 'JPG', 'JPEG',];
    fdata.append("managingId", ManagingId);
    fdata.append("name", Name);
    fdata.append("designation", Designation);
    fdata.append("address", Address);
    fdata.append("mobile", Mobile);
    fdata.append("filenmes", filenameorginal);
    fdata.append("description", DESCRIPTION);
    fdata.append("orderBy", OrderBy);
    var msg = validateManagingCommitte();
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
            url: '/Admin/clubmaster/IUManagingCommitte',
            dataType: "JSON",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: fdata,
            contentType: false,
            processData: false,
            success: function (result) {
                if (result.message == "This order number is already exit") {
                    alert(" This order number is already exit.");
                }
                else if (result.message == "Managing Committe added") {
                    alert(" Managing Committe Added successfully.");
                }
                else if (result.message == "Managing Committe not added") {
                    alert(" Managing Committe Added not successfully.");
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

function IUpdateManagingCommitte() {
    var ManagingId = $('#id').val();
    var Name = $('#txtname').val();
    var Designation = $('#txtdesignation').val();
    var Address = $('#txtaddress').val();
    var Mobile = $('#txtmobile').val();
    var DESCRIPTION = CKEDITOR.instances["Description"].getData();
    var OrderBy = $('#txtorderby').val();
    var ButtonValue = $('#btnsave').val();
    var fdata = new FormData();
    var fileExtension = ['png', 'img', 'jpg', 'jpeg', 'PNG', 'IMG', 'JPG', 'JPEG',];
    fdata.append("managingId", ManagingId);
    fdata.append("name", Name);
    fdata.append("designation", Designation);
    fdata.append("address", Address);
    fdata.append("mobile", Mobile);
    fdata.append("filenmes", filenameorginal);
    fdata.append("description", DESCRIPTION);
    fdata.append("orderBy", OrderBy);
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
        url: '/Admin/clubmaster/IUManagingCommitte',
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
            
            if (result.message == "This order number is already exit") {
                alert(" This order number is already exit.");
            }
            else if (result.message == "Managing Committe update") {
                alert(" Managing Committe update successfully.");
            }
            else if (result.message == "Managing Committe not update") {
                alert(" Managing Committe update not successfully.");
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
        url: '/Admin/clubmaster/ShowManagingCommitte',
        dataType: 'JSON',
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            console.log(data);
            var html = '';
            var index = 1;
            $.each(data, function (key, item) {
                html += '<tr class="hover-primary">';
                html += '<td style="display:none">' + item.managingId + '</td>';
                html += '<td>' + item.name + '</td>';
                html += '<td>' + item.designation + '</td>';
                html += '<td>' + item.address + '</td>';
                html += '<td>' + item.mobile + '</td>';
                html += '<td><img src="/ManagingCommitte/' + item.photo + '" alt="your image" width="75" height="80" /></td>';
                html += '<td>' + item.description + '</td>';
                html += '<td>' + item.orderBy + '</td>';
                html += '<td><a class="btn btn-sm" href="#" onclick="return Editbyid(' + item.managingId + ')"><i class="fa fa-edit"></i></a></td>';
                html += '<td><a class="btn btn-sm" href="#" onclick="return DeletebyId(' + item.managingId + ')"><i class="fa fa-trash-o"></i></a></td>';
                html += '</tr>';
                index++;
            });
            $('.SubCategorybody').html(html);
        }
    });
}

function DeletebyId(ManagingId) {
    var checkstr = confirm('Are You Sure You Want To Delete This?');
    var PetObj = JSON.stringify({ ManagingId: ManagingId });
    if (checkstr == true) {
        $.ajax({
            url: '/Admin/clubmaster/DeleteManagingCommitte',
            data: JSON.parse(PetObj),
            dataType: 'JSON',
            type: 'POST',
            success: function (result) {
                if (result.msg == "ManagingCommitte Delete Successfull!!") {
                    alert("ManagingCommitte Delete Success");
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

function Editbyid(ManagingId) {
    var PetObj = JSON.stringify({ ManagingId: ManagingId });

    $.ajax({
        url: '/Admin/clubmaster/EditManagingCommitte',
        data: JSON.parse(PetObj),
        dataType: 'JSON',
        type: 'POST',
        success: function (result) {
            console.log(result);
            $("#btnsv").val(result).hide();
            $("#btnupdt").val(result).show();
            $('#btnsave').html("UPDATE");
            $('#id').val(result.managingId);
            $('#txtname').val(result.name);
            $('#txtdesignation').val(result.designation);
            $('#txtaddress').val(result.address);
            $('#txtmobile').val(result.mobile);
            $('#imgfile').attr('src', "/ManagingCommitte/" + result.photo + "");
            filenameorginal = result.photo;
            CKEDITOR.instances["Description"].setData(result.description);
            $('#txtorderby').val(result.orderBy);
        },
        error: function () {
            alert(errormessage.responseText);
        }
    });
}


