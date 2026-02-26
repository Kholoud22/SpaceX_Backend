namespace SpaceX.API
{
    public class APIRoutes
    {
        public static class Launches
        {
            public const string GetAll = $"{Base}/period/{{period}}";

            public const string Get = $"{Base}/{{id}}";

            public const string Base = "/api/launches";

        }
    }
}
