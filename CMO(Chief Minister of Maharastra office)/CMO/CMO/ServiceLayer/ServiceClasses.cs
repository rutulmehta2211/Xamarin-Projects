using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CMO.ServicesClasses
{

    #region Foriegn VisitList


public class CmVisit
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string image_alt { get; set; }
        public string image_title { get; set; }
        public string image_width { get; set; }
        public string image_height { get; set; }
        public string description { get; set; }
        public string date { get; set; }
        public string twitter_link { get; set; }
        public string facebook_link { get; set; }
        public string photo_gallery_id { get; set; }
        public string video_gallery_id { get; set; }
        //public string leftimage { get; set; }
        //public string rightimage { get; set; }
        #region maharastra visit & jalyukta visit
        public GridLength ImageWidth { get; set; }
        public GridLength TitleWidth { get; set; }
        public double TitleFontSize { get; set; }
		public int SetFontSize { get; set; }
        #endregion
    }

    public class RootObjectCMVisitList
    {
        public int _resultflag { get; set; }
        public string message { get; set; }
        public int total_results { get; set; }
        public int search_results { get; set; }
        public List<CmVisit> cm_visit { get; set; }
    }

    #endregion

    #region Event List in CM Visit Module


    public class Event
    {
        public string id { get; set; }
        public string user_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string event_start_date { get; set; }
        public string event_end_date { get; set; }
        public string image { get; set; }
        public string image_width { get; set; }
        public string image_height { get; set; }
        public string twitter_link { get; set; }
        public string facebook_link { get; set; }
        public GridLength ImageWidth { get; set; }
        public GridLength TitleWidth { get; set; }
		public int SetFontSize { get; set; }
        public double TitleFontSize { get; set; }
    }

    public class RootObjectEventList
    {
        public int _resultflag { get; set; }
        public string message { get; set; }
        public int total_results { get; set; }
        public int search_results { get; set; }
        public List<Event> @event { get; set; }
    }

    #endregion
    public class RootObject
        {
            public int _resultflag { get; set; }
            public string page_id { get; set; }
            public string page_title { get; set; }
            public string base_url { get; set; }
            public string page_content { get; set; }
        }
        public class CMOffice_ContactCMO
        {
            public int _resultflag { get; set; }
            public string message { get; set; }

        }

    public class Get_Set_PageId
    {
        public string PageId
        {
            get;
            set;
        }
    }

    #region Photo Gallery List Page


    public class Photo
    {
        public string photo_title { get; set; }
        public string photo_path { get; set; }
        public string thumb_path { get; set; }
        public string ipad_thumb_path { get; set; }
    }

    public class GalleryList
    {
        public string page_id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public ObservableCollection<Photo> photo { get; set; }
    }

    public class RootObjectPhotoGalleryList
    {
        public int _resultflag { get; set; }
        public int total_results { get; set; }
        public string message { get; set; }
        public int search_results { get; set; }
        public List<GalleryList> gallery_list { get; set; }
    }


    #endregion
    #region New Gallery List Page

    public class Newslist
    {
        public string page_id { get; set; }
        public string title { get; set; }
        public string date { get; set; }
        public string news_photo { get; set; }
        public string large_news_photo { get; set; }
        public string content { get; set; }
        public GridLength ImageWidth { get; set; }
        public GridLength TitleWidth { get; set; }

        public double TitleFontSize {get ; set;}
		public int SetFontSize { get; set; }
    }

    public class RootObjectNewsGalleryList
    {
        public int _resultflag { get; set; }
        public int total_results { get; set; }
        public string message { get; set; }
        public List<Newslist> newslist { get; set; }
    }

    #endregion

    #region Video Gallery List Page
    public class Videolist
    {
        public int page_id { get; set; }
        public string title { get; set; }
        public string video_thumb_path { get; set; }
        public string video_url { get; set; }
        public string content { get; set; }
        public string ipad_thumb_path { get; set; }
    }

    public class RootObjectVideoGalleryList
    {
        public int _resultflag { get; set; }
        public int total_results { get; set; }
        public string message { get; set; }
        public int search_results { get; set; }
        public List<Videolist> videolist { get; set; }
    }
    #endregion

    #region Magazines Gallery List Page
    public class RootObjectMagazineGallery
    {
        public int _resultflag { get; set; }
        public string page_id { get; set; }
        public string page_title { get; set; }
        public string message { get; set; }
        public string page_content { get; set; }
        public string href { get; set; }
        public string src { get; set; }
    }
    #endregion

    #region Photo Gallery Detail Page

    public class PhotoDetailObject
    {
        public string photo_title { get; set; }
        public string thumb_path { get; set; }
        public string photo_path { get; set; }
    }

    public class PhotoDetailGalleryList
    {
        public string title { get; set; }
        public string url { get; set; }
        public List<Photo> photo { get; set; }
    }

    public class RootObjectPhotoDetailResponse
    {
        public int _resultflag { get; set; }
        public string message { get; set; }
        public PhotoDetailGalleryList gallery_list { get; set; }
    }

    #endregion

    #region Make in maharashtra goes international country list main page

    public class MakeinMahaGoesInternationalCountry
    {
        public string id { get; set; }
        public string title { get; set; }
        public string country { get; set; }
        public string date { get; set; }
        public string map_image { get; set; }
        public string url { get; set; }
    }

    public class MakeinMahaGoesInternationalCountryListRootObject
    {
        public int _resultflag { get; set; }
        public string message { get; set; }
        public int total_results { get; set; }
        public int search_results { get; set; }
        public List<MakeinMahaGoesInternationalCountry> foreignvisit { get; set; }
    }

    #endregion
}

