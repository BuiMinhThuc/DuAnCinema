using Cinema.Entities;
using Cinema.Payloads.DTO;

namespace Cinema.Payloads.Converters
{
    public class Converter_GeneralSettings
    {
        public DTO_GeneralSettings EntityToDTO(GeneralSetting generalSettings)
            => new DTO_GeneralSettings
            {
                BreakTime = generalSettings.BreakTime,
                BusinessHours= generalSettings.BusinessHours,
                CloseTime= generalSettings.CloseTime,
                FixedTiketPrice = generalSettings.FixedTiketPrice,
                PercentDay = generalSettings.PercentDay,
                PercenWeekend = generalSettings.PercenWeekend,
                TimeBeginToChage = generalSettings.TimeBeginToChage,
            };
    }
}
