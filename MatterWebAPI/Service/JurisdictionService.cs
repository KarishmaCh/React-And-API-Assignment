using MatterWebAPI.Dto;
using MatterWebAPI.Models;

namespace MatterWebAPI.Service
{
    public class JurisdictionService
    {
        private readonly AppDbContext _context;

        public JurisdictionService(AppDbContext context)
        {
            _context = context;
        }

        public List<JurisdictionDTO> GetAll()
        {
            // Get all jurisdictions from the database
            var jurisdictions = _context.Jurisdictions.ToList();

            // Map jurisdictions to DTOs
            var jurisdictionDTOs = jurisdictions.Select(j => new JurisdictionDTO
            {
                Id = j.Id,
                Name = j.Name
            }).ToList();

            return jurisdictionDTOs;
        }

        public JurisdictionDTO GetById(int id)
        {
            // Get jurisdiction by id from the database
            var jurisdiction = _context.Jurisdictions.FirstOrDefault(j => j.Id == id);

            // If no jurisdiction found, return null
            if (jurisdiction == null)
            {
                return null;
            }

            // Map jurisdiction to DTO
            var jurisdictionDTO = new JurisdictionDTO
            {
                Id = jurisdiction.Id,
                Name = jurisdiction.Name
            };

            return jurisdictionDTO;
        }

        public void Create(JurisdictionDTO jurisdictionDTO)
        {
            // Map DTO to jurisdiction
            var jurisdiction = new Jurisdiction
            {
                Name = jurisdictionDTO.Name
            };

            // Add jurisdiction to database and save changes
            _context.Jurisdictions.Add(jurisdiction);
            _context.SaveChanges();
        }

        public void Update(int id, JurisdictionDTO jurisdictionDTO)
        {
            // Get jurisdiction by id from the database
            var jurisdiction = _context.Jurisdictions.FirstOrDefault(j => j.Id == id);

            // If no jurisdiction found, return null
            if (jurisdiction == null)
            {
                return;
            }

            // Update jurisdiction with DTO values
            jurisdiction.Name = jurisdictionDTO.Name;

            // Save changes to database
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            // Get jurisdiction by id from the database
            var jurisdiction = _context.Jurisdictions.FirstOrDefault(j => j.Id == id);

            // If no jurisdiction found, return null
            if (jurisdiction == null)
            {
                return;
            }

            // Remove jurisdiction from database and save changes
            _context.Jurisdictions.Remove(jurisdiction);
            _context.SaveChanges();
        }
    }
}
