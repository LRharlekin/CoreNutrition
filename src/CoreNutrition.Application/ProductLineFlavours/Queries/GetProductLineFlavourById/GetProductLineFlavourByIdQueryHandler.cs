using MediatR;
using ErrorOr;

using CoreNutrition.Domain.ProductLineFlavourAggregate;
using CoreNutrition.Domain.ProductLineFlavourAggregate.ValueObjects;
using CoreNutrition.Domain.Common.Interfaces.Persistence;
using CoreNutrition.Domain.Common.DomainErrors;

namespace CoreNutrition.Application.ProductLineFlavours.Queries.GetProductLineFlavourById;

internal sealed class GetProductLineFlavourByIdQueryHandler
  : IRequestHandler<GetProductLineFlavourByIdQuery, ErrorOr<ProductLineFlavour>>
{
  private readonly IProductLineFlavourRepository _productLineFlavourRepository;

  public GetProductLineFlavourByIdQueryHandler(
    IProductLineFlavourRepository productLineFlavourRepository
  )
  {
    _productLineFlavourRepository = productLineFlavourRepository;
  }

  public async Task<ErrorOr<ProductLineFlavour>> Handle(
    GetProductLineFlavourByIdQuery query,
    CancellationToken cancellationToken)
  {
    await Task.CompletedTask; // TODO delete later

    Guid.TryParse(query.Id, out Guid guid);
    var productLineFlavourId = ProductLineFlavourId.Create(guid);

    var productLineFlavourResult = _productLineFlavourRepository.GetById(productLineFlavourId);

    if (productLineFlavourResult is null)
    {
      return Errors.ProductLineFlavour.NotFound;
    }

    return productLineFlavourResult;
  }
}