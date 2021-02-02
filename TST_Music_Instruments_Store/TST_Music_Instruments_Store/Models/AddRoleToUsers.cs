using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TST_Music_Instruments_Store.Models
{
    public class AddRoleToUsers
    {
        public List<String> Users_Emails { get; set; }
        [Display(Name = "Choose role")]
        public string SelectedRole { get; set; }
        public List<String> Roles { get; set; }
        [Display(Name = "Choose user")]
        public string SelectedUserEmail { get; set; }
        public AddRoleToUsers()
        {
            Users_Emails = new List<string>();
            Roles = new List<string>();
        }
    }
}