using Newtonsoft.Json;

namespace WeatherApp
{
    // Json triedy pre deserializaciu weather api
    public class WeatherData
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "";

        [JsonProperty("main")]
        public MainData Main { get; set; } = new MainData();

        [JsonProperty("weather")]
        public WeatherDescription[] Weather { get; set; } = new WeatherDescription[0];
    }

    //momentalna predpoved
    public class MainData
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }

        [JsonProperty("feels_like")]
        public double FeelsLike { get; set; }

        [JsonProperty("humidity")]
        public int Humidity { get; set; }
    }

    //momentalna predpoved
    public class WeatherDescription
    {
        [JsonProperty("main")]
        public string Main { get; set; } = "";

        [JsonProperty("description")]
        public string Description { get; set; } = "";
    }

    //deserializacia pre 5-day predpoved
    //hlavna triedna pre forecast
    public class ForecastData
    {
        [JsonProperty("list")]
        public ForecastItem[] List { get; set; } = new ForecastItem[0];
    }

    //forecast predpoved (jedna polozka predpovede)
    public class ForecastItem
    {
        [JsonProperty("dt")]
        public long DataTime { get; set; } //Unix timestamp

        [JsonProperty("main")]
        public MainData Main { get; set; } = new MainData(); //existujuca MainData

        [JsonProperty("weather")]
        public WeatherDescription[] Weather { get; set; } = new WeatherDescription[0];

        [JsonProperty("dt_txt")]
        public string DataTimeText { get; set; } = "";
    }
}