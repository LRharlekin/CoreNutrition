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
    var productLineFlavour = _productLineFlavourRepository.GetById(query.Id);

    if (productLineFlavour is null)
    {
      return Errors.ProductLineFlavour.NotFound;
    }

    return productLineFlavour;
  }
}