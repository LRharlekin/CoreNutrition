using MediatR;
using ErrorOr;

using CoreNutrition.Domain.ProductLineFlavourAggregate;
using CoreNutrition.Domain.ProductLineAggregate.ValueObjects;
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

    Guid.TryParse(command.ProductLineId, out Guid guid);
    var productLineId = ProductLineId.Create(guid);

    Uri.TryCreate(command.FlavourImageUrl, UriKind.Absolute, out var flavourImageUrl);

    ErrorOr<ProductLineFlavour> productLineFlavourResult = ProductLineFlavour.Create(
      command.Flavour,
      productLineId,
      flavourImageUrl!
    );

    if (productLineFlavourResult.IsError)
    {
      return productLineFlavourResult.Errors;
    }

    _productLineFlavourRepository.Add(productLineFlavourResult.Value);

    return productLineFlavourResult.Value;
  }
}