<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="block_category_left.ascx.cs" Inherits="Cb.Web.Controls.block_category_left" %>

<!--block_category_left-->
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Namespace="Cb.WebControls" Assembly="Cb.WebControls" TagPrefix="cc" %>
<%@ Register TagPrefix="dgc" TagName="top_menu" Src="~/Controls/top_menu.ascx" %>
<%@ Register TagPrefix="dgc" TagName="block_left" Src="~/Controls/block_left.ascx" %>
<%@ Register TagPrefix="dgc" TagName="block_right" Src="~/Controls/block_right.ascx" %>

<script>
    $(document).ready(function () {
        $('input[type=radio]').click(function () {
            $('input[type=radio]').removeAttr("checked");
            $(this).prop('checked', true);
        });

        jQuery("[name$='abc']").attr("name", jQuery("[name$='abc']").attr("name"));

        jQuery("[name$='abc]").click(function () {
            //set name for all to name of clicked 
            jQuery("[name$='abc]").attr("name", this.attr("name"));
        });



    });
</script>

<div class="header-outer-wrapper " runat="server" id="divBoxTop">
    <dgc:top_menu ID="top_menu" runat="server" />
    <asp:Literal runat="server" ID="ltrHeaderCategory"></asp:Literal>
</div>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="content-outer-wrapper mb31">
            <div class="page-full-wrapper">
                <div class="gdl-page-item">
                    <div class="container">
                        <div class="row divCategory">

                            <div class="col-sm-9 col-md-9 col-sm-push-9 col-md-push-3">
                                <div class="package-item-holder">

                                    <asp:Repeater runat="server" ID="rptResult" OnItemDataBound="rptResult_ItemDataBound">
                                        <ItemTemplate>
                                            <div class=" col-sm-6 col-sms-6 col-md-4">
                                                <div class="row gdl-package-widget mb14">
                                                    <div class="package-content-wrapper">
                                                        <div class="package-media-wrapper gdl-image">
                                                            <a runat="server" id="hypImg" class="hover-effect">
                                                                <img class="center-block" runat="server" id="img">
                                                                <asp:Literal runat="server" ID="ltrDiscountPercent"></asp:Literal>
                                                            </a>
                                                        </div>
                                                        <div class="detailsbox">
                                                            <a runat="server" id="hypTitle">
                                                                <div class="package-title">

                                                                    <asp:Literal runat="server" ID="ltrTitle"></asp:Literal>
                                                                </div>
                                                                <asp:Literal runat="server" ID="ltrPrice"></asp:Literal>
                                                                <div class="package-date">
                                                                    <i class="icon-time"></i>
                                                                    <asp:Literal runat="server" ID="ltrDate"></asp:Literal>
                                                                </div>
                                                                <div class="package-content">
                                                                    <asp:Literal runat="server" ID="ltrBrief"></asp:Literal>
                                                                </div>
                                                            </a>
                                                        </div>
                                                        <div class="clear"></div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </div>

                                <!--Padding-->
                                <div class="gdl-pagination">
                                    <cc:Pager ID="pager" runat="server" EnableViewState="true" OnCommand="pager_Command" CompactModePageCount="10" MaxSmartShortCutCount="0" RTL="False" PageSize="9" />

                                </div>
                            </div>

                            <!--Tour trip-->
                            <div class="col-sm-3 col-md-3 col-sm-pull-3 col-md-pull-9">
                                <div id="ctl00_mainContent_ctl00_divCustomTour" class="contact-box widget travelo-box" style="">
                                    <h3 class="widgettitle"><i class="fa fa-cubes fa-fw"></i>Tour trip</h3>
                                    <div class="form-inline">
                                        <asp:Repeater runat="server" ID="rptLeftCate" OnItemDataBound="rptLeftCate_ItemDataBound">
                                            <ItemTemplate>
                                                <div class="checkbox-list">
                                                    <asp:RadioButton CssClass="chkCate" runat="server" name="rdTrip" ID="rdTrip1" AutoPostBack="true" GroupName="rdGroupTrip" OnCheckedChanged="chk_CheckedChanged" />
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:Literal runat="server" ID="ltrRdLoop"></asp:Literal>
                                    </div>
                                </div>

                                <dgc:block_right ID="block_right" runat="server" />
                            </div>
                            <!--Tour trip-->

                        </div>
                        <div class="clear"></div>

                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>


</asp:UpdatePanel>

<cc1:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel1"
    runat="server">
    <Animations>

        <OnUpdating>
            <Parallel duration="0">
                                     
            </Parallel>
        </OnUpdating>    
        <OnUpdated>
            <Parallel duration="0">           
            </Parallel>
        </OnUpdated>     
    </Animations>
</cc1:UpdatePanelAnimationExtender>

<asp:HiddenField ID="hidSourceID" runat="server" />


<input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" value="" />
<input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="" />
<script>
   <%-- function SetSource(SourceID) {
        var hidSourceID =
        document.getElementById('<%=hidSourceID.ClientID%>');
        hidSourceID.value = SourceID;
    }--%>
    function __doPostBack(eventTarget, eventArgument) {
        if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
            theForm.__EVENTTARGET.value = eventTarget;
            theForm.__EVENTARGUMENT.value = eventArgument;
            theForm.submit();
        }
    }
</script>

