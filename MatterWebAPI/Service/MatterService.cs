﻿using MatterWebAPI.Models;
using MatterWebAPI.Dto;

namespace MatterWebAPI.Service
{
    public class MatterService
    {
        private readonly AppDbContext _dbContext;
        public MatterService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /*
        public void CreateNewMatter(MatterDto matterdto)
        {
            var matter = new Matter
            {
                Name = matterdto.Name,
                JurisdictionId = matterdto.JurisdictionId,
                BillingAttorneyId = matterdto.BillingAttorneyId,
                ResponsibleAttorneyId = matterdto.ResponsibleAttorneyId,
                ClientId = matterdto.ClientId,
                CreatedOn = matterdto.CreatedOn
            };
            _dbContext.Matters.Add(matter);
            _dbContext.SaveChanges();
        }*/

        public void CreateNewMatter(MatterDto matterDto)
        {
            var matter = new Matter
            {
                Name = matterDto.Name,
                JurisdictionId = matterDto.JurisdictionId,
                BillingAttorneyId = matterDto.BillingAttorneyId,
                ResponsibleAttorneyId = matterDto.ResponsibleAttorneyId,
                ClientId = matterDto.ClientId,
                CreatedOn = matterDto.CreatedOn
            };

            _dbContext.Matters.Add(matter);
            _dbContext.SaveChanges();
        }

        public List<Invoice> GetInvoicesByMatterId(int matterId)
        {
            var invoices = _dbContext.Invoices.Where(i => i.MatterId == matterId).ToList();
            return invoices;
        }


        public List<MatterDto> GetMattersByClientId(int clientId)
        {
            var matters = _dbContext.Matters.Where(m => m.ClientId == clientId).ToList();
            return matters.Select(m => new MatterDto(
                m.Id,
                m.Name, 
                (int)m.JurisdictionId,
                (int)m.BillingAttorneyId,
                (int)m.ResponsibleAttorneyId,
                (int)m.ClientId,
                m.CreatedOn
            )).ToList();
        }
    }
}
