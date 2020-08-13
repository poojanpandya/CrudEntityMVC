using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Entity_CRUD.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Hobby
    {
        [NotMapped]
        public bool IsChecked { get; set; }
    }

    public partial class User
    {
        [NotMapped]
        public string FullName
        {
            get
            {
                return Firstname + " " + Lastname;
            }

        }
    }
   

}