
var filenameorginal = "";
$(document).ready(function () {
    $("#imgfile").change(function () {
        filenameorginal = $('#imgfile').val();
    })
})

function validateVenue() {
    var msg = "";
    if ($('#ddlpaymode').val() == 0) { msg = "Select Payment Mode!!\n"; }
    if ($('#txtdate').val() == "") { msg = "Enter Booking Date !!\n"; }
    if ($('#ddlvenuemember').val() == 0) { msg = "Select Member Name!!\n"; }
    if ($('#ddlvenuetypeid').val() == 0) { msg = "Select Venue Type !!\n"; }
    if ($('#ddlvenueid').val() == 0) { msg = "Select Venue Name!!\n"; }
    return msg;
}

$("#ddlpaymode").change(function () {
    if ($("#ddlpaymode").val() == 1) {
        $.ajax({
            url: '/Admin/clubmaster/RefenrenceBind',
            type: "GET",
            data: { id: $("#ddlpaymode").val() },
            success: function (data) {
                console.log(data);
                $("#chkdate").val(data).hide();
                $("#chkno").val(data).hide();
                $("#rfrnceno").val(data).show();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    else if ($("#ddlpaymode").val() == 3) {
        $.ajax({
            url: '/Admin/clubmaster/CheckDateBind',
            type: "GET",
            data: { id: $("#ddlpaymode").val() },
            success: function (data) {
                console.log(data);
                $("#rfrnceno").val(data).hide();
                $("#chkdate").val(data).show();
                $("#chkno").val(data).show();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    else if ($("#ddlpaymode").val() == 3) {
        $.ajax({
            url: '/Admin/clubmaster/CheckNoBind',
            type: "GET",
            data: { id: $("#ddlpaymode").val() },
            success: function (data) {
                console.log(data);
                $("#chkno").val(data).show();
                $("#rfrnceno").val(data).hide();
                $("#chkdate").val(data).show();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
    else if ($("#ddlpaymode").val() == 2, 4, 5) {
        $.ajax({
            url: '/Admin/clubmaster/CheckDateBind',
            type: "GET",
            data: { id: $("#ddlpaymode").val() },
            success: function (data) {
                console.log(data);
                $("#rfrnceno").val(data).hide();
                $("#chkdate").val(data).hide();
                $("#chkno").val(data).hide();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
})

$("#ddlvenueid").change(function () {
    $.ajax({
        type: "GET",
        url: '/Admin/clubmaster/RentBind',
        data: { id: $("#ddlvenueid").val() },
        success: function (data) {
            console.log(data);
            $("#txtrental").val(data);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
})

$("#ddlvenuemember").change(function () {
    $('#txtmemid').val($("#ddlvenuemember").val());
})


function input() {
   $('#ddlvenuemember').val($("#txtmemid").val());
}


function Clear() {
    $('#id').val("0");
    $('#txtdate').val("");
    $('#ddlvenueid').val("0");
    $('#ddlvenuetypeid').val("0");
    $('#ddlvenuemember').val("0");
    $('#ddlpaymode').val("0");
    $('#txtrental').val("");
}


function Bindvenuetypeid() {
    $.ajax({
        type: "GET",
        url: '/Admin/clubmaster/TranVenuetypeBind',
        data: { id: $("#ddlvenueid").val() },
        async: false,
        success: function (json, result) {
            $("#ddlvenuetypeid").empty();
            json = json || {};
            $("#ddlvenuetypeid").append('<option value="0">Select Venue Type</option>');
            for (var i = 0; i < json.length; i++) {
                $("#ddlvenuetypeid").append('<option value="' + json[i].value + '">' + json[i].text + '</option>');
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function IUTranVenue() {
    var id = $('#id').val();
    var venueid = $('#ddlvenueid').val();
    var venuetypeid = $('#ddlvenuetypeid').val();
    var memberid = $('#ddlvenuemember').val();
    var VENUE_DATE = $('#txtdate').val();
    var PayMode = $('#ddlpaymode').val();
    var cgst = $('#txtcgst').val();
    var sgst = $('#txtsgst').val();
    var AC_Charge = $('#txtaccharge').val();
    var Maintenance_CHGS = $('#txtmaintenance').val();
    var SecurityCharge = $('#txtsecuritycharge').val();
    var rental = $('#txtrental').val();
    var curr_date = $('#txtcurrdate').val();
    var caldate = $('#txtcaldate').val();
    var partyno = $('#txtpartyno').val();
    var Totalvenueamt = $('#txttotalvenueamt').val();
    var totalvenuegst = $('#txttotalvenuegst').val();
    var RefrenceNo = $('#txtreference').val();
    var CheckDate = $('#txtcheckdate').val();
    var CheckNo = $('#txtcheckno').val();
    var orderid = $('#txtorderid').val();
    var flg = $('#txtflg').val();
    var CANCELFLAG = $('#txtcancelflg').val();
    var ButtonValue = $('#btnsave').val();
    var fdata = new FormData();
    fdata.append("id", id);
    fdata.append("venueid", venueid);
    fdata.append("venuetypeid", venuetypeid);
    fdata.append("memberid", memberid);
    fdata.append("VENUE_DATE", VENUE_DATE);
    fdata.append("PayMode", PayMode);
    fdata.append("cgst", cgst);
    fdata.append("sgst", sgst);
    fdata.append("AC_Charge", AC_Charge);
    fdata.append("Maintenance_CHGS", Maintenance_CHGS);
    fdata.append("SecurityCharge", SecurityCharge);
    fdata.append("rental", rental);
    fdata.append("curr_date", curr_date);
    fdata.append("caldate", caldate);
    fdata.append("partyno", partyno);
    fdata.append("Totalvenueamt", Totalvenueamt);
    fdata.append("totalvenuegst", totalvenuegst);
    fdata.append("RefrenceNo", RefrenceNo);
    fdata.append("CheckDate", CheckDate);
    fdata.append("CheckNo", CheckNo);
    fdata.append("orderid", orderid);
    fdata.append("flg", flg);
    fdata.append("CANCELFLAG", CANCELFLAG);
    var msg = validateVenue();
    if (msg == "") {
        $.ajax({
            type: "POST",
            url: '/Admin/clubmaster/IUTranVenue',
            dataType: "JSON",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: fdata,
            contentType: false,
            processData: false,
            success: function (result) {
                if (result.message == "This Booking is already exit") {
                    alert(" This Booking is already exit");
                }
                else if (result.message == "Booking added") {
                    alert(" Booking Added successfully.");
                }
                else if (result.message == "Booking not added") {
                    alert(" Booking Added not successfully.");
                }
                else if (result.message == "Booking update") {
                    alert(" Booking update successfully.");
                }
                else if (result.message == "Booking not update") {
                    alert(" Booking update not successfully.");
                }
                else if (result.message == "Please enter valid date for booking") {
                    alert(" Please enter valid date for booking");
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

function IUUpdateBooking() {
    var id = $('#id').val();
    var venueid = $('#ddlvenueid').val();
    var venuetypeid = $('#ddlvenuetypeid').val();
    var memberid = $('#ddlvenuemember').val();
    var VENUE_DATE = $('#txtdate').val();
    var PayMode = $('#ddlpaymode').val();
    var cgst = $('#txtcgst').val();
    var sgst = $('#txtsgst').val();
    var AC_Charge = $('#txtaccharge').val();
    var Maintenance_CHGS = $('#txtmaintenance').val();
    var SecurityCharge = $('#txtsecuritycharge').val();
    var rental = $('#txtrental').val();
    var curr_date = $('#txtcurrdate').val();
    var caldate = $('#txtcaldate').val();
    var partyno = $('#txtpartyno').val();
    var Totalvenueamt = $('#txttotalvenueamt').val();
    var totalvenuegst = $('#txttotalvenuegst').val();
    var RefrenceNo = $('#txtreference').val();
    var CheckDate = $('#txtcheckdate').val();
    var CheckNo = $('#txtcheckno').val();
    var orderid = $('#txtorderid').val();
    var flg = $('#txtflg').val();
    var CANCELFLAG = $('#txtcancelflg').val();
    var ButtonValue = $('#btnsave').val();
    var fdata = new FormData();
    fdata.append("id", id);
    fdata.append("venueid", venueid);
    fdata.append("venuetypeid", venuetypeid);
    fdata.append("memberid", memberid);
    fdata.append("VENUE_DATE", VENUE_DATE);
    fdata.append("PayMode", PayMode);
    fdata.append("cgst", cgst);
    fdata.append("sgst", sgst);
    fdata.append("AC_Charge", AC_Charge);
    fdata.append("Maintenance_CHGS", Maintenance_CHGS);
    fdata.append("SecurityCharge", SecurityCharge);
    fdata.append("rental", rental);
    fdata.append("curr_date", curr_date);
    fdata.append("caldate", caldate);
    fdata.append("partyno", partyno);
    fdata.append("Totalvenueamt", Totalvenueamt);
    fdata.append("totalvenuegst", totalvenuegst);
    fdata.append("RefrenceNo", RefrenceNo);
    fdata.append("CheckDate", CheckDate);
    fdata.append("CheckNo", CheckNo);
    fdata.append("orderid", orderid);
    fdata.append("flg", flg);
    fdata.append("CANCELFLAG", CANCELFLAG);
    $.ajax({
        type: "POST",
        url: '/Admin/clubmaster/IUTranVenue',
        dataType: "JSON",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: fdata,
        contentType: false,
        processData: false,
        success: function (result) {
            $("#btnsav").val(result).show();
            $("#btnupdt").val(result).hide();
            if (result.message == "This Booking is already exit") {
                alert(" This Booking is already exit");
            }
            else if (result.message == "Booking added") {
                alert(" Booking Added successfully.");
            }
            else if (result.message == "Booking not added") {
                alert(" Booking Added not successfully.");
            }
            else if (result.message == "Booking update") {
                alert(" Booking update successfully.");
            }
            else if (result.message == "Booking not update") {
                alert(" Booking update not successfully.");
            }
            ShowDataInTable();
            Clear();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function IUCancelBooking() {
    var id = $('#id').val();
    var venueid = $('#ddlvenueid').val();
    var venuetypeid = $('#ddlvenuetypeid').val();
    var memberid = $('#ddlvenuemember').val();
    var VENUE_DATE = $('#txtdate').val();
    var PayMode = $('#ddlpaymode').val();
    var cgst = $('#txtcgst').val();
    var sgst = $('#txtsgst').val();
    var AC_Charge = $('#txtaccharge').val();
    var Maintenance_CHGS = $('#txtmaintenance').val();
    var SecurityCharge = $('#txtsecuritycharge').val();
    var rental = $('#txtrental').val();
    var curr_date = $('#txtcurrdate').val();
    var caldate = $('#txtcaldate').val();
    var partyno = $('#txtpartyno').val();
    var Totalvenueamt = $('#txttotalvenueamt').val();
    var totalvenuegst = $('#txttotalvenuegst').val();
    var RefrenceNo = $('#txtreference').val();
    var CheckDate = $('#txtcheckdate').val();
    var CheckNo = $('#txtcheckno').val();
    var orderid = $('#txtorderid').val();
    var flg = $('#txtflg').val();
    var CANCELFLAG = $('#txtcancelflg').val();
    var ButtonValue = $('#btnsave').val();
    var fdata = new FormData();
    fdata.append("ButtonValue", ButtonValue);
    fdata.append("id", id);
    fdata.append("venueid", venueid);
    fdata.append("venuetypeid", venuetypeid);
    fdata.append("memberid", memberid);
    fdata.append("VENUE_DATE", VENUE_DATE);
    fdata.append("PayMode", PayMode);
    fdata.append("cgst", cgst);
    fdata.append("sgst", sgst);
    fdata.append("AC_Charge", AC_Charge);
    fdata.append("Maintenance_CHGS", Maintenance_CHGS);
    fdata.append("SecurityCharge", SecurityCharge);
    fdata.append("rental", rental);
    fdata.append("curr_date", curr_date);
    fdata.append("caldate", caldate);
    fdata.append("partyno", partyno);
    fdata.append("Totalvenueamt", Totalvenueamt);
    fdata.append("totalvenuegst", totalvenuegst);
    fdata.append("RefrenceNo", RefrenceNo);
    fdata.append("CheckDate", CheckDate);
    fdata.append("CheckNo", CheckNo);
    fdata.append("orderid", orderid);
    fdata.append("flg", flg);
    fdata.append("CANCELFLAG", CANCELFLAG);
    $.ajax({
        type: "POST",
        url: '/Admin/clubmaster/IUCancelBooking',
        dataType: "JSON",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: fdata,
        contentType: false,
        processData: false,
        success: function (result) {
            $("#btnsav").val(result).show();
            $("#btncncl").val(result).hide();
            if (result.message == "Booking cancelled") {
                alert(" Booking cancelled successfully.");
            }
            ShowDataInTable();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function ShowDataInTable() {
    $.ajax({
        url: '/Admin/clubmaster/ShowTranVenue',
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
                html += '<td style="display:none">' + item.venueid + '</td>';
                html += '<td style="display:none">' + item.venuetypeid + '</td>';
                html += '<td style="display:none">' + item.memberid + '</td>';
                html += '<td style="display:none">' + item.cgst + '</td>';
                html += '<td style="display:none">' + item.sgst + '</td>';
                html += '<td style="display:none">' + item.payMode + '</td>';
                html += '<td style="display:none">' + item.aC_Charge + '</td>';
                html += '<td style="display:none">' + item.maintenance_CHGS + '</td>';
                html += '<td style="display:none">' + item.rental + '</td>';
                html += '<td style="display:none">' + item.curr_date + '</td>';
                html += '<td style="display:none">' + item.caldate + '</td>';
                html += '<td style="display:none">' + item.partyno + '</td>';
                html += '<td style="display:none">' + item.securityCharge + '</td>';
                html += '<td style="display:none">' + item.totalvenueamt + '</td>';
                html += '<td style="display:none">' + item.totalvenuegst + '</td>';
                html += '<td style="display:none">' + item.refrenceNo + '</td>';
                html += '<td style="display:none">' + item.checkDate + '</td>';
                html += '<td style="display:none">' + item.checkNo + '</td>';
                html += '<td style="display:none">' + item.orderid + '</td>';
                html += '<td style="display:none">' + item.flg + '</td>';
                html += '<td style="display:none">' + item.cANCELFLAG + '</td>';
                html += '<td>' + item.venueName + '</td>';
                html += '<td>' + item.name + '</td>';
                html += '<td>' + item.memberName + '</td>';
                html += '<td>' + item.venuE_DATE + '</td>';
                html += '<td>' + item.payname + '</td>';
                html += '<td>' + item.status + '</td>';
                html += '<td><a class="btn btn-sm" href="#" onclick="return Editbyid(' + item.id + ' , ' + item.id + ')"><i class="fa fa-edit"></i></a></td>';
                html += '<td><a class="btn btn-sm" href="#" onclick="return DeletebyId(' + item.id + ')"><i class="fa fa-trash-o"></i></a></td>';
                html += '<td><a class="btn btn-sm" href="#" onclick="return Cancelbyid(' + item.id + ' , ' + item.id + ')"><i class="fa fa-trash-o"></i></a></td>';
                html += '</tr>';
                index++;
            });
            $('.SubCategorybody').html(html);
        }
    });
}

function DeletebyId(id) {
    var checkstr = confirm('Are you sure you want to delete ?');
    var PetObj = JSON.stringify({ id: id });
    if (checkstr == true) {
        $.ajax({
            url: '/Admin/clubmaster/DeleteTranVenue',
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

function Editbyid(id) {
    var PetObj = JSON.stringify({ id: id });
    $.ajax({
        url: '/Admin/clubmaster/EditTranVenue',
        data: JSON.parse(PetObj),
        dataType: 'JSON',
        type: 'POST',
        success: function (result) {
            console.log(result);
            $("#btnsav").val(result).hide();
            $("#btncncl").val(result).hide();
            $("#btnupdt").val(result).show();
            $('#btnsave').html("UPDATE");
            $('#id').val(result.id);
            $('#ddlvenueid').val(result.venueid);
            Bindvenuetypeid();
            $('#ddlvenuetypeid').val(result.venuetypeid);
            $('#ddlvenuemember').val(result.memberid);
            $('#txtdate').val(result.venuE_DATE);
            $('#txtcgst').val(result.cgst);
            $('#txtsgst').val(result.sgst);
            $('#ddlpaymode').val(result.payMode);
            $('#txtaccharge').val(result.aC_Charge);
            $('#txtmaintenance').val(result.maintenance_CHGS);
            $('#txtrental').val(result.rental);
            $('#txtcurrdate').val(result.curr_date);
            $('#txtcaldate').val(result.caldate);
            $('#txtpartyno').val(result.partyno);
            $('#txtsecuritycharge').val(result.securityCharge);
            $('#txttotalvenueamt').val(result.totalvenueamt);
            $('#txttotalvenuegst').val(result.totalvenuegst);
            $('#txtreference').val(result.refrenceNo);
            $('#txtcheckdate').val(result.checkDate);
            $('#txtcheckno').val(result.checkNo);
            $('#txtorderid').val(result.orderid);
            $('#txtflg').val(result.flg);
            $('#txtcancelflg').val(result.cANCELFLAG);
            $('#txtmemid').val(result.memberid);
        },
        error: function () {
            alert(errormessage.responseText);
        }
    });
}

function Cancelbyid(id) {
    var PetObj = JSON.stringify({ id: id });
    var checkstr = confirm('Are you sure you want to cancel this Booking ?');
    if (checkstr == true) {
        $.ajax({
            url: '/Admin/clubmaster/EditTranVenue',
            data: JSON.parse(PetObj),
            dataType: 'JSON',
            type: 'POST',
            success: function (result) {
                console.log(result);
                $("#btncncl").val(result).show();
                $("#btnsav").val(result).hide();
                $("#btnupdt").val(result).hide();
                $('#btnsave').html("CANCEL");
                $('#id').val(result.id);
                $('#ddlvenueid').val(result.venueid);
                Bindvenuetypeid();
                $('#ddlvenuetypeid').val(result.venuetypeid);
                $('#ddlvenuemember').val(result.memberid);
                $('#txtdate').val(result.venuE_DATE);
                $('#txtcgst').val(result.cgst);
                $('#txtsgst').val(result.sgst);
                $('#ddlpaymode').val(result.pay_status);
                $('#txtaccharge').val(result.aC_Charge);
                $('#txtmaintenance').val(result.maintenance_CHGS);
                $('#txtrental').val(result.rental);
                $('#txtcurrdate').val(result.curr_date);
                $('#txtcaldate').val(result.caldate);
                $('#txtpartyno').val(result.partyno);
                $('#txtsecuritycharge').val(result.securityCharge);
                $('#txttotalvenueamt').val(result.totalvenueamt);
                $('#txttotalvenuegst').val(result.totalvenuegst);
                $('#txtreference').val(result.refrenceNo);
                $('#txtcheckdate').val(result.checkDate);
                $('#txtcheckno').val(result.checkNo);
                $('#txtorderid').val(result.orderid);
                $('#txtflg').val(result.flg);
                $('#txtcancelflg').val(result.cANCELFLAG);
            },
            error: function () {
                alert(errormessage.responseText);
            }
        });
    }
    else {
        return false;
    }
}
