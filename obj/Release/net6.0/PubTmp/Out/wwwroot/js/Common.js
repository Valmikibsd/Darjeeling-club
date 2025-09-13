function Login(var_url, var_data, var_type, var_ct, var_dt) {

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
            console.log(data);
            //alert(data);
            if (data.status == 'Error') {
                $("#hed1").html(data.message);
            }
            else {
                window.location.href = '/Common/LoginOtp/';
            }
            //if (data.data == "") { } else { $("#Printdiv").html(data.Data) }


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

function VerifyLogin(var_url, var_data, var_type, var_ct, var_dt) {

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
            console.log(data);
            //alert(data);
            if (data.status == 'Error') {
               alert(data.message);
            }
            else {
                window.location.href = '/Common/Home/';
            }
            //if (data.data == "") { } else { $("#Printdiv").html(data.Data) }


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



