using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CargoMate.DataAccess.Contracts;
using CargoMate.Web.FrontEnd.Models;
using CargoMate.Web.FrontEnd.Shared;
using CargoMate.Web.FrontEnd.Models.VehicleQuery;

namespace CargoMate.Web.FrontEnd.APIs.V1
{
    public class SharedController : BaseController
    {
        public SharedController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<KeyValuePairViewModel> GetCountriesAutoComplete(string cultureCode = "en-US", string serachTerm = "")
        {
            return UnitOfWork.LocalizedCountries.GetWhere(c => c.CultureCode == cultureCode && c.Name.Contains(serachTerm)).Select(c => new KeyValuePairViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
        }

        public List<CountryViewModel> GetCountriesList(string cultureCode = "en-US")
        {
            return UnitOfWork.Countries.GetAll(includeProperties: "LocalizedCountries").Select(c => new CountryViewModel
            {
                Id = c.Id,
                Name = c.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                CountryCode = c.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).CountryCode,
                CurrencyCode = c.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).CurrencyCode,
                CurrencyLong = c.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).CurrencyLong,
                PhonCode = c.PhonCode,
                CurrencySymbol = c.CurrencySymbol,
                Flag = c.Flag
            }).ToList();
        }

        public CountryViewModel GetCountryById(long countryId, string cultureCode = "en-US")
        {
            return UnitOfWork.Countries.GetWhere(c => c.Id == countryId).Select(c => new CountryViewModel
            {
                Id = c.Id,
                Name = c.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).Name,
                CountryCode = c.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).CountryCode,
                CurrencyCode = c.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).CurrencyCode,
                CurrencyLong = c.LocalizedCountries.FirstOrDefault(lc => lc.CultureCode == cultureCode).CurrencyLong,
                PhonCode = c.PhonCode,
                CurrencySymbol = c.CurrencySymbol,
                Flag = c.Flag

            }).FirstOrDefault();
        }

        public CountryViewModel GetCountryByCountryCode(string countryCode = "SA", string cultureCode = "en-US")
        {
            return UnitOfWork.LocalizedCountries
                             .GetWhere(c => c.CountryCode == countryCode && c.CultureCode == cultureCode, "Country")
                             .Select(c => new CountryViewModel
                             {   
                                 Id = c.CountryId.Value,
                                 Name = c.Name,
                                 CountryCode = c.CountryCode,
                                 CurrencyCode = c.CurrencyCode,
                                 CurrencyLong = c.CurrencyLong,
                                 PhonCode = c.Country.PhonCode,
                                 CurrencySymbol = c.Country.CurrencySymbol,
                                 Flag = c.Country.Flag

                             }).FirstOrDefault();
        }

        public VehicleQuery GetVehicleForNomination()
        {
            return new VehicleQuery();
        }

    }
}
