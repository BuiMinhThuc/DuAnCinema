using Cinema.Entities;
using Cinema.Payloads.DTO.DTO_Food;

namespace Cinema.Payloads.Converters
{
    public class Converter_Food
    {
        public DTO_Food EntityToDTO(Food food)
            => new DTO_Food
               {
                 Id = food.Id,
                 Price = food.Price,
                 Description = food.Description,
                 Image = food.Image,
                 NameOfFood = food.NameOfFood,
                 IsActive = food.IsActive,
               };
        public DTO_TopFood EntityToDTOs(Food food,int quantity)
           => new DTO_TopFood
           {
               Id = food.Id,
               Price = food.Price,
               NameOfFood = food.NameOfFood,
               quantity=quantity
           };

    }
}
