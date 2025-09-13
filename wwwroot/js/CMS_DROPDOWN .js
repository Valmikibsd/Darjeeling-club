
function clear() {
    $('#id').val("0");
    $('#ddlcmsdropdown').val("0");
    CKEDITOR.instances["Description"].setData("");
}

function IUCMS_DROPDOWN() {
    var EventId = $('#id').val();
    var id = $('#ddlcmsdropdown').val();
    //var EventName = $('#Description').val();
    var EventName = CKEDITOR.instances["Description"].getData();

    $.ajax({
        type: "POST",
        url: '/Admin/clubmaster/IUCMS_DROPDOWN',
        data: { EventId: EventId, id: id, EventName: EventName },
        dataType: "JSON",
        success: function (result) {
            if (result.message == "This Event Name is already exit") {
                alert("This Event Name is already exit");
            }
            else if (result.message == "Event Name added") {
                alert("Event Name Added successfully.");
            }
            else if (result.message == "Event Name not added") {
                alert("Event Name modified successfully.");
            }
            ShowDataInTable();
            clear();

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

}

function IUpdateCMS_DROPDOWN() {
    var EventId = $('#id').val();
    //var EventName = $('#Description').val();
    var id = $('#ddlcmsdropdown').val();
    var EventName = CKEDITOR.instances["Description"].getData();
    $.ajax({
        type: "POST",
        url: '/Admin/clubmaster/IUCMS_DROPDOWN',
        data: { EventId: EventId, id: id, EventName: EventName },
        dataType: "JSON",
        success: function (result) {
            $("#btnsv").val(result).show();
            $("#btnupdt").val(result).hide();
            if (result.message == "This Event Name is already exit") {
                alert("This Event Name is already exit");
            }
            else if (result.message == "Event Name added") {
                alert("Event Name Added successfully.");
            }
            else if (result.message == "Event Name update") {
                alert("Event Name modified successfully.");
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
        url: '/Admin/clubmaster/ShowCMS_DROPDOWN',
        dataType: 'JSON',
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        data: { EventId: $("#ddlcmsdropdown").val() },
        success: function (data) {
            console.log(data);
            var html = '';
            var index = 1;
            $.each(data, function (key, item) {
                html += '<tr class="hover-primary">';
                html += '<td style="display:none">' + item.eventId + '</td>';
                html += '<td>' + item.name + '</td>';
                html += '<td>' + item.eventName + '</td>';
                html += '<td><a class="btn btn-sm" href="#" onclick="return Editbyid(' + item.eventId + ')"><i class="fa fa-edit"></i></a></td>';
                html += '<td><a class="btn btn-sm" href="#" onclick="return DeletebyId(' + item.eventId + ')"><i class="fa fa-trash-o"></i></a></td>';
                html += '</tr>';
                index++;
            });
            $('.Categorybody').html(html);
        }
    });
}

function Editbyid(EventId) {
    var PetObj = JSON.stringify({ EventId: EventId });
    $.ajax({
        url: '/Admin/clubmaster/EditCMS_DROPDOWN',
        data: JSON.parse(PetObj),
        dataType: 'JSON',
        type: 'POST',
        success: function (result) {
            console.log(result);
            $("#btnsv").val(result).hide();
            $("#btnupdt").val(result).show();
            $('#btnSave').val = "UPDATE";
            $('#id').val(result.eventId);
            $('#ddlcmsdropdown').val(result.eventId);
            //$('#Description').val(result.eventName);
            CKEDITOR.instances["Description"].setData(result.eventName);
        },
        error: function () {
            alert("Data Not Found !!");
        }
    });
}

function DeletebyId(EventId) {
    var checkstr = confirm('Are You Sure You Want To Delete This?');
    var PetObj = JSON.stringify({ EventId: EventId });
    if (checkstr == true) {
        $.ajax({
            url: '/Admin/clubmaster/DeleteCMS_DROPDOWN',
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

