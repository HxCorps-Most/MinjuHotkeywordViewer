using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using System.Web;
using System.Net;

namespace HotkeyViewerApp.ViewModels
{
    public class GetHotkeywordsViewModel : INotifyPropertyChanged
    {

        Dictionary<string, string> keyword;

        public event PropertyChangedEventHandler PropertyChanged;

        public void GetKeyword() {

            Dictionary<string, string> keyword = new Dictionary<string, string>();

            WebClient wc = new WebClient();

            wc.Encoding = Encoding.UTF8;

            String html = wc.DownloadString("http://www.naver.com");

            wc.Encoding = Encoding.UTF8;

            HtmlAgilityPack.HtmlDocument mydoc = new HtmlAgilityPack.HtmlDocument();
            mydoc.LoadHtml(html);

            
            HtmlAgilityPack.HtmlNodeCollection nodeCol = mydoc.DocumentNode.SelectNodes("//ul[@class='ah_l']");
            HtmlAgilityPack.HtmlNode innernode = nodeCol[0];
            HtmlAgilityPack.HtmlNodeCollection keywordlist = innernode.SelectNodes("//span[@class='ah_k']");
            HtmlAgilityPack.HtmlNodeCollection ranklist = innernode.SelectNodes("//span[@class='ah_r']");


            for (int i = 0; i < keywordlist.Count; i++) {

                string content = keywordlist[i].InnerText;
                string rank = ranklist[i].InnerText;

                keyword.Add(rank, content);
            }

            this.keyword = keyword;

            return;
        }

        public Dictionary<string, string> Keyword
        {
            set
            {
                if(keyword != value)
                {
                    keyword = value;

                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("keyword"));
                    }

                }
                
            }
            get
            {
                return keyword;
            }
        }

    }

}
