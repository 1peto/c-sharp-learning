namespace WeatherApp
{
    public static class AppConfig
    {
        //API nastavenia
        public const string API_KLUC = "c192e22b5e3142970a05ae25a3ff8d90";
        public const string API_BASE_URL = "https://api.openweathermap.org/data/2.5";
        public const int API_TIMEOUT_SECONDS = 10;
        public const string UNITS = "metric";
        public const string LANGUAGE = "sk";

        //Subory a cesty
        public const string FAVORITES_FILE = "oblubene_mesta.txt";

        //UI a zobrazenie
        public const int FORECAST_DAYS = 5;

        //UI design
        public const int MENU_WIDTH = 50;
        public const string SEPARATOR_THICK = "=";
        public const string SEPARATOR_THIN = "─";
        public const string APP_TITLE = "🌤️ WEATHER APP 🌤️";
    }
}