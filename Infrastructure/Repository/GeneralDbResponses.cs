using Application.DTO.Response;

namespace Infrastructure.Repository
{
    public static class GeneralDbResponses
    {
        public static ServiceResponse ItemAlreadyExist(string itemName) => 
            new(false, GeneralResponses.ItemAlreadyExist(itemName));

        public static ServiceResponse ItemNotFound(string itemName) => 
            new(false, GeneralResponses.ItemNotFound(itemName));

        public static ServiceResponse ItemCreated(string itemName) => 
            new(true, GeneralResponses.ItemCreated(itemName));

        public static ServiceResponse ItemUpdated(string itemName) => 
            new(true, GeneralResponses.ItemUpdated(itemName));

        public static ServiceResponse ItemDeleted(string itemName) => 
            new(true, GeneralResponses.ItemDeleted(itemName));
    }
}
