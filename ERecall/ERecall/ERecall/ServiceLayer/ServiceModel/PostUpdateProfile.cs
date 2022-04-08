﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERecall.ServiceLayer.ServiceModel
{
    public class PostUpdateProfile
    {
        public class UpdateProfile
        {
            public object Id { get; set; }
            public int UserId { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public object ZipCode { get; set; }
            public int CountryId { get; set; }
            public object CompanyName { get; set; }
            public object CompanyAddress { get; set; }
            public object CompanyWebsite { get; set; }
            public object PrimaryContactName { get; set; }
            public object PrimaryContactEmail { get; set; }
            public object SecondaryContactName { get; set; }
            public object SecondaryContactEmail { get; set; }
            public object Notes { get; set; }
            public object ProfileImage { get; set; }
        }
        public class RootObject
        {
            public object Id { get; set; }
            public string Email { get; set; }
            public object EmailConfirmed { get; set; }
            public object PasswordHash { get; set; }
            public object SecurityStamp { get; set; }
            public object PhoneNumber { get; set; }
            public object PhoneNumberConfirmed { get; set; }
            public object TwoFactorEnabled { get; set; }
            public object LockoutEndDateUtc { get; set; }
            public object LockoutEnabled { get; set; }
            public object AccessFailedCount { get; set; }
            public object UserName { get; set; }
            public int UserId { get; set; }
            public object Name { get; set; }
            public int ZipCode { get; set; }
            public int CountryId { get; set; }
            public object Country { get; set; }
            public object IsDeleted { get; set; }
            public object CreatedDate { get; set; }
            public object AccountId { get; set; }
            public object AccountNo { get; set; }
            public object AccountName { get; set; }
            public object AccountTypeId { get; set; }
            public object ParentAccountId { get; set; }
            public object ValidFrom { get; set; }
            public object ValidTo { get; set; }
            public object CompanyName { get; set; }
            public object CompanyAddress { get; set; }
            public object CompanyWebsite { get; set; }
            public object PrimaryContactName { get; set; }
            public object PrimaryContactEmail { get; set; }
            public object SecondaryContactName { get; set; }
            public object SecondaryContactEmail { get; set; }
            public object Notes { get; set; }
            public object ProfileImage { get; set; }
            public object AccountUserId { get; set; }
            public object AccountTypeName { get; set; }
            public object UserSubscriptionId { get; set; }
            public object UserSubscription { get; set; }
            public object SubscriptionFrequencyId { get; set; }
            public object SubscriptionFrequency { get; set; }
            public object SubscriptionCategoryId { get; set; }
            public object SubscriptionCategory { get; set; }
            public object UserImportedProductId { get; set; }
            public object ImporedProductsCount { get; set; }
            public object OpenTickets { get; set; }
            public object AvailableReports { get; set; }
            public object Credits { get; set; }
            public object SavedAmount { get; set; }
            public object AccountNumber { get; set; }
            public object MWS_SellerId { get; set; }
            public object MWS_MarketPlaceId { get; set; }
            public object ManufacturerName { get; set; }
            public object ManufacturerCode { get; set; }
        }
    }
}
