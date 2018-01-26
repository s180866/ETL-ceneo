using ETL_Ceneo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    public static bool extract = false;
    public static bool transform = false;
    public static bool load = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            
        }
    }

    protected void Button1_Click(Object sender, EventArgs e)
    {

        var id = String.Format("{0}", Request.Form["itemId"]);
        
        Button1.Enabled = false; //wylacza guzik
        Button1.Visible = false; //chowa guzik

        ScriptManager.RegisterClientScriptBlock(this, GetType(), "my key", "alertMe();", true); //wyswietla powiadomonie



        ETL_Ceneo.ExtractData ed = new ETL_Ceneo.ExtractData(id); //pobranie danych      

        if (ed.succsess) //jak pobranie danych sie uda, to transfort sie zaczyna
        {
            ETL_Ceneo.TransformData td = new ETL_Ceneo.TransformData(id);

            if (td.success)//jak transform danych sie uda, to load sie zaczyna
            {
                LoadData ld = new LoadData(id);
            }
        }

        Product p = ETL_Ceneo.TransformData.product_transformed_dicc[id];
        List<Opinion> opinions = ETL_Ceneo.TransformData.opinions_transformed_dicc[id];

        statsDiv.InnerHtml = "<h5 class=\"center\">Pobrano " + opinions.Count + " opini dla produktu o ID " + id + "</h5>";
        //Wyswietlanie recenzji, produkt raz, w petli opinie
        //var headerHTML = "<div class=\"container\"> <divclass=\"cols12m8offset-m2l6offset-l3\"> <divclass=\"card-panelgreylighten-5z-depth-1\"> <divclass=\"rowvalign-wrapper\"> <divclass=\"cols2\"> <imgsrc=\"" + p.ImagePath + "\"alt=\"\"class=\"circleresponsive-img\"> </div> <divclass=\"cols11\"> <spanclass=\"black-text\">" + p.Brand + "</h1> 				<h5>" + p.DeviceType + "</h5> </span> </div> </div> </div> </div> 	</div>";
        var headerHTML = "<div class=\"container\"><div class=\"col s12 m8 offset-m2 l6 offset-l3\"><div class=\"card-panel grey lighten-5 z-depth-1\"><div class=\"row valign-wrapper\"><div class=\"col s2\"><img src=\"" + p.ImagePath + "\" alt=\"\" class=\"circle responsive-img\"></div><div class=\"col s6\">\"<span class=\"black-text\"><h1><center>" + p.Brand + "</center></h1><h5>" + p.DeviceType + "</h5></span></div></div></div></div></div>";
        string reviews = "";
        foreach (var op in opinions)
        {
            string rec = "";
            if (op.Recommend == true) rec = "TAK";
            else rec = "NIE";

            var review = "<div class=\"col s12 m8 offset-m2 l6 offset-l3\">";
            review += "<div class=\"card-panel grey lighten-5 z-depth-1\">";
            review += "<div class=\"row valign-wrapper\">";
            review += "<div class=\"col s16\">";
            review += "<span class=\"black-text\">";
            review += "<h5>" + op.Author + "</h5>";
            review += "<h6>" + op.OpinionDateStr + "</h6>";          
            review += "<h6 >Polecam: " + rec + "</h6><br/>";
            review += "</span>";
            review += "<span class=\"black-text\">" + op.Comment + "</span><br /><br />";
            if (!String.IsNullOrEmpty(op.AdvantagesStr)) review += "<font color = \"green\">Zalety:</font> " + op.AdvantagesStr.Replace(';', ',') + "</font><br />";
            if (!String.IsNullOrEmpty(op.DisadvantagesStr)) review += "<font color = \"red\">Wady:</font> " + op.DisadvantagesStr + "<br />";
            review += "</div></div></div></div>";

            reviews += review + " ";
        }


        reviews = "<div class=\"container\"> " + reviews + "</div>";

        productHeader.InnerHtml = headerHTML; //dodajemy do niej kod html
        reviewsDiv.InnerHtml = reviews;
    }
    protected void Button2_Click(Object sender, EventArgs e)
    {

    }

    protected void Button3_Click(Object sender, EventArgs e)
    {
      
        //Usuwa dane z bazy danych
        ScriptManager.RegisterClientScriptBlock(this, GetType(), "my key", "alertDB();", true);       
        MySqlConnector.TruncateTable();

    }



}