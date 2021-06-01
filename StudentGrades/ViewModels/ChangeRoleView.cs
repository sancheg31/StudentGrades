﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace StudentGrades.ViewModels
{
    public class ChangeRoleView
    {
        public ChangeRoleView()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        
    }
}
