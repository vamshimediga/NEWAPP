using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels
{
    public class CustomerData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }           // Primary Key, Identity

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }          // NVARCHAR(50)

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }           // NVARCHAR(50)

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }              // NVARCHAR(100), Email validation

        [StringLength(15)]
        [Phone]
        public string PhoneNumber { get; set; }
    }

}
