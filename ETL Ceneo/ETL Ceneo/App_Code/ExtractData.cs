using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ETL_Ceneo
{
   public class ExtractData
    {
        //= new Dictionary<string, Product>();
        public static Dictionary<string, Product> product_dicc = new Dictionary<string, Product>();
        public static Dictionary<string, List<Opinion>> opinions_dicc = new Dictionary<string, List<Opinion>>();//= new Dictionary<string, Product>();
        private bool succsess = false;

        public ExtractData() { }

        public ExtractData(string id)
        {
            GetProductDetails(id);
            GetOpinions(id);
            this.succsess = true;
        }


        public static string UrlBuilderReview(string id)
        {
            return String.Concat("https://www.ceneo.pl/", id, "#tab=reviews");
        }

        public static string UrlBuilderReviewPages(string id, string page)
        {
            return String.Concat("https://www.ceneo.pl/", id, "/opinie-", page);
        }

        public static string GetHtmlContent(string url)
        {
            string resp = "";

            using (WebClient client = new WebClient()) // Korzysta z WebClient (wbudowana klasa) i pobiera dane ze strony pod podanym URL, potem zapisuje je jako ciag znakow i zwraca go
            {
                var data = client.DownloadData(url);
                resp = Encoding.UTF8.GetString(data);
            }
            return resp;
        }

        public static string GetRedirectUrl(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.AllowAutoRedirect = false;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                return response.Headers["Location"];
            }
        }

        public static List<string> GetRedirectUrlList(string id)
        {
            List<string> urls = new List<string>();
            urls.Add(UrlBuilderReview(id));
            bool test = true;
            string url = "";
            int i = 2;

            while (test)
            {
                url = UrlBuilderReviewPages(id, i.ToString());
                var resp = GetRedirectUrl(url);

                if (resp == null)
                    urls.Add(url);
                else
                    test = false;

                i++;
            }

            return urls;
        }

        public void GetOpinions(string id)
        {
            HtmlDocument doc = new HtmlDocument();
            Opinion op;
            List<Opinion> reviews = new List<Opinion>();
            bool firstLoop = true;
            

            string authorPattern = "<div class=\"reviewer-name-line\">\\s*(.+?)\\s*</div>";
            string starsPattern = "<span class=\"review-score-count\">\\s*(.+?)\\s*</span>";
            string commentPattern = "<p class=\"product-review-body\">\\s*(.+?)\\s*</p>";
            string recommendPattern = "<em class=\"product-recommended\">\\s*(.+?)\\s*</div>";
            string timePattern = "<time datetime=\"\\s*(.+?)\\s*\">";
            

            var urlsList = GetRedirectUrlList(id);

            foreach (var u in urlsList)
            {
                List<string> reviewDivHtml;

                var websiteContent = ExtractData.GetHtmlContent(u);

                doc.LoadHtml(websiteContent);
                if (firstLoop)
                {
                    reviewDivHtml = doc.DocumentNode.SelectNodes("//*[@id=\"body\"]/div[2]/div/div/div[2]/div[3]/div/ol/li")//this xpath selects all span tag having its class as hidden first
                                             .Select(p => p.InnerHtml)
                                             .ToList();
                    firstLoop = false;
                }
                else
                    reviewDivHtml = doc.DocumentNode.SelectNodes("//*[@id=\"body\"]/div[2]/div/div/div[2]/div[2]/ol/li")//this xpath selects all span tag having its class as hidden first
                                             .Select(p => p.InnerHtml)
                                             .ToList();


                foreach (var div in reviewDivHtml)
                {
                    op = new Opinion();
                    doc.LoadHtml(div.ToString());
                    op.Author = Regex.Match(div.ToString(), authorPattern, RegexOptions.Singleline).Groups[1].Value.ToString();
                    op.Stars = Regex.Match(div.ToString(), starsPattern, RegexOptions.Singleline).Groups[1].Value.ToString();
                    op.Comment = Regex.Match(div.ToString(), commentPattern, RegexOptions.Singleline).Groups[1].Value.ToString();
                    op.Recommend = !String.IsNullOrEmpty(Regex.Match(div.ToString(), recommendPattern, RegexOptions.Singleline).Groups[1].Value.ToString());
                    op.OpinionDateStr = Regex.Match(div.ToString(), timePattern, RegexOptions.Singleline).Groups[1].Value.ToString();
                     
                    try
                    {
                        op.Advantanges = doc.DocumentNode.SelectNodes("div/div[1]/div[3]/div[1]/ul/li")//this xpath selects all span tag having its class as hidden first
                                          .Select(p => p.InnerText)
                                          .ToList();
                    }
                    catch { }

                    try
                    {
                        op.Disadvantages = doc.DocumentNode.SelectNodes("div/div[1]/div[3]/div[2]/ul/li")//this xpath selects all span tag having its class as hidden first
                          .Select(p => p.InnerText)
                          .ToList();
                    }
                    catch { }

                    reviews.Add(op);
                    reviewDivHtml = null;
                }
            }

            if (!opinions_dicc.ContainsKey(id))
                opinions_dicc.Add(id, reviews);
        }

        public void GetProductDetails(string id)
        {
            Product p = new Product();
            string namePattern = "<h1 class=\"product-name js_product-h1-link js_product-force-scroll js_searchInGoogleTooltip\" data-onselect=\"true\" data-tooltip-autowidth=\"true\" itemprop=\"name\" productlink=\"/" + id + "#tab=click_scroll\">\\s*(.+?)\\s*</h1>";
            string imagePattern = "<meta property=\"og:image\" content=\"\\s*(.+?)\\s*\" />";
            string brandPattern = "<meta property=\"og:brand\" content=\"\\s*(.+?)\\s*\" />";

            string url = UrlBuilderReview(id);
            var websiteContent = ExtractData.GetHtmlContent(url);
            p.Id = id;
            p.DeviceType = Regex.Match(websiteContent, namePattern, RegexOptions.Singleline).Groups[1].Value.ToString();
            p.ImagePath = Regex.Match(websiteContent, imagePattern, RegexOptions.Singleline).Groups[1].Value.ToString();
            p.Brand = Regex.Match(websiteContent, brandPattern, RegexOptions.Singleline).Groups[1].Value.ToString();

            if (!product_dicc.ContainsKey(id))
                product_dicc.Add(id, p);

        }
    }
}
