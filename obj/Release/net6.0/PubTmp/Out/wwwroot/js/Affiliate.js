
//var filenameorginal = "";
//$(document).ready(function () {
//    $("#imgfile").change(function () {
//        filenameorginal = $('#imgfile').val();  
//    })
//})

//filetype.onchange = evt => {
//    const [file] = filetype.files
//    if (file) {
//        img.src = URL.createObjectURL(file)
//    }
//}
function Clear() {
    $('#id').val("");
    $('#txtclubname').val("");
    $('#txtaddress').val("");
    $('#txtemail').val("");
    $('#txtcontact').val("");
    $("#chkStatus").prop("checked", true);
}

function validateAffiliate() {
    var msg = "";
    if ($('#txtcontact').val() == "") { msg = "Please Enter Your Contact !!\n"; }
    if ($('#txtemail').val() == "") { msg = "Plese Enter Your Email !!\n"; }
    if ($('#txtaddress').val() == "") { msg = "Please Enter Your Address !!\n"; }
    if ($('#txtclubname').val() == "") { msg = "Please Enter Your Club Name !!\n"; }
    return msg;
}

function IUAffiliate() {
    var ID = $('#id').val();
   var ClubName = $('#txtclubname').val();
    var Address = $('#txtaddress').val();
    var Email = $('#txtemail').val();
    var Contact = $('#txtcontact').val();
    //var Item_Image = $('#txtproduct').val();
    var Status = $('#chkStatus').is(':checked') ? 1 : 0;
    var ButtonValue = $('#btnsave').val();
    var fdata = new FormData();
    fdata.append("id", ID);
    fdata.append("clubName", ClubName);
    fdata.append("address", Address);
    fdata.append("email", Email);
    fdata.append("contact", Contact);
    fdata.append("status", Status);
    /*fdata.append("filenmes", filenameorginal);*/
    var msg = validateAffiliate();
    if (msg == "") {
        $.ajax({
            type: "POST",
            url: '/Admin/clubmaster/IUAffiliate',
            dataType: "JSON",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: fdata,
            contentType: false,
            processData: false,
            success: function (result) {
                console.log(result);
                if (result.message == "This Affiliate is already exit") {
                    alert(" Affiliate is already exit.");
                }
                else if (result.message == "Affiliate added.") {
                    alert(" Affiliate Added successfully.");
                }
                else if (result.message == "Affiliate not added.") {
                    alert(" Affiliate Added not successfully.");
                }
                else if (result.message == "Affiliate updated.") {
                    alert(" Affiliate update successfully.");
                }
                else if (result.message == "Affiliate not updated.") {
                    alert(" Affiliate update not successfully.");
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

function IUUpdateAffiliate() {
    var ID = $('#id').val();
    var ClubName = $('#txtclubname').val();
    var Address = $('#txtaddress').val();
    var Email = $('#txtemail').val();
    var Contact = $('#txtcontact').val();
    //var Item_Image = $('#txtproduct').val();
    var Status = $('#chkStatus').is(':checked') ? 1 : 0;
    var ButtonValue = $('#btnsave').val();
    var fdata = new FormData();
    fdata.append("id", ID);
    fdata.append("clubName", ClubName);
    fdata.append("address", Address);
    fdata.append("email", Email);
    fdata.append("contact", Contact);
    fdata.append("status", Status);
    /*fdata.append("filenmes", filenameorginal);*/
    var msg = validateAffiliate();
    if (msg == "") {
        $.ajax({
            type: "POST",
            url: '/Admin/clubmaster/IUAffiliate',
            dataType: "JSON",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: fdata,
            contentType: false,
            processData: false,
            success: function (result) {
                console.log(result);
                $("#btnsv").val(result).show();
                $("#btnupdt").val(result).hide();
                if (result.message == "This Affiliate is already exit") {
                    alert(" Affiliate is already exit.");
                }
                else if (result.message == "Affiliate added.") {
                    alert(" Affiliate Added successfully.");
                }
                else if (result.message == "Affiliate not added.") {
                    alert(" Affiliate Added not successfully.");
                }
                else if (result.message == "Affiliate updated.") {
                    alert(" Affiliate update successfully.");
                }
                else if (result.message == "Affiliate not updated.") {
                    alert(" Affiliate update not successfully.");
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
        url: '/Admin/clubmaster/ShowAffiliate',
        dataType: 'JSON',
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
           console.log(data);
            var html = '';
            var index = 1;
            $.each(data, function (key, item) {
                html += '<tr class="hover-primary">';
                // html += '<td>' + index + '</td>';
                html += '<td style="display:none">' + item.id + '</td>';
                // html += '<td style="display:none">' + item.itemGroupcode + '</td>';
                html += '<td>' + item.clubName + '</td>';
                html += '<td>' + item.address + '</td>';
                html += '<td>' + item.email + '</td>';
                html += '<td>' + item.contact + '</td>';
                if (item.status == "1") {
                    html += '<td class="active">Active</td>';
                }
                else {
                    html += '<td class="active">InActive</td>';
                }
                html += '<td><a class="btn btn-sm" href="#" onclick="return Editbyid(' + item.id + ' , ' + item.status + ')"><i class="fa fa-edit"></i></a></td>';
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
    var PetObj = JSON.stringify({ ID : ID });
    if (checkstr == true) {
        $.ajax({
            url: '/Admin/clubmaster/DeleteAffiliate',
            data: JSON.parse(PetObj),
            dataType: 'JSON',
            type: 'POST',
            success: function (result) {

                if (result.msg == "Affiliate Delete Successfull!!") {
                    alert("Affiliate Delete Success");
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

function Editbyid(ID,status) {
    var PetObj = JSON.stringify({ ID: ID });

    //BindSubcategory(itemGroupcode);
    $.ajax({
        url: '/Admin/clubmaster/EditAffiliate',
        data: JSON.parse(PetObj),
        dataType: 'JSON',
        type: 'POST',
        success: function (result) {
            console.log(result);
            $("#btnsv").val(result).hide();
            $("#btnupdt").val(result).show();
            $('#btnsave').html("UPDATE");
            $('#id').val(result.id);
            $('#txtclubname').val(result.clubName);
            $('#txtaddress').val(result.address);
            $('#txtemail').val(result.email);
            $('#txtcontact').val(result.contact);
            if ((status) == "1") {
                $("#chkStatus").prop("checked", true);
            }
            else {
                $('#chkStatus').prop('checked', false);
            }
           
        },
        error: function () {
            alert(errormessage.responseText);
        }
    });
}


