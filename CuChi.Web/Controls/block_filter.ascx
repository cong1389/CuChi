<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="block_filter.ascx.cs" Inherits="Cb.Web.Controls.block_filter" %>

<!--block_filter-->
<div class="divFilter">
    <div class="container">
        <div class="search-container type2">
            <div class="dt-sc-tabs-container">
                <div class="dt-sc-tabs-frame-content col-sm-9 center-block col-sm-push-1 col-lg-push-1">
                    <div class="searchCol" style="margin-right: 0px">
                        <div class="">
                            <input type="text" id="txtItem" runat="server" name="txtItem" placeholder="Search terms...">
                        </div>
                    </div>
                    <div class="searchCol" style="width: 29%;">
                        <div class="selection-box">
                            <asp:DropDownList ID="drpDestination" runat="server" CssClass="form-control select">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="searchCol hidden">
                        <div class="selection-box">
                            <asp:DropDownList ID="drpDay" runat="server" CssClass="form-control select">
                                <asp:ListItem Value="" Text="How many days?"></asp:ListItem>
                                <asp:ListItem Value="1" Text="1 day"></asp:ListItem>
                                <asp:ListItem Value="2" Text="2 - 5 days"></asp:ListItem>
                                <asp:ListItem Value="3" Text="6 - 12 days"></asp:ListItem>
                                <asp:ListItem Value="4" Text="13+ days"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <button type='submit' id='btnSubmit' class='btnSearchTrip' onclick="return submitButtonSearchThemes(event)">
                        Find tour</button>
                    <%-- <button id="btnSearch" runat="server" class="btnSearchTrip" onclick="return submitButtonSearchThemes(event)">
                            Find tour
                        </button>--%>
                </div>
            </div>
        </div>
    </div>
</div>


<script>

    //Search Themes
    function submitButtonSearchThemes(task) {
        var txtItem = jQuery("#<% =txtItem.ClientID %>").val();
        var drpDestination = jQuery("#<% =drpDestination.ClientID %> option:selected").val();
        var drpDay = jQuery("#<% =drpDay.ClientID %> option:selected").val();
        var langId = '<%=LangId %>';
        if (txtItem != "" || drpDestination != "" || drpDay != "") {
            window.location = GetLink5Param('search', langId, txtItem, drpDestination, drpDay);
        }
        return false;
    }
</script>


<!--/block_filter-->
