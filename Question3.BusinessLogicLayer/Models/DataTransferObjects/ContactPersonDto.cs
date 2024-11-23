using Question3.DataAccessLayer.Entities.Base;
using Question3.Models.DataTransferObjects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question3.BusinessLogicLayer.Models.DataTransferObjects
{
    public class ContactPersonDto : BaseDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool IsPrimaryContact { get; set; }
    }
}
