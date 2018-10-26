using Fleck;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Talant.Models;

namespace Talant
{
    public partial class ServerTrivia : System.Web.UI.Page
    {
        List<Intrebare> _listIntrebari;
        Intrebare _randomIntrebare;
        static Color[] colors = { Color.Red, Color.Green, Color.Blue, Color.Brown, Color.BlueViolet };

        List<Profil> allSockets = new List<Profil>();
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var ctx = new TalantContext())
            {
                _listIntrebari = ctx.Intrebari.Where(intr => intr.Id > 38).ToList();
            }


            FleckLog.Level = LogLevel.Debug;



            var server = new WebSocketServer("ws://127.0.0.1:8181");
            PuneAltaIntrebare();

            int raspuns = 0;
            server.Start(socket =>
            {
                //        HttpContext.Current.Response.Redirect("http://www.google.com");
                socket.OnOpen = () =>
                {
                    //   System.Diagnostics.Debugger.Launch();
                    var user = socket.ConnectionInfo.Path.Remove(0, 1);

                    Profil conexiune = new Profil { Socket = socket, UserName = user, Color = GetRandomColor() };
                    //    ClientScript.RegisterStartupScript(this.GetType(), "alert(conexiune.UserName)",
                    //                   "alert(conexiune.UserName)", true);
                    allSockets.Add(conexiune);

                    List<Users> listUser = new List<Users>();
                    foreach (var el in allSockets)
                        listUser.Add(new Users { Username = el.UserName });

                    //allSockets.ToList().ForEach(s => s.Socket.Send(conexiune.UserName + " a intrat."));
                    var jj = JsonConvert.SerializeObject(listUser);
                    allSockets.ToList().ForEach(s => s.Socket.Send(JsonConvert.SerializeObject(listUser)));
                    socket.Send(FormatMessageIntrebare(_randomIntrebare.Enunt));
                };
                socket.OnClose = () =>
                {

                    System.Diagnostics.Debug.WriteLine("Close!");
                    var conexiune = allSockets.Where(s => s.Socket == socket).FirstOrDefault();
                    allSockets.Remove(conexiune);
                    List<Users> listUser = new List<Users>();
                    foreach (var el in allSockets)
                        listUser.Add(new Users { Username = el.UserName });
                    allSockets.ToList().ForEach(s => s.Socket.Send(JsonConvert.SerializeObject(listUser)));

                    allSockets.ToList().ForEach(s => s.Socket.Send(conexiune.UserName + " a iesit."));
                };
                socket.OnMessage = message =>
                {

                    Mesaj mesaj = new JavaScriptSerializer().Deserialize<Mesaj>(message);
                    System.Diagnostics.Debug.WriteLine(message);
                    allSockets.ToList().ForEach(s => s.Socket.Send(FormatMessage(mesaj)));
                    if (mesaj.Content == _randomIntrebare.Raspuns)
                    {
                        allSockets.ToList().ForEach(s => s.Socket.Send(mesaj.User + "WIN!!!"));
                        PuneAltaIntrebare();
                    }
                };

            });
        }
        private void PuneAltaIntrebare()
        {
            //  System.Diagnostics.Debugger.Launch();
            //  Console.sh
            //        System.Threading.Thread.Sleep(5000);
            Random rnd = new Random();
            int r = rnd.Next(_listIntrebari.Count);
            Intrebare intreb = new Intrebare { Enunt = "Ce??", Raspuns = "da" };
            _listIntrebari.Add(intreb);
            _randomIntrebare = _listIntrebari[r];
            allSockets.ToList().ForEach(s => s.Socket.Send(FormatMessageIntrebare(_randomIntrebare.Enunt)));
            //      if (ss > 3)
            ;//   ii++;



        }
        private string FormatMessage(Mesaj mesaj)
        {
            var profil = allSockets.Where(s => s.UserName == mesaj.User).FirstOrDefault();
            string detrimis = "<span style = \"color:" + ColorTranslator.ToHtml(profil.Color)
            + "\">" +
                   mesaj.User + "</span> @ " + DateTime.Now.ToString() +
                   ": " + mesaj.Content;

            return detrimis;

        }
        private string FormatMessageIntrebare(string mesaj)
        {

            string detrimis = "<span style = \"display:table; margin: 0 auto;\">" +
                    mesaj;

            return detrimis;

        }
        static Color GetRandomColor()
        {
            var random = new Random();
            return colors[random.Next(colors.Length)];
        }

    }
}