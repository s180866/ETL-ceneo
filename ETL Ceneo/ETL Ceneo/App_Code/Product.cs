using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_Ceneo
{
    public class Product
    {
        //Klasa do przechowywania informacji o produktach
        public string Model { get; set; }
        public string Brand { get; set; }
        public string DeviceType { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

        public int Recomend { get; set; }
        public int NotRecomendNumber { get; set; }

        public double Stars { get; set; }
        public string Id { get; set; }
        public int IdNumeric { get; set; }
    }
}
