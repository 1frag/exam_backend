using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class StaffModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(32)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(32)]
        public string LastName { get; set; }
        [DefaultValue(false)]
        public bool Dismissed { get; set; }
        [DefaultValue(typeof(List<string>))]
        public List<string>Hobbies { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "dd-mm-yyyy")]
        public DateTime JoinAt { get; set; }
    }
}