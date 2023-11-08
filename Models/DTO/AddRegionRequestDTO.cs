using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Demo.Models.DTO
{
    public class AddRegionRequestDTO
    {
        
        [Required]
        [MinLength(3,ErrorMessage ="Code has to be a minimum of 3 characters")]
        [MaxLength(4, ErrorMessage = "Code cannot be longer than 4 characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage ="Name has to be a maximum of 15characters")]
        public  string Name { get; set; }
        public  string? RegionImageUrl { get; set; }
    }
}