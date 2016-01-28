<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AdminUserAdd.aspx.cs" Inherits="AdminUser_AdminUserAdd" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 <!--     starts -->
    <div>
        <ul class="breadcrumb">
            <li><a href="AdminList.aspx">用户列表</a> <span class="divider">/</span> </li>
            <li><a href="javascript:;">添加</a> </li>           
        </ul>
    </div>
    <div class="row-fluid sortable">
        <div class="box span12">
            <div class="box-content">
                <form class="form-horizontal" action="AdminUserEdit.aspx"  method="post">
                <input type="hidden" id="hdID" runat="server" value="" />
                <fieldset>
                    <div class="control-group">
                        <label class="control-label" for="focusedInput">
                            姓名</label>
                        <div class="controls">
                            <input class="input-xlarge focused" id="focusedInput" runat="server" type="text" >
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label">
                            Uneditable input</label>
                        <div class="controls">
                            <span class="input-xlarge uneditable-input">Some value here</span>
                        </div>
                    </div>
                    <div class="control-group">
                        <label class="control-label" for="disabledInput">
                            Disabled input</label>
                        <div class="controls">
                            <input class="input-xlarge disabled" id="disabledInput" type="text" placeholder="Disabled input here…"
                                disabled="">
                        </div>
                    </div>
                      <div class="control-group">
								<label class="control-label" for="selectError3">Plain Select</label>
								<div class="controls">
								  <select id="selectError3">
									<option>Option 1</option>
									<option>Option 2</option>
									<option>Option 3</option>
									<option>Option 4</option>
									<option>Option 5</option>
								  </select>
								</div>
							  </div>
                    <div class="form-actions">
                        <button type="submit" class="btn btn-primary">
                            保存</button>
                        <button class="btn">
                            取消</button>
                    </div>
                </fieldset>
                </form>
            </div>
        </div>
        <!--/span-->
    </div>
    <!--/row-->
    <!-- content ends -->
</asp:Content>

