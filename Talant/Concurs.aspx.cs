using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Talant.Models;


namespace Talant
{
    public partial class Game : System.Web.UI.Page
    {
        List<Intrebare> _listIntrebari;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (var ctx = new TalantContext())
                {

                    ////         var blogs = from b in ctx.Intrebari select b;
                    //var intrebare = new Intrebare() { Categorie = 3, Editie = 2015, Punctaj = 3 };
                    //Referinta refer = new Referinta { Carte = "Ieremia", Capitol = 2, verset = 11, Altele = "Nimic" };

                    //intrebare.Referinte = new List<Referinta>();
                    //intrebare.Referinte.Add(refer);

                    //ctx.Intrebari.Add(intrebare);
                    //ctx.SaveChanges();

                    _listIntrebari = ctx.Intrebari.ToList();
                    GridView1.DataSource = _listIntrebari;

                    GridView1.DataBind();

                }
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var ctx = new TalantContext())
            {
                _listIntrebari = ctx.Intrebari.ToList();

                int idIntreb = Convert.ToInt16(GridView1.SelectedRow.Cells[1].Text.ToString());

                var intreb = _listIntrebari.Where(vvv => vvv.Id == idIntreb).SingleOrDefault();
                var listRef = intreb.Referinte;
                GridView2.DataSource = listRef;
                GridView2.DataBind();
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            using (var ctx = new TalantContext())
            {
                _listIntrebari = ctx.Intrebari.ToList();
                GridView1.DataSource = _listIntrebari;
                GridView1.DataBind();
            }

            //switch (e.CommandName)
            //{
            //    case "Edit":
            //        int index = Convert.ToInt32(e.CommandArgument);
            //        return;
            //    case "Delete":
            //        return;
            //    default:
            //        return;
            //}







            GridView1.EditIndex = e.NewEditIndex;
            GridView1.DataBind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridView1.DataSource = _listIntrebari;

            //  GridView1.DataBind();

            GridViewRow row = GridView1.Rows[e.RowIndex];
            int intrebareId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            string tip = (row.Cells[2].Controls[0] as TextBox).Text;
            string categorie = (row.Cells[3].Controls[0] as TextBox).Text;
            string enunt = (row.Cells[4].Controls[0] as TextBox).Text;
            string punctaj = (row.Cells[5].Controls[0] as TextBox).Text;
            string editie = (row.Cells[6].Controls[0] as TextBox).Text;
            string raspuns = (row.Cells[7].Controls[0] as TextBox).Text;

            using (var ctx = new TalantContext())
            {
                var intrebare = ctx.Intrebari.FirstOrDefault(a => a.Id == intrebareId);
                if (intrebare == null)
                {
                    intrebare = new Intrebare();
                    ctx.Intrebari.Add(intrebare);
                }

                intrebare.Tip = Convert.ToInt16(tip);
                intrebare.Categorie = Convert.ToInt16(categorie);
                intrebare.Enunt = Convert.ToString(enunt);
                intrebare.Punctaj = Convert.ToInt16(punctaj);
                intrebare.Editie = Convert.ToInt16(editie);
                intrebare.Raspuns = Convert.ToString(raspuns);

                ctx.SaveChanges();
                _listIntrebari = ctx.Intrebari.ToList();
                GridView1.DataSource = _listIntrebari;


            }


            GridView1.EditIndex = -1;
            GridView1.DataBind();
        }



        protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            using (var ctx = new TalantContext())
            {
                _listIntrebari = ctx.Intrebari.ToList();
                GridView1.DataSource = _listIntrebari;

                //  GridView1.DataBind();
            }

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int intrebareId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);

            using (var ctx = new TalantContext())
            {
                var intrebare = ctx.Intrebari.First(a => a.Id == intrebareId);

                //foreach (var el in intrebare.Referinte)
                {
                    if (intrebare.Referinte.Count > 0)
                        ctx.Referinte.Remove(intrebare.Referinte[0]);
                    //ctx.SaveChanges();
                }
                ctx.Intrebari.Remove(intrebare);
                ctx.SaveChanges();
                _listIntrebari = ctx.Intrebari.ToList();
                GridView1.DataSource = _listIntrebari;

                GridView1.DataBind();
            }


            GridView1.EditIndex = -1;
            GridView1.DataBind();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            using (var ctx = new TalantContext())
                //{
                //    var intrebare = ctx.Intrebari.FirstOrDefault(a => a.Id == intrebareId);
                //    if (intrebare == null)
                //    {
                //        intrebare = new Intrebare();
                //        ctx.Intrebari.Add(intrebare);
                //    }

                //    intrebare.Tip = Convert.ToInt16(tip);
                //    intrebare.Categorie = Convert.ToInt16(categorie);
                //    intrebare.Enunt = Convert.ToString(enunt);
                //    intrebare.Punctaj= Convert.ToInt16(punctaj);
                //    intrebare.Editie = Convert.ToInt16(editie);

                //    ctx.SaveChanges();
                _listIntrebari = ctx.Intrebari.ToList();
            GridView1.DataSource = _listIntrebari;

            //  //  GridView1.DataBind();
            //}

       
            GridView1.EditIndex = -1;
            GridView1.DataBind();


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (var ctx = new TalantContext())
            {
                _listIntrebari = ctx.Intrebari.ToList();
                _listIntrebari.Add(new Intrebare());
                GridView1.DataSource = _listIntrebari;


                //.FindControl("btnEdit");

                GridView1.EditIndex = _listIntrebari.Count - 1;
                GridView1.DataBind();
            }



        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //switch (e.CommandName)
            //{
            //    case "Edit":
            //        int index = Convert.ToInt32(e.CommandArgument);
            //        Button myButton = null;
            //        myButton = (Button)GridView1.Rows[index].Cells[0].Controls[0];
            //        myButton.Text = "Save";
            //        return;
            //    case "Delete":
            //        return;
            //    default:
            //        return;
            //}
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Intrebare intreb = _listIntrebari.LastOrDefault();
                if (intreb.Id == 0 && e.Row.RowIndex == _listIntrebari.Count - 1)
                {
                    LinkButton button1 = (LinkButton)e.Row.Cells[0].Controls[0];
                    button1.Text = "Save";
                }
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtb = e.Row.FindControl("TextBox") as TextBox;
                    // txtb.Width = 10;
                }
      //          e.Row.Cells[0].Attributes["width"] = "150px";
        //        e.Row.Cells[1].Attributes["width"] = "200px";
          //      e.Row.Cells[2].Attributes["width"] = "300px";
                e.Row.Cells[4].Attributes["width"] = "500px";
                //                GridView1.Columns[0].ItemStyle.Width = 50;
                //                GridView1.Columns[3].ItemStyle.Width = 50;
                //               GridView1.Columns[4].ItemStyle.Width = 50;

            }

        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }
    }
}