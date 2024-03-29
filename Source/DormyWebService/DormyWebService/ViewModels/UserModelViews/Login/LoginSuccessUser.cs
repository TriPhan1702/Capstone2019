﻿namespace DormyWebService.ViewModels.UserModelViews.Login
{
    //Used for returning access token and information after successfully authenticated
    public class LoginSuccessUser
    {
        public int Id { get; set; }

        public string AccessToken { get; set; }

        public string Role { get; set; }

        public string Status { get; set; }

        public string Email { get; set; }
    }
}