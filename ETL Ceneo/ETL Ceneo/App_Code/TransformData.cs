using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL_Ceneo
{
    public  class TransformData
    {
        public static Dictionary<string, Product> product_transformed_dicc = new Dictionary<string, Product>();
        public static Dictionary<string, List<Opinion>> opinions_transformed_dicc = new Dictionary<string, List<Opinion>>();//= new Dictionary<string, Product>();
        public bool success = false;
        public TransformData(string id)
        {
            // TransformationProduct(id);
            TransformationOpinions(id);
            this.success = true;
        }

        public static void TransformationProduct(string id)
        {
            //Produkt
            Product p = ExtractData.product_dicc[id];

            //Transformacja ID ze string to int
            try
            {
                p.IdNumeric = Convert.ToInt32(p.Id);
            }
            catch { }

        }

        public static void TransformationOpinions(string id)
        {
            Opinion op;
            double stars = 0;
            Product p = ExtractData.product_dicc[id];
            List<Opinion> opinions_trans = new List<Opinion>();

            //Transformacja ID ze string to int
            try
            {
                p.IdNumeric = Convert.ToInt32(p.Id);
            }
            catch { }


            foreach (var o in ExtractData.opinions_dicc[id])
            {
                op = new Opinion();

                op.ProductId = p.IdNumeric;
                //Autor
                if (String.IsNullOrEmpty(o.Author))
                    op.Author = "Anonim";
                else
                    op.Author = o.Author;

                try { op.StarsNumeric = Convert.ToDouble(o.Stars.Substring(0, o.Stars.LastIndexOf('/')).Replace(",", ".")); }
                catch { }

                try
                {
                    int year = Convert.ToInt32(o.OpinionDateStr.Substring(0, 4));
                    int month = Convert.ToInt32(o.OpinionDateStr.Substring(5, 2));
                    int day = Convert.ToInt32(o.OpinionDateStr.Substring(8, 2));
                    int hour = Convert.ToInt32(o.OpinionDateStr.Substring(11, 2));
                    int minutes = Convert.ToInt32(o.OpinionDateStr.Substring(14, 2));
                    int seconds = Convert.ToInt32(o.OpinionDateStr.Substring(17, 2));

                    op.OpinionDateStr = o.OpinionDateStr;
                    op.OpinionDate = new DateTime(year, month, day, hour, minutes, seconds);
                    op.Recommend = o.Recommend;
                }
                catch { }

                try
                {
                    if (o.Advantanges.Count > 0)
                        op.AdvantagesStr = string.Join(";", o.Advantanges.ToArray());

                    if (o.Disadvantages.Count > 0)
                        op.DisadvantagesStr = string.Join(";", o.Disadvantages.ToArray());

                }
                catch (Exception e) { }


                //Zliczanie poleceń (we wszystkich opiniach) i nie poleceń
                if (o.Recommend == true)
                    p.Recomend += 1;
                else
                    p.NotRecomendNumber += 1;

                stars += op.StarsNumeric;

                opinions_trans.Add(op);
            }

            p.Stars = stars / ExtractData.opinions_dicc[id].Count;

            if (!product_transformed_dicc.ContainsKey(id))
                product_transformed_dicc.Add(id, p);

            if (!opinions_transformed_dicc.ContainsKey(id))
                opinions_transformed_dicc.Add(id, opinions_trans);
        }

    }
}
