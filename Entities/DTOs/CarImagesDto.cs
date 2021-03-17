using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class CarImagesDto : IDto
    {
        public int CarImageId { get; set; }
        public int CarId { get; set; }
        public string BrandName { get; set; }
        public string ColorName { get; set; }
        public int ModelYear { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
