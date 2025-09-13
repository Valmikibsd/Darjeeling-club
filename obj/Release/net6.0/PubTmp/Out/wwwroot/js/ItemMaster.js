
var filenameorginal = "";
$(document).ready(function () {
    $("#imgfile").change(function () {
        filenameorginal = $('#imgfile').val();
    })
})

function Clear() {
    $('#id').val("0");
    $('#ddlgroupitem').val("0");
    $('#ddlsubiteam').val("0");
    $('#txtname').val("");
    $('#txtprice').val("");
}

function validateItemMaster() {
    var msg = "";
    if ($('#txtprice').val() == "") { msg = "Please Enter Price !!\n"; }
    if ($('#ddlsubiteam').val() == "0") { msg = "Please Select Sub Item !!\n"; }
    if ($('#ddlgroupitem').val() == "0") { msg = "Please Select Item Group Name !!\n"; }
    if ($('#txtname').val() == "") { msg = "Please Enter Item Name !!\n"; }
    return msg;
}

function BindSubcategory() {
    $.ajax({
        type: "GET",
        url: '/Admin/clubmaster/BindItemSubMaster',
        data: { ItemGroupcode: $("#ddlgroupitem").val() },
        async: false,
        success: function (json, result) {
            $("#ddlsubiteam").empty();
            json = json || {};
            $("#ddlsubiteam").append('<option value="0">Select Sub Item Group</option>');
            for (var i = 0; i < json.length; i++) {
                $("#ddlsubiteam").append('<option value="' + json[i].value + '">' + json[i].text + '</option>');
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function IUItemMaster() {
    var itemId = $('#id').val();
    var ItemGroupcode = $('#ddlgroupitem').val();
    var Itemsubgroupcode = $('#ddlsubiteam').val();
    var Itemname = $('#txtname').val();
    var ItemPrice = $('#txtprice').val();
    var display = $('#chkStatus').is(':checked') ? 1 : 0;
    var ImageDisplay = $('#chkImage').is(':checked') ? 1 : 0;
    var ButtonValue = $('#btnsave').val();
    var fdata = new FormData();
    var fileExtension = ['png', 'img', 'jpg', 'jpeg', 'PNG', 'IMG', 'JPG', 'JPEG',];
    fdata.append("itemId", itemId);
    fdata.append("ItemGroupcode", ItemGroupcode);
    fdata.append("Itemsubgroupcode", Itemsubgroupcode);
    fdata.append("Itemname", Itemname);
    fdata.append("ItemPrice", ItemPrice);
    fdata.append("display", display);
    fdata.append("ImageDisplay", ImageDisplay);
    fdata.append("filenmes", filenameorginal);
    var msg = validateItemMaster();
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
            url: '/Admin/clubmaster/IUItemMaster',
            dataType: "JSON",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: fdata,
            contentType: false,
            processData: false,
            success: function (result) {
                if (result.message == "This ItemMaster is already exit") {
                    alert(" ItemMaster is already exit.");
                }
                else if (result.message == "ItemMaster added") {
                    alert(" ItemMaster Added successfully.");
                }
                else if (result.message == "ItemMaster not added") {
                    alert(" ItemMaster Added not successfully.");
                }
                else if (result.message == "ItemMaster update") {
                    alert(" ItemMaster update successfully.");
                }
                else if (result.message == "ItemMaster not update") {
                    alert(" ItemMaster update not successfully.");
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

function IUUpdateItemMaster() {
    var itemId = $('#id').val();
    var ItemGroupcode = $('#ddlgroupitem').val();
    var Itemsubgroupcode = $('#ddlsubiteam').val();
    var Itemname = $('#txtname').val();
    var ItemPrice = $('#txtprice').val();
    var display = $('#chkStatus').is(':checked') ? 1 : 0;
    var ImageDisplay = $('#chkImage').is(':checked') ? 1 : 0;
    var ButtonValue = $('#btnsave').val();
    var fdata = new FormData();
    var fileExtension = ['png', 'img', 'jpg', 'jpeg', 'PNG', 'IMG', 'JPG', 'JPEG',];
    fdata.append("itemId", itemId);
    fdata.append("ItemGroupcode", ItemGroupcode);
    fdata.append("Itemsubgroupcode", Itemsubgroupcode);
    fdata.append("Itemname", Itemname);
    fdata.append("ItemPrice", ItemPrice);
    fdata.append("display", display);
    fdata.append("ImageDisplay", ImageDisplay);
    fdata.append("filenmes", filenameorginal);
    var msg = validateItemMaster();
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
            url: '/Admin/clubmaster/IUItemMaster',
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
                if (result.message == "This ItemMaster is already exit") {
                    alert(" ItemMaster is already exit.");
                }
                else if (result.message == "ItemMaster added") {
                    alert(" ItemMaster Added successfully.");
                }
                else if (result.message == "ItemMaster not added") {
                    alert(" ItemMaster Added not successfully.");
                }
                else if (result.message == "ItemMaster update") {
                    alert(" ItemMaster update successfully.");
                }
                else if (result.message == "ItemMaster not update") {
                    alert(" ItemMaster update not successfully.");
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

function ShowDataInTable() {
    $.ajax({
        url: '/Admin/clubmaster/ShowItemMaster',
        dataType: 'JSON',
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            var html = '';
            var index = 1;
            $.each(data, function (key, item) {
                html += '<tr class="hover-primary">';
                html += '<td style="display:none">' + item.itemId + '</td>';
                html += '<td style="display:none">' + item.itemsubgroupcode + '</td>';
                html += '<td>' + item.itemName + '</td>';
                html += '<td>' + item.itemgroup + '</td>';
                html += '<td>' + item.itemPrice + '</td>';
                html += '<td>' + item.itemsubgroup + '</td>';
                html += '<td><img src="/images/' + item.item_Image + '" alt="your image" width="75" height="80" /></td>';
                if (item.display == "1") {
                    html += '<td class="active">Active</td>';
                }
                else {
                    html += '<td class="active">InActive</td>';
                }
                if (item.imageDisplay == "1") {
                    html += '<td class ="active">Show</td>';
                }
                else {
                    html += '<td class="active">Hide</td>';
                }
                html += '<td><a class="btn btn-sm" href="#" onclick="return Editbyid(' + item.itemId + ' , ' + item.itemGroupcode + ', ' + item.display + ', ' + item.imageDisplay + ')"><i class="fa fa-edit"></i></a></td>';
                html += '<td><a class="btn btn-sm" href="#" onclick="return DeletebyId(' + item.itemId + ')"><i class="fa fa-trash-o"></i></a></td>';
                html += '</tr>';
                index++;
            });
            $('.SubCategorybody').html(html);
        }
    });
}

function DeletebyId(itemId) {
    var checkstr = confirm('Are You Sure You Want To Delete This?');
    var PetObj = JSON.stringify({ itemId: itemId });
    if (checkstr == true) {
        $.ajax({
            url: '/Admin/clubmaster/DeleteItemMaster',
            data: JSON.parse(PetObj),
            dataType: 'JSON',
            type: 'POST',
            success: function (result) {
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

function Editbyid(itemId, itemGroupcode, display, imageDisplay) {
    var PetObj = JSON.stringify({ itemId: itemId });

    $.ajax({
        url: '/Admin/clubmaster/EditItemMaster',
        data: JSON.parse(PetObj),
        dataType: 'JSON',
        type: 'POST',
        success: function (result) {
            console.log(result);
            $("#btnsv").val(result).hide();
            $("#btnupdt").val(result).show();
            $('#btnsave').html("UPDATE");
            $('#id').val(result.itemId);
            $('#ddlgroupitem').val(result.itemGroupcode);
            BindSubcategory();
            $('#ddlsubiteam').val(result.itemsubgroupcode);
            $('#txtname').val(result.itemName);
            $('#txtprice').val(result.itemPrice);
            if ((display) == "1") {
                $("#chkStatus").prop("checked", true);
            }
            else {
                $('#chkStatus').prop('checked', false);
            }
            if ((imageDisplay) == "1") {
                $("#chkImage").prop("checked", true);
            }
            else {
                $('#chkImage').prop('checked', false);
            }
            $('#imgfile').attr('src', "/Images/" + result.item_Image + "");
            filenameorginal = result.item_Image;

        },
        error: function () {
            alert(errormessage.responseText);
        }
    });
}

