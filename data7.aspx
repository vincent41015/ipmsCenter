<%@ Page Title="" Language="C#" MasterPageFile="~/App_MasterLayout/MasterPage_data.master" AutoEventWireup="true" CodeFile="data7.aspx.cs" Inherits="data7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>


<asp:Content ID="Content_content" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 

		<hgroup id="main-title" class="thin">
			<h1>登錄資料</h1> 
		</hgroup>

		<div class="with-padding">

			<table class="table responsive-table" id="mainDataTable">

				<thead>
					<tr>
						<th scope="col"><input type="checkbox" name="check-all" id="check-all" value="1"></th>
						<th scope="col">Text</th>
						<th scope="col" width="15%" class="align-center hide-on-mobile">Date</th>
						<th scope="col" width="15%" class="align-center hide-on-mobile-portrait">Status</th>
						<th scope="col" width="15%" class="hide-on-tablet">Tags</th>
						<th scope="col" width="60" class="align-center">Actions</th>
					</tr>
				</thead>

				<tfoot>
					<tr>
						<td colspan="6">
							6 entries found
						</td>
					</tr>
				</tfoot>

				<tbody>
					<tr>
						<th scope="row" class="checkbox-cell"><input type="checkbox" name="checked[]" id="check-1" value="1"></th>
						<td>John Doe</td>
						<td>Jul 5, 2011</td>
						<td>Enabled</td>
						<td><small class="tag">User</small> <small class="tag">Client</small> <small class="tag green-bg">Valid</small></td>
						<td class="low-padding align-center"><a href="#" class="button compact icon-gear">Edit</a></td>
					</tr>
					<tr>
						<th scope="row" class="checkbox-cell"><input type="checkbox" name="checked[]" id="check-2" value="2"></th>
						<td>John Appleseed</td>
						<td>Jul 5, 2011</td>
						<td>Enabled</td>
						<td><small class="tag orange-bg">Non-verified</small></td>
						<td class="low-padding align-center"><a href="#" class="button compact icon-gear">Edit</a></td>
					</tr>
					<tr>
						<th scope="row" class="checkbox-cell"><input type="checkbox" name="checked[]" id="check-3" value="3"></th>
						<td>Sheldon Cooper</td>
						<td>Jul 4, 2011</td>
						<td>Enabled</td>
						<td><small class="tag">User</small> <small class="tag green-bg">Valid</small></td>
						<td class="low-padding align-center"><a href="#" class="button compact icon-gear">Edit</a></td>
					</tr>
					<tr>
						<th scope="row" class="checkbox-cell"><input type="checkbox" name="checked[]" id="check-4" value="4"></th>
						<td>Rage Guy</td>
						<td>Jun 25, 2011</td>
						<td>Enabled</td>
						<td><small class="tag red-bg">Fake</small></td>
						<td class="low-padding align-center"><a href="#" class="button compact icon-gear">Edit</a></td>
					</tr>
					<tr>
						<th scope="row" class="checkbox-cell"><input type="checkbox" name="checked[]" id="check-5" value="5"></th>
						<td>Thomas A. Anderson</td>
						<td>Jun 16, 2011</td>
						<td>Enabled</td>
						<td><small class="tag">User</small> <small class="tag">Client</small> <small class="tag green-bg">Valid</small></td>
						<td class="low-padding align-center"><a href="#" class="button compact icon-gear">Edit</a></td>
					</tr>
					<tr>
						<th scope="row" class="checkbox-cell"><input type="checkbox" name="checked[]" id="check-6" value="6"></th>
						<td>Jane Doe</td>
						<td>May 19, 2011</td>
						<td>Enabled</td>
						<td><small class="tag">User</small> <small class="tag">Client</small></td>
						<td class="low-padding align-center"><a href="#" class="button compact icon-gear">Edit</a></td>
					</tr>
					<tr>
						<th scope="row" class="checkbox-cell"><input type="checkbox" name="checked[]" id="check-1" value="1"></th>
						<td>John Doe</td>
						<td>Jul 5, 2011</td>
						<td>Enabled</td>
						<td><small class="tag">User</small> <small class="tag">Client</small> <small class="tag green-bg">Valid</small></td>
						<td class="low-padding align-center"><a href="#" class="button compact icon-gear">Edit</a></td>
					</tr>
					<tr>
						<th scope="row" class="checkbox-cell"><input type="checkbox" name="checked[]" id="check-2" value="2"></th>
						<td>John Appleseed</td>
						<td>Jul 5, 2011</td>
						<td>Enabled</td>
						<td><small class="tag orange-bg">Non-verified</small></td>
						<td class="low-padding align-center"><a href="#" class="button compact icon-gear">Edit</a></td>
					</tr>
					<tr>
						<th scope="row" class="checkbox-cell"><input type="checkbox" name="checked[]" id="check-3" value="3"></th>
						<td>Sheldon Cooper</td>
						<td>Jul 4, 2011</td>
						<td>Enabled</td>
						<td><small class="tag">User</small> <small class="tag green-bg">Valid</small></td>
						<td class="low-padding align-center"><a href="#" class="button compact icon-gear">Edit</a></td>
					</tr>
					<tr>
						<th scope="row" class="checkbox-cell"><input type="checkbox" name="checked[]" id="check-4" value="4"></th>
						<td>Rage Guy</td>
						<td>Jun 25, 2011</td>
						<td>Enabled</td>
						<td><small class="tag red-bg">Fake</small></td>
						<td class="low-padding align-center"><a href="#" class="button compact icon-gear">Edit</a></td>
					</tr>
					<tr>
						<th scope="row" class="checkbox-cell"><input type="checkbox" name="checked[]" id="check-5" value="5"></th>
						<td>Thomas A. Anderson</td>
						<td>Jun 16, 2011</td>
						<td>Enabled</td>
						<td><small class="tag">User</small> <small class="tag">Client</small> <small class="tag green-bg">Valid</small></td>
						<td class="low-padding align-center"><a href="#" class="button compact icon-gear">Edit</a></td>
					</tr>
					<tr>
						<th scope="row" class="checkbox-cell"><input type="checkbox" name="checked[]" id="check-6" value="6"></th>
						<td>Jane Doe</td>
						<td>May 19, 2011</td>
						<td>Enabled</td>
						<td><small class="tag">User</small> <small class="tag">Client</small></td>
						<td class="low-padding align-center"><a href="#" class="button compact icon-gear">Edit</a></td>
					</tr>
					<tr>
						<th scope="row" class="checkbox-cell"><input type="checkbox" name="checked[]" id="check-1" value="1"></th>
						<td>John Doe</td>
						<td>Jul 5, 2011</td>
						<td>Enabled</td>
						<td><small class="tag">User</small> <small class="tag">Client</small> <small class="tag green-bg">Valid</small></td>
						<td class="low-padding align-center"><a href="#" class="button compact icon-gear">Edit</a></td>
					</tr>
					<tr>
						<th scope="row" class="checkbox-cell"><input type="checkbox" name="checked[]" id="check-2" value="2"></th>
						<td>John Appleseed</td>
						<td>Jul 5, 2011</td>
						<td>Enabled</td>
						<td><small class="tag orange-bg">Non-verified</small></td>
						<td class="low-padding align-center"><a href="#" class="button compact icon-gear">Edit</a></td>
					</tr>
					<tr>
						<th scope="row" class="checkbox-cell"><input type="checkbox" name="checked[]" id="check-3" value="3"></th>
						<td>Sheldon Cooper</td>
						<td>Jul 4, 2011</td>
						<td>Enabled</td>
						<td><small class="tag">User</small> <small class="tag green-bg">Valid</small></td>
						<td class="low-padding align-center"><a href="#" class="button compact icon-gear">Edit</a></td>
					</tr>
					<tr>
						<th scope="row" class="checkbox-cell"><input type="checkbox" name="checked[]" id="check-4" value="4"></th>
						<td>Rage Guy</td>
						<td>Jun 25, 2011</td>
						<td>Enabled</td>
						<td><small class="tag red-bg">Fake</small></td>
						<td class="low-padding align-center"><a href="#" class="button compact icon-gear">Edit</a></td>
					</tr>
					<tr>
						<th scope="row" class="checkbox-cell"><input type="checkbox" name="checked[]" id="check-5" value="5"></th>
						<td>Thomas A. Anderson</td>
						<td>Jun 16, 2011</td>
						<td>Enabled</td>
						<td><small class="tag">User</small> <small class="tag">Client</small> <small class="tag green-bg">Valid</small></td>
						<td class="low-padding align-center"><a href="#" class="button compact icon-gear">Edit</a></td>
					</tr>
					<tr>
						<th scope="row" class="checkbox-cell"><input type="checkbox" name="checked[]" id="check-6" value="6"></th>
						<td>Jane Doe</td>
						<td>May 19, 2011</td>
						<td>Enabled</td>
						<td><small class="tag">User</small> <small class="tag">Client</small></td>
						<td class="low-padding align-center"><a href="#" class="button compact icon-gear">Edit</a></td>
					</tr>
				</tbody>

			</table>


		</div>

		 
</asp:Content>

<asp:Content ID="Content_shortcut" ContentPlaceHolderID="ContentPlaceHolder_shortcut" Runat="Server">

	    <!-- Side tabs shortcuts -->
	     <ul id="shortcuts" role="complementary" class="children-tooltip tooltip-right">
		    <li><a href="#" onclick="menulink('main.aspx');" class="shortcut-dashboard" title="資訊看版">資訊看版</a></li>
		    <li><a href="#"  onclick="menulink('data1.aspx');" class="shortcut-messages" title="出入明細">出入明細</a></li>
		    <li><a href="#" onclick="menulink('data2.aspx');" class="shortcut-agenda" title="登錄資料">登錄資料</a></li>
		    <li><a href="#" onclick="menulink('data3.aspx');" class="shortcut-contacts" title="區域明細">區域明細</a></li>
		    <li><a href="#" onclick="menulink('data4.aspx');" class="shortcut-medias" title="車位查詢">車位查詢</a></li>
		    <li><a href="#" onclick="menulink('data5.aspx');" class="shortcut-stats" title="管理報表">管理報表</a></li>
            <li><a href="#" onclick="menulink('data8.aspx');" class="shortcut-stats" title="系統設定">系統設定</a></li>
		    <li class="at-bottom"><a href="#" onclick="menulink('data6.aspx');" class="shortcut-settings" title="權限管理">權限管理</a></li>
		    <%--<li class="current"><span class="shortcut-notes" title="系統設定">系統設定</span></li>--%>
	    </ul>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

	 

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" Runat="Server">

	  

</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" Runat="Server">

	    <!-- This is optional -->
		<footer id="menu-footer">
			<p class="button-height">
				<input type="checkbox" name="auto-refresh" id="auto-refresh" checked="checked" class="switch float-right">
				<label for="auto-refresh">Auto-refresh</label>
			</p>
		</footer> 

</asp:Content>


	