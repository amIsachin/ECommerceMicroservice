using Core.Entitites;
using Product.Application.Interfaces;

namespace Product.Application.Quesries;

//public record GetAllProductQuery(ProductEntity productEntity);

public class GetAllProductQueryHandler
{
    private readonly IGenericRepository<ProductEntity> _repository;

    public GetAllProductQueryHandler(IGenericRepository<ProductEntity> epository)
    {
        _repository = epository;
    }

    public async Task<IEnumerable<ProductEntity>> Handle()
    {
        var allProducts = await _repository.GetAllAsync();

        if (allProducts is not null)
        {
            return allProducts;
        }

        return null!;
    }
}
