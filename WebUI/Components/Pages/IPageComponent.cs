namespace WebUI.Components.Pages
{
    public interface IPageComponent
    {
        public string Name { get; }
        public Task<bool> FetchData();
    }
}
