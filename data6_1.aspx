<%@ Page Title="" Language="C#" MasterPageFile="~/App_MasterLayout/MasterPage_data_t.master"
    AutoEventWireup="true" CodeFile="data6_1.aspx.cs" Inherits="data6_1" %>

<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- jQuery Form Validation -->
    <link rel="stylesheet" href="<%=ResolveUrl("~/js/libs/formValidator/developr.validationEngine.css?v=1")%>">
    <script src="<%=ResolveUrl("~/js/libs/formValidator/jquery.validationEngine.js?v=1")%>"></script>
    <script src="<%=ResolveUrl("~/js/libs/formValidator/languages/jquery.validationEngine-en.js?v=1")%>"></script>
    <script>

        $(document).ready(function () {
            $("#aspnetForm").validationEngine();
            $("#ctl00_Btn_Logout").bind("click", function () {
                $("#aspnetForm").validationEngine('detach');
            });



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
            $("#aspnetForm").validationEngine('detach');
            __doPostBack('Edit', pid);
        }

        function uDel(pid) {
            $("#aspnetForm").validationEngine('detach');

            //        if ($("#aspnetForm").validationEngine('validate')) {
            //            __doPostBack('Del', pid);
            //        } else {
            //            console.log('Error');
            //        }

            if (window.confirm('確認刪除嗎?')) {
                console.log('確認刪除');
                __doPostBack('Del', pid);
                //return true;
            } else {
                console.log('取消刪除');
                return false;
            }


            //console.log(pid);

        }
    </script>
</asp:Content>
<asp:Content ID="Content_content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <hgroup id="main-title" class="thin">
        <h1>
            權限管理/使用者編輯</h1>
    </hgroup>
    <div class="with-padding" >
    <table style="width: 99%">
        <tr style="display: none;">
            <td class="divStyle">
                <asp:Label ID="lab_title" runat="server" Text="權限管理"></asp:Label>
                &nbsp;
                <img src="../images/help.png" width="25px" onmouseover="showhelp();" onmouseout="hidhelp();"
                    style="vertical-align: middle" />
                <div id="helpdiv" style="display: none; position: absolute; background-color: #FFFFCC;
                    font-size: small; color: Blue">
                    <table>
                        <tr>
                            <td>
                                1
                            </td>
                            <td>
                                權限管理可設定登入帳密與角色
                            </td>
                        </tr>
                        <tr>
                            <td>
                                2
                            </td>
                            <td>
                                每個角色之細部功能權限設定請至角色編輯功能
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" class="table_setting" style="text-align: left; font-size: 16px;
                    line-height: 24px;">
                    <tr>
                        <th>
                            帳號
                        </th>
                        <td>
                            <asp:TextBox ID="txt_account" runat="server" CssClass="input small-margin-right" ></asp:TextBox>
                        </td>
                        <th>
                            使用者名稱
                        </th>
                        <td>
                            <asp:TextBox ID="txt_UserName" runat="server" Font-Size="16px" CssClass="input small-margin-right" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="line-height: 10px;">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            密碼
                        </th>
                        <td>
                            <asp:TextBox ID="txt_Password" runat="server" TextMode="Password" Font-Size="16px" CssClass="input small-margin-right" ></asp:TextBox>
                        </td>
                        <th>
                            確認密碼
                        </th>
                        <td>
                            <asp:TextBox ID="txt_Password2" runat="server" TextMode="Password" Font-Size="16px"  CssClass="input small-margin-right" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="line-height: 10px;">
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            備註
                        </th>
                        <td>
                            <asp:TextBox ID="txt_remark" runat="server" Font-Size="16px" CssClass="input small-margin-right" ></asp:TextBox>
                        </td>
                        <th>
                            系統角色
                        </th>
                        <td>
                            <asp:DropDownList ID="ddl_role" runat="server" Font-Size="16px" CssClass="select" >
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
                            <asp:Button ID="btn_save" runat="server" Text="儲存" OnClick="btn_save_Click" Visible="False"
                                CausesValidation="False" CssClass="button" />
                            <asp:Button ID="btn_add" runat="server" Text="新增" OnClick="btn_add_Click" CssClass="button" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        
    </table>
    </div>
    
    <div class="with-padding">
        <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="itemDataBoundRepeater_ItemDataBound"
            OnItemCommand="Repeater1_ItemCommand">
            <HeaderTemplate>
                <table class="table responsive-table" id="mainDataTable" style="font-size: 16px;">
                    <thead>
                        <tr>
                            <th scope="col" width="10%" class="align-center ">
                                帳號
                            </th>
                            <th scope="col" width="10%" class="align-center ">
                                使用者名稱
                            </th>
                            <th scope="col" width="10%" class="align-center ">
                                系統角色
                            </th>
                            <th scope="col" width="10%" class="align-center ">
                                更新時間
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
                        <%# DataBinder.Eval(Container.DataItem, "Account")%>
                    </td>
                    <td class="align-center">
                        <%# DataBinder.Eval(Container.DataItem, "username")%>
                    </td>
                    
                    <td class="align-center">
                        <%# DataBinder.Eval(Container.DataItem, "r_name")%>
                    </td>
                    <td class="align-center">
                        <%# DataBinder.Eval(Container.DataItem, "UTime")%>
                    </td>
                    <td class="align-center">
                        <%--<asp:Button ID="Btn_del" CssClass = "button compact icon-gear" runat="server" Text="刪除資料" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "p_id") %>' />--%>
                        <%--<asp:Button ID="Button2" CssClass = "button compact icon-gear" runat="server" Text="修改資料" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "p_id") %>' />--%>
                        <%--<button runat="server" onclick="javascript:__doPostBack('uxSearch','123')" >DEL  </button>--%>
                        <button id="Button1" class="button compact icon-trash" runat="server" onclick='<%# "return uDel( \"" + DataBinder.Eval(Container.DataItem, "p_id") +"\");"  %>'>
                            DEL
                        </button>
                        <button id="Button3" class="button compact icon-gear" runat="server" onclick='<%# "uEdit( \"" + DataBinder.Eval(Container.DataItem, "p_id") +"\");"  %>'>
                            Edit
                        </button>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                <tfoot>
                    <tr>
                        <td colspan="5">
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
    <footer id="menu-footer" style="display: none">
        <p class="button-height">
            <input type="checkbox" name="auto-refresh" id="auto-refresh" checked="checked" class="switch float-right">
            <label for="auto-refresh">
                Auto-refresh</label>
        </p>
    </footer>
</asp:Content>
