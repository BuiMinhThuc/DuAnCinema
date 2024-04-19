using Cinema.DataContext;
using Cinema.Entities;
using Cinema.Payloads.Converters;
using Cinema.Payloads.DTO;
using Cinema.Payloads.Requests.Request_GeneralSettings;
using Cinema.Payloads.Response;
using Cinema.Services.Interfaces;

namespace Cinema.Services
{
    public class Service_GeneralSettings : IService_GeneralSettings
    {
        private readonly AppDbContext dbContext;
        private readonly ResponseObject<DTO_GeneralSettings> responseObject;
        private readonly Converter_GeneralSettings converter;

        public Service_GeneralSettings(AppDbContext dbContext, ResponseObject<DTO_GeneralSettings> responseObject, Converter_GeneralSettings converter)
        {
            this.dbContext = dbContext;
            this.responseObject = responseObject;
            this.converter = converter;
        }

       
        public async Task<ResponseObject<DTO_GeneralSettings>> CreateGeneralSettings(Request_Create_GeneralSettings request)
        {
            var generalSetting = new GeneralSetting
            {
                BreakTime = request.BreakTime,
                BusinessHours = request.BusinessHours,
                CloseTime  = request.CloseTime,
                FixedTiketPrice = request.FixedTiketPrice,
                PercentDay = request.PercentDay,
                PercenWeekend = request.PercenWeekend,
                TimeBeginToChage = request.TimeBeginToChage,
            };
            dbContext.generalSettings.Add(generalSetting);
            dbContext.SaveChanges();
            return responseObject.ResponseSuccess("Thêm thành công !", converter.EntityToDTO(generalSetting));
        }

        public async Task<ResponseObject<DTO_GeneralSettings>> UpdateGeneralSettings(Request_Update_GeneralSettings request)
        {
            var generalSetting = dbContext.generalSettings.Find(request.Id);
            if(generalSetting == null) { return responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy generelsetting này!", null); }
            generalSetting.BreakTime = request.BreakTime;
            generalSetting.BusinessHours = request.BusinessHours;
            generalSetting.CloseTime = request.CloseTime;
            generalSetting.FixedTiketPrice = request.FixedTiketPrice;
            generalSetting.PercentDay = request.PercentDay;
            generalSetting.PercenWeekend = request.PercenWeekend;
            generalSetting.TimeBeginToChage = request.TimeBeginToChage;
           
            dbContext.generalSettings.Update(generalSetting);
            dbContext.SaveChanges();
            return responseObject.ResponseSuccess("Sửa thành công !", converter.EntityToDTO(generalSetting));
        }
    }
}
