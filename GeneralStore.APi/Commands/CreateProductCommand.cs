using GeneralStore.Api.Dtos;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace GeneralStore.Api.Queries
{
    public class CreateProductCommand : IRequest<ProductDto>
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
        [Required]
        public Guid ManufacturerId { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
    }
}
