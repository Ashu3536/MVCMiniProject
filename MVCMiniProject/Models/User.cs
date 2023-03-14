using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCMiniProject.Models
{
    public class User
    {
        public int RegisterId { get; set; }
        public string Name { get; set; }   
     public string EmailId { get; set; }
     public int CityId { get; set; }
     public string Password { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
       
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public List<User> LstUser { get; set; }
        //public string UploadImage { get; set; }
        //public HttpPostedFileBase ImageFile { get; set; }

        //public string ProfileImage2 { get; set; }
        public int ID { get; set; }
        public string ImagePath { get; set; }

    }
}