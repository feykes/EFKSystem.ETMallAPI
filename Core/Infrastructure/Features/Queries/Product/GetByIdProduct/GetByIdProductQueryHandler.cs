﻿using EFKSystem.Application.Repositories.Product;
using EFKSystem.Domain.Entities;
using MediatR;

namespace EFKSystem.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository;

        public GetByIdProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            Products product = await _productReadRepository.GetByIdAsync(request.Id, false);
            return new(){
                Name=product.Name,
                Price=product.Price,
                Stock=product.Stock
            };
        }
    }
}
