using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_Ceneo
{
    public static class MySqlConnector
    {
        const string DB_CONN_STR = "Server=ec2-18-196-10-31.eu-central-1.compute.amazonaws.com;Port=3306;Uid=etlceneo;Pwd=EtlCeneo123;Database=etl_ceneo";

        public static int CountInsertedRows { get; set; }
        public static int CountUpdatedRows { get; set; }
        public static string[] GetDataFromMySQLArray(string query, string sqlColumnName)
        {
            string temp;
            List<string> listPaths = new List<string>();

            MySqlConnection connection = new MySqlConnection(DB_CONN_STR);
            connection.Open();

            MySqlCommand command = new MySqlCommand(query, connection);

            MySqlDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                temp = reader.GetString(reader.GetOrdinal(sqlColumnName));
                listPaths.Add(temp);
            }

            reader.Close();
            connection.Close();

            return listPaths.ToArray();
        }

        public static void TruncateTable()
        {
            MySqlConnection connection = new MySqlConnection(DB_CONN_STR);
            string query = "TRUNCATE TABLE " + "etl_ceneo.opinion";
            string query2 = "TRUNCATE TABLE " + "etl_ceneo.product";


            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlCommand cmd2 = new MySqlCommand(query2, connection);

            connection.Open();
            cmd.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            connection.Close();
        }
        public static void InsertProduct(Product prod, string queryType)
        {

            MySqlConnection connection = new MySqlConnection(DB_CONN_STR);
            string queryStr = "";
            if (queryType == "insert")
            {
                queryStr = "INSERT INTO etl_ceneo.product (ProductId, Model,Brand, DeviceType, Description, ImagePath, Stars, Recommend, NotRecommend)"
                                       + "VALUES (@id, @model, @brand, @deviceType, @description, @imagePath, @stars, @recommend, @notRecommend);";

            }
            else
            {
                queryStr = "UPDATE etl_ceneo.product SET Model = @model, Brand = @brand, DeviceType = @deviceType, Description = @description, ImagePath = @imagePath, Recommend = @recommend, NotRecommend = @notRecommend "
                        + " WHERE ProductId = @id;";


            }

            connection.Open();
            MySqlTransaction trans = connection.BeginTransaction();
            MySqlCommand cmd = new MySqlCommand(queryStr, connection, trans);

            //SELECT ProductId, Model,Brand, DeviceType, Description, ImagePath, Stars, Recommend, NotRecommend FROM etl_ceneo.product;

            cmd.Parameters.AddWithValue("@id", prod.IdNumeric);
            cmd.Parameters.AddWithValue("@model", prod.Model);
            cmd.Parameters.AddWithValue("@brand", prod.Brand);
            cmd.Parameters.AddWithValue("@deviceType", prod.DeviceType);
            cmd.Parameters.AddWithValue("@description", prod.Description);
            cmd.Parameters.AddWithValue("@imagePath", prod.ImagePath);
            cmd.Parameters.AddWithValue("@stars", prod.Stars);
            cmd.Parameters.AddWithValue("@recommend", prod.NotRecomendNumber);
            cmd.Parameters.AddWithValue("@notRecommend", prod.NotRecomendNumber);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch { }

            trans.Commit();
            connection.Close();

        }

        public static void InsertOpinion(List<Opinion> opinions, string queryType)
        {

            MySqlConnection connection = new MySqlConnection(DB_CONN_STR);
            string queryStr = "";
            if (queryType == "insert")
            {
                queryStr = "INSERT INTO etl_ceneo.opinion (OpinionId, ProductId, Author, Stars, OpinionDate, Advantages, Disadvantages, Recommend, Comment )"
                                       + "VALUES (@opinionId, @id, @author, @stars, @opinionDate, @advantages, @disadvantages, @recommend, @comment);";
                CountInsertedRows += 1;
            }
            else
            {
                queryStr = "UPDATE etl_ceneo.product SET ProductId = @id, Author = @author, Stars = @stars, OpinionDate = @opinionDate, Advantages = @advantages, Disadvantages = @disadvantages, Recommend = @recommend, Comment = @comment "
                        + " WHERE OpinionId = @opinionId;";
                CountUpdatedRows += 1;
            }

            connection.Open();
            MySqlTransaction trans = connection.BeginTransaction();

            foreach (var o in opinions)
            {
                MySqlCommand cmd = new MySqlCommand(queryStr, connection, trans);

                //OpinionId, ProductId, Author, Stars, OpinionDate, Advantages, Disadvantages, Recommend 

                cmd.Parameters.AddWithValue("@id", o.ProductId);
                cmd.Parameters.AddWithValue("@opinionId", string.Concat(o.Author, o.OpinionDateStr));
                cmd.Parameters.AddWithValue("@comment", o.Comment);
                cmd.Parameters.AddWithValue("@author", o.Author);
                cmd.Parameters.AddWithValue("@stars", o.Stars);
                cmd.Parameters.AddWithValue("@opinionDate", o.OpinionDate);
                cmd.Parameters.AddWithValue("@advantages", o.AdvantagesStr);
                cmd.Parameters.AddWithValue("@disadvantages", o.DisadvantagesStr);
                cmd.Parameters.AddWithValue("@recommend", o.Recommend);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch { }
            }

            trans.Commit();
            connection.Close();

        }
        public static DataSet select(string queryStr)
        {

            var ds = new DataSet();

            //constk string DB_CONN_STR = "Server=nettit-rds.cbclcaapul7u.eu-west-1.rds.amazonaws.com;Port=3306;Uid=root;Pwd=G4fFHDu#293u;Database=test;";
            //const string DB_CONN_STR = "Server=10.0.0.37;Port=3306;Uid=bonita;Pwd=bpm;Database=test;";
            MySqlConnection connection = new MySqlConnection(DB_CONN_STR);

            try
            {
                connection.Open();

                MySqlCommand command = new MySqlCommand(queryStr, connection);
                var adapter = new MySqlDataAdapter(command);

                adapter.Fill(ds);
                connection.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ds;
        }


    }
}
