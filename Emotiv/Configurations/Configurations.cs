public static class Configurations
{
    public static bool AnalizeData { get; set; }

    public static int ProcessingTimeSeconds { get; set; } = 4;
}

// TODO: move to appsettings.json
public static class WebsocketConfigurations
{
    public static string ClientSecret = "2ZCvP22OjdRy5CLQFDPUQ6gpseChy8gxdjvDzjLGa1ySDlig2jGNoJzsT2LRc0x5FQCDGJODspnZn1myPkTuuGHoaOXRCzkxQZEyzH00RKoDtW3LypUqRtORWD6EP967";
    public static string ClientId = "tJhtbPk5KrZvy8xaQAgaBc0GLCx1g30zAuVjWNO6";
    public static string Headset = "INSIGHT-A2D202C0";
}
