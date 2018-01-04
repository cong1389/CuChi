<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="leftmenu.ascx.cs" Inherits="Cb.Web.Admin.Controls.leftmenu" %>

<!--leftmenu--->


<div class="user-panel hidden">
    <div class="pull-left image">
        <a runat="server" id="hypLogo">
            <img src="/Admin/Images/Logo_admin.png" class="img-circle" alt="User Image">
        </a>
    </div>
    <div class="pull-left info">
        <p>Alexander Pierce</p>
        <a href="#"><i class="fa fa-circle text-success"></i>Online</a>
    </div>
</div>

<ul class="sidebar-menu tree" data-widget="tree">
    <li class="header">MAIN NAVIGATION</li>
    <li class="active treeview">
        <a href="#">
            <i class="fa fa-th"></i><span>Cấu hình</span>
            <span class="pull-right-container">
                <i class="fa fa-angle-left pull-right"></i>
            </span>
        </a>
        <ul class="treeview-menu" style="display: none;">
            <li><a runat="server" id="hypPage"><i class="fa fa-circle-o"></i>Cấu hình chung</a></li>
            <li class=""><a runat="server" id="hypManageUser"><i class="fa fa-circle-o"></i>Thông tin tài khoản</a></li>
            <li class="hidden"><a runat="server" id="hypSeo"><i class="fa fa-circle-o"></i>Robots file</a></li>
            <li class=""><a runat="server" id="hypConfiguration"><i class="fa fa-circle-o"></i>Kích thước hình - Email</a></li>
            <li class=""><a runat="server" id="hypClearCache"><i class="fa fa-circle-o"></i>Xóa cache</a></li>
        </ul>
    </li>

    <!---Sản phẩm - bài viết-->
    <li class="active treeview">
        <a href="#">
            <i class="fa fa-th"></i><span>Danh mục</span>
            <span class="pull-right-container">
                <i class="fa fa-angle-left pull-right"></i>
            </span>
        </a>
        <ul class="treeview-menu" style="display: none;">
            <li><a runat="server" id="hypManageCategories"><i class="fa fa-circle-o"></i>Danh mục sản phẩm</a></li>
            <li class=""><a runat="server" id="hypManageItem"><i class="fa fa-circle-o"></i>Xem tất cả bài viết</a></li>
            <li class="hidden"><a runat="server" id="hypManageGallery"><i class="fa fa-circle-o"></i>Gallery</a></li>
            <li class=""><a runat="server" id="hypSlide"><i class="fa fa-circle-o"></i>Slider - Partner</a></li>
            <li class=""><a runat="server" id="hypContentStatic"><i class="fa fa-circle-o"></i>Nội dung tĩnh</a></li>
        </ul>
    </li>
    <!---/Sản phẩm - bài viết-->

     <li class="treeview">
        <a href="#">
            <i class="fa fa-th"></i><span>Bài viết</span>
            <span class="pull-right-container">
                <i class="fa fa-angle-left pull-right"></i>
            </span>
        </a>
        <ul class="treeview-menu" style="display: none;">
            <asp:Repeater runat="server" ID="rptResult" OnItemDataBound="rptResult_ItemDataBound">
                <ItemTemplate>
                    <li class="treeview">
                        <a runat="server" id="hypName"><i class="fa fa-circle-o"></i>
                            <asp:Literal runat="server" ID="ltrName"></asp:Literal>
                            <asp:Literal runat="server" id="divIconSub"></asp:Literal>                            
                        </a>
                        <ul class="treeview-menu" runat="server" id="divSub" visible="false">

                            <asp:Repeater runat="server" ID="rptResultSub" OnItemDataBound="rptResultSub_ItemDataBound">
                                <ItemTemplate>
                                    <li class="treeview">
                                        <a runat="server" id="hypName"><i class="fa fa-circle-o"></i>
                                            <asp:Literal runat="server" ID="ltrName"></asp:Literal>
                                            <asp:Literal runat="server" id="divIconSub1"></asp:Literal>             
                                        </a>
                                        <ul class="treeview-menu" runat="server" id="divSub1" visible="false">

                                            <asp:Repeater runat="server" ID="rptResultSub1" OnItemDataBound="rptResultSub1_ItemDataBound">
                                                <ItemTemplate>
                                                    <li class="treeview">
                                                        <a runat="server" id="hypName"><i class="fa fa-circle-o"></i>
                                                            <asp:Literal runat="server" ID="ltrName"></asp:Literal>
                                                            <%--<span class="pull-right-container" runat="server" id="divIconSub1" style="display: none"><i class="fa fa-angle-left pull-right"></i></span>--%>
                                                        </a>

                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </ul>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>

                        </ul>
                    </li>
                </ItemTemplate>
            </asp:Repeater>

            
        </ul>
    </li>

    <!---Booking-->
    <li class="active treeview">
        <a href="#">
            <i class="fa fa-th"></i><span>Tour</span>
            <span class="pull-right-container">
                <i class="fa fa-angle-left pull-right"></i>
            </span>
        </a>
        <ul class="treeview-menu" style="display: none;">
            <li><a runat="server" id="hypManageBooking"><i class="fa fa-circle-o"></i>Manage Booking</a></li>
            <li class=""><a runat="server" id="hypManageBookingPrice"><i class="fa fa-circle-o"></i>Booking Price</a></li>
            <li class=""><a runat="server" id="hypTourPriceClass"><i class="fa fa-circle-o"></i>Price class</a></li>
            <li class=""><a runat="server" id="hypManageBookingGroup"><i class="fa fa-circle-o"></i>Group size</a></li>
            <li class=""><a runat="server" id="hypManageCountry"><i class="fa fa-circle-o"></i>Management country</a></li>
            <li class=""><a runat="server" id="hypExchageRate"><i class="fa fa-circle-o"></i>Exchage Rate</a></li>
        </ul>
    </li>
    <!---/Booking-->

    <!---Gallery-->
    <li class=" treeview">
        <a href="#">
            <i class="fa fa-th"></i><span>Gallery</span>
            <span class="pull-right-container">
                <i class="fa fa-angle-left pull-right"></i>
            </span>
        </a>
        <ul class="treeview-menu" style="display: none;">
            <li><a href="/adm/picture"><i class="fa fa-circle-o"></i>Picture</a></li>
            <li><a href="/adm/video"><i class="fa fa-circle-o"></i>Video</a></li>
        </ul>
    </li>
    <!---/Gallery-->

   

</ul>



<!-- sidebar: style can be found in sidebar.less -->
<%--<section class="sidebar" style="height: auto;">
        <!-- Sidebar user panel -->
        <div class="user-panel">

            <div class="block-left">
                <div class="img-logo hidden">
                    <a runat="server" id="hypLogo">
                        <img class="logo-admin" runat="server" id="imgLogo" src="/Admin/Images/Logo_admin.png" alt="Logo" />
                        <p class="admin-name txt-up txt-bold">admin</p>
                    </a>
                </div>

                <div class="left-icon"></div>

                <div class="block-menu-icon">

                    <!---Cấu hình-->
                    <div class="icon-parent">
                        <i class="fa fa-cogs"></i>
                        <p class="title-menu txt-bold">Cấu hình</p>
                        <img class="plug" src="/Admin/Images/plus.png" alt="plug" />
                    </div>
                    <div class="child-0">
                        <div class="title-parent">
                            <p><a runat="server" id="hypPage"><i class="fa fa-lg fa-fw fa-cog"></i>Cấu hình chung</a></p>
                        </div>
                        <div class="title-parent">
                              <p><a runat="server" id="hypManageUser"><i class="fa fa-lg fa-fw fa-user"></i>Thông tin tài khoản</a></p>
                        </div>
                        <div class="title-parent hidden">
                            <p><a runat="server" id="hypSeo"><i class="fa fa-lg fa-fw  fa-random"></i>Robots file</a></p>
                        </div>
                        <div class="title-parent">
                            <p><a runat="server" id="hypConfiguration"><i class="fa fa-lg fa-fw fa-arrows-alt"></i>Kích thước hình - Email</a></p>
                        </div>
                        <div class="title-parent">
                            <p><a runat="server" id="hypClearCache" onserverclick="hypClearCache_ServerClick"><i class="fa fa-refresh fa-fw"></i>Xóa cache</a></p>
                        </div>
                    </div>
                    <!---/Cấu hình-->

                    <!---Sản phẩm - bài viết-->
                     <div class="icon-parent">
                        <i class="fa fa-th"></i>
                        <p class="title-menu txt-bold">Sản phẩm - bài viết </p>
                        <img class="plug" src="/Admin/Images/plus.png" alt="plug" />
                    </div>
                    <div class="child-0 show-cur" style="display: block;">
                        <div class="title-parent">
                            <p><a runat="server" id="hypManageCategories"><i class="fa fa-lg fa-fw fa-tags"></i>Danh mục sản phẩm</a></p>
                        </div>
                        <div class="title-parent">
                            <p><a runat="server" id="hypManageItem"><i class="fa fa-lg fa-fw fa-tag"></i>Sản phẩm</a></p>
                        </div>

                        <div class="title-parent">
                            <p><a runat="server" id="hypManageGallery"><i class="fa fa-lg fa-fw fa-tag"></i>Gallery</a></p>
                        </div>

                        <div class="title-parent">
                            <p><a runat="server" id="hypSlide"><i class="fa fa-lg fa-fw fa-folder-open"></i>Slider - Partner</a></p>
                        </div>
                        <div class="title-parent">
                            <p><a runat="server" id="hypContentStatic"><i class="fa fa-lg fa-fw fa-folder-open"></i>Nội dung tĩnh</a></p>
                        </div>
                    </div>
                    <!---/Sản phẩm - bài viết-->

                    <!---Bài viết-->
                    <div class="icon-parent">
                        <i class="fa fa-cogs"></i>
                        <p class="title-menu txt-bold">Bài viết </p>
                        <img class="plug" src="/Admin/Images/plus.png" alt="plug" />
                    </div>
                    <div class="child-0">
                        <div class="title-parent">
                            <p><a runat="server" id="A3" href="/adm/picture"><i class="fa fa-lg fa-fw fa-tag"></i>Picture</a></p>
                            <p><a runat="server" id="A4" href="/adm/video"><i class="fa fa-lg fa-fw fa-tag"></i>Video</a></p>

                        </div>
                    </div>
                    <!---/Bài viết-->

                    <!---Booking-->
                     <div class="icon-parent">
                        <i class="fa fa-cogs"></i>
                        <p class="title-menu txt-bold">Tour </p>
                        <img class="plug" src="/Admin/Images/plus.png" alt="plug" />
                    </div>
                    <div class="child-0">
                        <div class="title-parent">
                            <p><a runat="server" id="hypManageBooking"><i class="fa fa-lg fa-fw fa-tag"></i>Manage Booking</a></p>
                            <p class="hidden"><a runat="server" id="hypManageBookingPrice"><i class="fa fa-lg fa-fw fa-tag"></i>Booking Price</a></p>
                            <p><a runat="server" id="hypTourPriceClass"><i class="fa fa-lg fa-fw fa-tag"></i>Price class</a></p>
                            <p><a runat="server" id="hypManageBookingGroup"><i class="fa fa-lg fa-fw fa-tag"></i>Group size</a></p>
                            <p class="hidden"><a runat="server" id="hypManageCountry"><i class="fa fa-lg fa-fw fa-tag"></i>Management country</a></p>

                            <p><a runat="server" id="hypExchageRate"><i class="fa fa-lg fa-fw fa-tag"></i>Exchage Rate</a></p>

                        </div>
                    </div>
                    <!---/Booking-->

                    <!---Gallery-->
                    <div class="icon-parent">
                        <i class="fa fa-cogs"></i>
                        <p class="title-menu txt-bold">Gallery </p>
                        <img class="plug" src="/Admin/Images/plus.png" alt="plug" />
                    </div>
                    <div class="child-0">
                        <div class="title-parent">
                            <p><a runat="server" id="A1" href="/adm/picture"><i class="fa fa-lg fa-fw fa-tag"></i>Picture</a></p>
                            <p><a runat="server" id="A2" href="/adm/video"><i class="fa fa-lg fa-fw fa-tag"></i>Video</a></p>

                        </div>
                    </div>
                    <!---/Gallery-->

                </div>
            </div>

        </div>
        <!-- search form -->

    </section>--%>
<!-- /.sidebar -->



