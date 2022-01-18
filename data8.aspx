<%@ Page Title="" Language="C#" MasterPageFile="~/App_MasterLayout/MasterPage_data.master"
    AutoEventWireup="true" CodeFile="data8.aspx.cs" Inherits="data8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content_content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <hgroup id="main-title" class="thin">
        <h1>
            系統設定</h1>
    </hgroup>
    <div class="with-padding">
        <h3>
            <hr />
            <a href="System_Settings/TimeSetting.aspx" class="button huge "><span class="button-icon"><span
                class="icon-page"></span></span>時段設定</a>
            <br />
            <br />
            <a href="System_Settings/EmailSetting.aspx" class="button huge "><span class="button-icon"><span
                class="icon-page"></span></span>Email設定</a>
            <br />
            <hr />
        </h3>
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
            <li class="current"><a href="#" onclick="menulink('data8.aspx');" class="shortcut-notes" title="系統設定">系統設定</a></li>
		    <li class="at-bottom"><a href="#" onclick="menulink('data6.aspx');" class="shortcut-settings" title="權限管理">權限管理</a></li>
		    <li><span class="shortcut-notes" title="系統設定">系統設定</span></li>
	    </ul>
    --%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" runat="Server">
    <!-- This is optional -->
    <%--<footer id="menu-footer">
			<p class="button-height">
				<input type="checkbox" name="auto-refresh" id="auto-refresh" checked="checked" class="switch float-right">
				<label for="auto-refresh">Auto-refresh</label>
			</p>
		</footer>--%>
</asp:Content>
