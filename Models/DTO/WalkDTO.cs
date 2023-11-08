using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models.DTO
{
    public class WalkDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        // public Guid DifficultyId { get; set; }
        // public  Guid RegionId { get; set; }
        public DifficultyDTO difficulty{get;set;}
        public RegionDto region{get;set;}
        
    }
}