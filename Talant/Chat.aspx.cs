using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using talant;

namespace Talant
{
    public partial class Concurs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var a = NHSession.Getsession().QueryOver<Users>().List();


        }
    }
}