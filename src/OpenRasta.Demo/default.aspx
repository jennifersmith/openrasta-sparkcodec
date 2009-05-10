<%@ Page Language="C#" AutoEventWireup="false" Inherits="System.Web.UI.Page"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title></title>
    <meta http-equiv="Refresh" content="0;url=/home" />
</head>
<body>
</body>
</html>

<%--
The internal visual studio web server (cassini) doesn't correctly initialize http
modules and handlers for the root of a web project. Declare the home resource on
both "/" and "/home" uris and ensure default.aspx has a meta refresh to redirect automatically.
--%>