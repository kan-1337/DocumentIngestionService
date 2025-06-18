namespace DocumentIngestion.Integration.Tests.Models;

public static class InvoiceQueryTheoryData
{
    public static IEnumerable<object[]> GetPagedCases()
    {
        var supplier1 = Guid.NewGuid();
        var supplier2 = Guid.NewGuid();

        // 1st test: page 1, pageSize 2, expect 2 results
        yield return new object[]
        {
            new InvoicePagedQueryTestData
            {
                TestName = "First page, expect 2",
                Page = 1,
                PageSize = 2,
                ExpectedCount = 2,
                SupplierId = null
            }
        };
        // 2nd test: filter by supplier
        yield return new object[]
        {
            new InvoicePagedQueryTestData
            {
                TestName = "Filter by Supplier1, expect 1",
                Page = 1,
                PageSize = 5,
                ExpectedCount = 1,
                SupplierId = supplier1
            }
        };
    }
}
