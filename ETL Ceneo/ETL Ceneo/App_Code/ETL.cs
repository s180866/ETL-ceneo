using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Opis podsumowujący dla ETL
/// </summary>
public class ETL
{
	public ETL()
	{
		//
		// TODO: Tutaj dodaj logikę konstruktora
		//
	}

    public static void StartETL(string id)
    {
        ETL_Ceneo.ExtractData ed = new ETL_Ceneo.ExtractData(id);

        ETL_Ceneo.TransformData td = new ETL_Ceneo.TransformData(id);

        LoadData ld = new LoadData(id);
    }
}