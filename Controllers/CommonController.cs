using MVC_Entity_CRUD.Data;
using MVC_Entity_CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Entity_CRUD.Controllers
{
    public class CommonController : Controller
    {
        #region  Global Variable and Model Declaration
        CommonData cd = new CommonData();
        MVCEntityCRUD_dbEntities db = new MVCEntityCRUD_dbEntities(); 
        #endregion

        #region GetStateListbyCountryId
        public JsonResult GetStateListbyCountryId(int countryId)
        {
            try
            {
                // Get StateList From CommonData By Passing CountryId 
                SelectList StateList = new SelectList(cd.GetStatesListbyCountryId(countryId), "Value", "Text");
                return Json(StateList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region GetCityListbyStateId
        public JsonResult GetCityListbyStateId(int stateId)
        {
            try
            {
                // Get cityList From CommonData By Passing StateId 
                SelectList CityList = new SelectList(cd.GetCitiesListbyStateId(stateId), "Value", "Text");
                return Json(CityList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region GetPricebyProductId
        public JsonResult GetPricebyProductId(int productId)
        {
            try
            {
                // Get Price From CommonData By Passing productId 
                decimal? price = cd.GetPriceProductId(productId);
                return Json(price, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetUserProductDetail
        [HttpGet]
        public ActionResult GetUserProductDetail(int id)
        {
            try
            {
                #region Variable and Model Declaration
                List<UserProduct> userProducts = new List<UserProduct>();
                var products = new List<ProductModel>();
                #endregion
                // Get User Selected products from database 
                userProducts = db.UserProducts.Where(room => room.UserId == id).ToList();
                foreach (var item in userProducts)
                {
                    Product obj = db.Products.Where(x => x.ProductId == item.ProductId).ToList().FirstOrDefault();
                    products.Add(new ProductModel
                    {
                        ProductId = item.ProductId.Value,
                        ProductName = obj.ProductName,
                        Price = obj.Price.Value,
                        Recievername = item.Recievername
                    });
                }
                return Json(new { data = products }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}