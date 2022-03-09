using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using System.Text.RegularExpressions;
using CMO.MenuController;

namespace CMO.CMOffice
{
    public partial class Contact_CMO : ContentPage
    {
        public Contact_CMO()
        {
            InitializeComponent();
            #region Title Labels 
            ContactName.Text = AppResources.LContactName;
            ContactEmail.Text = AppResources.LContactEmail;
            ContactSubject.Text = AppResources.LSubject;
            ContactMobileNumber.Text = AppResources.LContactMobileNumber;
            ContactMessage.Text = AppResources.LMessage;
            this.Title = AppResources.LContactcmo.ToUpper();
            SubmitButton.Text = AppResources.LCMOfficeContactUsSubmitButton;
            #region office address
            OfficeName.Text = AppResources.LCMOfficeName;
            OfficeAddress.Text = AppResources.LCMOfficeAddress;
            OfficePhoneNumber1.Text = AppResources.LCMOfficePhoneNumber1;
            OfficePhoneNumber2.Text = AppResources.LCMOfficePhoneNumber2;
            OfficeFaxNumber.Text = AppResources.LCMOfficeFaxNumber;
            OfficeEmail.Text = AppResources.LCMOfficeEmail;
            OfficePhoneNumber1.ClassId = AppResources.LCMOfficePhoneNumber1;
            OfficePhoneNumber2.ClassId = AppResources.LCMOfficePhoneNumber2;
            OfficeEmail.ClassId = AppResources.LCMOfficeEmail;
            #endregion
            #endregion
            #region Text Changed Events
            txtName.TextChanged += OnTextChangedEntry;
            txtEmail.TextChanged += OnTextChangedEntry;
            txtMobile.TextChanged += OnTextChangedEntry;
            txtSubject.TextChanged += OnTextChangedEntry;
            txtMessage.TextChanged += OnTextChangedEditor;
            #endregion
            //if (TargetPlatform.Android == Device.OS)
            //{
            //    stack1.Padding = new Thickness(5, 7, 0, 0);
            //    stack2.Padding = new Thickness(5, 7, 0, 0);
            //    stack3.Padding = new Thickness(5, 7, 0, 0);
            //    stack4.Padding = new Thickness(5, 7, 0, 0);
            //}
			setfontsize();
        }

		public void setfontsize()
		{
			ContactName.FontSize = App.GetFontSizeMedium();
			ContactEmail.FontSize = App.GetFontSizeMedium();
			ContactSubject.FontSize = App.GetFontSizeMedium();
			ContactMobileNumber.FontSize = App.GetFontSizeMedium();
			ContactMessage.FontSize = App.GetFontSizeMedium();
			SubmitButton.FontSize = App.GetFontSizeMedium();

			txtName.FontSize = App.GetFontSizeSmall();
            txtEmail.FontSize = App.GetFontSizeSmall();
            txtMobile.FontSize = App.GetFontSizeSmall();
            txtSubject.FontSize = App.GetFontSizeSmall();
            txtMessage.FontSize = App.GetFontSizeSmall();

			OfficeName.FontSize = App.GetFontSizeSmall();
			OfficeEmail.FontSize = App.GetFontSizeSmall();
			OfficeAddress.FontSize = App.GetFontSizeSmall();
			OfficeFaxNumber.FontSize = App.GetFontSizeSmall();
			OfficePhoneNumber1.FontSize = App.GetFontSizeSmall();
			OfficePhoneNumber2.FontSize = App.GetFontSizeSmall();

			NameAlert.FontSize = App.GetFontSizeSmall();
			EmailAlert.FontSize = App.GetFontSizeSmall();
			MobileAlert.FontSize = App.GetFontSizeSmall();
			SubjectAlert.FontSize = App.GetFontSizeSmall();
			MessageAlert.FontSize = App.GetFontSizeSmall();
		}

		protected override void OnAppearing()
        {
            Application.Current.Properties["CurrentPage"] = "contactcmo";
        }
        #region Setting Max length for all the fields
        void OnTextChangedEntry(object sender, EventArgs e)
        {
            Entry entry = sender as Entry;
            String val = entry.Text; //Get Current Text
            var maxLength = entry.ClassId;
            if (val.Length > Int32.Parse(maxLength))//If it is more than your character restriction
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                entry.Text = val; //Set the Old value
            }
        }
        void OnTextChangedEditor(object sender, EventArgs e)
        {
            Editor entry = sender as Editor;
            String val = entry.Text; //Get Current Text
            var maxLength = entry.ClassId;
            if (val.Length > Int32.Parse(maxLength))//If it is more than your character restriction
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                entry.Text = val; //Set the Old value
            }
        }
        #endregion
        public async void OnSubmitButtonClicked(object sender, EventArgs args)
        {
            bool NameFlag = true, EmailFlag = true, MessageFlag = true, SubjectFlag = true, NumberFlag = true;
            var _mobileNumber = txtMobile.Text;
            var _message = txtMessage.Text;
            var _name = txtName.Text;
            var _subject = txtSubject.Text;
            #region Name Validate
            if (string.IsNullOrEmpty(txtName.Text))
            { NameAlert.Text = AppResources.LContactName + AppResources.LFieldIsRequired; NameFlag = false; NameAlert.IsVisible = true; }
            else if (_name.Length < 3)
            { NameAlert.Text = AppResources.LNameLengthCheck; NameFlag = false; NameAlert.IsVisible = true; }
            else {
                txtName.Text = Regex.Replace(txtName.Text, @"[^\w\.-]","",
                    RegexOptions.None, TimeSpan.FromSeconds(0.5));
                #region disable all alerts
                NameAlert.IsVisible = false;
                NameFlag = true;
                #endregion
            }
            #endregion

            #region Email Vadidate
            if (string.IsNullOrEmpty(txtEmail.Text))
            { EmailAlert.Text = AppResources.LContactEmail + AppResources.LFieldIsRequired; EmailFlag = false; EmailAlert.IsVisible = true; }
            else if (!(Regex.Match(txtEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success))
            { EmailAlert.Text = AppResources.LEmailCheck; EmailFlag = false; EmailAlert.IsVisible = true; }
            else
            {
               
                #region disable all alerts
                EmailAlert.IsVisible = false;
                    EmailFlag = true;
                    #endregion
            }
            #endregion

            #region Mobile Validate
            if (string.IsNullOrEmpty(txtMobile.Text) || string.IsNullOrWhiteSpace(txtMobile.Text) )
            { MobileAlert.Text = AppResources.LContactMobileNumber + AppResources.LFieldIsRequired; NumberFlag = false; MobileAlert.IsVisible = true; }
            else if (_mobileNumber.Length < 10)
            { MobileAlert.Text = AppResources.LMobileNumberCheck; NumberFlag = false; MobileAlert.IsVisible = true; }
            else
            {
                txtMobile.Text = Regex.Replace(txtMobile.Text, @"[^\w\.-]", "",
                   RegexOptions.None, TimeSpan.FromSeconds(0.5));
                #region disable all alerts
                MobileAlert.IsVisible = false;
                                NumberFlag = true;
                                #endregion
            }
            #endregion

            #region Subject Validate
            if (string.IsNullOrEmpty(txtSubject.Text))
            { SubjectAlert.Text = AppResources.LSubject + AppResources.LFieldIsRequired; SubjectFlag = false; SubjectAlert.IsVisible = true; }
            else if (_subject.Length < 3)
            { SubjectAlert.Text = AppResources.LSubjectLengthCheck; SubjectFlag = false; SubjectAlert.IsVisible = true; }
            else
            {
                txtSubject.Text = Regex.Replace(txtSubject.Text, @"[^\w\.-]", "",
                  RegexOptions.None, TimeSpan.FromSeconds(0.5));
                #region disable all alerts
                SubjectAlert.IsVisible = false;
                            SubjectFlag = true;
                             #endregion
            }
            #endregion

            #region Message Valiodate
            if (string.IsNullOrEmpty(txtMessage.Text))
            { MessageAlert.Text = AppResources.LMessage + AppResources.LFieldIsRequired; MessageFlag = false; MessageAlert.IsVisible = true; }
            else if (_message.Length < 3)
            { MessageAlert.Text = AppResources.LMessageLengthCheck; MessageFlag = false; MessageAlert.IsVisible = true; }
            else
            {
                txtMessage.Text = Regex.Replace(txtMessage.Text, @"[^\w\.-]", "",
                 RegexOptions.None, TimeSpan.FromSeconds(0.5));
                #region disable all alerts
                MessageAlert.IsVisible = false;
                                MessageFlag = true;
                                #endregion
            }
            #endregion

            if (((MessageFlag == true) && (EmailFlag == true) && (NameFlag == true) && (SubjectFlag == true) && (NumberFlag == true)))
            {
                #region disable all alerts
                NameAlert.IsVisible = false;
                EmailAlert.IsVisible = false;
                MobileAlert.IsVisible = false;
                SubjectAlert.IsVisible = false;
                MessageAlert.IsVisible = false;
                #endregion
                if (!App.CheckInternetConnection())
                {
                    await DisplayAlert(AppResources.LNetworkError, AppResources.LNoInternetConnection, AppResources.LNo);
                }
                else
                {
                    try
                    {
                        List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>();
                        values.Add(new KeyValuePair<string, string>("name", txtName.Text.Trim()));
                        values.Add(new KeyValuePair<string, string>("email", txtEmail.Text.Trim()));
                        values.Add(new KeyValuePair<string, string>("mobile", txtMobile.Text));
                        values.Add(new KeyValuePair<string, string>("subject", txtSubject.Text.Trim()));
                        values.Add(new KeyValuePair<string, string>("message", txtMessage.Text.Trim()));

                        if (values != null)
                        {
                            var response = await GeneralClass.GetResponse<CMO.ServicesClasses.CMOffice_ContactCMO>("http://14.141.36.212/maharastracmo/api/contactusform", values);
                            if (response != null)
                            {
                                if (response._resultflag == 1)
                                {
                                    await DisplayAlert(AppResources.LMessage, AppResources.LThankYouMsg, AppResources.LOk);
                                    //   lblstatus.Text = AppResources.LThankYouMsg;
                                    txtName.Text = "";
                                    txtEmail.Text = "";
                                    txtMobile.Text = "";
                                    txtSubject.Text = "";
                                    txtMessage.Text = "";
                                    //  lblstatus.TextColor = Color.Green;
                                }
                                else
                                {
									if (App.CurrentPage() == "contactcmo")
                                    await DisplayAlert(AppResources.LError, AppResources.LSomethingWentWrong, AppResources.LOk);
                                }
                            }
                            else
                            {
								if (App.CurrentPage() == "contactcmo")
                                await DisplayAlert(AppResources.LError, AppResources.LSomethingWentWrong, AppResources.LOk);
                            }
                        }
                    }

                    catch (Exception ex)
                    {
						if (App.CurrentPage() == "contactcmo")
						{
							if (ex.Message.Contains("Network"))
								await DisplayAlert(AppResources.LError, AppResources.LWebserverNotResponding, AppResources.LOk);
						}
                    }
                }
            }
            else
            { }
        }
        public async  void OnPhoneTapped(object sender, EventArgs e)
        {
            var obj = sender as Label;
            if (await this.DisplayAlert(AppResources.LDialNumber, AppResources.LCall +obj.ClassId +"?",AppResources.LYes,AppResources.LNo))
            {
                var dialer = DependencyService.Get<IDialer>();
                if (dialer != null)
                {
                    dialer.Dial(obj.ClassId);
                }
            }
        }
        public void OnEmailTapped(object sender, EventArgs e)
        {
            var obj = sender as Label;
            string EmailId = "mailto:" + obj.ClassId;
            var uri = new Uri(EmailId);
            Device.OpenUri(uri);
        }
        #region Exit Application
        private bool _canClose = true;

        protected override bool OnBackButtonPressed()
        {
            //if (_canClose)
            //{
            //    ShowExitDialog();
            //}
            //return _canClose;
            Application.Current.MainPage = new SideMenu();
            return true;
        }

        private async void ShowExitDialog()
        {
            var answer = await DisplayAlert(AppResources.LExit, AppResources.LExitApp, AppResources.LYes, AppResources.LNo);
            if (answer)
            {
                if (Device.OS == TargetPlatform.Android)
                {
                    DependencyService.Get<IAndroidMethods>().CloseApp();
                }
                _canClose = false;
                OnBackButtonPressed();
            }
        }
        #endregion
    }
    public interface IDialer
    {
        bool Dial(string number);
    }
}
