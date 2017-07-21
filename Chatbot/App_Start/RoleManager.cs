using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chatbot.App_Start
{
    public static class RoleManager
    {

        public static bool isLoggedIn(User user)
        {
            return user != null;
        }

        public static bool isAdmin(User user)
        {
            return isLoggedIn(user) && Role.Admin.ToString() == user.Role;
        }

        public static bool isDoctor(User user)
        {
            return isLoggedIn(user) && Role.Doctor.ToString() == user.Role;
        }

        public static bool isPatient(User user)
        {
            return isLoggedIn(user) && Role.Patient.ToString() == user.Role;
        }

    }

    public enum Role
    {
        Patient,
        Doctor,
        Admin
    }
}