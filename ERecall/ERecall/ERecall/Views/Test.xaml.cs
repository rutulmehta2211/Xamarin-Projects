using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ERecall.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Test : ContentPage
    {
        public Test()
        {
            InitializeComponent();
            HtmlWebViewSource htmlsource = new HtmlWebViewSource
            {
                //Html = string.IsNullOrEmpty(Service_response.response[0].Content) ? @"<html><body><p> - </p></body></html>" : @"<html><body>"+ Service_response.response[0].Content + "</body></html>"
                Html = CreateHTML("\r\n\t\t\t<p>trident seafoods corporation is issuing a voluntary recall of select lots of frozen multi-grain alaskan cod, net wt. 12, oz., because they may contain small pieces of plastic. consumption of these products may pose a choking hazard or other physical injury to the mouth.</p><p>the following retail product is subject to the voluntary recall:</p><p>trident seafoods multi-grain alaskan cod (frozen), net wt. 12oz., upc 0 28029 21048 4</p><p>recalled lot numbers with associated best by dates are printed on one end of the individual retail cartons and on the case label.</p><p>&bull;&nbsp;&nbsp;&nbsp; lot number a633511, best by: 11/30/2018 <br>&bull;&nbsp;&nbsp;&nbsp; lot number a636225, best by: 12/27/2018</p><p>lot number a633511 - distributed between 12/1/2016 and 2/9/2017 to arizona, california, colorado, illinois, kansas, massachusetts, minnesota, new jersey, pennsylvania, texas, washington, and wisconsin</p><p>lot number a636225 - distributed between 1/23/2017 and 2/17/2017 to arizona, california, colorado, illinois, kansas, massachusetts, texas, washington, and wisconsin</p><p>these products are sold at albertsons, amazon, cub foods, jewel, morey&rsquo;s, plaza extra, shaw's, shoprite, sprouts, supervalu, and woodman&rsquo;s retailers.</p><p>this issue was discovered through consumer feedback. the source of the white plastic has been identified as inspection tags used by an ingredient supplier. trident seafoods takes food safety very seriously and is investigating this situation thoroughly.</p><p>there have been no reports of injury or illness related to the recalled products to date, however anyone concerned about an injury or illness should contact a healthcare provider.</p><p>consumers who have purchased these products are urged not to consume them. these products should be thrown away or returned to the place of purchase. for additional information consumers can call ms. trev foley, consumer affairs manager, at 206-297-5825, monday &ndash; friday, 8 a.m. &ndash; 5 p.m. pst, or send email to <a href=\"mailto:trevf@tridentseafoods.com\">trevf@tridentseafoods.com </a>.</p> \r\n\t\t\t<p style=\"margin-bottom:0; letter-spacing: .125em; text-align: center;\"></p>\r\n\t\t")
            };
            WebViewRecallNotice.Source = htmlsource;
        }
        private string CreateHTML(string webcontent)
        {
            string css = @"<!doctype html>" +
                              "<body>" +
                                  webcontent +
                              "</body>" +
                          "</html>";
            return css;
        }
    }
}