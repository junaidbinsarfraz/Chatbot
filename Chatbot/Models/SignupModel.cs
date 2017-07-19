using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatbot.Models
{
    public class SignupModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public long Age { get; set; }
        public string Email { get; set; }
        public SignupAs signupAs { get; set; }
    }

    public enum SignupAs
    {
        Patient,
        Doctor
    }
}