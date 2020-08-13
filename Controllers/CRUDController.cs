using AutoMapper;
using MVC_Entity_CRUD.Data;
using MVC_Entity_CRUD.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Entity_CRUD.Controllers
{
    public class CRUDController : Controller
    {
        #region Global Variable and Model Declaration
        MVCEntityCRUD_dbEntities db = new MVCEntityCRUD_dbEntities();
        UserModel user = new UserModel();
        CommonData cd = new CommonData();
        private string absoluteUri;
        #endregion

        #region Using Entity
        #region User List
        public ActionResult Index()
        {
            try
            {
                //Get User List
                var userList = db.Users.ToList();
                return View(userList);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Add User
        public ActionResult AddUser()
        {
            try
            {
                //Get CountryList
                SelectList CountryList = new SelectList(cd.GetCountryList(), "Value", "Text");
                //Get ProductList
                SelectList ProductList = new SelectList(cd.GetProductList(), "Value", "Text");
                //Get HobbyList and Bind in UserHobbyList
                user.Hobbies = db.Hobbies.ToList();
                //Added in ViewBag
                ViewBag.CountryList = CountryList;
                ViewBag.ProductList = ProductList;
                return View(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Post Add Update User 
        [HttpPost]
        public ActionResult AddUpdateUser()
        {
            try
            {
                #region Variable and Model Declaration
                User user = new User();
                UserHobby userHobbies = new UserHobby();
                UserProduct userProduct = new UserProduct();
                string filename = "";
                int userId = 0;
                #endregion

                // Get Model data from js side in string format
                string json = Request.Form["model"];
                // Deserialization of Usermodel
                var model = JsonConvert.DeserializeObject<UserModel>(json);
                HttpFileCollectionBase files = Request.Files;

                // Check file exist or not
                if (files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        // Give File Path
                        var uploads = Path.Combine("~/Uploads");

                        if (!Directory.Exists(uploads))
                        {
                            Directory.CreateDirectory(uploads);
                        }

                        HttpPostedFileBase file = files[i];
                        filename = Path.GetFileNameWithoutExtension(file.FileName) + "_" + DateTime.Now.Ticks + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(Server.MapPath(uploads), filename);
                        // Get the complete folder path and store the file inside it.  
                        file.SaveAs(filePath);

                    }
                    model.FileName = filename;
                    model.FilePath = "/Uploads/" + filename;
                }
                if (model.UserId > 0)
                {
                    // Get Db User Data By UserId For Update
                    user = db.Users.Where(x => x.UserId == model.UserId).FirstOrDefault();
                }
                user.Firstname = model.Firstname;
                user.Lastname = model.Lastname;
                user.Dateofbirth = model.Dateofbirth;
                // If File is not null then insert new file path 
                if (model.FilePath != null)
                {
                    user.Profilepicture = model.FilePath;
                }
                // else use db file path
                else
                {
                    user.Profilepicture = user.Profilepicture;
                }
                user.Age = model.Age;
                user.Gender = model.Gender;
                user.CityId = model.CityId;
                user.Address = model.Address;
                user.Phoneno = model.Phoneno;
                // For Update Process
                if (model.UserId > 0)
                {
                    // If any of above data has changed it will updated
                    db.Entry(user).State = EntityState.Modified;
                }

                //For Insert Process
                else
                {
                    db.Users.Add(user);
                }

                db.SaveChanges();

                if (model.UserId > 0)
                {
                    // For Update use db Id
                    userId = model.UserId;
                    // For Update checkboxes and Jquery table delete previous inserted data from database
                    cd.DeleteData(userId);
                }
                else
                {
                    // For Insert use Inserted Id
                    userId = user.UserId;
                }
                // Fetch Hobbies List and insert into Hobbies Table
                foreach (var item in model.Hobbies)
                {
                    userHobbies.UserId = userId;
                    userHobbies.HobbyId = item.HobbyId;
                    db.UserHobbies.Add(userHobbies);
                    db.SaveChanges();
                }
                // Fetch Product List and insert into Product Table
                foreach (var item in model.Products)
                {
                    userProduct.UserId = userId;
                    userProduct.ProductId = item.ProductId;
                    userProduct.Recievername = item.Recievername;
                    db.UserProducts.Add(userProduct);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region EditUser
        [Obsolete]
        public ActionResult EditUser(Int32 id)
        {
            try
            {
                #region Variable and Model Declaration
                List<UserHobby> userHobbies = new List<UserHobby>();
                List<Hobby> Hobbies = new List<Hobby>();
                int? statedId = 0;
                int? countryId = 0;
                // Get Current Domain
                HttpContext httpContextAccessor = System.Web.HttpContext.Current;
                var request = httpContextAccessor.Request;
                absoluteUri = request.Url.GetLeftPart(UriPartial.Authority);
                #endregion

                // Get Data from UserId
                var data = db.Users.Where(x => x.UserId == id).FirstOrDefault();
                var map = Mapper.CreateMap<User,UserModel>();
                var userDto = Mapper.Map<User, UserModel>(data);

                userDto.convertedDate = data.Dateofbirth.Value.ToString("yyyy/MM/dd");

                ViewBag.profilepicture = (data.Profilepicture == null ? null : absoluteUri + data.Profilepicture);
              
                // Get StateId from CityId 
                statedId = db.Cities.Where(room => room.CityId == data.City.CityId).ToList().Select(x => x.StateId).FirstOrDefault();
                // Get countryId from StateId 
                countryId = db.States.Where(room => room.StateId == statedId).ToList().Select(x => x.CountryId).FirstOrDefault();
                // Bind CountryList with Selected value
                userDto.CountryId = countryId.Value;
                SelectList CountryList = new SelectList(cd.GetCountryList(), "Value", "Text");
                ViewBag.CountryList = CountryList;
                // Bind StateList with Selected value
                userDto.StateId = statedId.Value;
                SelectList StateList = new SelectList(cd.GetStatesListbyCountryId(countryId.Value), "Value", "Text");
                ViewBag.StateList = StateList;
                // Bind CityList with Selected value
                SelectList CityList = new SelectList(cd.GetCitiesListbyStateId(statedId.Value), "Value", "Text");
                ViewBag.CityList = CityList;
                // Bind HobbiList
                userDto.Hobbies = db.Hobbies.ToList();
                foreach (var item in userDto.Hobbies)
                {
                    // Get User Selected Hobbies From Database and match with HobbyId
                    UserHobby obj = db.UserHobbies.Where(x => x.UserId == id).ToList().Where(x => x.HobbyId == item.HobbyId).FirstOrDefault();

                    // If HobbyId matches With User Selected Hobby Table and Hobby Table then we will check that hobby to true else False  
                    if (obj != null)
                    {
                        Hobbies.Add(new Hobby
                        {
                            HobbyId = obj.HobbyId.Value,
                            HobbyName = item.HobbyName,
                            IsChecked = true
                        });
                    }
                    else
                    {
                        Hobbies.Add(new Hobby
                        {
                            HobbyId = item.HobbyId,
                            HobbyName = item.HobbyName,
                            IsChecked = false
                        });
                    }
                }
                // Bind HobbiList with checked values
                userDto.Hobbies = Hobbies;
                // Bind Product List
                SelectList ProductList = new SelectList(cd.GetProductList(), "Value", "Text");
                ViewBag.ProductList = ProductList;
                userDto.UserId = id;
                return View(userDto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DeleteUser
        public ActionResult DeleteUser(Int32 id)
        {
            try
            {
                // Get User from UserId
                var user = db.Users.Where(x => x.UserId == id).FirstOrDefault();
                // Delete User
                db.Entry(user).State = EntityState.Deleted;
                // Delete all tables data that associated with deleted user
                cd.DeleteData(id);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion 
        #endregion

        #region Using Entity Stored Procedures
        #region User List SP
        public ActionResult IndexSP()
        {
            try
            {
                //Get User List
                var userList = db.SP_GetUserList();
                return View(userList);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Post Add Update User SP
        [HttpPost]
        public ActionResult AddUpdateUserSP()
        {
            try
            {
                #region Variable and Model Declaration
                UserHobby userHobbies = new UserHobby();
                UserProduct userProduct = new UserProduct();
                string filename = "";
                int userId = 0;
                #endregion

                // Get Model data from js side in string format
                string json = Request.Form["model"];
                // Deserialization of Usermodel
                var model = JsonConvert.DeserializeObject<UserModel>(json);
                HttpFileCollectionBase files = Request.Files;

                // Check file exist or not
                if (files.Count > 0)
                {
                    for (int i = 0; i < files.Count; i++)
                    {
                        // Give File Path
                        var uploads = Path.Combine("~/Uploads");

                        if (!Directory.Exists(uploads))
                        {
                            Directory.CreateDirectory(uploads);
                        }

                        HttpPostedFileBase file = files[i];
                        filename = Path.GetFileNameWithoutExtension(file.FileName) + "_" + DateTime.Now.Ticks + Path.GetExtension(file.FileName);
                        var filePath = Path.Combine(Server.MapPath(uploads), filename);
                        // Get the complete folder path and store the file inside it.  
                        file.SaveAs(filePath);

                    }
                    model.FileName = filename;
                    model.FilePath = "/Uploads/" + filename;
                }
                ObjectParameter myOutputParamInt = new ObjectParameter("UserScalarId", typeof(Int32));
                // For Update Process
                if (model.UserId > 0)
                {
                    var data = db.SP_InsertUpdateUser(model.UserId, model.Firstname, model.Lastname, model.Dateofbirth, model.FilePath, model.Age, model.Gender, model.CityId, model.Address, model.Phoneno, myOutputParamInt);
                    db.Database.Connection.Close();
                }

                //For Insert Process
                else
                {
                    userId = db.SP_InsertUpdateUser(model.UserId, model.Firstname, model.Lastname, model.Dateofbirth, model.FilePath, model.Age, model.Gender, model.CityId, model.Address, model.Phoneno, myOutputParamInt).FirstOrDefault().Value;
                }

                if (model.UserId > 0)
                {
                    // For Update use db Id
                    userId = model.UserId;
                }
                // Fetch Hobbies List and insert into Hobbies Table
                foreach (var item in model.Hobbies.ToList())
                {
                    db.SP_InsertHobbies(userId, item.HobbyId);
                }
                // Fetch Product List and insert into Product Table
                foreach (var item in model.Products)
                {
                    db.SP_InsertProducts(userId, item.ProductId, item.Recievername);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Delete User SP 
        public ActionResult DeleteUserSP(Int32 id)
        {
            try
            {
                // Get User from UserId
                var user = db.SP_DeleteUser(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion 
        #endregion
    }
}