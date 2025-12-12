using DocumentIngestion.Api.ExternalSystems;
using DocumentIngestion.Api.Invoices.Dtos;
using DocumentIngestion.Api.Invoices.Models;
using DocumentIngestion.Api.Invoices.Repositories;
using DocumentIngestion.Api.Invoices.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace DocumentIngestion.Unit.Tests
{
    public class InvoiceServiceTests
    {
        [Fact]
        public async Task CreateInvoiceAsync_WithValidData_SavesInvoiceAndPublishesEvent()
        {
            // Arrange
            var mockRepo = new Mock<IInvoiceRepository>();
            var mockNotifier = new Mock<IExternalSystemClient>();
            var mockLogger = new Mock<ILogger<InvoiceService>>();

            var service = new InvoiceService(
                                repo: mockRepo.Object, 
                                external: mockNotifier.Object, 
                                logger: mockLogger.Object);

            var request = new CreateInvoiceRequest
            {
                InvoiceNumber = "INV-001",
                SupplierId = Guid.NewGuid(),
                InvoiceDate = DateTime.UtcNow,
                Currency = "DKK",
                Lines = new List<CreateInvoiceLineRequest>
            {
                new() { Description = "Test item", Quantity = 2, UnitPrice = 50 }
            }
            };

            // Act
            var result = await service.CreateInvoiceAsync(request);

            // Assert
            result.Should().NotBeEmpty();

            mockRepo.Verify(r => r.SaveAsync(It.Is<Invoice>(i =>
                i.InvoiceNumber == request.InvoiceNumber &&
                i.Lines.Count == 1
            )), Times.Once);

            mockNotifier.Verify(n => n.NotifySupplierAsync(request.SupplierId), Times.Once);
        }
    }
}
