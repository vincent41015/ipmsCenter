<%@ Page Title="" Language="C#" MasterPageFile="~/App_MasterLayout/MasterPage_data_t.master"
    AutoEventWireup="true" CodeFile="data6_3.aspx.cs" Inherits="data6_3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content_content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <hgroup id="main-title" class="thin">
        <h1>
            權限管理/角色編輯/功能設定</h1>
    </hgroup>
    <table width="100%" style="text-align: left; font-size: 16px;">
        <tr>
            <td width="10%">
            </td>
            <td width="80%">
                <table width="100%">
                    <tr>
                        <td>
                            <asp:Button ID="btn_selectAll" runat="server" Text="全選" OnClientClick="return checkAll()"
                                CssClass="button" UseSubmitBehavior="False" />
                            <asp:Button ID="btn_disselectAll" runat="server" Text="取消全選" OnClientClick=" return discheckAll() "
                                CssClass="button" UseSubmitBehavior="False" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <!--
                        <tr>
                            <td>
                                <asp:GridView ID="GridView2" runat="server" Height="100%" Width="100%" AutoGenerateColumns="False"
                                    OnRowDataBound="GridView2_RowDataBound" DataKeyNames="f_id" 
                                    CellPadding="3" GridLines="Vertical" BackColor="White" 
                                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
                                    EnableModelValidation="True" ForeColor="Black">
                                    <RowStyle CssClass="gridview_row1" />
                                    <Columns>
                                        <asp:BoundField HeaderText="功能名稱" DataField="f_name" />
                                        <asp:TemplateField HeaderText="操作權限">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cb_operate_enable" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <PagerStyle HorizontalAlign="Center" CssClass="gridview_head" 
                                        BackColor="#999999" ForeColor="Black" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle CssClass="gridview_head" Font-Bold="True" ForeColor="White" 
                                        BackColor="Black" />
                                    <AlternatingRowStyle CssClass="gridview_row2" BackColor="#CCCCCC" />
                                </asp:GridView>
                            </td>
                        </tr>
                        -->
                    <tr>
                        <td>
                            <asp:TreeView ID="TreeView1" runat="server" OnTreeNodeCheckChanged="TreeView1_TreeNodeCheckChanged"
                                Font-Size="24px">
                                <NodeStyle NodeSpacing="5px" VerticalPadding="5px" />
                            </asp:TreeView>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button ID="Button3" CssClass="button" runat="server" Text="儲存" OnClick="Button3_Click" />
                            <asp:Button ID="btn_back" CssClass="button" runat="server" Text="上一頁" OnClick="btn_back_Click" />
                        </td>
                    </tr>
                </table>
            </td>
            <td width="10%">
            </td>
        </tr>
    </table>
    <div class="with-padding">
        <br />
        <br />
        <a   href="javascript:history.back()" class="button icon-reply" >上一頁</a>  <a href="javascript:window.history.forward()" class="button icon-fwd" >下一頁</a>  
    </div>
</asp:Content>
<asp:Content ID="Content_shortcut" ContentPlaceHolderID="ContentPlaceHolder_shortcut"
    runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" runat="Server">
    <!-- This is optional -->
    <footer id="menu-footer" style="display: none">
       
    </footer>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder5" runat="server">
    <script>
        $(document).ready(function () {
            //setInterval( checkAlarm , 5000);

            // Table sort - DataTables
            var table = $('#mainDataTable'); //只有針對id=mainDataTable 的table
            //var table = $('table');//針對所有table
            table.dataTable({
                'aoColumnDefs': [
				{ 'bSortable': true, 'aTargets': [0] }
			],
                "iDisplayLength": 25,
                "aLengthMenu": [[25, 50, 100, -1], [25, 50, 100, "All"]],
                'sPaginationType': 'full_numbers',
                'sDom': '<"dataTables_header"lfr>t<"dataTables_footer"ip>',
                'fnInitComplete': function (oSettings) {
                    // Style length select
                    table.closest('.dataTables_wrapper').find('.dataTables_length select').addClass('select blue-gradient glossy').styleSelect();
                    tableStyled = true;
                }
            });
            $('#mainDataTable').show();
        });

    </script>
    <script>
        function checkAll() {
            $("input:checkbox").prop('checked', true);
            return false;
        }

        function discheckAll() {
            $("input:checkbox").prop('checked', false);

            //$("input:checkbox").removeAttr('checked');

            return false;
        }
    </script>
</asp:Content>
