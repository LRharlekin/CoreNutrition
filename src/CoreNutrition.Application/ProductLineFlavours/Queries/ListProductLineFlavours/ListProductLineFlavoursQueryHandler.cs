using MediatR;
using ErrorOr;

using CoreNutrition.Domain.ProductLineFlavourAggregate;
using CoreNutrition.Domain.ProductLineFlavourAggregate.ValueObjects;
using CoreNutrition.Domain.Common.Interfaces.Persistence;
using CoreNutrition.Domain.Common.DomainErrors;

namespace CoreNutrition.Application.ProductLineFlavours.Queries.ListProductLineFlavours;

internal sealed class ListProductLineFlavoursQueryHandler
  : IRequestHandler<ListProductLineFlavoursQuery, ErrorOr<List<ProductLineFlavour>>>
{
  private readonly IProductLineFlavourRepository _productLineFlavourRepository;

  public ListProductLineFlavoursQueryHandler(
    IProductLineFlavourRepository productLineFlavourRepository
  )
  {
    _productLineFlavourRepository = productLineFlavourRepository;
  }

  public async Task<ErrorOr<List<ProductLineFlavour>>> Handle(
    ListProductLineFlavoursQuery query,
    CancellationToken cancellationToken)
  {

    /* pagination */
    // .Skip((query.Page - 1) * query.PageSize)
    // .Take(query.PageSize);

    await Task.CompletedTask; // TODO delete later
    var productLineFlavoursResult = _productLineFlavourRepository.GetAll();

    if (productLineFlavoursResult is null)
    {
      return Errors.ProductLineFlavour.ListNotFound;
    }

    return productLineFlavoursResult;
  }
}