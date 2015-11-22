<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EasyLid.aspx.cs" Inherits="Maticsoft.Web.EasyLid" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>短信兑换平台</title>
    <style type="text/css">
        /* 选项卡关键属性 */
        #tab
        {
            width: 100%;
            height: 100%;
            position: relative;
        }
        /*设置容器高宽等*/
        html > body #tab
        {
            width: 100%;
            height: 100%;
        }
        /*兼容IE6:IE6下宽度不够*/
        #tab div
        {
            position: absolute;
            top: 26px;
            left: 0;
            width: 100%;
            height: 100%;
            border: solid #eee;
            border-width: 0 1px 1px;
        }
        
        /*选中的被操作容器*/
        #tab h3
        {
            float: left;
            width: 114px;
            height: 26px;
            line-height: 26px;
            margin: 0 -1px 0 0;
            font-size: 14px;
            cursor: pointer;
            font-weight: normal;
            text-align: center;
            color: #00007F;
            background: #eee url(Images/block.gif) no-repeat;
        }
        /*默认标题样式*/
        #tab .up
        {
            background: #fff url(Images/up.gif) no-repeat;
        }
        #subframe1, #subframe2
        {
            width: 100%;
            height: 800px;
        }
    </style>
    <script type="text/javascript">

        function go_to(ao) {
            if (ao == 1) {
                document.getElementById("subframe1").style.display = "block";
                document.getElementById("subframe2").style.display = "none";
                document.getElementById("h1").className = "up";
                document.getElementById("h2").className = "block";
            }
            if (ao == 2) {
                document.getElementById("subframe2").style.display = "block";
                document.getElementById("subframe1").style.display = "none";
                document.getElementById("h2").className = "up";
                document.getElementById("h1").className = "block";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="tab">
        <h3 id="h1" class="up" onclick="go_to(1);">
            呼叫中心</h3>
        <h3 id="h2" onclick="go_to(2);">
            兑换码信息</h3>
        <div style="width: 100%; height: 1000px">
            <iframe id="subframe1" src="CallCenter.aspx"></iframe>
            <iframe id="subframe2" src="CodeExchange.aspx" style="display:none"></iframe>
        </div>
    </div>
    </form>
</body>
</html>
