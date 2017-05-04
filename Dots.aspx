<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Dots.aspx.vb" Inherits="Dots" %>
<%
    If Request.QueryString("clear") <> Nothing Then
        ClearDots()
    Else If Request.QueryString("x") <> Nothing AndAlso Request.QueryString("y") <> Nothing Then
        'Response.Write(Request.QueryString("x") & ", " & Request.QueryString("y"))
        
        Dim x As Integer = Int32.Parse(Request.QueryString("x"))
        Dim y As Integer = Int32.Parse(Request.QueryString("y"))

        Response.Write(AddDot(x, y))
        Response.Write(vbLf & x & ", " & y & vbLf)
        'AddDot(x, y)
    Else
        Response.Write(GetDots())
    End If
%>
