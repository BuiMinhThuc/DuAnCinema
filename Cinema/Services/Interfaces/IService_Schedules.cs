using Cinema.Payloads.DTO;
using Cinema.Payloads.Requests.Request_Schedules;
using Cinema.Payloads.Response;

namespace Cinema.Services.Interfaces
{
    public interface IService_Schedules
    {
        public ResponseObject<DTO_Schedules> CreateSchedules(Request_CreateSchedule request);
        public ResponseObject<DTO_Schedules> UpdateSchedules(Request_UpdateSchedules request);
        public string DeleteSchedules(int id);
        public IQueryable<DTO_Schedules> GetAllSchedules(int pageSize,int pageNumber);

    }
}
