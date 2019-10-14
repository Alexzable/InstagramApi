using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class User
    {   [Key]
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string ProfePicture { get; set; }
        public string LinkBio { get; set; }
        public double Comments { get; set; }
        public double Likes { get; set; }
        public double IDInsta { get; set; }
        public string Location { get; set; }
        public string Html { get; set; }
        public double Media { get; set; }
        public double Followers { get; set; }

    }
}
