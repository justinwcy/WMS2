namespace WebUI.Components.Pages
{
    public interface IPageComponent
    {
        public Task<bool> FetchData();
    }
}
