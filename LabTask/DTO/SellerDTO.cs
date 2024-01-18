using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabTask.DTO
{
    public class SellerDTO
    {

        public int S_id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Name cannot contain numbers or special characters")]
        public string S_name { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string S_gender { get; set; }

        [Required(ErrorMessage = "Address is required")]

        public string S_address { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^01[0-9]{9}$", ErrorMessage = "Phone number must start with '01' and have exactly 11 digits.")]
        public string S_phone { get; set; }



        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[A-Za-z].*[A-Za-z])(?=.*\d)(?=.*[@#$%^&+=!]).{5,}$", ErrorMessage = "Password must be at least 5 characters long and contain at least 2 alphabetic characters, 1 number, and 2 special characters.")]

        public string Password { get; set; }


        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]

        public string Confirm_Password { get; set; }
    }
}