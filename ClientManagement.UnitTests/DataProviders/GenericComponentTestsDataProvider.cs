using Core.Presentation.Models.DataTransferObjects;
using Core.Presentation.Models.Models.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.UnitTests.DataProviders
{
    public static class GenericComponentTestsDataProvider
    {
        public static IEnumerable<TestCaseData>GenericComponentWorks_DP()
        {
            yield return new TestCaseData(
                new GenericListViewModel<ContactPersonDto>(Enumerable.Empty<ContactPersonDto>().Append(new ContactPersonDto())),
                new ContactPersonDto { Archived = true, Email = "tgnkuna768@gmail.com"},
                "tgnkuna768@gmail.com",
                true
                )
            .Returns(true);
        }

    }
}
