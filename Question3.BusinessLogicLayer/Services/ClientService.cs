using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Question3.BusinessLogicLayer.Interfaces;
using Question3.BusinessLogicLayer.Services.Base;
using Question3.DataAccessLayer;
using Question3.DataAccessLayer.Entities;
using Core.Presentation.Models.DataTransferObjects;
using System.Linq.Expressions;


namespace Question3.BusinessLogicLayer.Services
{
    public class ClientService : GenericService<ClientDto, ClientEntity>, IClientService
    {
        public ClientService(WebDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<bool> AddOrUpdate(ClientDto client)
        {
            if (client is { PrimaryContactEmail.Length: > 0 } or { PrimaryContactName.Length: > 0 } or { PrimaryContactPhone.Length: > 0 })
            {
               var removedContacts =  (await this.Get(x => x.Id == client.Id))
                    .SelectMany(x => x.ContactPerson)
                    .Select(x => new ContactPersonEntity { Id = x.Id});
                this._dbContext.RemoveRange(removedContacts);
                client.ContactPerson = client.ContactPerson
                   .Where(x => !x.IsPrimaryContact)
                   .Append(new()
                   {
                       Email = client.PrimaryContactEmail,
                       IsPrimaryContact = true,
                       Phone = client.PrimaryContactPhone,
                       Name = client.PrimaryContactName,
                   }).ToList();
               
            }

            if (client.Id != Guid.Empty)
            {
               return await this.Update(new() { client });
            }
            else
            {
                return await this.Insert(new() { client });
            }
        }

        public async Task<bool> Archive(IEnumerable<Guid> identifiers)
        {
            var toBeArchived = (await this.Get(x => !x.Archived && identifiers.Contains(x.Id))).Select(x =>
            {
                x.Archived = true;
                return x;
            }).ToList();
            return await this.Update(toBeArchived);
        }

        public IEnumerable<ClientDto> Get(ClientDto filter)
        {
            var query = this._entitySet.AsNoTracking();
            if (filter.Archived)
            {
                query = query.Where(x => x.Archived);
            }
            else
            {
                query = query.Where(x => !x.Archived);
            }

            if(filter.CompanyName?.Trim() is string validCompanyName and { Length: > 0 })
            {
                query = query.Where(x => x.CompanyName.Contains(filter.CompanyName));
            }
            var returned = this._mapper.Map<List<ClientDto>>(query.Include(x => x.ContactPerson).OrderByDescending(x => x.CreatedOn).ToList());
            returned.ForEach(x =>
            {
                var primaryContact = x.ContactPerson.FirstOrDefault(x => x.IsPrimaryContact);
                x.PrimaryContactEmail  = primaryContact?.Email ?? string.Empty;
                x.PrimaryContactPhone = primaryContact?.Phone ?? string.Empty;
                x.PrimaryContactName = primaryContact?.Name ?? string.Empty;
            });
            return returned;
        }

        public override Task<IEnumerable<ClientDto>> Get(Expression<Func<ClientEntity, bool>> filter)
        {
           var clients =  this._entitySet.Where(filter).AsNoTracking().Include(x => x.ContactPerson).ToList();
           return Task.FromResult<IEnumerable<ClientDto>>(this._mapper.Map<IEnumerable<ClientDto>>(clients));
        }

        public async override Task<bool> Delete(IEnumerable<Guid> identifiers)
        {
            var contactsToBeRemoved = (await this.Get(x => identifiers.Contains(x.Id)))
               .SelectMany(x => x.ContactPerson);
            var removedContacts = this._mapper.Map<IEnumerable<ContactPersonEntity>>(contactsToBeRemoved);
            this._dbContext.Contacts.RemoveRange(removedContacts);
            var removedClients = await this.Get(x => identifiers.Contains(x.Id));
            var removedClientE = this._mapper.Map<IEnumerable<ClientEntity>>(removedClients);
            this._entitySet.RemoveRange(removedClientE);
            return await this._dbContext.SaveChangesAsync() > 0;
        }
    }
}
