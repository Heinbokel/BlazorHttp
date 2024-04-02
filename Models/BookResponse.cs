using System.Text.Json.Serialization;

namespace BlazorHttp.Models;

public class BookResponse {

    [JsonPropertyName("id")]
    public int Id {get; set;}

    [JsonPropertyName("title")]
    public string Title {get; set;}

    [JsonPropertyName("synopsis")]
    public string Synopsis {get; set;}

    [JsonPropertyName("publicationDate")]
    public DateOnly PublicationDate {get; set;}

    [JsonPropertyName("isbn")]
    public string ISBN {get; set;}

}