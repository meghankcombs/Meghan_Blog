(function ($) {
    'use strict';

    // Doesn't block the load event
    function createIframes() {
        $('.soundcloud, .vimeo, .youtube').each(function(key, item) {
            var i = document.createElement("iframe");
            i.src = $(item).data('src');
            i.scrolling = "auto";
            i.frameborder = "0";
            i.width = $(item).data('width');
            i.height = $(item).data('height');
            $(item).append(i);
        });
    }

    $(window).load(function () {
        createIframes();

        /* Preloader */
        $('#preloader').fadeOut( 400, function () {
            $(this).remove();
        });


        /* Image cache */
        // $('.gallery-item').each(function() {
        //     var src = $(this).attr('href');
        //     var img = document.createElement('img');
        //     img.src = src;
        //     $('#image-cache').append(img);
        // });


        // Owl Carousel

        $('.owl-carousel').owlCarousel({
            items:1,
            loop:true,
            nav:true,
            navText: [
                "<i class='fa fa-angle-left' aria-hidden='true'></i>",
                "<i class='fa fa-angle-right' aria-hidden='true'></i>"
            ],
            dots: true,
            margin:10,
            autoplay: true,
            autoplayTimeout: 11000,
            autoplayHoverPause: true,
            autoplaySpeed: 1250,
            autoHeight:true
        });


    });

    $(document).ready(function () {

        /* Sticky Scroll */

        $(".sticky-column").stickit({
            screenMinWidth: 1024    // apply if width >= 1024
        });
        

        /* Contact Form */
        //(function () {
        //    // Get the form.
        //    var form = $('#ajax-contact');

        //    // Get the messages div.
        //    var formMessages = $('#form-messages');

        //    // Set up an event listener for the contact form.
        //    $(form).submit(function (e) {
        //        // Stop the browser from submitting the form.
        //        e.preventDefault();

        //        // Serialize the form data.
        //        var formData = $(form).serialize();

        //        // Submit the form using AJAX.
        //        $.ajax({
        //                type: 'POST',
        //                url: $(form).attr('action'),
        //                data: formData
        //            })
        //            .done(function (response) {
        //                // Make sure that the formMessages div has the 'success' class.
        //                $(formMessages).removeClass('alert alert-danger');
        //                $(formMessages).addClass('alert alert-success');

        //                // Set the message text.
        //                $(formMessages).text(response);

        //                // Clear the form.
        //                $('#name').val('');
        //                $('#email').val('');
        //                $('#message').val('');
        //            })
        //            .fail(function (data) {
        //                // Make sure that the formMessages div has the 'error' class.
        //                $(formMessages).removeClass('alert alert-success');
        //                $(formMessages).addClass('alert alert-danger');

        //                // Set the message text.
        //                if (data.responseText !== '') {
        //                    $(formMessages).text(data.responseText);
        //                } else {
        //                    $(formMessages).text('Oops! An error occured and your message could not be sent.');
        //                }
        //            });
        //    });

        //})();
        


        /* Google map */
        (function () {
            if (!$('#google-map').length) {
                return false;
            }

            initGmap();

            function initGmap() {

                // Create an array of styles.
                var styles = [
                    {
                        stylers: [
                            {saturation: -100}
                        ]
                    }, {
                        featureType: "road",
                        elementType: "geometry",
                        stylers: [
                            {lightness: 100},
                            {visibility: "simplified"}
                        ]
                    }, {
                        featureType: "road",
                        elementType: "labels",
                        stylers: [
                            {visibility: "off"}
                        ]
                    }
                ];

                // Create a new StyledMapType object, passing it the array of styles,
                // as well as the name to be displayed on the map type control.
                var styledMap = new google.maps.StyledMapType(styles, {name: "Styled Map"});

                // Create a map object, and include the MapTypeId to add
                // to the map type control.
                var $latlng = new google.maps.LatLng(29.198351, -81.028046),
                    $mapOptions = {
                        zoom: 13,
                        center: $latlng,
                        panControl: false,
                        zoomControl: true,
                        scaleControl: false,
                        mapTypeControl: false,
                        scrollwheel: false,
                        mapTypeControlOptions: {
                            mapTypeIds: [google.maps.MapTypeId.ROADMAP, 'map_style']
                        }
                    };
                var map = new google.maps.Map(document.getElementById('google-map'), $mapOptions);

                google.maps.event.trigger(map, 'resize');

                //Associate the styled map with the MapTypeId and set it to display.
                map.mapTypes.set('map_style', styledMap);
                map.setMapTypeId('map_style');

                var marker = new google.maps.Marker({
                    position: $latlng,
                    map: map,
                    title: ""
                });

            }

        })();

        /* Back to top */
        (function () {
            $("#back-top").hide();

            $(window).scroll(function () {
                if ($(this).scrollTop() > 100) {
                    $('#back-top').fadeIn();
                } else {
                    $('#back-top').fadeOut();
                }
            });

            $('#back-top a').click(function () {
                $('body,html').animate({
                    scrollTop: 0
                }, 600);
                return false;
            });
        })();

        // Dropdown on Hover
        // $('.navbar .dropdown').hover(function() {
        //     $(this).find('.dropdown-menu').first().stop(true, true).slideDown(150);
        // }, function() {
        //     $(this).find('.dropdown-menu').first().stop(true, true).slideUp(105)
        // });


    });

})(jQuery);

//IN-LINE SCRIPTS FROM TEMPLATE
addEventListener("load", function () { setTimeout(hideURLbar, 0); }, false); function hideURLbar() { window.scrollTo(0, 1); }

$(function () {
    $("#slider").responsiveSlides({
        auto: true,
        nav: true,
        speed: 500,
        namespace: "callbacks",
        pager: true,
    });
});

$("span.menu").click(function () {
    $(".head-nav ul").slideToggle(300, function () {
        // Animation complete.
    });
});

$(window).load(function () {

    $("#flexiselDemo3").flexisel({
        visibleItems: 5,
        animationSpeed: 1000,
        autoPlay: true,
        autoPlaySpeed: 3000,
        pauseOnHover: true,
        enableResponsiveBreakpoints: true,
        responsiveBreakpoints: {
            portrait: {
                changePoint: 480,
                visibleItems: 2
            },
            landscape: {
                changePoint: 640,
                visibleItems: 3
            },
            tablet: {
                changePoint: 768,
                visibleItems: 3
            }
        }
    });

});

