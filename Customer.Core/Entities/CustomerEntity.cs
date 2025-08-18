using Core.Entitites;

namespace Customer.Core.Entities;

public class CustomerEntity
{
    public int Id { get; set; }

    public string CustomerName { get; set; } = null!;
}
