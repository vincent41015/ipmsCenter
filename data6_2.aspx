<%@ Page Title="" Language="C#" MasterPageFile="~/App_MasterLayout/MasterPage_data_t.master"
    AutoEventWireup="true" CodeFile="data6_2.aspx.cs" Inherits="data6_2" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    function uEdit(pid) {        
        __doPostBack('Edit', pid);
    }

    function uDel(pid) {
        
        if (window.confirm('確認刪除嗎?')) {
            //console.log('確認刪除');
            __doPostBack('Del', pid);
            //return true;
        } else {
            //console.log('取消刪除');
            return false;
        }        

    }
</script>

</asp:Content>
<asp:Content ID="Content_content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <hgroup id="main-title" class="thin">
        <h1>
            權限管理/角色編輯</h1>
    </hgroup>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table width="100%" style="text-align: left; font-size: 16px;">
                <tr>
                    <td width="10%">
                    </td>
                    <td width="80%">
                        <table width="100%" class="table_setting">
                            <tr>
                                <th>
                                    角色名稱
                                </th>
                                <td>
                                    <asp:TextBox ID="txt_RoleName" runat="server" Font-Size="16px" CssClass="input small-margin-right" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_RoleName"
                                        ErrorMessage="請輸入角色名稱"></asp:RequiredFieldValidator>
                                </td>
                                <th>
                                    描述
                                </th>
                                <td>
                                    <asp:TextBox ID="txt_Describe" runat="server" Font-Size="16px" CssClass="input small-margin-right" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    權限等級
                                </th>
                                <td>
                                    <asp:DropDownList ID="ddl_rlevel" runat="server" CssClass="select" >
                                    </asp:DropDownList>
                                    <asp:Label ID="Lb_rlevel" runat="server" Text="" Visible="false" ></asp:Label>
                                </td>
                                <th>
                                    <%--type--%>
                                </th>
                                <td>
                                    <asp:DropDownList ID="ddl_rtype" runat="server" Visible="false" >
                                    </asp:DropDownList>

                                </td>

                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center">
                                    <asp:Button ID="btn_add" runat="server" Text="新增" OnClick="btn_add_Click" CssClass="button" />
                                    <asp:Button ID="btn_save" runat="server" Text="儲存" OnClick="btn_save_Click" Visible="False"
                                        CssClass="button" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" Width="100%"
                                        OnRowCommand="GridView3_RowCommand" OnRowDataBound="GridView3_RowDataBound" DataKeyNames="r_id"
                                        AllowPaging="True" OnPageIndexChanging="GridView3_PageIndexChanging" CellPadding="3"
                                        GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
                                        BorderWidth="1px" EnableModelValidation="True" ForeColor="Black" Visible="false" >
                                        <RowStyle CssClass="gridview_row1" />
                                        <Columns>
                         
                                            <asp:BoundField HeaderText="角色名稱" DataField="r_name" />
                                            <asp:BoundField HeaderText="描述" DataField="r_describe" />
                                            <asp:BoundField HeaderText="權限等級" DataField="r_level" />
                                            
                                            <asp:ButtonField Text="編輯" CommandName="RoleEdit" ButtonType="Image" HeaderText="修改"
                                                ImageUrl="~/images/1749915572235274321_s.PNG" />
                                            <asp:TemplateField HeaderText="刪除">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" CommandName="del" CommandArgument='<%# Eval("r_id") %>'
                                                        ImageUrl="~/images/red_minus white.png" OnClientClick="return confirm('確定刪除?');" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:ButtonField Text="編輯權限" CommandName="PermisEdit" />
                                        </Columns>
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <PagerStyle HorizontalAlign="Center" CssClass="gridview_head" BackColor="#999999"
                                            ForeColor="Black" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle CssClass="gridview_head" Font-Bold="True" ForeColor="White" BackColor="Black" />
                                        <AlternatingRowStyle CssClass="gridview_row2" BackColor="#CCCCCC" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="10%">
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_add" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btn_save" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="with-padding">
        <asp:Repeater ID="Repeater1" runat="server" >
            <HeaderTemplate>
                <table class="table responsive-table" id="mainDataTable" style="font-size: 16px;">
                    <thead>
                        <tr>
                            <th scope="col" width="10%" class="align-center ">
                                角色名稱
                            </th>
                            <th scope="col" width="10%" class="align-center ">
                                描述
                            </th>
                            <th scope="col" width="10%" class="align-center ">
                                權限等級
                            </th>                            
                            <th scope="col" width="20%" class="align-center ">
                                Action
                            </th>
                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td class="align-center">
                        <%# DataBinder.Eval(Container.DataItem, "r_name")%>
                    </td>
                    <td class="align-center">
                        <%# DataBinder.Eval(Container.DataItem, "r_describe")%>
                    </td>
                    <td class="align-center">
                        <%# DataBinder.Eval(Container.DataItem, "r_level")%>
                    </td>                    
                    <td class="align-center">
                        <%--<asp:Button ID="Btn_del" CssClass = "button compact icon-gear" runat="server" Text="刪除資料" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "p_id") %>' />--%>
                        <%--<asp:Button ID="Button2" CssClass = "button compact icon-gear" runat="server" Text="修改資料" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "p_id") %>' />--%>
                        <%--<button runat="server" onclick="javascript:__doPostBack('uxSearch','123')" >DEL  </button>--%>
                        <button id="Button1" Class = "button compact icon-trash"  runat="server" onclick='<%# "return uDel( \"" + DataBinder.Eval(Container.DataItem, "r_id") +"\");"  %>' >DEL  </button>
                        <button id="Button2" Class = "button compact icon-gear"  runat="server"  onclick='<%# "uEdit( \"" + DataBinder.Eval(Container.DataItem, "r_id") +"\");"  %>' >Edit  </button>
                        <button id="Button3" Class = "button compact icon-list-add"  runat="server"  onclick='<%# "uEdit( \"" + DataBinder.Eval(Container.DataItem, "r_id") +"\");"  %>' >Setting  </button>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <tfoot>
                    <tr>
                        <td colspan="4">
                            <%--<asp:Literal ID="Li_count" runat="server" Text="0"></asp:Literal>entries found--%>
                        </td>
                    </tr>
                </tfoot>
                </tbody> </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>

    <div class="with-padding" >
        <br />
        <br />
        <a   href="javascript:history.back()" class="button icon-reply" >上一頁</a>  <a href="javascript:window.history.forward()" class="button icon-fwd" >下一頁</a>  
    </div>

</asp:Content>
<asp:Content ID="Content_shortcut" ContentPlaceHolderID="ContentPlaceHolder_shortcut"
    runat="Server">
    <!-- Side tabs shortcuts -->
    <%--<ul id="shortcuts" role="complementary" class="children-tooltip tooltip-right">
		    <li><a href="#" onclick="menulink('main.aspx');" class="shortcut-dashboard" title="資訊看版">資訊看版</a></li>
		    <li><a href="#"  onclick="menulink('data1.aspx');" class="shortcut-messages" title="出入明細">出入明細</a></li>
		    <li><a href="#" onclick="menulink('data2.aspx');" class="shortcut-agenda" title="登錄資料">登錄資料</a></li>
		    <li><a href="#" onclick="menulink('data3.aspx');" class="shortcut-contacts" title="區域明細">區域明細</a></li>
		    <li><a href="#" onclick="menulink('data4.aspx');" class="shortcut-medias" title="車位查詢">車位查詢</a></li>
		    <li><a href="#" onclick="menulink('data5.aspx');" class="shortcut-stats" title="管理報表">管理報表</a></li>
		    <li class="at-bottom current"><a href="#" onclick="menulink('data6.aspx');" class="shortcut-settings" title="權限管理">權限管理</a></li>
		    <li><span class="shortcut-notes" title="系統設定">系統設定</span></li>
	    </ul>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" runat="Server">
    <!-- This is optional -->
    <footer id="menu-footer" style="display:none" >
        <p class="button-height">
            <input type="checkbox" name="auto-refresh" id="auto-refresh" checked="checked" class="switch float-right">
            <label for="auto-refresh">
                Auto-refresh</label>
        </p>
    </footer>
</asp:Content>
