var filenameorginal = "";
$(document).ready(function () {
    //BindCategory();
    $("#imgfile").change(function () {
        filenameorginal = $('#imgfile').val();
    })
});

function BindCategory() {
    $.ajax({
        url: '/Admin/Admin/BindCategory',
        dataType: 'JSON',
        type: 'GET',
        success: function (data) {
            console.log(data);
            $.each(data, function (index, item) {
                $('#ddlCategory').append(
                    '<option value="' + item.cId + '">' + item.categoryName + '</option>'
                );
            });
        }
    });
}

function Clear() {
    $('#txtSubject').val("");
    $('#ddlcatecory').val("0");
    $("#imgfile").val("");
    $("#Description").val("");
    $("#chkStatus").Clear();
    CKEDITOR.instances["Description"].setData("");
}



function getmemberdata() {
    $.ajax({
        type: "GET",
        url: '/Admin/clubmaster/getcomMember',
        data: { id: $("#ddlcatecory").val() },
        async: false,
        success: function (json, result) {
         
            json = json || {};
           
            //var row = "";
            //for (var i = 0; i < json.length; i++) {
            //    // $("#ddlmember").append('<option value="' + json[i].value + '">' + json[i].text + '</option>');
            //    row += `<tr><td style='display:block;'>${json[i].id}</td><td>${json[i].name}</td><td><input type='checkbox' class='chkbx'/></td></tr>`

            //}
            CreateTableFromArray(JSON.parse(json), 'printdiv');
           // $(".tblmember").html(row);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function validateCommunication() {
    var msg = "";
    if ($('#txtSubject').val() == "") { msg = "Please Enter Subject !!\n"; }
    if ($('#ddlcatecory').val() == "") { msg = "Select Category !!\n"; }
    if ($('#imgfile').val() == "") { msg = "Select file !!\n"; }
    if ($('#Description').val() == "") { msg = "Please Enter Description !!\n"; }

    alert(msg);
}

function InsertCommunication() {
    //var DESCRIPTION = CKEDITOR.instances["Description"].getData();
    var fileExtension = ['png', 'img', 'jpg', 'jpeg', 'PNG', 'IMG', 'JPG', 'JPEG',];
    var fdata = new FormData();
    var subject = $('#txtSubject').val();
    var comid = $("#hidcomid").val();
    var Category = $('#ddlcatecory').val();
    var fileUpload = $('#imgfile').val();
    var DESCRIPTION = CKEDITOR.instances["Description"].getData();
    var isActive = $('#chkStatus').is(':checked') ? 1 : 0;

    jsonObj = [];

    $("#data-table tbody tr").each(function () {
        var row = $(this);
        var item = {}
        var check = parseInt(row.find('input.chkbx').is(':checked') ? 1 : 0);
        if (check == 1) {
            item.memid = row.find('.Memid').html();
            item.memname = row.find('.Name').html();
            item.contactno = row.find('.Hid_contact').html();
            item.email = row.find('.Hid_Email').html();
            item.cat = Category;
            item.subject = subject;
            item.comid = comid;
            item.DESCRIPTION = DESCRIPTION;
            item.status = isActive;
            //item.cat = $("#ddlype").val();
            jsonObj.push(item);
        }

    })

    fdata.append("data", JSON.stringify(jsonObj));
    //fdata.append("Category", Category);
    //fdata.append("comid", comid);
    //fdata.append("isActive", isActive);
    fdata.append("filenmes", filenameorginal);
    //fdata.append("description", DESCRIPTION);


    var filename = $('#imgfile').val();
    if (filenameorginal == filename) {
        //if (filename.length == 0) {
        //    alert("Please select a file.");
        //    return false;
        //}
        //else {
        if (filename.length != 0) {
            var extension = filename.replace(/^.*\./, '');
            if ($.inArray(extension, fileExtension) == -1) {
                alert("Please select only PNG/IMG/JPG files.");
                return false;
            }
            fdata.append("flg", "okg");
            var fileUpload = $("#imgfile").get(0);
            var files = fileUpload.files;
            fdata.append(files[0].name, files[0]);
        } else {
            // }
            fdata.append("flg", "ok");
        }
    }
    else {
        fdata.append("flg", "ok");

    }

    $.ajax({
        type: "POST",
        url: '/Admin/clubmaster/Savecomm',
        dataType: "JSON",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: fdata,
        contentType: false,
        processData: false,
        success: function (data) {
            console.log(data);
            alert(data);

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

function ChesdfdsgckUncheckHeader() {
    //Determine the reference CheckBox in Header row.
    var chkAll = document.getElementById("chkAll");

    //By default set to Checked.
    chkAll.checked = true;

    //Fetch all rows of the Table.
    var rows = document.getElementById("data-table").rows;

    //Execute loop on all rows excluding the Header row.
    for (var i = 1; i < rows.length; i++) {
        if (!rows[i].getElementsByTagName("INPUT")[0].checked) {
            chkAll.checked = false;
            break;
        }
    }
};

function ShowCommunication() {
    $.ajax({
        url: '/Admin/clubmaster/ShowCommunication',
        dataType: 'JSON',
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            console.log(data);

        }
    });
}



