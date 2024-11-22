using Question3.Models.DataTransferObjects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question3.Models.DataTransferObjects
{
    public class ClientDto : BaseDto
    {
        public string CompanyName { get; set; } = string.Empty;
        public string TradingAs { get; set; } = string.Empty;
        public string LandlineNumber { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        
    }
}
