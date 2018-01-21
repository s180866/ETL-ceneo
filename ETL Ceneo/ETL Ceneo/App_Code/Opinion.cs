using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_Ceneo
{
    class Opinion
    {
        //Klasa do przechowywania opinii
        public int ProductId { get; set; }
        public string Author { get; set; }
        public string Stars { get; set; }
        public string Comment { get; set; }
        public double StarsNumeric { get; set; }
        public bool Recommend { get; set; }
        public bool NotRecommend { get; set; }

        public int RecommendNumeric { get; set; }
        public int NotRecommendNumeric { get; set; }

        public int Useful { get; set; }
        public int NotUseful { get; set; }
        public DateTime OpinionDate { get; set; }

        public string OpinionDateStr { get; set; }

        public string AdvantagesStr { get; set; }

        public string DisadvantagesStr { get; set; }

        public List<string> Advantanges { get; set; }
        public List<string> Disadvantages { get; set; }
    }
}
