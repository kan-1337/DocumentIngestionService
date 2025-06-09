using DocumentIngestion.Api.Invoices.Dtos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace DocumentIngestion.Integration.Tests
{
    public class DocumentIngestionIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public DocumentIngestionIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task PostInvoice_WithLines_ReturnsInvoiceIdSuccessfully()
        {
            // Arrange: Create a client to interact with the API
            var client = _factory.CreateClient();
            var newInvoice = new
            {
                InvoiceNumber = "INV-420",
                SupplierId = Guid.NewGuid(),
                InvoiceDate = DateTime.UtcNow,
                TotalAmount = 100.00m,
                Currency = "DKK",
                Lines = new[]
                {
                    new { Description = "Socks", Quantity = 2, UnitPrice = 50.00m }
                }
            };

            // Act: Post a new invoice
            var postResponse = await client.PostAsJsonAsync("/invoices", newInvoice);
            var result = await postResponse.Content.ReadFromJsonAsync<Dictionary<string, Guid>>();

            // Assert: Check if the response is successful and contains the expected data
            result.Should().NotBeNull();
            result["invoiceId"].Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetInvoice_WithInvoiceId_ReturnsInvoiceSuccessfully()
        {
            // Arrange: Create a client to interact with the API
            var client = _factory.CreateClient();
            var newInvoice = new
            {
                InvoiceNumber = "INV-420",
                SupplierId = Guid.NewGuid(),
                InvoiceDate = DateTime.UtcNow,
                TotalAmount = 100.00m,
                Currency = "DKK",
                Lines = new[]
                {
                    new { Description = "Socks", Quantity = 2, UnitPrice = 50.00m }
                }
            };

            // Act: Post a new invoice
            var postResponse = await client.PostAsJsonAsync("/invoices", newInvoice); // Create it in memory first
            var responseData = await postResponse.Content.ReadFromJsonAsync<Dictionary<string, Guid>>();
            var invoiceId = responseData?["invoiceId"];
            var result = await client.GetAsync("/invoices/" + invoiceId);


            // Assert: Check if the response is successful and contains the expected data
            result.Should().NotBeNull();
            var invoice = await result.Content.ReadFromJsonAsync<InvoiceResponse>();
            invoice.Should().NotBeNull();
            invoice!.InvoiceNumber.Should().Be("INV-420");
            invoice.TotalAmount.Should().Be(100.00m);
            invoice.Lines.Should().HaveCount(1);
            invoice.Lines[0].Description.Should().Be("Socks");
        }

        [Fact]
        public async Task PostInvoice_WithNoLines_ReturnsBadRequestWithMessage()
        {
            // Arrange
            var client = _factory.CreateClient();
            var invalidInvoice = new
            {
                InvoiceNumber = "INV-999",
                SupplierId = Guid.NewGuid(),
                InvoiceDate = DateTime.UtcNow,
                TotalAmount = 0,
                Currency = "DKK",
                Lines = new List<object>()
            };

            // Act
            var response = await client.PostAsJsonAsync("/invoices", invalidInvoice);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            error.Should().NotBeNull();
            error["message"].Should().Be("At least one line item is required.");
        }

        [Fact]
        public async Task PostInvoice_WithSupplierId_ReturnsBadRequestWithMessage()
        {
            // Arrange
            var client = _factory.CreateClient();
            var invalidInvoice = new
            {
                InvoiceNumber = "INV-420",
                InvoiceDate = DateTime.UtcNow,
                Currency = "DKK",
                Lines = new[]
                {
                    new { Description = "Socks", Quantity = 2, UnitPrice = 50.00m }
                }
            };

            // Act
            var response = await client.PostAsJsonAsync("/invoices", invalidInvoice);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();

            error.Should().NotBeNull();
            error["error"].Should().Be("Must provide a supplier id");
        }
    }
}
