<%@ Page Language="C#" AutoEventWireup="true" %>

<%
Response.ContentType="text/event-stream";
Response.Expires = -1;
%>
retry:3000;
data: Demo at Time <%= Date.Now.ToLongTimeString() %>