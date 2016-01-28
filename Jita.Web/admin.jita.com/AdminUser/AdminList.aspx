<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AdminList.aspx.cs" Inherits="AdminUser_AdminList" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function UpdateStatus(id) {
            $.ajax({
                type: "POST",    //页面请求的类型，通常使用POST,那么处理页需要使用Request.Form["参数名称"]来获取页面传递的参数
                url: "/AjaxOper/UserDelete.ashx",   //处理页的相对地址
                data: { id: id, status: 0 },
                success: function (msg) {    //这是处理后执行的函数，msg是处理页返回的数据
                    alert(msg);
                    location.reload(true);
                }
            });
        }
    </script>
    <!--     starts -->
    <div>
        <ul class="breadcrumb">
            <li><a href="../Default.aspx">首页</a> <span class="divider">/</span> </li>
            <li><a href="javascript:;">用户列表</a> </li>
            <li><a href="#" class="btn btn-info btn-setting">点击弹框</a></li>
            <li class="pull-right"><a href="AdminUserAdd.aspx">添加</a></li>
        </ul>
    </div>
    <div class="row-fluid sortable">
        <div class="box span12">
            <div class="box-content">
                <asp:Repeater ID="tableContent" runat="server">
                    <HeaderTemplate>
                        <table class="table table-striped table-bordered bootstrap-datatable datatable">
                            <thead>
                                <tr>
                                    <th>
                                        姓名
                                    </th>
                                    <th>
                                        年龄
                                    </th>
                                    <th>
                                        性别
                                    </th>
                                     <th>
                                        状态
                                    </th>
                                    <th>
                                        操作
                                    </th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <a href="#" class="btn btn-danger" data-rel="popover" data-content="<%#DataBinder.Eval(Container.DataItem,"Decription") %>"
                                    title="描述">
                                    <%#DataBinder.Eval(Container.DataItem,"Name") %></a>
                            </td>
                            <td class="center">
                                <%#DataBinder.Eval(Container.DataItem, "Age")%>
                            </td>
                            <td class="center">
                                <%#DataBinder.Eval(Container.DataItem, "Sex")%>
                            </td>
                             <td class="center">
                                <%#DataBinder.Eval(Container.DataItem, "Status")%>
                            </td>
                            <td class="center">
                                <%-- <a class="btn btn-success" href="#"><i class="icon-zoom-in icon-white"></i>查看 </a>--%>
                                <a class="btn btn-info" href="AdminUserEdit.aspx"><i class="icon-edit icon-white"></i>
                                    修改 </a><a style="margin-left: 10px;" class="btn btn-danger" onclick="UpdateStatus(<%#DataBinder.Eval(Container.DataItem,"Age") %>)"
                                        href="javascript:void(0);"><i class="icon-trash icon-white"></i>删除 </a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
        <!--/span-->
    </div>
    <!--/row-->
    <!-- content ends -->
       <!-- jQuery -->
    <script src="../jsjquery-1.7.2.min.js"></script>
    <!-- jQuery UI -->
    <script src="../jsjquery-ui-1.8.21.custom.min.js"></script>
    <!-- transition / effect library -->
    <script src="../jsbootstrap-transition.js"></script>
    <!-- alert enhancer library -->
    <script src="../jsbootstrap-alert.js"></script>
    <!-- modal / dialog library -->
    <script src="../jsbootstrap-modal.js"></script>
    <!-- custom dropdown library -->
    <script src="../jsbootstrap-dropdown.js"></script>
    <!-- scrolspy library -->
    <script src="../jsbootstrap-scrollspy.js"></script>
    <!-- library for creating tabs -->
    <script src="../jsbootstrap-tab.js"></script>
    <!-- library for advanced tooltip -->
    <script src="../jsbootstrap-tooltip.js"></script>
    <!-- popover effect library -->
    <script src="../jsbootstrap-popover.js"></script>
    <!-- button enhancer library -->
    <script src="../jsbootstrap-button.js"></script>
    <!-- accordion library (optional, not used in demo) -->
    <script src="../jsbootstrap-collapse.js"></script>
    <!-- carousel slideshow library (optional, not used in demo) -->
    <script src="../jsbootstrap-carousel.js"></script>
    <!-- autocomplete library -->
    <script src="../jsbootstrap-typeahead.js"></script>
    <!-- tour library -->
    <script src="../jsbootstrap-tour.js"></script>
    <!-- library for cookie management -->
    <script src="../jsjquery.cookie.js"></script>
    <!-- calander plugin -->
    <script src='../jsfullcalendar.min.js'></script>
    <!-- data table plugin -->
    <script src='../jsjquery.dataTables.min.js'></script>
    <!-- chart libraries start -->
    <script src="../jsexcanvas.js"></script>
    <script src="../jsjquery.flot.min.js"></script>
    <script src="../jsjquery.flot.pie.min.js"></script>
    <script src="../jsjquery.flot.stack.js"></script>
    <script src="../jsjquery.flot.resize.min.js"></script>
    <!-- chart libraries end -->
    <!-- select or dropdown enhancer -->
    <script src="../jsjquery.chosen.min.js"></script>
    <!-- checkbox, radio, and file input styler -->
    <script src="../jsjquery.uniform.min.js"></script>
    <!-- plugin for gallery image view -->
    <script src="../jsjquery.colorbox.min.js"></script>
    <!-- rich text editor library -->
    <script src="../jsjquery.cleditor.min.js"></script>
    <!-- notification plugin -->
    <script src="../jsjquery.noty.js"></script>
    <!-- file manager library -->
    <script src="../jsjquery.elfinder.min.js"></script>
    <!-- star rating plugin -->
    <script src="../jsjquery.raty.min.js"></script>
    <!-- for iOS style toggle switch -->
    <script src="../jsjquery.iphone.toggle.js"></script>
    <!-- autogrowing textarea plugin -->
    <script src="../jsjquery.autogrow-textarea.js"></script>
    <!-- multiple file upload plugin -->
    <script src="../jsjquery.uploadify-3.1.min.js"></script>
    <!-- history.js for cross-browser state change on ajax -->
    <script src="../jsjquery.history.js"></script>
    <!-- application script for Charisma demo -->
    <script src="../jscharisma.js"></script>
</asp:Content>
