using Core.Entitites;
using Product.Application.Interfaces;

namespace Product.Application.Commands;

public record AddProductCommand(ProductEntity productEntity);

public class AddProductCommandHandler
{
    private readonly IGenericRepository<ProductEntity> _repository;

    public AddProductCommandHandler(IGenericRepository<ProductEntity> repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(AddProductCommand command)
    {
        var product = new ProductEntity
        {
            ProductName = command.productEntity.ProductName
        };

        await _repository.AddAsync(product);
        await _repository.SaveAsync();
        return product.Id;
    }
}
