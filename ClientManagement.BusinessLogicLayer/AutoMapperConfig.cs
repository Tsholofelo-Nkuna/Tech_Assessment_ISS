using AutoMapper;
using Core.Presentation.Models.DataTransferObjects;
using ClientManagement.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Presentation.Models.Models.DataTransferObjects;

namespace ClientManagement.BusinessLogicLayer
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
            this.CreateMap<ProductDto, ProductEntity>()
                .ReverseMap();
        }
    }
}
