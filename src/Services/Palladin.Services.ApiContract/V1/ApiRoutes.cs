namespace Palladin.Services.ApiContract.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Identity
        {
            public const string Login = Base + "/identity/login";
        }

        public static class Dashboard
        {
            public const string GetCriticalSummary = Base + "/dashboard/getCriticalSummary";
            public const string GetHighSummary = Base + "/dashboard/getHighSummary";
            public const string GetMiddleSummary = Base + "/dashboard/getMiddleSummary";
            public const string GetLowSummary = Base + "/dashboard/getLowSummary";
        }
    }
}
