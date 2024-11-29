using ClientManagement.UnitTests.DataProviders;
using Core.Presentation.Models.DataTransferObjects;
using Core.Presentation.Models.DataTransferObjects.Base;
using Core.Presentation.Models.Interfaces.Base;
using Core.Presentation.Models.Models.Base;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClientManagement.UnitTests
{
    [TestFixture]
    public  class GenericComponentTests
    {
        [TestCaseSource(typeof(GenericComponentTestsDataProvider), nameof(GenericComponentTestsDataProvider.GenericComponentWorks_DP))]
        public static bool GenericComponentBaseWorks(
            GenericListViewModel<ContactPersonDto> viewModel, 
            ContactPersonDto expectedOutput,
            string expectedEmail,
            bool expectedArchived) 
         
        {
            Assert.That(viewModel.ViewModelState.Single(x => x.Id == Guid.Empty).Archived, Is.False);

            return false;
        }
    }
}
