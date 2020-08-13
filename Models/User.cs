//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_Entity_CRUD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.UserHobbies = new HashSet<UserHobby>();
            this.UserProducts = new HashSet<UserProduct>();
        }
    
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

     
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> Dateofbirth { get; set; }
        public string Profilepicture { get; set; }
        public Nullable<decimal> Age { get; set; }
        public Nullable<decimal> Gender { get; set; }
        public Nullable<int> CityId { get; set; }
        public string Address { get; set; }

        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> Phoneno { get; set; }

        public virtual City City { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserHobby> UserHobbies { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProduct> UserProducts { get; set; }
    }
}