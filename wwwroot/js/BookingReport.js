
function Bindvenuetypeid() {
    $.ajax({
        type: "GET",
        url: '/Admin/clubmaster/VenueTypeAvlbleReportBind',
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

function ShowDataInTable() {
	$.ajax({
		url: '/Admin/clubmaster/ShowVenueAvailability',
		dataType: 'JSON',
		data: { venueid: $("#ddlvenueid").val(), venuetypeid: $("#ddlvenuetypeid").val(), VENUE_DATE1: $("#txtdate1").val(), VENUE_DATE2: $("#txtdate2").val() },
		type: 'GET',
		contentType: 'application/json; charset=utf-8',
		success: function (data) {
			console.log(data);
			var html = '';
			var index = 1;
			$.each(data, function (key, item) {
				html += '<tr class="hover-primary">';
				html += '<td>' + item.venueName + '</td>';
				html += '<td>' + item.name + '</td>';
				html += '<td>' + item.rateForMember + '</td>';
				html += '</tr>';
				index++;
			});
			$('.SubCategorybody').html(html);
		}
	});
}