using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.Messaging;
using Xamarin.Forms;
using ERecall.Views.InviteFriendsModule;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace ERecall.Droid.Activities
{
    [Activity(Label = "ContactApplication")]
    public class ContactApplicationActivity : Activity
    {
        public object smsTask { get; private set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var contactPickerIntent = new Intent(Intent.ActionPick,
                Android.Provider.ContactsContract.Contacts.ContentUri);
            StartActivityForResult(contactPickerIntent, 101);
        }

        protected override void OnRestart()
        {
            base.OnRestart();
            FinishAndRemoveTask();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (InviteFriends.IsSMS && !InviteFriends.IsEmail)
            {
                if (requestCode == 101 && resultCode == Result.Ok)
                {
                    //Ensure we have data returned
                    if (data == null || data.Data == null)
                        return;
                    var addressBook = new Xamarin.Contacts.AddressBook(this);
                    addressBook.PreferContactAggregation = false;

                    //Load the contact via the android contact id in the last segment of the Uri returned by the  android contact picker
                    var contact = addressBook.Load(data.Data.LastPathSegment);

                    //Use linq to find a mobile number
                    var mobile = (from p in contact.Phones
                                  where p.Type == Xamarin.Contacts.PhoneType.Mobile
                                  select p.Number).FirstOrDefault();

                    //See if the contact has a mobile number
                    if (string.IsNullOrEmpty(mobile))
                    {
                        Toast.MakeText(this, "No Mobile Number for contact!",
                            ToastLength.Short).Show();
                        return;
                    }
                    else
                    {
                        if (CrossMessaging.Current.SmsMessenger.CanSendSms)
                        {
                            CrossMessaging.Current.SmsMessenger.SendSms(mobile, "I just downloaded eRecall app on my Android. Download it for free: ");
                        }
                    }
                }
            }
            else if (!InviteFriends.IsSMS && InviteFriends.IsEmail)
            {
                //Ensure we have data returned
                if (data == null || data.Data == null)
                    return;
                var addressBook = new Xamarin.Contacts.AddressBook(this);
                addressBook.PreferContactAggregation = false;

                //Load the contact via the android contact id in the last segment of the Uri returned by the android contact picker
                var contact = addressBook.Load(data.Data.LastPathSegment);

                //Use linq to find a mobile number
                var emailid = (from p in contact.Emails
                               where p.Type == Xamarin.Contacts.EmailType.Home
                               select p.Address).FirstOrDefault();

                //See if the contact has a mobile number
                if (string.IsNullOrEmpty(emailid))
                {
                    Toast.MakeText(this, "No Email for contact!",
                        ToastLength.Short).Show();
                    return;
                }
                else
                {
                    var email = BuildSampleEmail(emailid, true).Build();
                    if (CrossMessaging.Current.EmailMessenger.CanSendEmail)
                    {
                        CrossMessaging.Current.EmailMessenger.SendEmail(email);
                    }
                }
            }

        }
        public static EmailMessageBuilder BuildSampleEmail(string ToEmailId, bool sendAsHtml = false)
        {
            //var builder = new EmailMessageBuilder()
            //    .To(ToEmailId)
            //    .Cc("cc.plugins@xamarin.com")
            //    .Bcc(new[] { "bcc1.plugins@xamarin.com", "bcc2.plugins@xamarin.com" })
            //    .Subject("eRecall App");

            var builder = new EmailMessageBuilder()
                .To(ToEmailId)
                .Subject("eRecall App");

            if (sendAsHtml)
                builder.BodyAsHtml("<b> I just downloaded eRecall app on my Android. Download it for free:  </b>");

            return builder;
        }
    }
}