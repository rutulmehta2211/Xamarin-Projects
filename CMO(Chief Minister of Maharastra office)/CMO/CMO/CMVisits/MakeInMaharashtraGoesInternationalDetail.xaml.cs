using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CMO
{
	public partial class MakeInMaharashtraGoesInternationalDetail : ContentPage
	{
        int widthdevice;
        string countryId;
        public MakeInMaharashtraGoesInternationalDetail()
		{
			InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            DetailImage.HeightRequest = App.DeviceHeight * 0.40;
            DetailImageBackgrnd.HeightRequest = App.DeviceHeight * 0.40;
            DetailImageBackgrnd.WidthRequest = widthdevice;
			setfontsize();
        }
	    public void setfontsize()
		{
			Titles.FontSize = App.GetFontSizeMedium();
			Date.FontSize = App.GetFontSizeSmall();
			ContentDetail.FontSize = App.GetFontSizeSmall();
		}
		public MakeInMaharashtraGoesInternationalDetail(int width, string CountryId)
		{
			InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            countryId = CountryId;
			widthdevice = width;
            DetailImage.HeightRequest = App.DeviceHeight * 0.40;
            DetailImageBackgrnd.HeightRequest = App.DeviceHeight * 0.40;
            DetailImageBackgrnd.WidthRequest = widthdevice;
			setfontsize();
          
		}
		protected override async void OnAppearing()
		{
			
            DetailImage.HeightRequest = App.DeviceHeight * 0.40;
            DetailImageBackgrnd.HeightRequest = App.DeviceHeight * 0.40;
            DetailImageBackgrnd.WidthRequest = widthdevice;
            await CallWebServiceForForiegnVisitsDetail();
        }

		public async Task CallWebServiceForForiegnVisitsDetail()
		{
            if (!App.CheckInternetConnection())
            {
                await DisplayAlert(AppResources.LNetworkError, AppResources.LNoInternetConnection, AppResources.LOk);
            }
            else
            {
                try
                {
                    string lang = "";
                    if (Application.Current.Properties.ContainsKey("Language"))
                    { lang = Application.Current.Properties["Language"] as string; }

                    List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();
                    values.Add(new KeyValuePair<string, string>("lang", lang));
                    values.Add(new KeyValuePair<string, string>("title", countryId));
                    values.Add(new KeyValuePair<string, string>("country", ""));
                    values.Add(new KeyValuePair<string, string>("index", "0"));
                    values.Add(new KeyValuePair<string, string>("limit", "10"));

                    var response = await GeneralClass.GetResponse<CMO.ServicesClasses.RootObjectCMVisitList>("http://14.141.36.212/maharastracmo/api/cmforeignvisits", values);
                    if (response != null)
                    {
                        if (response._resultflag == 1)
                        {
                            this.Title = response.cm_visit[0].title.ToUpper();

                            for (int i = 0; i < response.cm_visit.Count; i++)
                            {
                                DateTime oDate = Convert.ToDateTime(response.cm_visit[i].date);
                                response.cm_visit[i].date = oDate.ToString("MMM. dd, yyyy");
                            }

                            DetailImage.Source = response.cm_visit[0].image;
                            Titles.Text = response.cm_visit[0].title;
                            Date.Text = response.cm_visit[0].date;

                            ContentDetail.Text = response.cm_visit[0].description.Replace("<p>", "").Replace("</p>", "");
                            DetailImage.WidthRequest = widthdevice;
                          
                            //  DetailImage.HeightRequest = widthdevice / 2;
                            CalenderImage.IsVisible = true;
                            //GalleryList.ItemsSource = response.cm_visit;
                            //GalleryList.RowHeight = widthdevice / 2;
                            ImageGrid.IsVisible = true;
                            DataStackGrid.IsVisible = true;
                            
                        }
                        else
                        {
							if (App.CurrentPage() == "makeinmaharashtrainternational")
								await DisplayAlert(AppResources.LError, AppResources.LSomethingWentWrong, AppResources.LOk);
                        }
                    }
                    else
                    {
						if (App.CurrentPage() == "makeinmaharashtrainternational")
						{
                          await DisplayAlert(AppResources.LError, AppResources.LSomethingWentWrong, AppResources.LOk);
						}
                    }
                }

                catch (WebException exception)
                {
					if (App.CurrentPage() == "makeinmaharashtrainternational")
					{
						if (exception.Message.Contains("Network"))
							await DisplayAlert(AppResources.LError, AppResources.LWebserverNotResponding, AppResources.LOk);
					}
                }
                loading.IsRunning = false;
            }
		}
	}
}

