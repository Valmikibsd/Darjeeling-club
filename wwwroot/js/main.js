"use strict";
$(document).ready(function () {
    
    /* url  navigation active */
    var url = window.location;
    function menuitems() {
        var element = $('.sidebar .navbar .nav-item a').filter(function () {
            return this.href == url;
            console.log(url)
        }).addClass('active').closest(".dropdown-menu").addClass('show').closest("li.dropdown").addClass('show');
    }
    menuitems();

    
    
    /* sidebar left  expand collapase */
    $('.menu-left').on('click', function () {
        $('body').addClass('menu-left-open');
        $('body .wrapper').append('<div class="backdrop"></div>');
        $('.backdrop').on('click', function () {
            $('body').removeClass('menu-left-open');
            $('.backdrop').fadeOut().remove();
        });
    });

    $('.sidebar-close').on('click', function () {
        $('body').removeClass('menu-left-open');
        $('.backdrop').fadeOut().remove();
    });

    /* sideabr right expand collapase */
    $('.menu-right').on('click', function () {
        $('body').addClass('menu-right-open')
        $('body .wrapper').append('<div class="backdrop-right"></div>');
        $('.backdrop-right, .menu-left-close').on('click', function () {
            $('body').removeClass('menu-right-open');
            $('.backdrop-right').fadeOut().remove();
        });
    });

    /* search control visible slide hide slide */
    $('.searchbtn').on('click', function () {
        $('.searchcontrol').addClass('active');
    });
    $('.close-search').on('click', function () {
        $('.searchcontrol').removeClass('active');
    });

    /* theme cookie usage */

    /* theme color cookie */
    if ($.type($.cookie("theme-color")) != 'undefined' && $.cookie("theme-color") != '') {
        $('body').removeClass('color-theme-blue');
        $('body').addClass($.cookie("theme-color"));
    }
    $('.theme-color .btn').on('click', function () {
        $('body').removeClass('color-theme-blue');
        $('body').removeClass($.cookie("theme-color"));
        $.cookie("theme-color", $(this).attr('data-theme'), {
            expires: 1
        });
        $('body').addClass($.cookie("theme-color"));

    });

    /* theme layout cookie */
    if ($.type($.cookie("theme-color-layout")) != 'undefined' && $.cookie("theme-color-layout") != '') {
        $('body').removeClass('theme-light');
        $('body').addClass($.cookie("theme-color-layout"));
    }
    $('.theme-layout .btn').on('click', function () {
        $('body').removeClass('theme-light');
        $('body').removeClass($.cookie("theme-color-layout"));
        $.cookie("theme-color-layout", $(this).attr('data-layout'), {
            expires: 1
        });
        $('body').addClass($.cookie("theme-color-layout"));

    });

    if ($.type($.cookie("theme-color-layout")) != 'theme-light' && $.cookie("theme-color-layout") != 'theme-light') {
        $('#theme-dark').prop('checked', true);
    }
    $('#theme-dark').on('change', function () {
        if ($(this).is(':checked') === true) {
            $('body').removeClass('theme-light');
            $('body').removeClass($.cookie("theme-color-layout"));
            $.cookie("theme-color-layout", 'theme-dark', {
                expires: 1
            });
            $('body').addClass($.cookie("theme-color-layout"));
        } else {
            $('body').removeClass('theme-dark');
            $('body').removeClass($.cookie("theme-color-layout"));
            $.cookie("theme-color-layout", 'theme-light', {
                expires: 1
            });
            $('body').addClass($.cookie("theme-color-layout"));
        }


    });


    /* page content height for sticky footer */
    $('.content-sticky-footer').css({
        'padding-bottom': ($('.footer-wrapper').height() + 35)
    });
    $('.footer-wrapper').css('margin-top', -($('.footer-wrapper').height() + 20));


    /* page inside iframe just for demo app */
    if (self !== top) {
        $('body').addClass('max-demo-frame')
    }

});
$(window).on('load', function () {
    $('.loader').remove();
});



window.onload = function () {
        //Reference the DropDownList.
        var ddlYears = document.getElementById("ddlYears");
 
        //Determine the Current Year.
        var currentYear = (new Date()).getFullYear();
 
        //Loop and add the Year values to DropDownList.
        for (var i = 1950; i <= currentYear; i++) {
            var option = document.createElement("OPTION");
            option.innerHTML = i;
            option.value = i;
            ddlYears.appendChild(option);
        }
    };




    
            mobiscroll.setOptions({
        locale: mobiscroll.localeEn,                // Specify language like: locale: mobiscroll.localePl or omit setting to use default
        theme: 'ios',                               // Specify theme like: theme: 'ios' or omit setting to use default
        themeVariant: 'light'                       // More info about themeVariant: https://docs.mobiscroll.com/5-19-2/javascript/eventcalendar#opt-themeVariant
    });
    
    mobiscroll.momentTimezone.moment = moment;
    
    var calendarInst = mobiscroll.eventcalendar('#demo-showing-events-timezone', {
        timezonePlugin: mobiscroll.momentTimezone,  // More info about timezonePlugin: https://docs.mobiscroll.com/5-19-2/javascript/eventcalendar#opt-timezonePlugin
        dataTimezone: 'utc',                        // More info about dataTimezone: https://docs.mobiscroll.com/5-19-2/javascript/eventcalendar#opt-dataTimezone
        displayTimezone: 'utc',                     // More info about displayTimezone: https://docs.mobiscroll.com/5-19-2/javascript/eventcalendar#opt-displayTimezone
        view: {                                     // More info about view: https://docs.mobiscroll.com/5-19-2/javascript/eventcalendar#opt-view
            calendar: {
                    labels: true                    // More info about labels: https://docs.mobiscroll.com/5-19-2/javascript/eventcalendar#opt-labels
                }
        },
        dragToCreate: true,                         // More info about dragToCreate: https://docs.mobiscroll.com/5-19-2/javascript/eventcalendar#opt-dragToCreate
        dragToMove: true,                           // More info about dragToMove: https://docs.mobiscroll.com/5-19-2/javascript/eventcalendar#opt-dragToMove
        dragToResize: true,                         // More info about dragToResize: https://docs.mobiscroll.com/5-19-2/javascript/eventcalendar#opt-dragToResize
        data: [{
            start: '2022-11-07T18:00',
            end: '2022-11-07T19:30',
            title: '',
            color: '#ED2525'
        },
        {
            start: '2022-11-06T18:00',
            end: '2022-11-06T19:30',
            title: '',
            color: '#ED2525'
        },
        {
            start: '2022-11-05T18:00',
            end: '2022-11-05T19:30',
            title: '',
            color: '#ED2525'
        }, 
        {
            start: '2022-11-08T14:00',
            end: '2022-11-08T18:00',
            title: '',
            color: '#FEA83B'
        },
        {
            start: '2022-11-09T14:00',
            end: '2022-11-09T18:00',
            title: '',
            color: '#FEA83B'
        },
        {
            start: '2022-11-10T14:00',
            end: '2022-11-10T18:00',
            title: '',
            color: '#FEA83B'
        },{
            start: '2022-11-11T14:00',
            end: '2022-11-11T18:00',
            title: '',
            color: '#FEA83B'
        },
        {
            start: '2022-11-13T14:00',
            end: '2022-11-13T18:00',
            title: '',
            color: '#12C039'
        },
        {
            start: '2022-11-14T14:00',
            end: '2022-11-14T18:00',
            title: '',
            color: '#12C039'
        },
         {
            start: '2022-11-15T20:00',
            end: '2022-11-15T21:50',
            title: '',
            color: '#12C039'
        }],
        renderHeader: function () {                 // More info about renderHeader: https://docs.mobiscroll.com/5-19-2/javascript/eventcalendar#opt-renderHeader
            return '<div mbsc-calendar-nav class="md-timezone-nav"></div>' +
                '<div class="md-timezone-header">' +
                '<div mbsc-calendar-prev class="md-timezone-prev"></div>' +
                // '<div mbsc-calendar-today class="md-timezone-today"></div>' +
                '<div mbsc-calendar-next class="md-timezone-next"></div>' +
                // '<label><input id="md-timezone-input" class="md-timezone-picker" mbsc-input data-dropdown="true" data-input-style="box" /></label>' +
                // '<select id="md-timezone-select">' +
                // '<option value="America/Los_Angeles">America/Los Angeles</option>' +
                // '<option value="America/Chicago">America/Chicago</option>' +
                // '<option value="America/New_York">America/New York</option>' +
                // '<option value="utc" selected>UTC</option>' +
                // '<option value="Europe/London">Europe/London</option>' +
                // '<option value="Europe/Berlin">Europe/Berlin</option>' +
                // '<option value="Europe/Bucharest">Europe/Bucharest</option>' +
                // '<option value="Asia/Shanghai">Asia/Shanghai</option>' +
                // '<option value="Asia/Tokyo">Asia/Tokyo</option>' +
                // '</select>' +
                '</div>';
        }
    })
    
    mobiscroll.select('#md-timezone-select', {
        inputElement: document.getElementById('md-timezone-input'),
        display: 'anchored',                        // Specify display mode like: display: 'bottom' or omit setting to use default
        touchUi: false,
        onChange: function (ev) {
            calendarInst.setOptions({
                displayTimezone: ev.value,          // More info about displayTimezone: https://docs.mobiscroll.com/5-19-2/javascript/eventcalendar#opt-displayTimezone
            });
        }
    });