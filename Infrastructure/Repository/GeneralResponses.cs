namespace Infrastructure.Repository
{
    public static class GeneralResponses
    {
        public static string ItemAlreadyExist(string itemName) => $"{itemName} already exists";

        public static string ItemNotFound(string itemName) => $"{itemName} not found";

        public static string ItemCreated(string itemName) => $"{itemName} successfully created";

        public static string ItemUpdated(string itemName) => $"{itemName} successfully updated";

        public static string ItemDeleted(string itemName) => $"{itemName} successfully deleted";
    }
}
