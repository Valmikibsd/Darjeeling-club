$(document).ready(function () {
    //highlight selected field

    $('td').live('mouseover', function () {

        if ($(this).css("background-color") == "rgb(255, 255, 255)") {            
            $(this).css("background-color", "#FFDFCC");
        }
    });

    $('td').live('mouseleave', function () {

        if ($(this).css("background-color") == "rgb(255, 223, 204)") {
            $(this).css("background-color", "rgb(255, 255, 255)");
        }
    });

   
    $(".month").live('click', function () {
        $('.loader_cont').show();
        var object = $(this).attr("id");
        var str = object.split('/');
        // str[0] contains "month"
        // str[1] contains "year"
        var venid = $("#venuid").val();

        $.ajax
            ({
                url: '/Common/AsyncUpdateCalender',
                type: 'GET',
                traditional: true,
                contentType: 'application/json',
                data: { month: str[0], year: str[1], vanuid: venid },
                success: function (result) {
                    if (!jQuery.isEmptyObject(result)) {

                        var week1 = $("#week1");
                        week1.empty();
                        var week2 = $("#week2");
                        week2.empty();
                        var week3 = $("#week3");
                        week3.empty();
                        var week4 = $("#week4");
                        week4.empty();
                        var week5 = $("#week5");
                        week5.empty();
                        var week6 = $("#week6");
                        week6.empty();
                        var newHeader = $('<a id=' + result.prevMonth + ' class="month" style="float:left">Prev</a>' + getMonth(str[0]) + ' ' + str[1] + '<a id=' + result.nextMonth + ' class="month" style="float:right">Next</a>');
                        $("#component-header").empty();
                        $("#component-header").append(newHeader);


                        $.each(result.week1, function (i, item) {

                            var htmlStr = null;
                            if (jQuery.isEmptyObject(item)) {
                                htmlStr = $('<td></td>');
                                week1.append(htmlStr);
                            } else {
                                if (item.daycolumn == 0 || item.daycolumn == 6) {
                                    htmlStr = $('<td></td>');

                                    htmlStr.append('<SPAN STYLE="font-weight:bold;font-size:20px">' + item.dtDay + '</span>');

                                    for (var i = 0; i < item.venuecdata.length; i++) {

                                        if (item.flg.length > 0) {
                                            if (item.flg.indexOf(item.venuecdata[i].id) != -1) {

                                                htmlStr.append('<div style="color:red">' + item.venuecdata[i].venue + '</div>')
                                            }
                                            else {

                                                htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                            }

                                        }
                                        else {
                                            htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                        }

                                    }


                                }



                                else if (item._Date != getTodayDate()) {
                                    htmlStr = $('<td></td>');
                                    htmlStr.append('<SPAN STYLE="font-weight:bold;font-size:20px">' + item.dtDay + '</span>');
                                    for (var i = 0; i < item.venuecdata.length; i++) {

                                        if (item.flg.length > 0) {
                                            if (item.flg.indexOf(item.venuecdata[i].id) != -1) {

                                                htmlStr.append('<div style="color:red">' + item.venuecdata[i].venue + '</div>')
                                            }
                                            else {

                                                htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                            }

                                        }
                                        else {
                                            htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                        }

                                    }
                                } else {
                                    htmlStr = $('<td></td>');
                                    htmlStr.append('<SPAN STYLE="font-weight:bold;font-size:20px">' + item.dtDay + '</span>');
                                    for (var i = 0; i < item.venuecdata.length; i++) {

                                        if (item.flg.length > 0) {
                                            if (item.flg.indexOf(item.venuecdata[i].id) != -1) {

                                                htmlStr.append('<div style="color:red">' + item.venuecdata[i].venue + '</div>')
                                            }
                                            else {

                                                htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                            }

                                        }
                                        else {
                                            htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                        }

                                    }
                                }

                                week1.append(htmlStr);

                            }

                        });
                        $.each(result.week2, function (i, item) {
                            var htmlStr = null;
                            if (jQuery.isEmptyObject(item)) {
                                htmlStr = $('<td></td>');
                                week2.append(htmlStr);
                            } else {

                                if (item.daycolumn == 0 || item.daycolumn == 6) {
                                    htmlStr = $('<td></td>');
                                    htmlStr.append('<SPAN STYLE="font-weight:bold;font-size:20px">' + item.dtDay + '</span>');
                                    for (var i = 0; i < item.venuecdata.length; i++) {

                                        if (item.flg.length > 0) {
                                            if (item.flg.indexOf(item.venuecdata[i].id) != -1) {

                                                htmlStr.append('<div style="color:red">' + item.venuecdata[i].venue + '</div>')
                                            }
                                            else {

                                                htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                            }

                                        }
                                        else {
                                            htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                        }

                                    }
                                }
                                else if (item.Date != getTodayDate()) {
                                    htmlStr = $('<td></td>');
                                    htmlStr.append('<SPAN STYLE="font-weight:bold;font-size:20px">' + item.dtDay + '</span>');
                                    for (var i = 0; i < item.venuecdata.length; i++) {

                                        if (item.flg.length > 0) {
                                            if (item.flg.indexOf(item.venuecdata[i].id) != -1) {

                                                htmlStr.append('<div style="color:red">' + item.venuecdata[i].venue + '</div>')
                                            }
                                            else {

                                                htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                            }

                                        }
                                        else {
                                            htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                        }

                                    }
                                } else {
                                    htmlStr = $('<td></td>');
                                    htmlStr.append('<SPAN STYLE="font-weight:bold;font-size:20px">' + item.dtDay + '</span>');
                                    for (var i = 0; i < item.venuecdata.length; i++) {

                                        if (item.flg.length > 0) {
                                            if (item.flg.indexOf(item.venuecdata[i].id) != -1) {

                                                htmlStr.append('<div style="color:red">' + item.venuecdata[i].venue + '</div>')
                                            }
                                            else {

                                                htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                            }

                                        }
                                        else {
                                            htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                        }

                                    }
                                }
                                week2.append(htmlStr);
                            }
                        });
                        $.each(result.week3, function (i, item) {
                            var htmlStr = null;
                            if (jQuery.isEmptyObject(item)) {
                                htmlStr = $('<td></td>');
                                week3.append(htmlStr);
                            } else {
                                if (item.daycolumn == 0 || item.daycolumn == 6) {
                                    htmlStr = $('<td></td>');
                                    htmlStr.append('<SPAN STYLE="font-weight:bold;font-size:20px">' + item.dtDay + '</span>');
                                    for (var i = 0; i < item.venuecdata.length; i++) {

                                        if (item.flg.length > 0) {
                                            if (item.flg.indexOf(item.venuecdata[i].id) != -1) {

                                                htmlStr.append('<div style="color:red">' + item.venuecdata[i].venue + '</div>')
                                            }
                                            else {

                                                htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                            }

                                        }
                                        else {
                                            htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                        }

                                    }
                                }
                                else if (item.Date != getTodayDate()) {
                                    htmlStr = $('<td></td>');
                                    htmlStr.append('<SPAN STYLE="font-weight:bold;font-size:20px">' + item.dtDay + '</span>');
                                    for (var i = 0; i < item.venuecdata.length; i++) {

                                        if (item.flg.length > 0) {
                                            if (item.flg.indexOf(item.venuecdata[i].id) != -1) {

                                                htmlStr.append('<div style="color:red">' + item.venuecdata[i].venue + '</div>')
                                            }
                                            else {

                                                htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                            }

                                        }
                                        else {
                                            htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                        }

                                    }
                                } else {
                                    htmlStr = $('<td></td>');
                                    htmlStr.append('<SPAN STYLE="font-weight:bold;font-size:20px">' + item.dtDay + '</span>');
                                    for (var i = 0; i < item.venuecdata.length; i++) {

                                        if (item.flg.length > 0) {
                                            if (item.flg.indexOf(item.venuecdata[i].id) != -1) {

                                                htmlStr.append('<div style="color:red">' + item.venuecdata[i].venue + '</div>')
                                            }
                                            else {

                                                htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                            }

                                        }
                                        else {
                                            htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                        }

                                    }
                                }
                                week3.append(htmlStr);
                            }
                        });
                        $.each(result.week4, function (i, item) {
                            var htmlStr = null;
                            if (jQuery.isEmptyObject(item)) {
                                htmlStr = $('<td></td>');
                                week4.append(htmlStr);
                            } else {
                                if (item.daycolumn == 0 || item.daycolumn == 6) {
                                    htmlStr = $('<td></td>');
                                    htmlStr.append('<SPAN STYLE="font-weight:bold;font-size:20px">' + item.dtDay + '</span>');
                                    for (var i = 0; i < item.venuecdata.length; i++) {

                                        if (item.flg.length > 0) {
                                            if (item.flg.indexOf(item.venuecdata[i].id) != -1) {

                                                htmlStr.append('<div style="color:red">' + item.venuecdata[i].venue + '</div>')
                                            }
                                            else {

                                                htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                            }

                                        }
                                        else {
                                            htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                        }

                                    }
                                }
                                else if (item.Date != getTodayDate()) {
                                    htmlStr = $('<td></td>');
                                    htmlStr.append('<SPAN STYLE="font-weight:bold;font-size:20px">' + item.dtDay + '</span>');
                                    for (var i = 0; i < item.venuecdata.length; i++) {

                                        if (item.flg.length > 0) {
                                            if (item.flg.indexOf(item.venuecdata[i].id) != -1) {

                                                htmlStr.append('<div style="color:red">' + item.venuecdata[i].venue + '</div>')
                                            }
                                            else {

                                                htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                            }

                                        }
                                        else {
                                            htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                        }

                                    }
                                } else {
                                    htmlStr = $('<td></td>');
                                    htmlStr.append('<SPAN STYLE="font-weight:bold;font-size:20px">' + item.dtDay + '</span>');
                                    for (var i = 0; i < item.venuecdata.length; i++) {

                                        if (item.flg.length > 0) {
                                            if (item.flg.indexOf(item.venuecdata[i].id) != -1) {

                                                htmlStr.append('<div style="color:red">' + item.venuecdata[i].venue + '</div>')
                                            }
                                            else {

                                                htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                            }

                                        }
                                        else {
                                            htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                        }

                                    }
                                }
                                week4.append(htmlStr);
                            }
                        });
                        $.each(result.week5, function (i, item) {
                            var htmlStr = null;
                            if (jQuery.isEmptyObject(item)) {
                                htmlStr = $('<td></td>');
                                week5.append(htmlStr);
                            } else {
                                if (item.daycolumn == 0 || item.daycolumn == 6) {
                                    htmlStr = $('<td></td>');
                                    htmlStr.append('<SPAN STYLE="font-weight:bold;font-size:20px">' + item.dtDay + '</span>');

                                    for (var i = 0; i < item.venuecdata.length; i++) {

                                        if (item.flg.length > 0) {
                                            if (item.flg.indexOf(item.venuecdata[i].id) != -1) {

                                                htmlStr.append('<div style="color:red">' + item.venuecdata[i].venue + '</div>')
                                            }
                                            else {

                                                htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                            }

                                        }
                                        else {
                                            htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                        }

                                    }
                                }
                                else if (item.Date != getTodayDate()) {
                                    htmlStr = $('<td></td>');
                                    htmlStr.append('<SPAN STYLE="font-weight:bold;font-size:20px">' + item.dtDay + '</span>');
                                    for (var i = 0; i < item.venuecdata.length; i++) {

                                        if (item.flg.length > 0) {
                                            if (item.flg.indexOf(item.venuecdata[i].id) != -1) {

                                                htmlStr.append('<div style="color:red">' + item.venuecdata[i].venue + '</div>')
                                            }
                                            else {

                                                htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                            }

                                        }
                                        else {
                                            htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                        }

                                    }
                                } else {
                                    htmlStr = $('<td></td>');
                                    htmlStr.append('<SPAN STYLE="font-weight:bold;font-size:20px">' + item.dtDay + '</span>');
                                    for (var i = 0; i < item.venuecdata.length; i++) {

                                        if (item.flg.length > 0) {
                                            if (item.flg.indexOf(item.venuecdata[i].id) != -1) {

                                                htmlStr.append('<div style="color:red">' + item.venuecdata[i].venue + '</div>')
                                            }
                                            else {

                                                htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                            }

                                        }
                                        else {
                                            htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                        }

                                    }
                                }
                                week5.append(htmlStr);
                            }
                        });
                        $.each(result.week6, function (i, item) {
                            var htmlStr = null;
                            if (jQuery.isEmptyObject(item)) {
                                htmlStr = $('<td></td>');
                                week6.append(htmlStr);
                            } else {
                                if (item.daycolumn == 0 || item.daycolumn == 6) {
                                    htmlStr = $('<td></td>');
                                    htmlStr.append('<SPAN STYLE="font-weight:bold;font-size:20px">' + item.dtDay + '</span>');
                                    for (var i = 0; i < item.venuecdata.length; i++) {

                                        if (item.flg.length > 0) {
                                            if (item.flg.indexOf(item.venuecdata[i].id) != -1) {

                                                htmlStr.append('<div style="color:red">' + item.venuecdata[i].venue + '</div>')
                                            }
                                            else {

                                                htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                            }

                                        }
                                        else {
                                            htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                        }

                                    }
                                }
                                else if (item.Date != getTodayDate()) {
                                    htmlStr = $('<td></td>');
                                    htmlStr.append('<SPAN STYLE="font-weight:bold;font-size:20px">' + item.dtDay + '</span>');
                                    for (var i = 0; i < item.venuecdata.length; i++) {

                                        if (item.flg.length > 0) {
                                            if (item.flg.indexOf(item.venuecdata[i].id) != -1) {

                                                htmlStr.append('<div style="color:red">' + item.venuecdata[i].venue + '</div>')
                                            }
                                            else {

                                                htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                            }

                                        }
                                        else {
                                            htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                        }

                                    }
                                } else {
                                    htmlStr = $('<td></td>');
                                    htmlStr.append('<SPAN STYLE="font-weight:bold;font-size:20px">' + item.dtDay + '</span>');
                                    for (var i = 0; i < item.venuecdata.length; i++) {

                                        if (item.flg.length > 0) {
                                            if (item.flg.indexOf(item.venuecdata[i].id) != -1) {

                                                htmlStr.append('<div style="color:red">' + item.venuecdata[i].venue + '</div>')
                                            }
                                            else {

                                                htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                            }

                                        }
                                        else {
                                            htmlStr.append('<div style="color:green">' + item.venuecdata[i].venue + '</div>')
                                        }

                                    }
                                }
                                week6.append(htmlStr);
                            }
                        });

                        $("#component-table").trigger("update");
                        $('.loader_cont').hide();
                    } else {
                        alertMsg('Oops, errors occur in retrieving calender');
                        $('.loader_cont').hide();
                    }
                }
            });


    });
   
  
});




function getTodayDate() {
    var localdate = new Date;
    var localday = localdate.getDate();
//    if (localday < 10) {
//        localday = '0' + localday;
//    }
    var localmonth = localdate.getMonth() + 1;
//    if (localmonth < 10) {
//        localmonth = '0' + localmonth;
//    }
    var localyear = localdate.getFullYear();

    var local = localmonth + "/" + localday + "/" + localyear;
    return local;
}


//function alertMsg(text) {
//    $("#dialog-modal").dialog({
//        title: text,
//        modal: true
//    });
//}


function getMonth(m) {
    var month;
         switch (m) {
        case "1":
            month = "Jan";
            return month;
            break;
        case "2":
            month = "Feb";
            return month;
            break;
        case "3":
            month = "Mar";
            return month;
            break;
        case "4":
            month = "Apr";
            return month;
            break;
        case "5":
            month = "May";
            return month;
            break;
        case "6":
            month = "Jun";
            return month;
            break;
        case "7":
            month = "Jul";
            return month;
            break;
        case "8":
            month = "Aug";
            return month;
            break;
        case "9":
            month = "Sep";
            return month;
            break;
        case "10":
            month = "Oct";
            return month;
            break;
        case "11":
            month = "Nov";
            return month;
            break;
        case "12":
            month = "Dec";
            return month;
            break;
        default:
            month = "non";
            return month;
            break;
   }
}




