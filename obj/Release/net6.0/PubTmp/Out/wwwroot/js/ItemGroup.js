

//------------ItemGroup----------------

function validatecategory() {
    var msg = "";
    if ($('#txtcate').val() == "") { msg = "Item Name  can not left Blank !! \n"; }
    return msg;
}

function clear() {
    $('#id').val("0");
    $('#txtcate').val("");
    $("#chkStatus").prop("checked", true);
}

function IUItemGroup() {
    var ItemGroupCode = $('#id').val();
    var ItemGroup = $('#txtcate').val();
    var Status = $('#chkStatus').is(':checked') ? 1 : 0;
    var msg = validatecategory();
    if (msg == "") {
        $.ajax({
            type: "POST",
            url: '/Admin/clubmaster/IUItemGroup',
            data: { ItemGroupCode: ItemGroupCode, ItemGroup: ItemGroup, Status: Status },
            dataType: "JSON",
            success: function (result) {
                if (result.message == "This ItemGroup is already exit") {
                    alert("This Category is already exit");
                }
                else if (result.message == "ItemGroup added.") {
                    alert("ItemGroup Added successfully.");
                }
                else if (result.message == "ItemGroup updated.") {
                    alert("ItemGroup modified successfully.");
                }
                clear();
                ShowDataInTable();

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

function IUUpdateItemGroup() {
    var ItemGroupCode = $('#id').val();
    var ItemGroup = $('#txtcate').val();
    var Status = $('#chkStatus').is(':checked') ? 1 : 0;
    $.ajax({
        type: "POST",
        url: '/Admin/clubmaster/IUItemGroup',
        data: { ItemGroupCode: ItemGroupCode, ItemGroup: ItemGroup, Status: Status },
        dataType: "JSON",
        success: function (result) {
            $("#btnsv").val(result).show();
            $("#btnupdt").val(result).hide();
            if (result.message == "This ItemGroup is already exit") {
                alert("This Category is already exit");
            }
            else if (result.message == "ItemGroup added.") {
                alert("ItemGroup Added successfully.");
            }
            else if (result.message == "ItemGroup updated.") {
                alert("ItemGroup modified successfully.");
            }
            ShowDataInTable();
            clear();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function ShowDataInTable() {
    $.ajax({
        url: '/Admin/clubmaster/ShowItemGroup',
        dataType: 'JSON',
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            var html = '';
            var index = 1;
            $.each(data, function (key, item) {
                html += '<tr class="hover-primary">';
                html += '<td style="display:none">' + item.itemGroupCode + '</td>';
                html += '<td>' + item.itemGroup + '</td>';
                if (item.status == "1") {
                    html += '<td class="active">Active</td>';
                }
                else {
                    html += '<td class="active">InActive</td>';
                }

                html += '<td><a class="btn btn-sm" href="#" onclick="return Editbyid(' + item.itemGroupCode + ', ' + item.status + ')"><i class="fa fa-edit"></i></a></td>';
                html += '<td><a class="btn btn-sm" href="#" onclick="return DeletebyId(' + item.itemGroupCode + ')"><i class="fa fa-trash-o"></i></a></td>';
                html += '</tr>';
                index++;
            });
            $('.Categorybody').html(html);
        }
    });
}

function Editbyid(itemGroupCode, Status) {
    var PetObj = JSON.stringify({ ItemGroupCode: itemGroupCode });
    $.ajax({
        url: '/Admin/clubmaster/EditItemGroup',
        data: JSON.parse(PetObj),
        dataType: 'JSON',
        type: 'POST',
        success: function (result) {
            $("#btnsv").val(result).hide();
            $("#btnupdt").val(result).show();
            $('#btnSave').val = "UPDATE";
            $('#id').val(result.itemGroupCode);
            $('#txtcate').val(result.itemGroup);
            if ((Status) == "1") {
                $("#chkStatus").prop("checked", true);
            }
            else {
                $('#chkStatus').prop('checked', false);
            }
        },
        error: function () {
            alert("Data Not Found !!");
        }
    });
}

function DeletebyId(itemGroupCode) {
    var checkstr = confirm('Are You Sure You Want To Delete This?');
    var PetObj = JSON.stringify({ ItemGroupCode: itemGroupCode });
    if (checkstr == true) {
        $.ajax({
            url: '/Admin/clubmaster/DeleteItemGroup',
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

//--------------------------ItemSubGroup----------------------



function validateSubcategory() {
    var msg = "";
    if ($('#txtsubcate').val() == "") { msg = "IteamSubGroup  can not left Blank !! \n"; }
    if ($('#ddlcategory').val() == 0) { msg = " Select ItemGroup  !! \n"; }
    return msg;
}

function Clear() {
    $('#id').val("");
    $('#ddlcategory').val("0");
    $('#txtsubcate').val("");
    $("#chkStatus").prop("checked", true);
}

function IUItemSubGroup() {
    var Itemsubgroupcode = $('#id').val();
    var ItemGroupcode = $('#ddlcategory').val();
    var Itemsubgroup = $('#txtsubcate').val();
    var Status = $('#chkStatus').is(':checked') ? 1 : 0;
    var msg = validateSubcategory();
    if (msg == "") {
        $.ajax({
            type: "POST",
            url: '/Admin/clubmaster/IUItemSubGroup',
            data: { Itemsubgroupcode: Itemsubgroupcode, ItemGroupcode: ItemGroupcode, Itemsubgroup: Itemsubgroup, Status: Status },
            dataType: "JSON",
            success: function (result) {
                if (result.message == "This IteamSubGroup is already exit") {
                    alert("This IteamSubGroup is already exit");
                }
                else if (result.message == "IteamSubGroup added.") {

                    alert("IteamSubGroup Added successfully.");
                }
                else if (result.message == "IteamSubGroup updated.") {
                    alert("IteamSubGroup Modify successfully.");
                }
                Clear();
                ShowItemSubGroup();

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

function IUUpdateItemSubGroup() {
    var Itemsubgroupcode = $('#id').val();
    var ItemGroupcode = $('#ddlcategory').val();
    var Itemsubgroup = $('#txtsubcate').val();
    var Status = $('#chkStatus').is(':checked') ? 1 : 0;
    $.ajax({
        type: "POST",
        url: '/Admin/clubmaster/IUItemSubGroup',
        data: { Itemsubgroupcode: Itemsubgroupcode, ItemGroupcode: ItemGroupcode, Itemsubgroup: Itemsubgroup, Status: Status },
        dataType: "JSON",
        success: function (result) {
            $("#btnsv").val(result).show();
            $("#btnupdt").val(result).hide();
            if (result.message == "This IteamSubGroup is already exit") {
                alert("This IteamSubGroup is already exit");
            }
            else if (result.message == "IteamSubGroup added.") {

                alert("IteamSubGroup Added successfully.");
            }
            else if (result.message == "IteamSubGroup updated.") {
                alert("IteamSubGroup Modify successfully.");
            }
            Clear();
            ShowItemSubGroup();

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function ShowItemSubGroup() {
    $.ajax({
        url: '/Admin/clubmaster/ShowItemSubGroup',
        dataType: 'JSON',
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            var html = '';
            var index = 1;
            $.each(data, function (key, item) {
                html += '<tr class="hover-primary">';
                html += '<td style="display:none">' + item.itemsubgroupcode + '</td>';
                html += '<td>' + item.itemgroup + '</td>';
                html += '<td>' + item.itemsubgroup + '</td>';
                if (item.status == "1") {
                    html += '<td class="active">Active</td>';
                }
                else {
                    html += '<td class="active">InActive</td>';
                }
                html += '<td><a class="btn btn-sm" href="#" onclick="return EditItemSubGroup(' + item.itemsubgroupcode + ' , ' + item.status + ')"><i class="fa fa-edit"></i></a></td>';
                html += '<td><a class="btn btn-sm" href="#" onclick="return DeleteItemSubGroup(' + item.itemsubgroupcode + ')"><i class="fa fa-trash-o"></i></a></td>';
                html += '</tr>';
                index++;
            });
            $('.SubCategorybody').html(html);
        }
    });
}

function EditItemSubGroup(itemsubgroupcode, status) {
    var PetObj = JSON.stringify({ itemsubgroupcode: itemsubgroupcode });
    $.ajax({
        url: '/Admin/clubmaster/EditItemSubGroup',
        data: JSON.parse(PetObj),
        dataType: 'JSON',
        type: 'POST',
        success: function (result) {
            $("#btnsv").val(result).hide();
            $("#btnupdt").val(result).show();
            $('#btnSave').val = "UPDATE";
            $('#id').val(result.itemsubgroupcode);
            $('#ddlcategory').val(result.itemGroupcode);
            $('#txtsubcate').val(result.itemsubgroup);
            if ((status) == "1") {
                $("#chkStatus").prop("checked", true);
            }
            else {
                $('#chkStatus').prop('checked', false);
            }
        },
        error: function () {
            alert("Data Not Found !!");
        }
    });
}

function DeleteItemSubGroup(itemsubgroupcode) {
    var checkstr = confirm('Are You Sure You Want To Delete This?');
    var PetObj = JSON.stringify({ itemsubgroupcode: itemsubgroupcode });
    if (checkstr == true) {
        $.ajax({
            url: '/Admin/clubmaster/DeleteItemSubGroup',
            data: JSON.parse(PetObj),
            dataType: 'JSON',
            type: 'POST',
            success: function (result) {
                if (result.msg == "Delete Successfull!!") {
                    alert("Delete Success");
                    ShowItemSubGroup();
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