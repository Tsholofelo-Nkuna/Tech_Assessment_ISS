﻿using Question3.DataAccessLayer.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question3.DataAccessLayer.Entities
{
    public class ContactPersonEntity: BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone {  get; set; } = string.Empty;
    }
}
