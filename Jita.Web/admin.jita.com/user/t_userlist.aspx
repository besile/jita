<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="t_userlist.aspx.cs" Inherits="user_t_userlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <form class="form-inline definewidth m20" action="t_userlist.aspx" method="get">
    角色名称：
    <input type="text" name="rolename" id="rolename" class="abc input-default" placeholder=""
        value="" />&nbsp;&nbsp;
    <button type="submit" class="btn btn-primary">
        查询</button>&nbsp;&nbsp;
    <button type="button" class="btn btn-success" id="addnew">
        新增角色</button>
    </form>
    <table class="table table-bordered table-hover definewidth m10">
        <thead>
            <tr>
                <th>
                    角色id
                </th>
                <th>
                    角色名称
                </th>
                <th>
                    状态
                </th>
                <th>
                    操作
                </th>
            </tr>
        </thead>
        <tr>
            <td>
                5
            </td>
            <td>
                管理员
            </td>
            <td>
                1
            </td>
            <td>
                <a href="t_useredit.aspx">编辑</a>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="Server">
    <script type="text/javascript">
        $(function () {
            $('#addnew').click(function () {
                window.location.href = "t_useradd.aspx";
            });
        });
        function del(id) {
            if (confirm("确定要删除吗？")) {
                var url = "index.html";
                window.location.href = url;
            }
        }
    </script>
</asp:Content>
