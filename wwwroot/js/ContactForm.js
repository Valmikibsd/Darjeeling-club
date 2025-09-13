


function validatecontactform() {
    var msg = "";
    if ($('#txtmsg').val() == "") { msg = " Please Write Something Here... !! \n"; }
    if ($('#txtemail').val() == "") { msg = " Please Enter Your Email !! \n"; }
    if ($('#txtmobileno').val() == "") { msg = " Please Enter Your Mobile No !! \n"; }
    if ($('#txtfullname').val() == "") { msg = " Please Enter Your Full Name !! \n"; }
    return msg;
}

function Clear() {
    $('#id').val("0");
    $('#txtfullname').val("");
    $('#txtmobileno').val("");
    $('#txtemail').val("");
    $('#txtmsg').val("");
}

function IUContactForm() {
    var msg = validatecontactform();
    if (msg == "") {
        $.ajax({
            type: "POST",
            url: '/Admin/clubmaster/IUContactForm',
            data: { id: $("#id").val(), FullName: $("#txtfullname").val(), MobileNo: $("#txtmobileno").val(), Email: $("#txtemail").val(), Msg: $("#txtmsg").val() },
            dataType: "JSON",
            success: function (result) {
                if (result.message == "Contact Form added.") {
                    alert("Contact Form Added successfully.");
                }
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