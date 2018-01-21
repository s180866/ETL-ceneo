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
        var IdList = MySqlConnector.GetDataFromMySQLArray("SELECT ProductId FROM etl_ceneo.product;", "ProductId");
        var IdListOpinion = MySqlConnector.GetDataFromMySQLArray("SELECT OpinionId FROM etl_ceneo.opinion;", "OpinionId");

        //Udate/Insert Product
        if (IdList.Contains(id))
            MySqlConnector.InsertProduct(TransformData.product_transformed_dicc[id], "update");
        else
            MySqlConnector.InsertProduct(TransformData.product_transformed_dicc[id], "insert");

        //Update/Insert Opinion
        if (IdListOpinion.Contains(id))
            MySqlConnector.InsertOpinion(TransformData.opinions_transformed_dicc[id], "update");
        else
            MySqlConnector.InsertOpinion(TransformData.opinions_transformed_dicc[id], "insert");

        return string.Concat("Załadowano ", MySqlConnector.CountInsertedRows.ToString(), " oraz zaktualizowano ", MySqlConnector.CountUpdatedRows.ToString(), " opinii dla produktu o ID: ", id);
    }
    public static int CountInsertedRows { get; set; }
    public static int CountUpdatedRows { get; set; }

    public string Message { get; set; }

}