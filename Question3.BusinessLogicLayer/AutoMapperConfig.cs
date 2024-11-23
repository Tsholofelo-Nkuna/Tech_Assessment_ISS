using AutoMapper;
using Question3.BusinessLogicLayer.Models.DataTransferObjects;
using Question3.DataAccessLayer.Entities;
using Question3.Models.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question3.BusinessLogicLayer
{
    internal class AutoMapperConfig : Profile
    {
        public AutoMapperConfig() {
            this.
                CreateMap<ClientDto, ClientEntity>()
                .ReverseMap();
            this
                .CreateMap<ContactPersonDto, ContactPersonEntity>()
                .ReverseMap();
        }
    }
}
