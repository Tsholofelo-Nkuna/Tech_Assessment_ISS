using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question3.Models.DataTransferObjects.Base
{
    public class BaseDto
    {
        public Guid Id { get; set; }
        public bool Archived { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
