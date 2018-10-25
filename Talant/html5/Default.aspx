<%@ Page Language="C#" AutoEventWireup="true" %>
<%
    Response.ContentType="text/event-stream";
    Response.Expires  = -1;
    Response.Write("data: Demo at Time " + DateTime.Now.Minute.ToString());
    Response.Write("data: The server time is: " + DateTime.Now.ToLongTimeString());
    Response.Flush();
     %>
<%--data: Demo at Time <%= DateTime.Now.Minute.ToString() %>--%>



