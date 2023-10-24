using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareEngineeringAndDesignPatterns
{
    internal class UserData
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool TwoFactorAuthentication { get; set; }
        public bool IsAdmin { get; set; }
        public List<string> Tags { get; set; }
    }
}
