﻿namespace FinTrackApi.Models.RequestModels
{
    public  class LoginModel
    {
        public required string Username { get; set; }

        public required string Password { get; set; }
    }
}
