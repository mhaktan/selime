using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;

namespace Selime.PartCatalogs.Dto
{
    [AutoMapTo(typeof(Entities.PartCatalog))]
    public class CreatePartCatalogDto
    {
        [Required]
        [MaxLength(100)]
        public string PartNumber { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(50)]
        public string UnitOfMeasure { get; set; }

        public int StockQuantity { get; set; }

    }
}