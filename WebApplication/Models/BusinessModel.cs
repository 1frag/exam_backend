using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class BusinessModel
    {
        [Required(ErrorMessage = "You are anonymous?)")]
        [MaxLength(30, ErrorMessage = "nobody cannot read so long your name")]
        [MinLength(3, ErrorMessage = "You really? Turn on you imagination!")]
        public string Name { get; set; }

        [Required] public int CountStaff { get; set; }
        [DefaultValue(false)] public bool IsClosed { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateCreated
        {
            get => _dateCreated ?? DateTime.Now;
            set => _dateCreated = value;
        }

        private DateTime? _dateCreated;

        [DefaultValue(typeof(List<StaffModel>))]
        public List<StaffModel> StaffCollection { get; set; }
        [EmailAddress]
        [Required]
        public string OwnerEmail { get; set; }

        public override string ToString()
        {
            return $"Name = {Name}; " +
                   $"CountStaff = {CountStaff}; " +
                   $"DateCreated = {DateCreated}; " +
                   $"IsClosed = {IsClosed}; " +
                   $"OwnerEmail = {OwnerEmail}; " +
                   $"len(StaffCollection) = {StaffCollection?.Count}";
        }
    }
}