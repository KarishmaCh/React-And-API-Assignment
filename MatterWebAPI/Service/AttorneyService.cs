using System.Collections.Generic;
using System.Linq;

using MatterWebAPI.Dto;
using MatterWebAPI.Interface;
using MatterWebAPI.Models;

namespace MatterWebAPI.Services
{
    public class AttorneyService : IAttorneyService
    {
        private readonly AppDbContext _context;

        public AttorneyService(AppDbContext context)
        {
            _context = context;
        }

        public List<AttorneyDTO> GetAttorneys()
        {
            var attorneys = _context.Attorneys
                .Select(a => new AttorneyDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                    Role = a.Role,
                    JurisdictionId = a.JurisdictionId
                })
                .ToList();

            return attorneys;
        }

        public AttorneyDTO GetAttorneyById(int id)
        {
            var attorney = _context.Attorneys
                .Where(a => a.Id == id)
                .Select(a => new AttorneyDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                    Role = a.Role,
                    JurisdictionId = a.JurisdictionId
                })
                .FirstOrDefault();

            return attorney;
        }

        public void CreateAttorney(AttorneyDTO attorneyDto)
        {
            var attorney = new Attorney
            {
                Name = attorneyDto.Name,
                Role = attorneyDto.Role,
                JurisdictionId = attorneyDto.JurisdictionId
            };

            _context.Attorneys.Add(attorney);
            _context.SaveChanges();
        }

        public void UpdateAttorney(int id, AttorneyDTO attorneyDto)
        {
            var attorney = _context.Attorneys.FirstOrDefault(a => a.Id == id);

            if (attorney != null)
            {
                attorney.Name = attorneyDto.Name;
                attorney.Role = attorneyDto.Role;
                attorney.JurisdictionId = attorneyDto.JurisdictionId;

                _context.SaveChanges();
            }
        }

        public void DeleteAttorney(int id)
        {
            var attorney = _context.Attorneys.FirstOrDefault(a => a.Id == id);

            if (attorney != null)
            {
                _context.Attorneys.Remove(attorney);
                _context.SaveChanges();
            }
        }
    }
}