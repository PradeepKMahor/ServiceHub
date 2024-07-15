namespace ServiceHub.Domain.Context
{
    public class ExecContextOption
    {
        public ExecContextOption(bool disableCache)
        {
            DisableCache = disableCache;
        }

        public bool DisableCache { get; set; }
    }
}