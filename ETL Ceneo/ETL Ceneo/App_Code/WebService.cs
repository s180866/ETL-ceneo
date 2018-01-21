using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Web;
using System.Web.Services;

/// <summary>
/// Opis podsumowujący dla WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Aby zezwalać na wywoływanie tej usługi sieci Web ze skryptu za pomocą kodu ASP.NET AJAX, usuń znaczniki komentarza z następującego wiersza. 
 [System.Web.Script.Services.ScriptService]
public class WebService :  System.Web.Services.WebService {

    public WebService() {

        //Usuń znaczniki komentarza z następującego wiersza, jeśli używane są zaprojektowane składniki 
        //InitializeComponent(); 
    }

    [WebMethod]
    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
    public static void StartETL(string id)
    {

        ETL_Ceneo.ExtractData ed = new ETL_Ceneo.ExtractData(id);

        if (ed.succsess)
        {
            ETL_Ceneo.TransformData td = new ETL_Ceneo.TransformData(id);

            if (td.success)
            {
                LoadData ld = new LoadData(id);
            }
        }
    }

}
