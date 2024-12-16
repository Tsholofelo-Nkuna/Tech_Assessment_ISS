using Core.Presentation.Models.DataTransferObjects;
using Core.Presentation.Models.DataTransferObjects.Base;
using Core.Presentation.Models.Base;
using Microsoft.AspNetCore.Http.Extensions;
using System.Globalization;

namespace Core.Presentation.Models
{
    public class TableComponentViewModel<TRecordType> : GenericListViewModel<TRecordType>, IFilterable<BaseDto>
        where TRecordType : BaseDto, new()
    {
       public TableComponentViewModel(): this(Enumerable.Empty<TRecordType>()){}
       public TableComponentViewModel(IEnumerable<TRecordType> modelState): base(modelState) { }
       public List<TableComponentColumnConfig> ColumnConfigs = new List<TableComponentColumnConfig>();
       public bool ShowCreateNewButton { get; set; }
       public string DeleteAction = string.Empty;
       public string DeleteController = string.Empty;
       public string ArchiveAction = string.Empty;
       public string ArchiveController = string.Empty;  
       public string ViewAction {  get; set; } = string.Empty;
       public string ViewController { get; set; } = string.Empty;
       private BaseDto _filter = new BaseDto();
       public virtual BaseDto Filter
       {
            get
            {
                return _filter;
            }
            set
            {
              
                _filter = value;
            }
        }
        public virtual string QueryString(BaseDto filterSource)
        {
            var qB = new QueryBuilder();
            if (filterSource.Archived)
            {
                qB.Add(nameof(filterSource.Archived), filterSource.Archived.ToString());
            }
           
            return qB.ToString();
        }
    }

    public class TableComponentColumnConfig
    {
        public string Index { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

   
}
