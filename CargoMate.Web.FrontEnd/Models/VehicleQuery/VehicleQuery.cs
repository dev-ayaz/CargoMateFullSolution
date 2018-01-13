using CargoMate.Web.FrontEnd.Models.VehicleViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargoMate.Web.FrontEnd.Models.VehicleQuery
{
    public class VehicleQuery
    {

        public long QueryId { get; set; }
        public CustomerViewModels.CustomerViewModel Customer { get; set; }
        //public List<VehicleBiddingPack> vehicleBiddingList = new ArrayList<>();
        public SearchVehicleViewModels.SearchVehicleViewModel SelectedVehicle { get; set; }
        public String selectedVehicleLocation { get; set; }
        public VehicleParaPack VehicleParaPack { get; set; }
        public GeoAddress PickupAddress { get; set; }
        public GeoAddress DropoffAddress { get; set; }
        public GeoAddress SearchAddress { get; set; }
        public string PickupLocation { get; set; }
        public string DropoffLocation { get; set; }
        public float Distance { get; set; }
        public float Time { get; set; }
        public float JobWorth { get; set; }
        public float PriceSurge { get; set; }
        public bool InsuredNeeded { get; set; }
        public bool FixedRate { get; set; }
        public bool DriverJobAccessed { get; set; }
        public float InsuranceAmount { get; set; }
        public List<PayLoadTypeViewModel> PayloadTypeList { get; set; }
        public TripTypesViewModel TripType { get; set; }
        public float PayloadWeight { get; set; }
        public string PayloadWeightUnit { get; set; }
        public int PayloadQty { get; set; }
        public string PayloadQtyUnit { get; set; }
        public string RecipientName { get; set; }
        public string RecipientPhone { get; set; }
        public string RecipientId { get; set; }
        public bool RecipientNotified { get; set; }
        public Dictionary<String, long> JobStatusMap { get; set; }
        public JobStatusEnums JobStatus { get; set; }
        public CountryViewModel NationalityPref { get; set; }
        public string CodeQRCustomer { get; set; }
        public string CodeQRRecipient { get; set; }
        public string PayloadSealCode { get; set; }
        public bool PayloadSealed { get; set; }
        public bool CodeQRCustomerVerified { get; set; }
        public bool CodeQRRecipientVerified { get; set; }
        public bool DriverDocVerified { get; set; }
        public bool VehicleDocVerified { get; set; }
        public float PayloadConditionRating { get; set; }
        public string PayloadConditionRemarks { get; set; }

    }
}