﻿using System.ComponentModel.DataAnnotations;

namespace DormyWebService.ViewModels.UserModelViews
{
    public class SocialUser
    {
//        public int Id { get; set; }

        [Required]
        public string IdToken { get; set; }
        //        public string AuthToken { get; set; }

        [Required]
        public string Email { get; set; }

//        public string FirstName { get; set; }
//        public string LastName { get; set; }
//        public string Name { get; set; }
//        public string PhotoUrl { get; set; }
//        public string Provider { get; set; }
    }
}