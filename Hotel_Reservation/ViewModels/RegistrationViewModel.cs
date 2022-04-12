using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Hotel_Reservation.ViewModels
{
    [Index(nameof(PhoneNumber), IsUnique = true, Name  = "Unique Phone Number")]
    [Index(nameof(Email), IsUnique = true, Name = "Unique Email Address")]

    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Name is rquired")]
        public string Name { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone Number is required")]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^01[0125][0-9]{8}$", ErrorMessage = "Please Enter Valid Mobile Number.")]

        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [Display(Name = "Nationality Id")]
        public double NationalityId { get; set; }

        [Required(ErrorMessage = "Nationality is required")]

        public string Nationality { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]

        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is required")]

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }
    }
}
