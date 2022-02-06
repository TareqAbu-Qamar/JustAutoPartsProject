using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace JustAutoPartsProject.Models
{
    public class User : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required(ErrorMessage = "UserName is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }
        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            var exist = false;
            Countries.Countries c = new Countries.Countries();
            foreach (var city in Countries.Countries.countries)
            {
                if (city.Value.Equals(Address))
                {
                    exist = true;
                    break;
                }

            }
            if (!exist)
                yield return new ValidationResult("outside jordan location");
        }
        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [RegularExpression(@"(?=.*\d)(?=.*[A-Za-z]).{8,}", ErrorMessage = "Your password must be at least 8 characters long and contain at least 1 letter and 1 number")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNum { get; set; }

        private string role = "Customer";
        public string Role
        {
            get { return this.role; }
            set { this.role = value; }
        }
        private double balance = 3000;
        public double Balance
        {
            get { return this.balance; }
            set { this.balance = value; }
        }
        public void pay(double price)
        {
            if (price <= balance)
            {
                this.balance -= price;
            }
        }
        public virtual VirtualWallet virtualWallet { get; set; }
        public virtual ShoppingCart Cart { get; set; }
        public User() { }
        public User(int userID, string userName, string address, string email, string password, string phoneNum)
        {
            UserID = userID;
            UserName = userName;
            Address = address;
            Email = email;
            Password = password;
            PhoneNum = phoneNum;
        }
    }
}