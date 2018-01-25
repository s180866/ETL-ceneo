using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Opis podsumowujący dla LoadData
/// </summary>
public class LoadData
{
    public LoadData(string id)
    {
        Message = LoadDataToDB(id);
    }
    public static string LoadDataToDB(string id)
    {
        //Pobiera liste istenijacych id i kluczy w bazie i sprawdza czy ma dodac czy zrobic update
        var IdList = ETL_Ceneo.MySqlConnector.GetDataFromMySQLArray("SELECT ProductId FROM etl_ceneo.product;", "ProductId");
        var IdListOpinion = ETL_Ceneo.MySqlConnector.GetDataFromMySQLArray("SELECT OpinionId FROM etl_ceneo.opinion;", "OpinionId");

        //Udate/Insert Product
        if (IdList.Contains(id))
            ETL_Ceneo.MySqlConnector.InsertProduct(ETL_Ceneo.TransformData.product_transformed_dicc[id], "update");
        else
            ETL_Ceneo.MySqlConnector.InsertProduct(ETL_Ceneo.TransformData.product_transformed_dicc[id], "insert");

        //Update/Insert Opinion
        if (IdListOpinion.Contains(id))
            ETL_Ceneo.MySqlConnector.InsertOpinion(ETL_Ceneo.TransformData.opinions_transformed_dicc[id], "update");
        else
            ETL_Ceneo.MySqlConnector.InsertOpinion(ETL_Ceneo.TransformData.opinions_transformed_dicc[id], "insert");

        return string.Concat("Załadowano ", ETL_Ceneo.MySqlConnector.CountInsertedRows.ToString(), " oraz zaktualizowano ", ETL_Ceneo.MySqlConnector.CountUpdatedRows.ToString(), " opinii dla produktu o ID: ", id);
    }
    public static int CountInsertedRows { get; set; }
    public static int CountUpdatedRows { get; set; }

    public string Message { get; set; }

}