using Core.Presentation.Models.DataTransferObjects.Base;
using Core.Presentation.Models.Models.Base;
using System.Globalization;

namespace Core.Presentation.Models
{
    public class TableComponentViewModel<TRecordType> : GenericListViewModel<TRecordType> where TRecordType : BaseDto, new()
    {
       public TableComponentViewModel(): this(Enumerable.Empty<TRecordType>()){}
       public TableComponentViewModel(IEnumerable<TRecordType> modelState): base(modelState) { }
       public List<TableComponentColumnConfig> ColumnConfigs = new List<TableComponentColumnConfig>();
       public string DeleteAction = string.Empty;
       public string DeleteController = string.Empty;
       public string ArchiveAction = string.Empty;
       public string ArchiveController = string.Empty;  
       public string ViewAction {  get; set; } = string.Empty;
       public string ViewController { get; set; } = string.Empty;
    }

    public class TableComponentColumnConfig
    {
        public string Index { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
