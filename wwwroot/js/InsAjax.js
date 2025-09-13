function InsUpt_Ajax(var_url, var_data, var_type, var_ct, var_dt) {

    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";



    $.ajax({
        url: var_url,
        data: var_data,
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {
            //console.log(data);
            if (data.status == "Successfull") {
                alert("Successfull");
            }
            if (data.data == "") { } else { $("#Printdiv").html(data.data); $('#data-table').DataTable(); }


        },
        error: function (data) {
            var data = {
                status: "Error",
                msg: "Error on server.",
                data: [],
            }

        },
    });
}
function BindDiv(var_url, var_data, var_type, var_ct, var_dt) {
    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";


    $.ajax({
        url: var_url,
        data: var_data,
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {

            $("#Printdiv").html(data)
            //loadOtherExpand();
            $('#data-table').DataTable();


        },
        error: function (data) {
            var data = {
                status: "Error",
                msg: "Error on server.",
                data: [],
            }

        },
    });

}
function BindDropdown(var_url, var_data, var_type, var_ct, var_dt, var_id) {
    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    $.ajax({
        type: var_type,
        url: var_url,
        data: var_data,
        async: false,
        success: function (json, result) {
            $(var_id).empty();
            json = json || {};
            $(var_id).append('<option value="0">Select</option>');
            for (var i = 0; i < json.length; i++) {
                $(var_id).append('<option value="' + json[i].value + '">' + json[i].text + '</option>');
            }

        },
        error: function () {
            alert("Data Not Found");
        }
    });
}

function BindTextbox(var_url, var_data, var_type, var_ct, var_dt, var_id) {
    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";


    $.ajax({
        url: var_url,
        data: var_data,
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {

            $(var_id).val(data[0].printOrder);


        },
        error: function (data) {
            var data = {
                status: "Error",
                msg: "Error on server.",
                data: [],
            }

        },
    });
}

function MenuRightst_Ajax(var_url, var_data, var_type, var_ct, var_dt) {

    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";


    $.ajax({
        url: var_url,
        data: JSON.stringify(var_data),
        type: var_type,
        contentType: var_ct,
        dataType: var_dt,
        success: function (msg) {
            //return data;
            alert(msg.status);
            if (msg.data == "") { } else { $("#Printdiv").html(msg.data) }


        },
        error: function (data) {
            var data = {
                status: "Error",
                msg: "Error on server.",
                data: [],
            }

        },
    });
}
function Bindrpt1(var_url, var_data, var_type, var_ct, var_dt) {
    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";


    $.ajax({
        url: var_url,
        data: var_data,
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (response) {
            if (response.length == 0)
                alert('Some error occured while downaload Format');
            else {
                $('#Printdiv').html(response);
                //exportexcel('xlsx');
            }
        },
        error: function (data) {
            var data = {
                status: "Error",
                msg: "Error on server.",
                data: [],
            }

        },
    });
}



function CommonAjax(var_url, var_data, var_type, var_ct, var_dt) {

    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    //alert(var_data);

    $.ajax({
        url: var_url,
        data: {
            JsonData: var_data
        },
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {
            //console.log(data)
            if (data.status == "Successfull") {
                alert("Successfull");
            }
            clear();
            if (data.datagrid == "") { } else {
                $("#Printdiv").html(data.datagrid);
                //$('#data-table').DataTable();
                
            }
            $('#data-table').DataTable();


        },
        error: function (data) {
            var data = {
                status: "Error",
                msg: "Error on server.",
                data: [],
            }

        },
    });
}

function BindDivCommon(var_url, var_data, var_type, var_ct, var_dt) {
    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";


    $.ajax({
        url: var_url,
        data: {
            JsonData: var_data
        },
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {

            $("#Printdiv").html(data)
            //loadOtherExpand();
            $('#data-table').DataTable().delay(5000);


        },
        error: function (data) {
            var data = {
                status: "Error",
                msg: "Error on server.",
                data: [],
            }

        },
    });

}

function getDatagrid(var_url, var_data, var_type, var_ct, var_dt) {
    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    $.ajax({
        url: var_url,
        data: var_data,
        type: var_type,
        datatype: var_dt,
        success: function (data) {
            $("#Printdiv1").html(data.stddata);
            $("#Printdiv2").html(data.cdata);
            /*-------*/
           

        }, error: function (er) {
            alert(er);
        }
    });
}

function saveWithFile(var_url, var_data, var_type, var_ct, var_dt) {
    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    $.ajax({
        url: var_url,
        type: var_type,
        contentType: false,
        processData: false,
        data: var_data,
        success: function (data) {
            if (data.status == "Successfull") {
                alert("Successfull");
            }
            //if (data.messa != "") {
            //    swal("Message", data.messa, data.messa == "Saved Successfully" ? "success" : "error");
            //    //swal(data.messa);
            //}
            clear();
            if (data.datagrid == "") { } else { $("#Printdiv").html(data.datagrid) }
             $('#data-table').DataTable();
        },
        error: function (err) {
            alert(err.statusText);
        }
    });
}

function getFileUrl(fileId,folder) {
    var currentdate = new Date();
    var currdate = currentdate.getDate() + "" + currentdate.getMonth() + "" + currentdate.getFullYear() + "" + currentdate.getHours() + "" + currentdate.getMinutes() + "" + currentdate.getSeconds() + "" + currentdate.getMilliseconds();
    var filename = $(fileId).val();
    var name = filename.substr(0, filename.lastIndexOf('.'));
    var dataimg = name.split("\\");
    var extension = filename.replace(/^.*\./, '');
    var url_add = window.location.href;
    var data = url_add.split("://")
    var protocol = data[0];
    data = data[1].split("/");
    var domain = data[0];
    var urlimd = protocol + "://" + domain + "/" + folder+ "/" + dataimg[2] + currdate + "." + extension;
    console.log(urlimd);
    return {url:urlimd,fname: dataimg[2] + currdate + "." + extension };
}

function getstudata(var_url, var_data, var_type, var_ct, var_dt) {

    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";

    //alert(var_data);

    $.ajax({
        url: var_url,
        data: {
            JsonData: var_data
        },
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {
            //console.log(data)
            if (data != "") {

                $("#candiv").html(data);
            }
            else {
                alert("Data Not Available..!");
                $("#candiv").empty();
            }
            


        },
        error: function (data) {
            var data = {
                status: "Error",
                msg: "Error on server.",
                data: [],
            }

        },
    });
}




function BindDivModal(var_url, var_data, var_type, var_ct, var_dt) {
    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";


    $.ajax({
        url: var_url,
        data: var_data,
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {

            $("#PrintdivModal").html(data)
            //loadOtherExpand();
            //$('#data-table1').DataTable();


        },
        error: function (data) {
            var data = {
                status: "Error",
                msg: "Error on server.",
                data: [],
            }

        },
    });

}


function Bindrpt(var_url, var_data, var_type, var_ct, var_dt) {
    if (var_type == "")
        var_type = "POST";

    if (var_ct == "")
        var_ct = "application/json;charset=utf-8";

    if (var_dt == "")
        var_dt = "json";


    $.ajax({
        url: var_url,
        data: {
            JsonData: var_data
        }, 
        type: var_type,
        //contentType: var_ct,
        dataType: var_dt,
        success: function (data) {
            //data = JSON.parse(data);
            //console.log(data);
            //response = JSON.parse(response)
            //console.log(response);
            if (data.length == 0)
                alert('Some error occured while downaload Format');
            else {
                $('#Printdiv').html(data);
                $('#data-table').DataTable();

            }
        },
        error: function (data) {
            var data = {
                status: "Error",
                msg: "Error on server.",
                data: [],
            }

        },
    });
}




