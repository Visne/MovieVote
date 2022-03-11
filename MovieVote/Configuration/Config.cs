using Newtonsoft.Json;

namespace MovieVote.Configuration;

public class Config
{
    [JsonProperty(PropertyName = "port")]
    public int Port = 2000;

    [JsonProperty(PropertyName = "database")]
    public string Database = "database.db";

    [JsonProperty(PropertyName = "discord", Required = Required.Always)]
    public DiscordConfig Discord = null!;
}