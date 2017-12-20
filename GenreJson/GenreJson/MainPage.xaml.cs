using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Net.Http;


namespace GenreJson
{
    public partial class MainPage : ContentPage
    {
        private string url;
        static string requestUrl;
        private Entry booksGenreId;

        public MainPage()
        {
            InitializeComponent();


            url = "https://app.rakuten.co.jp/services/api/BooksGenre/Search/20121128?format=json&applicationId=1051637750796067320";


            var layout = new StackLayout { HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };

            booksGenreId = new Entry    //EntryでISBNコードを入力
            {
                Placeholder = "ジャンルコードを入力",
                PlaceholderColor = Color.Gray,
                WidthRequest = 170
            };
            layout.Children.Add(booksGenreId);

            //string requestUrl = url + "&isbn=" + 9784046022257;    //URLにISBNコードを挿入
            //https://app.rakuten.co.jp/services/api/BooksBook/Search/20170404?format=json&applicationId=1051637750796067320&isbn=9784838729036



            var Serch = new Button
            {
                WidthRequest = 60,
                Text = "Serch!",
                TextColor = Color.Red,
            };
            layout.Children.Add(Serch);
            Serch.Clicked += Serch_Click;

            Content = layout;
        }

        private async void Serch_Click(object sender, EventArgs e)
        {
            try
            {
                var layout2 = new StackLayout { HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
                var scrView = new ScrollView { Orientation = ScrollOrientation.Vertical };
                layout2.Children.Add(scrView);
                var layout = new StackLayout { HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
                scrView.Content = layout;

                string genrecode = booksGenreId.Text;
                requestUrl = url + "&booksGenreId=" + genrecode;    //URLにジャンルIDを挿入


                int kk = k();



                //-------------------------------------ボタン再配置--------------------------
                booksGenreId = new Entry    //EntryでISBNコードを入力
                {
                    Placeholder = "ISBNコードを入力",
                    PlaceholderColor = Color.Gray,
                    WidthRequest = kk
                };
                layout.Children.Add(booksGenreId);

                var Serch = new Button
                {
                    WidthRequest = 60,
                    Text = "Serch!",
                    TextColor = Color.Red,
                };
                layout.Children.Add(Serch);
                Serch.Clicked += Serch_Click;
                //-------------------------------------ボタン再配置--------------------------


                /*
                //HTTPアクセス //書き方が古いらしい
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(requestUrl);
                req.Method = "GET";
                HttpWebResponse res = req.GetResponseAsync();
                */

                /*
                // HTTPアクセス 
                var req = WebRequest.Create(requestUrl);
                var res = req.GetResponseAsync();

                // レスポンス(JSON)をオブジェクトに変換 
                Stream s = GetMemoryStream(res);
                StreamReader sr = new StreamReader(s);
                string str = sr.ReadToEnd();
                */

                //HTTPアクセスメソッドを呼び出す
                string APIdata = await GetApiAsync();

                //この辺よく分からん
                Stream s = GetMemoryStream(APIdata);
                StreamReader sr = new StreamReader(s);
                string str = sr.ReadToEnd();

                await DisplayAlert("警告", str, "ok");

                var info = JsonConvert.DeserializeObject<RakutenGenre>(str); //JSON形式から戻す…戻したい 

                

                foreach (var r in info.children)
                {
                    layout.Children.Add(new Label { Text = $"ID: { r.booksGenreId}" });
                    layout.Children.Add(new Label { Text = $"GenreName: { r.booksGenreName }" });
                };
                layout.Children.Add(new Label { Text = "読み取り終了", TextColor = Color.Black });
                layout.Children.Add(new Label { Text = str }); //JSON形式で書き出す

                Content = layout2;
            }
            catch  (Exception x){ await DisplayAlert("警告", x.ToString(), "ok"); }
        }


        //HTTPアクセスメソッド
        public static async Task<string> GetApiAsync()
        {
            string APIurl = requestUrl;

            using (HttpClient client = new HttpClient())
                try
                {
                    string urlContents = await client.GetStringAsync(APIurl);
                    return urlContents;
                }
                catch (Exception e)
                {
                    string a = e.ToString();
                    return a;
                }
        }
        //何かしてるメソッド
        public MemoryStream GetMemoryStream(string text)
        {
            string a = text;
            return new MemoryStream(Encoding.UTF8.GetBytes(a));
        }
        public int k()
        {
            int a=0;
            for(int i = 0;170 > i;i++)
            a++;
            return a;
        }
    }
}
