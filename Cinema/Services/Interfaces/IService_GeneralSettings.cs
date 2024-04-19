using Cinema.Payloads.DTO;
using Cinema.Payloads.Requests.Request_GeneralSettings;
using Cinema.Payloads.Response;

namespace Cinema.Services.Interfaces
{
    public interface IService_GeneralSettings
    {
        public Task<ResponseObject<DTO_GeneralSettings>> CreateGeneralSettings(Request_Create_GeneralSettings request);
        public Task<ResponseObject<DTO_GeneralSettings>> UpdateGeneralSettings(Request_Update_GeneralSettings request);
    }
}
