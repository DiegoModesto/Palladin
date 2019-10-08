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
            public const string RefreshToken = Base + "/identity/refresh";
        }

        public static class Project
        {
            public const string GetAll = Base + "/project/getAll";
            public const string GetById = Base + "/project/getById";
            public const string Create = Base + "/project/create";
            public const string Update = Base + "/project/update";
            public const string Delete = Base + "/project/delete";
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
