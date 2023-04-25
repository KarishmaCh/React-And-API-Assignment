using MatterWebAPI.Dto;

namespace MatterWebAPI.Interface
{
    public interface IAttorneyService
    {
        List<AttorneyDTO> GetAttorneys();
        AttorneyDTO GetAttorneyById(int id);
        void CreateAttorney(AttorneyDTO attorneyDto);
        void UpdateAttorney(int id, AttorneyDTO attorneyDto);
        void DeleteAttorney(int id);

    }
}
