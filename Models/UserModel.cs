using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Entity_CRUD.Models
{
    public class UserModel
    {
        public int UserId { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$",ErrorMessage = "Only alphabets are allowed.")]
        public string Firstname { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$",ErrorMessage = "Only alphabets are allowed.")]
        public string Lastname { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Dateofbirth { get; set; }

        public string Profilepicture { get; set; }

        [Required]
        public int CountryId { get; set; }

        [Required]
        public int StateId { get; set; }

        [Required]
        public int CityId { get; set; }

        public decimal? Age { get; set; }

        [Required]
        public decimal? Gender { get; set; }

        public string Address { get; set; }

        [Required]
        [RegularExpression(@"^[0-9*#+]+$")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> Phoneno { get; set; }

        public int ProductId { get; set; }

        public string Recievername { get; set; }

        public decimal? Price { get; set; }

        public List<Hobby> Hobbies { get; set; }

        public List<UserProduct> Products { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string convertedDate { get; set; }

    }
}