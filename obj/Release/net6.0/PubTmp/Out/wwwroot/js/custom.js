/*************------------------------------


JS INDEX
    ===================

    01. Counter
    02. Menu
    03. Scroll Top
    04. OWL SLider
    05. Header Sticky


------------------------------*************/


(function ($) {
    $(document).on('ready', function() {
     "use strict";


 /****---- Header Sticky Start ----****/

    $(window).on("scroll", function () {
             var scroll = $(window).scrollTop();
            if (scroll >= 100) {
                $("header").addClass("sticky");
            } else {
                $("header").removeClass("sticky");
            }
    });

 /****---- Header Sticky End ----****/



/*------- OWL SLIDER START -------*/

/****---- service Slider Start ----****/

$('.service-slider').owlCarousel({
        loop:true, 
        margin:20,
        dots:false,
        autoplay:true,
        responsive:{

           0:{
                items:1
            },

            640:{
                items:2
            },

            1280:{
                items:3
            },

            1400:{
                items:4
            }
        }
    });

/****---- service Slider End ----****/


/****---- Portfolio Slider Start ----****/

$('.portfolio-slider').owlCarousel({
        loop:true, 
        dots:false,
        autoplay:true,
        responsive:{

           0:{
                items:1
            },

           568:{
                items:1
            },

            767:{
                items:2
            },

            990:{
                items:3
            },

            1280:{
                items:3,
                margin:20
            },
            1400:{
                items:4,
                margin:50
            }
        }
    });

/****---- Portfolio Slider End ----****/
/****---- Portfolio Slider Start ----****/

$('.ue').owlCarousel({
        loop:false, 
        dots:false,
        autoplay:true,
		autoplayHoverPause:true,
		nav:true,
		navText:["<",">"],
		autoplayHoverPause:true,
		nav:true,
		navText:["<",">"],
		margin:0,
        responsive:{

           0:{
                items:1
            },

           568:{
                items:1
            },

            767:{
                items:2
            },

            990:{
                items:1
            },

            1280:{
                items:1
            },
            1400:{
                items:1
            }
        }
    });
$('.pe').owlCarousel({
        loop:false, 
        dots:false,
        autoplay:true,
		autoplayHoverPause:true,
		nav:true,
		navText:["<",">"],
		autoplayHoverPause:true,
		nav:true,
		navText:["<",">"],
		margin:30,
        responsive:{

           0:{
                items:1
            },

           568:{
                items:1
            },

            767:{
                items:2
            },

            990:{
                items:3
            },

            1280:{
                items:4
            },
            1400:{
                items:4
            }
        }
    });
/****---- Portfolio Slider End ----****/
/****---- Blog Slider Start ----****/

$('.blog-slider').owlCarousel({
        loop:true, 
        margin:30,
        autoplay:true,
        responsive:{

           0:{
                items:1
            },

           480:{
                items:1
            },

            640:{
                items:1
            },

            990:{
                items:2
            },

            1140:{
                items:2
            }
        }
    });

/****---- Blog Slider End ----****/

/****---- Testimonials Slider Start ----****/


$('.testimonials-slider').owlCarousel({
        loop:true, 
        margin:30,
        autoplay:true,
		autoHeight: true,
		autoplayHoverPause:true,
        responsive:{

           0:{
                items:1
            },

           480:{
                items:1
            },

            640:{
                items:1
            },

            990:{
                items:1
            },

            1140:{
                items:2
            }
        }
    });
$('.testimonials-sliders').owlCarousel({
        loop:true, 
        margin:0,
        autoplay:false,
		autoHeight: true,
		autoplayHoverPause:true,
        responsive:{

           0:{
                items:1
            },

           480:{
                items:1
            },

            640:{
                items:1
            },

            990:{
                items:1
            },

            1140:{
                items:1
            }
        }
    });
/****---- Testimonials Slider Start ----****/

/****---- Blog 2 Slider Start ----****/

$('.blog-slider-2').owlCarousel({
        loop:true, 
        margin:50,
        autoplay:true,
        responsive:{

           0:{
                items:1
            },

           480:{
                items:1
            },

            640:{
                items:2
            },

            990:{
                items:3
            },

            1140:{
                items:3
            },
            1400:{
                items:4
            }

        }
    });

/****---- Blog 2 Slider End ----****/

/****---- Client Slider Start ----****/

$('.client').owlCarousel({
        loop:true, 
        margin:50,
        dots:false,
        autoplay:true,
        responsive:{

           0:{
                items:2
            },

           480:{
                items:2
            },

            640:{
                items:3
            },

            990:{
                items:4
            },

            1140:{
                items:5
            },
            
            1400:{
                items:6
            }

        }
    });

/****---- Client Slider End ----****/

/****---- Our Proud Members Start ----****/

$('.our_proud_members').owlCarousel({
        loop:true, 
        margin:50,
        dots:false,
        autoplay:true,
        responsive:{

           0:{
                items:1
            },

           480:{
                items:2
            },

            640:{
                items:2
            },

            990:{
                items:3
            },

            1140:{
                items:3
            },
            
            1400:{
                items:3
            }

        }
    });

/****---- Our Proud Members End ----****/

/****---- Page Main Slider Start ----****/

$('.owl-main').owlCarousel({
    loop:true,
    margin:0,
    animateOut: 'fadeOut',
    animateIn: 'fadeIn',
    autoplay:true,
    autoplayTimeout:7000,
    autoplayHoverPause:false,
    responsive:{
        320:{
            items:1
        }
        ,
       1920:{
            items:1
        }
    }
});
$('.associates').owlCarousel({
    loop:true,
    margin:30,
    animateOut: 'fadeOut',
    animateIn: 'fadeIn',
    autoplay:true,
    autoplayTimeout:3000,
    autoplayHoverPause:true,
    responsive:{
        320:{
            items:1
        }
        ,
       767:{
            items:4
        }
    }
});

/****---- Page Main Slider End ----****/


/*------- OWL SLIDER END -------*/


/****---- Counter Start ----****/

    $('.counter').counterUp({
        delay: 10,
        time: 1000
    });

/****---- Counter End ----****/



/****---- Scroll Top Start ----****/

 $(window).on("scroll", function(){
        if ($(this).scrollTop() > 100) { 
            $('#scroll').fadeIn(); 
        } else { 
            $('#scroll').fadeOut(); 
        } 
    }); 
    $('#scroll').on("click", function(){ 
        $("html, body").animate({ scrollTop: 0 }, 600); 
        return false; 
    }); 

/****---- Scroll Top End ----****/


 /****---- Menu Start ----****/


    /*--------------- SMARTMENU ---------------*/

    $('#main-menu').smartmenus({
        mainMenuSubOffsetX: -1,
        mainMenuSubOffsetY: 4,
        subMenusSubOffsetX: 6,
        subMenusSubOffsetY: -6
    });

    /*--------------- SMARTMENUS MOBILE MENU TOGGLE BUTTON ---------------*/

    var $mainMenuState = $('#main-menu-state');
    if ($mainMenuState.length) {
        // animate mobile menu
        $mainMenuState.on('change', function () {
            var $menu = $('#main-menu');
            if (this.checked) {
                $menu.hide().slideDown(250, function () {
                    $menu.css('display', '');
                });
            } else {
                $menu.show().slideUp(250, function () {
                    $menu.css('display', '');
                });
            }
        });
        // hide mobile menu beforeunload
        $(window).on('bind', 'beforeunload unload', function () {
            if ($mainMenuState[0].checked) {
                $mainMenuState[0].on("click");
            }
        });
    }
    $(function () {
        // use the whole parent item as sub menu toggle button
        $('#main-menu').on('bind', 'click.smapi', function (e, item) {
            var obj = $(this).data('smartmenus');
            if (obj.isCollapsible()) {
                var $sub = $(item).dataSM('sub');
                if ($sub && $sub.is(':visible')) {
                    obj.menuHide($sub);
                    return false;
                }
            }
        });
    });


    /*--------------- navigation menu---------------*/

    function mainmenu() {
        if ($(window).width() < 992) {
            $('.navbar .dropdown-item').on('click', function (e) {
                var $el = $(this).children('.dropdown-toggle');
                var $parent = $el.offsetParent(".dropdown-menu");
                $(this).parent("li").toggleClass('open');

                if (!$parent.parent().hasClass('navbar-nav')) {
                    if ($parent.hasClass('show')) {
                        $parent.removeClass('show');
                        $el.next().removeClass('show');
                        $el.next().css({
                            "top": -999,
                            "left": -999
                        });
                    } else {
                        $parent.parent().find('.show').removeClass('show');
                        $parent.addClass('show');
                        $el.next().addClass('show');
                        $el.next().css({
                            "top": $el[0].offsetTop,
                            "left": $parent.outerWidth() - 4
                        });
                    }
                    e.preventDefault();
                    e.stopPropagation();
                }
            });

            $('.navbar .dropdown').on('hidden.bs.dropdown', function () {
                $(this).find('li.dropdown').removeClass('show open');
                $(this).find('ul.dropdown-menu').removeClass('show open');
            });
        }
    }
    
    
 /****---- Menu End ----****/

 });

})
(jQuery);

$(document).ready(function() {
	var showChar = 170;
	var ellipsestext = "...";
	var moretext = "more";
	var lesstext = "...less";
	$('.more').each(function() {
		var content = $(this).html();
		if(content.length > showChar) {
			var c = content.substr(0, showChar);
			var h = content.substr(showChar, content.length - showChar);
			var html = c + '<span class="moreelipses">'+ellipsestext+'</span><span class="morecontent"><span>' + h + '</span><a href="" class="morelink">'+moretext+'</a></span>';

			$(this).html(html);
		}

	});

	$(".morelink").click(function(){
		if($(this).hasClass("less")) {
			$(this).removeClass("less");
			$(this).html(moretext);
		} else {
			$(this).addClass("less");
			$(this).html(lesstext);
		}
		$(this).parent().prev().toggle();
		$(this).prev().toggle();
		return false;
	});
});
$(document).ready(function() {
	var showChars = 120;
	var ellipsestexts = "...";
	var moretexts = "more";
	var lesstexts = "...less";
	$('.mores').each(function() {
		var contents = $(this).html();
		if(contents.length > showChars) {
			var cc = contents.substr(0, showChars);
			var hh = contents.substr(showChars, contents.length - showChars);
			var htmls = cc + '<span class="moreelipses">'+ellipsestexts+'</span><span class="morecontents"><span>' + hh + '</span><a href="" class="morelinks">'+moretexts+'</a></span>';

			$(this).html(htmls);
		}

	});

	$(".morelinks").click(function(){
		if($(this).hasClass("less")) {
			$(this).removeClass("less");
			$(this).html(moretexts);
		} else {
			$(this).addClass("less");
			$(this).html(lesstexts);
		}
		$(this).parent().prev().toggle();
		$(this).prev().toggle();
		return false;
	});
});