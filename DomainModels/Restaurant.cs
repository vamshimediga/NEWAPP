using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class Restaurant
    {
        [Required(ErrorMessage = "The RestaurantID is required.")]
        public int RestaurantID { get; set; }

        [Required(ErrorMessage = "The restaurant name is required.")]
        [StringLength(100, ErrorMessage = "The restaurant name cannot be longer than 100 characters.")]
        public string RestaurantName { get; set; }

        [Required(ErrorMessage = "The Address is required.")]
        [StringLength(200, ErrorMessage = "The address cannot be longer than 200 characters.")]
        public string Address { get; set; }


        [Required(ErrorMessage = "The City is required.")]
        [StringLength(100, ErrorMessage = "The city cannot be longer than 100 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "The State is required.")]
        [StringLength(50, ErrorMessage = "The state cannot be longer than 50 characters.")]
        public string State { get; set; }


        [Required(ErrorMessage = "The ZipCode is required.")]
        [StringLength(20, ErrorMessage = "The zip code cannot be longer than 20 characters.")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "The Phone is required.")]
        [StringLength(20, ErrorMessage = "The phone number cannot be longer than 20 characters.")]
        public string Phone { get; set; }
    }

}
