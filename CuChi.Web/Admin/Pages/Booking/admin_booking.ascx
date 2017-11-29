<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="admin_booking.ascx.cs"
    Inherits="Cb.Web.Admin.Pages.Booking.admin_booking" %>

<%@ Register Namespace="Cb.WebControls" Assembly="Cb.WebControls" TagPrefix="cc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<script language="javascript" type="text/javascript">
    function submitButton(task) {
        var frm = document.getElementById('aspnetForm');
        //alert(task);
        if (task == 'new' || task == 'search' || task == 'edit' || task == 'delete' || task == 'publish' || task == 'unpublish'            
            || ('<%=msg_no_selected_item%>')) {
            switch (task) {
                case 'delete':
                    if (!confirm('<%=msg_confirm_delete_item%>')) {
                        break;
                    }
                default:
                    submitForm(frm, task);
            }
        }
    }
</script>

<!-- Event btn-->
<section class="content-header">
    <div class="row">

        <div class="col-lg-8 col-xs-12 btnGroup">

            <a href="javascript:submitButton('new');" class="btn green-haze btn-outline btn-circle btn-sm">
                <i class="fa fa-plus"></i>
                <span class="hidden-xs">
                    <asp:Literal ID="ltrAdminAddNew" runat="server" Text="Thêm mới"></asp:Literal>
                </span>
            </a>

            <div class="btn-group">
                <a class="btn green-haze btn-outline btn-circle btn-sm" href="javascript:;" data-toggle="dropdown" data-hover="dropdown" data-close-others="true" aria-expanded="true">Chọn thao tác
                    <i class="fa fa-angle-down"></i>
                </a>
                <ul class="dropdown-menu pull-right">
                    <li>
                        <a class="" href="javascript:submitButton('publish');">
                            <i class="fa fa-check-circle-o fa-fw"></i>
                            <asp:Literal ID="ltrAdminPublish" runat="server" Text="strAdminPublish"></asp:Literal>
                        </a>
                    </li>
                    <li>
                        <a class="" href="javascript:submitButton('unpublish');">
                            <i class="fa fa-times-circle-o fa-fw"></i>
                            <asp:Literal ID="ltrAminUnPublish" runat="server" Text="strAdminUnpublish"></asp:Literal>
                        </a>
                    </li>
                    <li></li>
                    <li class="divider"></li>
                    <li>
                        <a class=" hidden" href="javascript:submitButton('edit');">
                            <i class="fa fa-pencil fa-fw"></i>
                            <asp:Literal ID="ltrAdminEdit" runat="server" Text="Chỉnh sửa"></asp:Literal>
                        </a>
                    </li>
                    <li>
                        <a class="" href="javascript:submitButton('delete');">
                            <i class="fa fa-trash-o fa-fw"></i>
                            <asp:Literal ID="ltrAdminDelete" runat="server" Text="Xóa"></asp:Literal>
                        </a>
                    </li>
                </ul>
            </div>

            <button runat="server" id="btnSave" class="btn btn-block btn-social btn-twitter hidden"
                title="Lưu" onserverclick="btnSave_Click">
                <i class="fa fa-floppy-o fa-2x"></i>&nbsp Lưu
            </button>

        </div>
    </div>
</section>

<!-- BEGIN show_msg -->
<%--<%=show_msg%>--%>
<!-- END show_msg -->

<section class="content">
    <div class="row1 ">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <div class="form-horizontal">

                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Từ ngày </label>
                            <div class="input-group">
                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <ajax:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtFromDate"
                                    Format="dd/MM/yyyy" Enabled="True">
                                </ajax:CalendarExtender>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Đến ngày </label>
                            <div class="input-group">
                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <ajax:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                                    Format="dd/MM/yyyy" Enabled="True">
                                </ajax:CalendarExtender>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <label style="color: #ecf0f5">Tìm kiếm </label>
                            <div class="input-group">
                                <button runat="server" id="btnSearchDatetime" class="btn btn-default"
                                    title="Lưu" onserverclick="btnSearchDatetime_ServerClick">
                                    <i class="fa fa-floppy-o fa-fw"></i>Tìm kiếm
                                </button>
                            </div>
                        </div>
                    </div>

                    <!--Họ tên--->
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Họ tên</label>
                            <div class="input-group">
                                <input type="text" id="txtFullName" name="txtFullName" class="form-control" runat="server"
                                    onkeypress="return search_keypress(event);" placeholder="Nhập họ tên" />
                                <div class="input-group-btn">
                                    <button id="btnFullName" runat="server" class="btn btn-default" onserverclick="btnFullName_ServerClick"
                                        style="height: 34px !important">
                                        <i class="fa fa-search fa-1x"></i>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--/Họ tên--->

                    <!--Số điện thoại--->
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Số điện thoại </label>
                            <div class="input-group">
                                <input type="text" id="txtSearchPhone" name="txtSearchPhone" class="form-control" runat="server"
                                    onkeypress="return search_keypress(event);" placeholder="Nhập số điện thoại" />
                                <div class="input-group-btn">
                                    <button id="btnSearchPhone" runat="server" class="btn btn-default" onserverclick="btnSearchPhone_ServerClick"
                                        style="height: 34px !important">
                                        <i class="fa fa-search fa-1x"></i>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--/Số điện thoại--->

                </div>

                <div class="clearfix"></div>

                <div class="table-responsive">
                    <table class="table table-bordered table-hover tbl-news">
                        <thead>
                            <tr class="txt-bold tbl-title ">
                                <th width="2%" class="hidden">#
                                </th>
                                <th width="3%" class="title">
                                    <input class="txt" type="checkbox" name="checkedAll" onclick="checkAll(<%=records%>);" />
                                </th>
                                <th class="title">
                                    <asp:Literal ID="ltrAdminHeaderProductCategory" runat="server" Text="Họ tên"></asp:Literal>
                                </th>
                                <th width="15%">
                                    <asp:Literal ID="Literal1" runat="server" Text="Số điện thoại"></asp:Literal>
                                </th>
                                <th width="10%" class="hidden">
                                    <asp:Literal ID="ltrAdminHeaderOrder" runat="server" Text="strOrdering"></asp:Literal>
                                </th>
                                <th width="15%">
                                    <asp:Literal ID="ltrAdminHeaderDate" runat="server" Text="Ngày cập nhật"></asp:Literal>
                                </th>
                                <th width="10%">
                                    <asp:Literal ID="ltrAdminHeaderPublic" runat="server" Text="Đã liên hệ"></asp:Literal>
                                </th>
                            </tr>

                        </thead>
                        <!-- BEGIN list -->
                        <asp:Repeater ID="rptResult" runat="server" OnItemDataBound="rptResult_ItemDataBound">
                            <ItemTemplate>
                                <tr id="trList" runat="server">
                                    <td class="hidden">
                                        <input type="button" id="btId" runat="server" />
                                    </td>
                                    <td>
                                        <asp:Literal ID="ltrchk" runat="server"></asp:Literal>
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hdflink" runat="server">
                                            <asp:Literal ID="ltrName" runat="server"></asp:Literal>
                                        </asp:HyperLink>
                                    </td>
                                    <td>
                                        <asp:Literal ID="txtPhone" runat="server"></asp:Literal>
                                    </td>
                                    <td class="hidden">
                                        <input id="txtOrder" disabled class="form-control text-center txtOrder" runat="server" />
                                    </td>
                                    <td id="trUpdateDate" runat="server">
                                        <%#String.Format("{0:dd/MM/yyyy}", Eval("UpdateDate"))%>
                                    </td>
                                    <td align="center" id="tdbtn" runat="server">
                                        <asp:ImageButton CssClass="toolbar" ID="btnPublish" runat="server" Width="12" Height="12"
                                            ValidationGroup="admincontent" AlternateText="Publish" title="Publish" ImageUrl="~/admdgc/images/write_f2.png" />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>

                    <!-- Begin paging -->
                    <div class="text-right">
                        <cc:Pager ID="pager" runat="server" EnableViewState="true" OnCommand="pager_Command"
                            CompactModePageCount="10" MaxSmartShortCutCount="0" RTL="False" />
                    </div>
                    <!-- End paging -->

                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</section>

<input type="hidden" name="boxchecked" value="0" />
<input type="hidden" name="task" value="" />

<ajax:UpdatePanelAnimationExtender ID="UpdatePanelAnimationExtender1" TargetControlID="UpdatePanel1"
    runat="server">
    <Animations>
                    <OnUpdating>
                       <Parallel duration="0">                            
                                     <ScriptAction Script="OnUpdating();" />                           
                             <EnableAction AnimationTarget="btnSearchDatetime" Enabled="false" />      

                        </Parallel>
                    </OnUpdating>    
                    <OnUpdated>
                        <Parallel duration="0">
                         <ScriptAction Script="OnUpdated();" />
                                <EnableAction AnimationTarget="btnSearchDatetime" Enabled="true" />           
                        </Parallel>
                    </OnUpdated>     
    </Animations>
</ajax:UpdatePanelAnimationExtender>
