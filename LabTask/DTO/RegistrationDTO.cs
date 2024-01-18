using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabTask.DTO
{
    public class RegistrationDTO
    {
        //[Required(ErrorMessage = "Name is required")]
        //[RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Name cannot contain numbers or special characters")]
        public string C_name { get; set; }

        // [Required(ErrorMessage = "Owner Name is required")]
        // [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Name cannot contain numbers or special characters")]

        public string C_phone { get; set; }
        // [Required(ErrorMessage = "Address is required")]

        public string C_address { get; set; }

        

        // [Required(ErrorMessage = "Password is required")]
        // [RegularExpression(@"^(?=.*[A-Za-z].*[A-Za-z])(?=.*\d)(?=.*[@#$%^&+=!]).{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least 2 alphabetic characters, 1 number, and 2 special characters.")]

        public string Password { get; set; }


        //  [Required(ErrorMessage = "Confirm Password is required")]
        //  [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]

        public string Confirm_Password { get; set; }
    }
}