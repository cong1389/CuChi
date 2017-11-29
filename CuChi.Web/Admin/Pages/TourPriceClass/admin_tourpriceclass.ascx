<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="admin_tourpriceclass.ascx.cs" Inherits="Cb.Web.Admin.Pages.TourPriceClass.admin_tourpriceclass" %>

<!--TourPriceClass-->

<div id="dvGrid" style="padding: 10px; width: 550px">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:GridView ID="grdTourPriceClass"
                runat="server"
                AutoGenerateColumns="False"
                AutoGenerateEditButton="false"
                AllowPaging="True"
                AllowSorting="True"
                ShowFooter="True"
                OnRowEditing="grdTourPriceClass_RowEditing"
                OnRowUpdating="grdTourPriceClass_RowUpdating"
                OnPageIndexChanging="grdTourPriceClass_PageIndexChanging"
                OnRowCancelingEdit="grdTourPriceClass_RowCancelingEdit"
                OnRowDeleting="grdTourPriceClass_RowDeleted"                
                PagerStyle-CssClass="bs-pagination"
                ShowHeaderWhenEmpty="True"
                EmptyDataText="No Records Found"
                CssClass="table table-bordered table-hover"
                DataKeyNames="ID">
                <Columns>

                    <asp:TemplateField ItemStyle-Width="30px" HeaderText="ID" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbID" runat="server" Text='<%# Eval("ID")%>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtID" Width="40px" MaxLength="5" runat="server"></asp:TextBox>
                        </FooterTemplate>
                    </asp:TemplateField>                    

                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="lbName" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtName" Width="150px" Text='<%# Eval("Name")%>' CssClass="txt" runat="server" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtName" Width="150px" runat="server" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtName" Display="Dynamic"
                                CssClass="text-danger" ErrorMessage="Name field is required." ValidationGroup="InsertTourPriceClass" />
                        </FooterTemplate>
                    </asp:TemplateField>  

                    <asp:CommandField ShowEditButton="True" ValidationGroup="EditTourPriceClass" EditImageUrl="/Admin/images/edit.png"
                        UpdateImageUrl="/Admin/images/update-arrows.png" UpdateText="Update"
                        CancelImageUrl="/Admin/images/cancel.png" CancelText="Cancel"
                        ButtonType="Image" HeaderText="Edit"></asp:CommandField>

                    <asp:CommandField ShowDeleteButton="True" DeleteImageUrl="/Admin/images/thung-rac.png"
                        ButtonType="Image" HeaderText="Remove" />

                    <asp:TemplateField ItemStyle-Width="50px">
                        <FooterTemplate>
                            <asp:Button ID="btnAdd" runat="server" Text="Add" ValidationGroup="InsertTourPriceClass" CssClass="btn btn-primary btn-sm"
                                OnClick="AddNewCustomer" />
                        </FooterTemplate>
                    </asp:TemplateField>

                </Columns>
                <AlternatingRowStyle BackColor="#C2D69B" />

            </asp:GridView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="grdTourPriceClass" />
        </Triggers>
    </asp:UpdatePanel>

</div>

<!--/TourPriceClass-->
