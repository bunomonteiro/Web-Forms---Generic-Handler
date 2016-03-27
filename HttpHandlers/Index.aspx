<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="HttpHandlers.Index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Http Handlers</title>
    <style> html, body { background: no-repeat center center fixed;  -webkit-background-size: cover; -moz-background-size: cover; -o-background-size: cover; -ms-background-size: cover; background-size: cover; height: 100%; overflow: hidden; width: 100% } </style>
</head>
<body>
    <script>
        var body = document.querySelector("body"),
            xhr = new XMLHttpRequest();

        xhr.open('GET', '<%= this.ResolveUrl("~/Handlers/Image/") %>', true);
        xhr.responseType = 'blob';
        xhr.onreadystatechange = function(event) {
            if (this.readyState === 4 && this.status === 200) {
                body.style.backgroundImage = "url('"+ window.URL.createObjectURL(this.response) +"')";
            }
        };

        xhr.send();
    </script>
</body>
</html>