<%@ Page Title="Trivia" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Trivia.aspx.cs" Inherits="Talant.Trivia" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <script type="text/javascript">
            var start = function () {
                var inc = document.getElementById('incomming');
                var wsImpl = window.WebSocket || window.MozWebSocket;
                var form = document.getElementById('sendForm');
                var logOut = document.getElementById('logOut');
                var logIn = document.getElementById('logIn');
                var input = document.getElementById('sendText');
                var userName = document.getElementById('userr');
                if (window.ws != null)
                    ws.close();
                logOut.disabled = true;

                inc.innerHTML += "connecting to server ..<br/>";
                // create a new websocket and connect
                //  window.ws = new wsImpl('ws://topsecretlog-001-site1.dtempurl.com:8083/' + userName.value);
                window.ws = new wsImpl('ws://127.0.0.1:8181/' + userName.value);
                alert(ws.url);
                //    ws.send("000"+userName);
                // when data is comming from the server, this metod is called
                ws.onmessage = function (evt) {
                    //   alert(evt.data);
                    inc.innerHTML += evt.data + '<br/>';
                };
                // when the connection is established, this method is called
                ws.onopen = function () {
                    inc.innerHTML += '.. connection open<br/>';
                    logIn.disabled = true;
                    logOut.disabled = false;

                };
                // when the connection is closed, this method is called
                ws.onclose = function () {
                    inc.innerHTML += '.. connection closed<br/>';
                    logIn.disabled = false;
                    logOut.disabled = true;
                }

                trimite.addEventListener('click', function (e) {
                    e.preventDefault();
                    var val = input.value;
                    var us = userName.value;
                    //alert(us);
                    var mesaj = { User: us, Content: val, TipMesaj: "log" };
                    ws.send(JSON.stringify(mesaj));
                    input.value = "";
                });
                logOut.addEventListener('click', function (e) {
                    e.preventDefault();
                    ws.close();
                    //ws.send(JSON.stringify(mesaj));
                    //input.value = "";
                });
            }


            // window.onload = start;
        </script>
    </head>

    <body>
        <asp:Label ID="labelNume" runat="server">aaa</asp:Label>
        <br />
        <br />
        <br />
        <input type="button" id="logIn" value="Log" onclick="start();" />
        <input id="userr" placeholder="username" />

        <input type="button" id="logOut" value="LogOut" onclick="start.cl;" />
        <br />
        <br />
        <form id="sendForm">


            <input id="sendText" placeholder="Text to send" />

            <input id="trimite" type="submit" value="Trimite" />
        </form>
        <pre id="incomming"></pre>




    </body>
    </html>

    </asp:Content>