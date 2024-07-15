namespace ServiceHub.Domain.Context
{
    public class ViewContextOption
    {
        public ViewContextOption(bool disableCache)
        {
            DisableCache = disableCache;
        }

        public bool DisableCache { get; set; }
    }
}