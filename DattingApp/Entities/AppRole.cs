﻿using Microsoft.AspNetCore.Identity;

namespace DattingAppApi.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; } = [];
    }
}
