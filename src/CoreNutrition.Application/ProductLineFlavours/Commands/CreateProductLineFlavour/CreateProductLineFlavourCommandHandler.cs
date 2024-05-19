using MediatR;
using ErrorOr;

using CoreNutrition.Domain.ProductLineFlavourAggregate;
using CoreNutrition.Domain.Common.Interfaces.Persistence;

namespace CoreNutrition.Application.ProductLineFlavours.Commands.CreateProductLineFlavour;

internal sealed class CreateProductLineFlavourCommandHandler
  : IRequestHandler<CreateProductLineFlavourCommand, ErrorOr<ProductLineFlavour>>
{
  private readonly IProductLineFlavourRepository _productLineFlavourRepository;

  public CreateProductLineFlavourCommandHandler(
    IProductLineFlavourRepository productLineFlavourRepository
  )
  {
    _productLineFlavourRepository = productLineFlavourRepository;
  }

  public async Task<ErrorOr<ProductLineFlavour>> Handle(
    CreateProductLineFlavourCommand command,
    CancellationToken cancellationToken)
  {
    await Task.CompletedTask; // TODO delete later

    ErrorOr<ProductLineFlavour> productLineFlavourResult = ProductLineFlavour.Create(
      command.Flavour,
      command.ProductLineId,
      command.FlavourImageUrl
    );

    if (productLineFlavourResult.IsError)
    {
      return productLineFlavourResult.Errors; // errors bybass mapping pipeline
    }

    _productLineFlavourRepository.Add(productLineFlavourResult.Value);

    return productLineFlavourResult.Value;
  }
}