using MatterWebAPI.Models;

namespace MatterWebAPI.Dto
{
    public class MatterDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int JurisdictionId { get; set; }
        public int BillingAttorneyId { get; set; }
        public int ResponsibleAttorneyId { get; set; }
        public int ClientId { get; set; }
        public DateTime CreatedOn { get; set; }

        public MatterDto(int id, string name, int jurisdictionId, int billingAttorneyId, int responsibleAttorneyId, int clientId, DateTime createdOn)
        {
            Id = id;
            Name = name;
            JurisdictionId = jurisdictionId;
            BillingAttorneyId = billingAttorneyId;
            ResponsibleAttorneyId = responsibleAttorneyId;
            ClientId = clientId;
            CreatedOn = createdOn;
        }



    }
}
