using Newtonsoft.Json;

namespace MovieVote.Configuration;

public class Config
{
    /// <summary>
    /// Server port.
    /// </summary>
    [JsonProperty(PropertyName = "port")]
    public int Port = 2000;

    /// <summary>
    /// Path to database file.
    /// </summary>
    [JsonProperty(PropertyName = "database")]
    public string Database = "database.db";

    /// <summary>
    /// Discord specific configuration.
    /// </summary>
    [JsonProperty(PropertyName = "discord", Required = Required.Always)]
    public DiscordConfig Discord = null!;

    /// <summary>
    /// Session cookie expiry time in minutes.
    /// </summary>
    [JsonProperty(PropertyName = "expiry")]
    public long Expiry = 60;
}