using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERecall.ServiceLayer
{
    public class ServiceURLs
    {
        //public static string BaseURL = "http://apps.atozinfoway.in/dev/eRecallAPI";
        public static string BaseURL = "http://erecall.atozinfoway.com";

        #region Login_SignUp
        public static string Getcountries = BaseURL + "/api/mobile/countriesMobile/Getcountries";

        public static string SignIn = BaseURL + "/api/mobile/Default/SignIn?";// email=vinesh.patel@atozinfoway.com&password=Vinesh123";

        public static string SignUp = BaseURL + "/api/mobile/Default/SignUp"; 
        #endregion

        #region Feeds
        public static string GetLatestProducts = BaseURL + "/api/mobile/ProductRecallMobile/GetLatestProducts?";// PageIndex=0&PageSize=5";

        public static string GetPopularProducts = BaseURL + "/api/mobile/ProductRecallMobile/GetPopularProducts?";// PageIndex=0&PageSize=5";

        public static string GetMegaProducts = BaseURL + "/api/mobile/ProductRecallMobile/GetMegaProducts?";// PageIndex=0&PageSize=5"; 

        public static string GetRecalledItemDetails = BaseURL + "/api/mobile/RecalledItemMobile/GetRecalledItemDetails?";//recallId=297105

        public static string GetSearchResultList = BaseURL + "/api/SearchMobile/GetSearchResultList?";//searchFilter=t";

        public static string GetReleventRecall = BaseURL + "/api/RecalledItemMobile/GetReleventRecall?";//recallId=297104"

        #endregion

        #region Master Page (User Detail)
        public static string GetAccountProfileByUserId = BaseURL + "/api/mobile/AccountManagementMobile/GetAccountProfileByUserId/";//1 
        #endregion

        #region My Favorite
        public static string GeteRecallSearch = BaseURL + "/api/MyeRecallMobile/GeteRecallSearch?";//userId=8&catId=0"; 

        public static string GeteRecallSearch_searching = BaseURL + "/api/MyeRecallMobile/GeteRecallSearch?";//userId=8&catId=0&content=t";

        #endregion

        #region Vehicles

        public static string GetVehicleMakes = BaseURL + "/api/ProductsCenterMobile/GetVehicleMakes";

        public static string GetVehicleModels = BaseURL + "/api/ProductsCenterMobile/GetVehicleModels?";//makeId=221";

        public static string GetYearsforvehiclesModels = BaseURL + "/api/ProductsCenterMobile/GetYearsforvehiclesModels";

        public static string PostUserVehicle = BaseURL + "/api/ProductsCenterMobile/PostUserVehicle";

        #endregion

        #region Common Pages
        public static string PostIOwnThisProduct = BaseURL + "/api/mobile/RecalledItemMobile/PostIOwnThisProduct";

        public static string PostMyRecall = BaseURL + "/api/mobile/RecalledItemMobile/PostMyRecall";
        #endregion

        #region Product Center
        public static string GetListOfImportedProductsByCategory = BaseURL + "/api/mobile/ProductsCenterMobile/GetListOfImportedProductsByCategory?";//userId=8&catId=5";

        public static string deleteUserProducts = BaseURL + "/api/mobile/ProductsCenterMobile/deleteUserProducts";
        #endregion

        #region Settings
        public static string GetUserMobileSetting = BaseURL + "/api/mobile/UserMobileSetting/GetUserMobileSetting/";//31;

        public static string AddUsermobileSetting = BaseURL + "/api/mobile/UserMobileSetting/AddUsermobileSetting";
        #endregion

        #region My Account
        public static string PostUpdateProfile = BaseURL + "/api/mobile/AccountManagement/PostUpdateProfile";
        public static string ResetPassword = BaseURL + "/api/mobile/Default/ResetPassword?";//email=melzoghbi@gmail.com&password=admin123";
        #endregion

        #region New Claim
        public static string PostUserClaim = BaseURL + "/api/mobile/ResolutionCenterMobile/PostUserClaim";

        public static string GetSupplierList = BaseURL + "/api/ResolutionCenterMobile/GetSupplierList?";//recallId=297077";
        #endregion

        #region Resolutions
        public static string ProductClaimList = BaseURL + "/api/mobile/ResolutionCenterMobile/ProductClaimList?";//AccountId=1;

        public static string GetClaimsLists = BaseURL + "/api/mobile/ResolutionCenterMobile/GetClaimsLists?";//userId=8";

        public static string GetMessagesList = BaseURL + "/api/mobile/ResolutionCenterMobile/GetMessagesList?";//userId=1&claimId=2";

        public static string GetClaimListForBusinessUser = BaseURL + "/api/ResolutionCenterMobile/GetClaimListForBusinessUser?";//recallId=6331&accountId=2";

        public static string SubmitClaimMessages = BaseURL + "/api/ResolutionCenterMobile/SubmitClaimMessages";

        #endregion

        #region Notification
        public static string Notifications = BaseURL + "/api/mobile/SharedMobile/Notifications?";//userName=vinesh.patel@atozinfoway.com";

        public static string NotificationRead = BaseURL + "/api/mobile/SharedMobile/NotificationRead?";//NotificationId=127546"; 

        public static string RegisterDeviceGCM = BaseURL + "/api/Register/RegisterDeviceGCM?tags=8";//&deviceToken=APA91bEADoYwG4t3N_M4xnJgZWkz3MxH_HQz2cqZU5Id7CiuvB30jtgZdeVt2RN_DIDFPcf16vaD3LoROfo75bTOHU15xjA9e068wh3gzfU-YlcK1NIy_P-X_u8Bv6XU0aU07DUDCFuQWOItYxcaYu1c9k1lj9mSUw
        #endregion

        #region Scan
        public static string GetRecallStatusByUPCForMobile = BaseURL + "/api/Barcode/GetRecallStatusByUPCForMobile?";// upc=080006108863&userId=8&src=Amazon"; 
        #endregion

    }
}
