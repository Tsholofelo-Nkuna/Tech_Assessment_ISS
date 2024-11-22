using System.Globalization;

namespace Question3.PresentationLayer.Models
{
    public class TableComponentViewModel
    {
       public  List<dynamic> data =  new List<dynamic>();
       public List<TableComponentColumnConfig> ColumnConfigs = new List<TableComponentColumnConfig>();
       public string DeleteAction = string.Empty;
       public string DeleteController = string.Empty;
       public string ArchiveAction = string.Empty;
       public string ArchiveController = string.Empty;  

    }

    public class TableComponentColumnConfig
    {
        public string Index { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
