using EFKSystem.Application.Repositories.Product;
using EFKSystem.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFKSystem.Application.Features.Queries.ProductImageFile.GetPtoductImages
{
    public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, List<GetProductImagesQueryResponse>>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IConfiguration _configuration;

        public GetProductImagesQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration)
        {
            _productReadRepository = productReadRepository;
            _configuration = configuration;
        }

        public async Task<List<GetProductImagesQueryResponse>> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
        {
            Products? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
              .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

            return product.ProductImageFiles.Select(p => new GetProductImagesQueryResponse
            {
                Path = $"{_configuration["BaseStorageUrl"]}/{p.Path}",
               FileName= p.FileName,
               Id= p.Id
            }).ToList();
        }
    }
}
