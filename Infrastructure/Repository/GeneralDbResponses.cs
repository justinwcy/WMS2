using Application.DTO.Response;

namespace Infrastructure.Repository
{
    public static class GeneralDbResponses
    {
        public static ServiceResponse ItemAlreadyExist(string itemName) => 
            new(false, $"{itemName} already exists");

        public static ServiceResponse ItemNotFound(string itemName) => 
            new(false, $"{itemName} not found");

        public static ServiceResponse ItemCreated(string itemName) => 
            new(true, $"{itemName} successfully created");

        public static ServiceResponse ItemUpdated(string itemName) => 
            new(true, $"{itemName} successfully updated");

        public static ServiceResponse ItemDeleted(string itemName) => 
            new(true, $"{itemName} successfully deleted");
    }
}
