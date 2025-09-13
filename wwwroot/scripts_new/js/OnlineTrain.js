

$('#CompanyName').on('keypress', function () {
    if ($('#CompanyName').val().trim() == "") {
        $("#CompanyNameerror").text("Please Enter Company Name").show();
        isValid = false;
    }
    else {
        $("#CompanyNameerror").text("Please Enter Company Name").hide();
    }
});
$('#ContactPersonName').on('keypress', function () {
    if ($('#ContactPersonName').val().trim() == "") {
        $("#ContactPersonNameerror").text("Please Enter Contact Person Name").show();
        isValid = false;
    }
    else {
        $("#ContactPersonNameerror").text("Please Enter Contact Person Name").hide();
    }
});
$('#Designation').on('keypress', function () {
    if ($('#Designation').val().trim() == "") {
        $("#Designationerror").text("Please Enter Designation Name").show();
        isValid = false;
    }
    else {
        $("#Designationerror").text("Please Enter Designation Name").hide();
    }
});
$('#Email').on('keypress', function () {
    if ($('#Email').val().trim() == "") {
        $("#txtEmailerror").text("Please Enter Email..").show();
        isValid = false;
    }
    else {
        var inputVal = $('#Email').val();
        var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
        if (!emailReg.test(inputVal)) {
            $("#txtEmailerror").text("Please Enter Valid Email").show();
        }
        else {
            $("#txtEmailerror").text("Please Enter Valid Email").hide();
        }
    }
});

$('#Email').keydown(function () {
    var inputVal = $('#Email').val().trim();
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    if (!emailReg.test(inputVal)) {
        $("#txtEmailerror").text("Please Enter Valid Email").show();
    }
    else {
        $("#txtEmailerror").text("Please Enter Valid Email").hide();
    }
});


$("#ContactNumber").keypress(function (e) {
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        $("#ContactNumbererror").text("Please Enter Only Number").show();
        return false;
    }
    else
    {
        $("#ContactNumbererror").text("Please Enter Only Number").hide();
    }
});