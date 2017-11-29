<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryDetail.ascx.cs" Inherits="Cb.Web.Pages.CategoryManagement.CategoryDetail" %>

<!--CategoryDetail-->
<%@ Register TagPrefix="dgc" TagName="top_menu" Src="~/Controls/top_menu.ascx" %>

<%@ Register TagPrefix="dgc" TagName="block_productrelate" Src="~/Controls/block_productrelate.ascx" %>
<%@ Register TagPrefix="dgc" TagName="block_like" Src="~/Controls/block_like.ascx" %>
<%@ Register TagPrefix="dgc" TagName="block_googlemap_detail" Src="~/Controls/block_googlemap_detail.ascx" %>
<%@ Register TagPrefix="dgc" TagName="block_programtour" Src="~/Controls/block_programtour.ascx" %>
<%@ Register TagPrefix="dgc" TagName="block_bookingprice" Src="~/Controls/block_bookingprice.ascx" %>

<link href="/assets/wp-content/plugins/bootstrap-rating/star-rating.min.css" media="all" rel="stylesheet" type="text/css" />
<script type='text/javascript' src='/assets/wp-content/plugins/bootstrap-rating/star-rating.min.js'></script>
<script type='text/javascript' src='/assets/wp-content/plugins/bootstrap-rating/themes/krajee-svg/theme.js'></script>
<script type='text/javascript' src='/assets/wp-content/plugins/bootstrap-rating/themes/locales/es.js'></script>

<link href="/Admin/Components/WebOne/css/plugins.min.css" rel="stylesheet" type="text/css" media="all" />
<link href="/Admin/Components/WebOne/css/components.min.css" rel="stylesheet" type="text/css" media="all" />

<div class="header-outer-wrapper no-top-slider">
    <dgc:top_menu ID="top_menu" runat="server" />
    <asp:Literal runat="server" ID="ltrHeaderCategory"></asp:Literal>

</div>
<div class="content-outer-wrapper mb31" id="divDetail">

    <div class="content-wrapper container main ">

        <section id="content">
            <div class="">
                <div id="main">
                    <div class="row">
                        <div class="col-sm-6 col-md-8 mt20 mb31">
                            <div id="slider1_container" style="position: relative; top: 0px; left: 0px; width: 700px; height: 456px; background: #fff; overflow: hidden;">

                                <!-- Loading Screen -->
                                <div u="loading" style="position: absolute; top: 0px; left: 0px;">
                                    <div style="filter: alpha(opacity=70); opacity: 0.7; position: absolute; display: block; background-color: #fff; top: 0px; left: 0px; width: 100%; height: 100%;">
                                    </div>
                                    <div style="position: absolute; display: block; background: url(/Images/loading.gif) no-repeat center center; top: 0px; left: 0px; width: 100%; height: 100%;">
                                    </div>
                                </div>

                                <!-- Slides Container -->
                                <div u="slides" style="cursor: move; position: absolute; left: 0px; top: 0px; width: 700px; height: 356px; overflow: hidden;">

                                    <asp:Repeater runat="server" ID="rptResult" OnItemDataBound="rptResult_ItemDataBound">
                                        <ItemTemplate>
                                            <div>
                                                <img u="image" runat="server" id="img" />
                                                <img u="thumb" runat="server" id="imgThumb" />
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </div>

                                <!-- Arrow Navigator Skin Begin -->
                                <!-- Arrow Left -->
                                <span u="arrowleft" class="jssora05l" style="width: 40px; height: 40px; top: 158px; left: 8px;"></span>
                                <!-- Arrow Right -->
                                <span u="arrowright" class="jssora05r" style="width: 40px; height: 40px; top: 158px; right: 8px"></span>
                                <!-- Arrow Navigator Skin End -->

                                <!-- Thumbnail Navigator Skin Begin -->
                                <div u="thumbnavigator" class="jssort01" style="position: absolute; width: 700px; height: 100px; left: 0px; bottom: 0px;">
                                    <!-- Thumbnail Item Skin Begin -->
                                    <style>
                                     
                                    </style>
                                    <div u="slides" style="cursor: move;">
                                        <div u="prototype" class="p" style="position: absolute; width: 72px; height: 72px; top: 0; left: 0;">
                                            <div class="w">
                                                <thumbnailtemplate style="width: 100%; height: 100%; border: none; position: absolute; top: 0; left: 0;"></thumbnailtemplate>
                                            </div>
                                            <div class="c">
                                            </div>
                                        </div>
                                    </div>
                                    <!-- Thumbnail Item Skin End -->
                                </div>
                                <!-- Thumbnail Navigator Skin End -->

                            </div>



                        </div>
                        <div class="col-sm-6 col-md-4">
                            <div class="travelo-box attribute mt20 divInfoTour ">
                                <i class="fa fa-lightbulb-o"></i>
                                <h3 class="widgettitle">Tour Information</h3>
                                <ul class="list-11">
                                    <li>Tour Code:
                                        <asp:Literal runat="server" ID="ltrTourCode"></asp:Literal></li>
                                    <li>Length: 
                                        <asp:Literal runat="server" ID="ltrLength"></asp:Literal></li>
                                    <li>Start From: 
                                        <asp:Literal runat="server" ID="ltrStartFrom"></asp:Literal></li>
                                    <li>Tour Type:<div class="tour-info-sidebar">
                                        <asp:Literal runat="server" ID="ltrTourType"></asp:Literal></div>
                                    </li>

                                    <li>Reviews:
                                <a href="javascript:;" id="scrollReview" class="hidden" rel="nofollow">Be the first to review this tour</a></li>

                                    <li>
                                        <div class="tour-info-sidebar">
                                            <asp:Literal runat="server" ID="ltrPriceFrom"></asp:Literal>
                                        </div>
                                    </li>

                                </ul>

                                <!--Call now-->
                                <div class="row mb17">
                                    <div class="col-xs-4 ">
                                        <div class="pull-left mb15 imgBoxCall">
                                            <img src="/Images/tchotel_2016_LL_TM-11655-2.jpg" />
                                        </div>
                                    </div>

                                    <div class=" col-xs-8">
                                        <img src="/images/supportgirl.png" class="img-circle face pull-left" />
                                        <div class="call pull-left">
                                            Call us!                                   
                                        </div>

                                        <div class="phonenumber pull-left">
                                            <asp:Literal runat="server" ID="ltrPhone"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                                <!--/Call now-->
                                <div class="text-center col-xs-5">
                                    <div class="mb15">
                                        <a class="row button btn-small full-width" href="/booking/vn" runat="server" id="hypBookingNow">Book now</a>
                                    </div>
                                </div>
                                <div class="mb11 pull-right">
                                    <dgc:block_like ID="block_like" runat="server" />
                                </div>
                                <div class="clearfix"></div>



                                <!--Download file-->
                                <div class="text-center divDownload mb17" runat="server" id="divPdf" style="display: none">
                                    <div class="col-xs-6">
                                        <asp:LinkButton runat="server" ID="lbnViewFile" OnCommand="lbnViewFile_Command" CssClass="row button btn-small full-width" Text="Itinerary"> <i class="fa fa-arrows-alt fa-fw"></i>&nbsp Itinerary</asp:LinkButton>
                                    </div>
                                    <div class="col-xs-6">
                                        <button class="row button btn-small full-width" onclick="PrintFile();"><i class="fa fa-cloud-download fa-fw"></i>&nbsp Print Itinerary</button>
                                    </div>
                                </div>
                                <!--/Download file-->

                            </div>
                        </div>

                        <div class="text-center">
                            <div class="mb15">
                            </div>
                        </div>
                        <!--/Call now-->

                        <div class="text-justify mce-content">
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-sm-8 col-md-9">
                            <div id="hotel-features" class=" tabbable tabbable-tabdrop tab-container">
                                <ul class="nav nav-tabs" id="tabDetail">

                                    <li class="active">
                                        <a href="#tab_2" data-toggle="tab">Highlight
                                        </a>
                                    </li>
                                    <li class="">
                                        <a href="#tab_3" data-toggle="tab">Itinerary 
                                          
                                        </a>
                                    </li>
                                    <li class="">
                                        <a href="#tab_4" data-toggle="tab">Price & Bookings 
                                        </a>
                                    </li>
                                    <li class="">
                                        <a href="#tab_5" data-toggle="tab">Reviews
                                        </a>
                                    </li>
                                    <li class="">
                                        <a href="#tab_6" data-toggle="tab">Video
                                        </a>
                                    </li>
                                    <li class="" onclick="ResetMap(this);">
                                        <a href="#tabMaps" data-toggle="tab">Maps
                                        </a>
                                    </li>
                                </ul>
                                <div class="tab-content">

                                    <div class="tab-pane active" id="tab_2">
                                        <div class="panel-group accordion">
                                            <div class="panel panel-default">
                                                <asp:Literal runat="server" ID="ltrDetail"></asp:Literal>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane divBooking" id="tab_3">
                                        <div class="panel-group accordion">
                                            <div class="panel panel-default">
                                                <dgc:block_programtour ID="block_programtour" runat="server" />

                                                <asp:Literal runat="server" ID="ltrDetailedItinerary"></asp:Literal>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="tab_4">
                                        <div class="panel-group accordion">
                                            <div class="panel panel-default">
                                                <dgc:block_bookingprice ID="block_bookingprice" runat="server" />
                                                <asp:Literal runat="server" ID="ltrPriceServices"></asp:Literal>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="tab_5">
                                        <div class="col-xs-12 divRate">
                                            <div class="boxRate col-xs-4">
                                                <input id="input-id" type="text" class="rating" data-size="md" data-show-clear="false" data-show-caption="false">
                                            </div>
                                            <div class="col-xs-7" style="font-size: 43px;">
                                                <asp:Literal runat="server" ID="ltrViewCount"></asp:Literal>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="tab-pane" id="tab_6">
                                        <div class="embed-responsive embed-responsive-16by9 videobox">
                                            <iframe runat="server" id="ifrVideo" class="embed-responsive-item" src="https://www.youtube.com/embed/zJJ_tz-_cX4" frameborder="0" allowfullscreen="true"></iframe>
                                        </div>
                                    </div>

                                    <div class="tab-pane" id="tabMaps">

                                        <dgc:block_googlemap_detail ID="block_googlemap_detail" runat="server" />
                                    </div>

                                </div>

                                <%-- <!--maps-->
                                <div class="">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            <h4 class="panel-title">
                                                <a data-toggle="collapse" data-parent="#adn" id="tab_7" class="accordion-toggle accordion-toggle-styled collapsed" aria-expanded="false">Maps
                                                </a>
                                            </h4>
                                        </div>
                                        <div class="panel-collapse collapse in">
                                            <div class="panel-body">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--/maps-->--%>
                            </div>
                        </div>


                        <div class="col-sm-4 col-md-3 sidebar">

                            <!--Top Destination-->
                            <div class="widget travelo-box" runat="server" id="divTop" style="display: none">
                                <h3 class="widgettitle"><i class="fa fa-signal fa-fw"></i>Top Destination</h3>
                                <div class="image-box style14">

                                    <asp:Repeater runat="server" ID="rptTopDetination" OnItemDataBound="rptTopDetination_ItemDataBound">
                                        <ItemTemplate>
                                            <article class="box">
                                                <figure>
                                                    <a runat="server" id="hypImg">
                                                        <img width="298" height="199" runat="server" id="img"
                                                            class="attachment-thumbnail size-thumbnail wp-post-image">
                                                    </a>
                                                </figure>
                                                <div class="details">
                                                    <h3 class="title1">
                                                        <a runat="server" id="hypTitle">
                                                            <asp:Literal runat="server" ID="ltrTitle"></asp:Literal></a>
                                                    </h3>
                                                </div>
                                            </article>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                            <!--/Top Destination-->

                            <!--Customize Tours-->
                            <div class="contact-box widget travelo-box" runat="server" id="divCustomTour" style="display: none">
                                <h3 class="widgettitle"><i class="fa fa-cubes fa-fw"></i>Customize Tours</h3>
                                <p>
                                    <asp:Literal runat="server" ID="ltrCustomTours"></asp:Literal>
                                </p>
                            </div>
                            <!--/Customize Tours-->

                            <!--Need Help--->
                            <div class="contact-box widget travelo-box">
                                <h3 class="widgettitle"><i class="fa fa-umbrella fa-fw"></i>Need Help?</h3>
                                <p>We would be more than happy to help you. Our team advisor are 24/7 at your service to help you.</p>
                                <div class="faq-box">
                                    <i class="fa fa-crosshairs" style="color: #cc0033;"></i>
                                    <a href="/contact/vn" rel="nofollow">F.A.Qs</a>
                                </div>
                                <address class="contact-details">
                                    <span class="contact-phone"><span class="fa fa-headphones " style="color: #cc0033;"></span>
                                        <asp:Literal runat="server" ID="ltrPhoneValue"></asp:Literal></span><br>
                                    <a class="contact-email" runat="server" id="hypEmail" rel="nofollow">
                                        <span class="fa fa-envelope " style="color: #cc0033;"></span>
                                        <asp:Literal runat="server" ID="ltrEmail"></asp:Literal></a>
                                </address>
                            </div>
                            <!--/Need Help--->

                            <div class="contact-box widget travelo-box">
                                <h3 class="widgettitle"><i class="fa fa-book fa-fw"></i>How to book tour ?</h3>
                                <p>
                                    <asp:Literal runat="server" ID="ltrHowToBook"></asp:Literal>
                                </p>
                            </div>

                            <div class="contact-box widget travelo-box">
                                <h3 class="widgettitle"><i class="fa fa-paypal fa-fw"></i>Accept PayPal</h3>
                                <img alt="" src="/Admin/Images/icon_bank.png" style="width: 300px; height: 23px;">
                            </div>

                            <dgc:block_productrelate ID="block_productrelate" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
</div>


<script>
    $(document).ready(function () {
        var heigthLeft = $('#slider1_container').height();
        //alert(heigthLeft);
        $('.divInfoTour ').css('height', heigthLeft + 47.6);
        //map = new google.maps.Map(document.getElementById('ctl00_mainContent_ctl00_block_googlemap_detail_GMap1'), '');
        //google.maps.event.trigger(map, 'resize');

        // initialize with defaults

        $("#input-id").rating();

        // with plugin options (do not attach the CSS class "rating" to your input if using this approach)
        // $("#input-id").rating({ 'size': 'md' }, cap);

        SetSlideDetail();


    });


</script>
