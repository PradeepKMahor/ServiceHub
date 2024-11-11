﻿namespace ServiceHub.WebApp.Models
{
    public class UserRolesViewModel
    {
        public string? UserId { get; set; } = string.Empty;
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string UserName { get; set; }
        public string? Email { get; set; } = string.Empty;
        public IEnumerable<string> Roles { get; set; }
    }
}