﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="t_useradd.aspx.cs" Inherits="user_t_useradd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <form action="t_useradd.aspx" method="post" class="definewidth m20">
    <table class="table table-bordered table-hover definewidth m10">
        <tr>
            <td width="10%" class="tableleft">
                登录名
            </td>
            <td>
                <input type="text" name="username" />
            </td>
        </tr>
        <tr>
            <td class="tableleft">
                密码
            </td>
            <td>
                <input type="password" name="password" />
            </td>
        </tr>
        <tr>
            <td class="tableleft">
                真实姓名
            </td>
            <td>
                <input type="text" name="realname" />
            </td>
        </tr>
        <tr>
            <td class="tableleft">
                邮箱
            </td>
            <td>
                <input type="text" name="email" />
            </td>
        </tr>
        <tr>
            <td class="tableleft">
                状态
            </td>
            <td>
                <input type="radio" name="status" value="1" checked="checked" />
                启用
                <input type="radio" name="status" value="0" />
                禁用
            </td>
        </tr>
        <tr>
            <td class="tableleft">
                角色
            </td>
            <td>
                {$role_checkbox}
            </td>
        </tr>
        <tr>
            <td class="tableleft">
            </td>
            <td>
                <button type="submit" class="btn btn-primary" >
                    保存</button>
                &nbsp;&nbsp;
                <button type="button" class="btn btn-success" name="backid" id="backid">
                    返回列表</button>
            </td>
        </tr>
    </table>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="foot" runat="Server">
</asp:Content>
