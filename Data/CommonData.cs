using MVC_Entity_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Entity_CRUD.Data
{
    public class CommonData
    {
        MVCEntityCRUD_dbEntities db = new MVCEntityCRUD_dbEntities();
        public List<SelectListItem> GetCountryList()
        {
            try
            {
                List<SelectListItem> countrylist = new List<SelectListItem>();

                countrylist = (from data in db.Countries
                               select data).ToList().Select(x =>
                                new SelectListItem()
                                {
                                    Text = x.CountryName.ToString(),
                                    Value = x.CountryId.ToString()
                                }).ToList();
                return countrylist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SelectListItem> GetProductList()
        {
            try
            {
                List<SelectListItem> productlist = new List<SelectListItem>();

                productlist = (from data in db.Products
                               select data).ToList().Select(x =>
                                new SelectListItem()
                                {
                                    Text = x.ProductName.ToString(),
                                    Value = x.ProductId.ToString()
                                }).ToList();
                return productlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SelectListItem> GetStatesListbyCountryId(int? countryId)
        {
            try
            {
                List<SelectListItem> statelist = new List<SelectListItem>();
                statelist = db.States.Where(room => room.CountryId == countryId).ToList().Select(x =>
                new SelectListItem()
                {
                    Text = x.StateName.ToString(),
                    Value = x.StateId.ToString()
                }).ToList();

                return statelist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<SelectListItem> GetCitiesListbyStateId(int? stateId)
        {
            try
            {
                List<SelectListItem> citylist = new List<SelectListItem>();
                citylist = db.Cities.Where(room => room.StateId == stateId).ToList().Select(x =>
                new SelectListItem()
                {
                    Text = x.CityName.ToString(),
                    Value = x.CityId.ToString()
                }).ToList();

                return citylist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal? GetPriceProductId(int productId)
        {
            try
            {
                decimal? price;
                price = db.Products.Where(room => room.ProductId == productId).Select(x => x.Price).FirstOrDefault();
                return price;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void DeleteData(int id)
        {
            var userHobbie = db.UserHobbies.Where(x => x.UserId == id).ToList();
            var userProd = db.UserProducts.Where(x => x.UserId == id).ToList();
            foreach (var item in userHobbie)
            {
                db.Entry(item).State = EntityState.Deleted;
            }
            foreach (var item in userProd)
            {
                db.Entry(item).State = EntityState.Deleted;
            }
            db.SaveChanges();
        }
    }
}