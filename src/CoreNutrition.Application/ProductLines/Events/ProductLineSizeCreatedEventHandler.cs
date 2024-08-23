using MediatR;

using CoreNutrition.Domain.ProductLineAggregate;
using CoreNutrition.Domain.ProductLineSizeAggregate.ValueObjects;
using CoreNutrition.Domain.ProductLineSizeAggregate.Events;
using CoreNutrition.Domain.Common.Interfaces.Persistence;

namespace CoreNutrition.Application.ProductLines.Events;

public class ProductLineSizeCreatedEventHandler
  : INotificationHandler<ProductLineSizeCreated>
{
  private readonly IProductLineRepository _productLineRepository;

  public ProductLineSizeCreatedEventHandler(IProductLineRepository productLineRepository)
  {
    _productLineRepository = productLineRepository;
  }
  public async Task Handle(
    ProductLineSizeCreated productLineSizeCreatedEvent,
    CancellationToken cancellationToken)
  {
    // if (await _productLineRepository.GetByIdAsync(productLineSizeCreatedEvent.ProductLineSize.ProductLineId) is not ProductLine productLine)
    // {
    //   // throw productLineId not found error
    // }

    // productLine.AddProductLineSizeId((ProductLineSizeId)productLineSizeCreatedEvent.ProductLineSize.Id);

    // await _productLineRepository.UpdateAsync(productLineSizeCreatedEvent);

    Console.WriteLine("Event Handler: ProductLineSize Created");
    throw new NotImplementedException();
  }
}