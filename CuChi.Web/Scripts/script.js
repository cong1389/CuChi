
(function (jQuery) {


    jQuery(document).ready(function () {
        jQuery('#hisella-minimize').click(function () {
            if (jQuery('#hisella-facebook').css('opacity') == 0) {
                jQuery('#hisella-facebook').css('opacity', 1);
                jQuery('.hisella-messages').animate({ left: '0' }).animate({ bottom: '0' });
            } else {
                jQuery('.hisella-messages').animate({ bottom: '-300px' }).animate({ left: '-0px' }, 400,
                    function () {
                        jQuery('#hisella-facebook').css('opacity', 0);
                    });
            }
        })
    });
})(jQuery);

$(document).ready(function () {



    // testimonial
    var testimonial_carousel = jQuery('.gdl-carousel-testimonial .testimonial-item-wrapper');
    if (testimonial_carousel.length > 0) {
        var testimonial_nav = testimonial_carousel.find('.testimonial-navigation');
        gdl_cycle_resize(testimonial_carousel);
        testimonial_carousel.cycle({
            fx: 'fade', slideResize: 5, width: '100%',
            pager: testimonial_nav
        });
    }

    OpenMenu();

    $(".touchpin").TouchSpin({
        initval: 0,
        max: 9000000000
    });

    $('#divInfoTour').css('height', '205');

});

//Replace ký tự đặc biệt
function RemoveUnicode(text) {
    var result;

    //Đổi chữ hoa thành chữ thường
    result = text.toLowerCase();

    // xóa dấu
    result = result.replace(/(à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ)/g, 'a');
    result = result.replace(/(è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ)/g, 'e');
    result = result.replace(/(ì|í|ị|ỉ|ĩ)/g, 'i');
    result = result.replace(/(ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ)/g, 'o');
    result = result.replace(/(ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ)/g, 'u');
    result = result.replace(/(ỳ|ý|ỵ|ỷ|ỹ)/g, 'y');
    result = result.replace(/(đ)/g, 'd');
    // Xóa ký tự đặc biệt
    result = result.replace(/([^0-9a-z-\s])/g, '');
    // Xóa khoảng trắng thay bằng ký tự -
    result = result.replace(/(\s+)/g, '-');
    // xóa phần dự - ở đầu
    result = result.replace(/^-+/g, '');
    // xóa phần dư - ở cuối
    result = result.replace(/-+$/g, '');

    return result;
}

function GetLink3Param(pageName, langId, catId) {
    re = "/" + pageName + "/" + langId + "/" + catId;
    return re;
}

function GetLink5Param(pageName, langId, item, destination, day) {
    var result = "/" + pageName + "/" + langId;
    if (item != "") {
        result = result + "/" + RemoveUnicode(item);
    }
    if (destination != "") {
        result = result + "/" + destination;
    }
    if (day != "") {
        result = result + "/" + day;
    }
    re = result;
    return re;
}

function checkEnter(e) { //e is event object passed from function invocation
    var characterCode //literal character code will be stored in this variable

    if (e && e.which) { //if which property of event object is supported (NN4)
        e = e;
        characterCode = e.which;  //character code is contained in NN4's which property
    }
    else {
        e = event;
        characterCode = e.keyCode;  //character code is contained in IE's keyCode property
    }
    if (characterCode == 13) { //if generated character code is equal to ascii 13 (if enter key)
        submitButtonSearchThemes('search'); //submit the form
        return false;
    }
    else {
        return true;
    }
}

function OpenMenu() {
    var e = $(".overlapblackbg, .slideLeft, .wsmenuMobile"),
        n = $(".wsmenucontent, .wsmenuMobile"),
        l = function () {
            $(e).removeClass("menuclose").addClass("menuopen")
        },
        u = function () {
            $(e).removeClass("menuopen").addClass("menuclose")
        };
    $("#navToggle").click(function () {
        $(n.hasClass("menuopen") ? $(e).removeClass("menuopen").addClass("menuclose") : $(e).removeClass("menuclose").addClass("menuopen"));
    })
        , n.click(function () {
            n.hasClass("menuopen") && $(u)
        }), $("#navToggle,.overlapblackbg").on("click", function () {
            $(".wsmenucontainer").toggleClass("mrginleft")
        })
        , $(".wsmenu-list li").has(".wsmenu-submenu, .wsmenu-submenu-sub, .wsmenu-submenu-sub-sub").prepend('<span class="wsmenu-click"><i class="wsmenu-arrow fa fa-angle-down"></i></span>'), $(".wsmenu-list li").has(".megamenu").prepend('<span class="wsmenu-click"><i class="wsmenu-arrow fa fa-angle-down"></i></span>')
        , $(".wsmenu-mobile").click(function () {
            $(".wsmenu-list").slideToggle("slow")
        }), $(".wsmenu-click").click(function () {
            $(this).siblings(".wsmenu-submenu").slideToggle("slow")
                , $(this).children(".wsmenu-arrow").toggleClass("wsmenu-rotate")
                , $(this).siblings(".wsmenu-submenu-sub").slideToggle("slow")
                , $(this).siblings(".wsmenu-submenu-sub-sub").slideToggle("slow")
                , $(this).siblings(".megamenu").slideToggle("slow")
        })
};

function SetSlideDetail() {

    var _SlideshowTransitions = [
        //Fade in L
        { $Duration: 1200, x: 0.3, $During: { $Left: [0.3, 0.7] }, $Easing: { $Left: $JssorEasing$.$EaseInCubic, $Opacity: $JssorEasing$.$EaseLinear }, $Opacity: 2 }
        //Fade out R
        , { $Duration: 1200, x: -0.3, $SlideOut: true, $Easing: { $Left: $JssorEasing$.$EaseInCubic, $Opacity: $JssorEasing$.$EaseLinear }, $Opacity: 2 }
        //Fade in R
        , { $Duration: 1200, x: -0.3, $During: { $Left: [0.3, 0.7] }, $Easing: { $Left: $JssorEasing$.$EaseInCubic, $Opacity: $JssorEasing$.$EaseLinear }, $Opacity: 2 }
        //Fade out L
        , { $Duration: 1200, x: 0.3, $SlideOut: true, $Easing: { $Left: $JssorEasing$.$EaseInCubic, $Opacity: $JssorEasing$.$EaseLinear }, $Opacity: 2 }

        //Fade in T
        , { $Duration: 1200, y: 0.3, $During: { $Top: [0.3, 0.7] }, $Easing: { $Top: $JssorEasing$.$EaseInCubic, $Opacity: $JssorEasing$.$EaseLinear }, $Opacity: 2, $Outside: true }
        //Fade out B
        , { $Duration: 1200, y: -0.3, $SlideOut: true, $Easing: { $Top: $JssorEasing$.$EaseInCubic, $Opacity: $JssorEasing$.$EaseLinear }, $Opacity: 2, $Outside: true }
        //Fade in B
        , { $Duration: 1200, y: -0.3, $During: { $Top: [0.3, 0.7] }, $Easing: { $Top: $JssorEasing$.$EaseInCubic, $Opacity: $JssorEasing$.$EaseLinear }, $Opacity: 2 }
        //Fade out T
        , { $Duration: 1200, y: 0.3, $SlideOut: true, $Easing: { $Top: $JssorEasing$.$EaseInCubic, $Opacity: $JssorEasing$.$EaseLinear }, $Opacity: 2 }

        //Fade in LR
        , { $Duration: 1200, x: 0.3, $Cols: 2, $During: { $Left: [0.3, 0.7] }, $ChessMode: { $Column: 3 }, $Easing: { $Left: $JssorEasing$.$EaseInCubic, $Opacity: $JssorEasing$.$EaseLinear }, $Opacity: 2, $Outside: true }
        //Fade out LR
        , { $Duration: 1200, x: 0.3, $Cols: 2, $SlideOut: true, $ChessMode: { $Column: 3 }, $Easing: { $Left: $JssorEasing$.$EaseInCubic, $Opacity: $JssorEasing$.$EaseLinear }, $Opacity: 2, $Outside: true }
        //Fade in TB
        , { $Duration: 1200, y: 0.3, $Rows: 2, $During: { $Top: [0.3, 0.7] }, $ChessMode: { $Row: 12 }, $Easing: { $Top: $JssorEasing$.$EaseInCubic, $Opacity: $JssorEasing$.$EaseLinear }, $Opacity: 2 }
        //Fade out TB
        , { $Duration: 1200, y: 0.3, $Rows: 2, $SlideOut: true, $ChessMode: { $Row: 12 }, $Easing: { $Top: $JssorEasing$.$EaseInCubic, $Opacity: $JssorEasing$.$EaseLinear }, $Opacity: 2 }

        //Fade in LR Chess
        , { $Duration: 1200, y: 0.3, $Cols: 2, $During: { $Top: [0.3, 0.7] }, $ChessMode: { $Column: 12 }, $Easing: { $Top: $JssorEasing$.$EaseInCubic, $Opacity: $JssorEasing$.$EaseLinear }, $Opacity: 2, $Outside: true }
        //Fade out LR Chess
        , { $Duration: 1200, y: -0.3, $Cols: 2, $SlideOut: true, $ChessMode: { $Column: 12 }, $Easing: { $Top: $JssorEasing$.$EaseInCubic, $Opacity: $JssorEasing$.$EaseLinear }, $Opacity: 2 }
        //Fade in TB Chess
        , { $Duration: 1200, x: 0.3, $Rows: 2, $During: { $Left: [0.3, 0.7] }, $ChessMode: { $Row: 3 }, $Easing: { $Left: $JssorEasing$.$EaseInCubic, $Opacity: $JssorEasing$.$EaseLinear }, $Opacity: 2, $Outside: true }
        //Fade out TB Chess
        , { $Duration: 1200, x: -0.3, $Rows: 2, $SlideOut: true, $ChessMode: { $Row: 3 }, $Easing: { $Left: $JssorEasing$.$EaseInCubic, $Opacity: $JssorEasing$.$EaseLinear }, $Opacity: 2 }

        //Fade in Corners
        , { $Duration: 1200, x: 0.3, y: 0.3, $Cols: 2, $Rows: 2, $During: { $Left: [0.3, 0.7], $Top: [0.3, 0.7] }, $ChessMode: { $Column: 3, $Row: 12 }, $Easing: { $Left: $JssorEasing$.$EaseInCubic, $Top: $JssorEasing$.$EaseInCubic, $Opacity: $JssorEasing$.$EaseLinear }, $Opacity: 2, $Outside: true }
        //Fade out Corners
        , { $Duration: 1200, x: 0.3, y: 0.3, $Cols: 2, $Rows: 2, $During: { $Left: [0.3, 0.7], $Top: [0.3, 0.7] }, $SlideOut: true, $ChessMode: { $Column: 3, $Row: 12 }, $Easing: { $Left: $JssorEasing$.$EaseInCubic, $Top: $JssorEasing$.$EaseInCubic, $Opacity: $JssorEasing$.$EaseLinear }, $Opacity: 2, $Outside: true }

        //Fade Clip in H
        , { $Duration: 1200, $Delay: 20, $Clip: 3, $Assembly: 260, $Easing: { $Clip: $JssorEasing$.$EaseInCubic, $Opacity: $JssorEasing$.$EaseLinear }, $Opacity: 2 }
        //Fade Clip out H
        , { $Duration: 1200, $Delay: 20, $Clip: 3, $SlideOut: true, $Assembly: 260, $Easing: { $Clip: $JssorEasing$.$EaseOutCubic, $Opacity: $JssorEasing$.$EaseLinear }, $Opacity: 2 }
        //Fade Clip in V
        , { $Duration: 1200, $Delay: 20, $Clip: 12, $Assembly: 260, $Easing: { $Clip: $JssorEasing$.$EaseInCubic, $Opacity: $JssorEasing$.$EaseLinear }, $Opacity: 2 }
        //Fade Clip out V
        , { $Duration: 1200, $Delay: 20, $Clip: 12, $SlideOut: true, $Assembly: 260, $Easing: { $Clip: $JssorEasing$.$EaseOutCubic, $Opacity: $JssorEasing$.$EaseLinear }, $Opacity: 2 }
    ];

    var options = {
        $AutoPlay: true,                                    //[Optional] Whether to auto play, to enable slideshow, this option must be set to true, default value is false
        $AutoPlayInterval: 1500,                            //[Optional] Interval (in milliseconds) to go for next slide since the previous stopped if the slider is auto playing, default value is 3000
        $PauseOnHover: 1,                                //[Optional] Whether to pause when mouse over if a slider is auto playing, 0 no pause, 1 pause for desktop, 2 pause for touch device, 3 pause for desktop and touch device, 4 freeze for desktop, 8 freeze for touch device, 12 freeze for desktop and touch device, default value is 1

        $DragOrientation: 3,                                //[Optional] Orientation to drag slide, 0 no drag, 1 horizental, 2 vertical, 3 either, default value is 1 (Note that the $DragOrientation should be the same as $PlayOrientation when $DisplayPieces is greater than 1, or parking position is not 0)
        $ArrowKeyNavigation: true,   			            //[Optional] Allows keyboard (arrow key) navigation or not, default value is false
        $SlideDuration: 800,                                //Specifies default duration (swipe) for slide in milliseconds

        $SlideshowOptions: {                                //[Optional] Options to specify and enable slideshow or not
            $Class: $JssorSlideshowRunner$,                 //[Required] Class to create instance of slideshow
            $Transitions: _SlideshowTransitions,            //[Required] An array of slideshow transitions to play slideshow
            $TransitionsOrder: 1,                           //[Optional] The way to choose transition to play slide, 1 Sequence, 0 Random
            $ShowLink: true                                    //[Optional] Whether to bring slide link on top of the slider when slideshow is running, default value is false
        },

        $ArrowNavigatorOptions: {                       //[Optional] Options to specify and enable arrow navigator or not
            $Class: $JssorArrowNavigator$,              //[Requried] Class to create arrow navigator instance
            $ChanceToShow: 1                               //[Required] 0 Never, 1 Mouse Over, 2 Always
        },

        $ThumbnailNavigatorOptions: {                       //[Optional] Options to specify and enable thumbnail navigator or not
            $Class: $JssorThumbnailNavigator$,              //[Required] Class to create thumbnail navigator instance
            $ChanceToShow: 2,                               //[Required] 0 Never, 1 Mouse Over, 2 Always

            $ActionMode: 1,                                 //[Optional] 0 None, 1 act by click, 2 act by mouse hover, 3 both, default value is 1
            $SpacingX: 8,                                   //[Optional] Horizontal space between each thumbnail in pixel, default value is 0
            $DisplayPieces: 10,                             //[Optional] Number of pieces to display, default value is 1
            $ParkingPosition: 360                          //[Optional] The offset position to park thumbnail
        }
    };

    var jssor_slider1 = new $JssorSlider$("slider1_container", options);
    //responsive code begin
    //you can remove responsive code if you don't want the slider scales while window resizes
    function ScaleSlider() {
        var parentWidth = jssor_slider1.$Elmt.parentNode.clientWidth;
        if (parentWidth)
            jssor_slider1.$ScaleWidth(Math.max(Math.min(parentWidth, 770), 300));
        else
            window.setTimeout(ScaleSlider, 30);
    }

    ScaleSlider();

    if (!navigator.userAgent.match(/(iPhone|iPod|iPad|BlackBerry|IEMobile)/)) {
        $(window).bind('resize', ScaleSlider);
    }


    //if (navigator.userAgent.match(/(iPhone|iPod|iPad)/)) {
    //    $(window).bind("orientationchange", ScaleSlider);
    //}
    //responsive code end
};

//Reset tab in tabjquery


//<![CDATA[
//function ResetMap(mapss) { var mapOptions = { zoom: 15, center: new google.maps.LatLng(-37.8122172, 144.965374), mapTypeId: google.maps.MapTypeId.ROADMAP }; mapid = 'ctl00_mainContent_ctl00_block_googlemap_detail_GMap1'; var map = new google.maps.Map(document.getElementById(mapid), mapOptions); var center = map.getCenter(); google.maps.event.trigger(map, 'resize'); map.setCenter(center); $('#tabDetail a[href="#tabMaps"]').on('click', function () { google.maps.event.trigger(map, 'resize'); }); google.maps.event.trigger(map, 'resize'); }//]]>


function ResetMap(mapss) {
    //var mapOptions = {
    //    zoom: 15,
    //    center: new google.maps.LatLng(-37.8122172, 144.965374), mapTypeId: google.maps.MapTypeId.ROADMAP
    //};

    google.maps.event.trigger($("#div_ID")[0], 'resize');

    mapid = 'ctl00_mainContent_ctl00_block_googlemap_detail_GMap1';
    var map = new google.maps.Map(document.getElementById(mapid), mapOptions);
    //  var center = map.getCenter();
    // alert(center);
    //google.maps.event.trigger(map, 'resize');
    // map.setCenter(center);


    //$('a[href="#tabMaps"]').on('click', function () {
    //    // alert('fd');
    //    google.maps.event.trigger(map, 'resize');
    //});
    alert('1');
    $('#tabDetail a[href="#tabMaps"]').on('click', function () { google.maps.event.trigger(map, 'resize'); });
    google.maps.event.trigger(map, 'resize');
}

