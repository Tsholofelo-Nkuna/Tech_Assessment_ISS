using ClientManagement.UnitTests.DataProviders;

using Core.Presentation.Models;
using Core.Presentation.Models.DataTransferObjects.Base;
using Core.Presentation.ViewComponents.Components;

namespace ClientManagement.UnitTests
{
    [TestFixture]
    public class TableComponentTests
    {
        [TestCaseSource(typeof(TableComponentTestsDataProvider), nameof(TableComponentTestsDataProvider.CreateTableComponentWorks_DP))]
        public bool CreateTableComponentWorks<TDto>(TableComponentViewModel<TDto> model) where TDto : BaseDto, new()
        {
            var tComponent = new TableComponent<TDto>();
            Assert.That((tComponent.Model?.ViewModelState?.Count() ?? -1) >= 0, Is.True);

            tComponent.Model = model;
            tComponent.Model.ViewModelState =  tComponent.Model.ViewModelState.Append(new TDto());
           
            Assert.That(tComponent.Model.Get(Guid.Empty, nameof(BaseDto.Archived), typeof(bool)), Is.False);
            tComponent.Model.Set(Guid.Empty, nameof(BaseDto.Archived), true);
            return tComponent.Model.Get(Guid.Empty, nameof(BaseDto.Archived), typeof(bool)) ;
        }
    }


}
