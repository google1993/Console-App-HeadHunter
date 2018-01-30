using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace JSON_Data
{
    public partial class Data
    {
        [JsonProperty("clusters")]
        public object Clusters { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        [JsonProperty("pages")]
        public long Pages { get; set; }

        [JsonProperty("arguments")]
        public object Arguments { get; set; }

        [JsonProperty("found")]
        public long Found { get; set; }

        [JsonProperty("alternate_url")]
        public string AlternateUrl { get; set; }

        [JsonProperty("per_page")]
        public long PerPage { get; set; }

        [JsonProperty("page")]
        public long Page { get; set; }
    }

    public partial class Item
    {
        [JsonProperty("salary")]
        public Salary Salary { get; set; }

        [JsonProperty("snippet")]
        public Snippet Snippet { get; set; }

        [JsonProperty("archived")]
        public bool Archived { get; set; }

        [JsonProperty("premium")]
        public bool Premium { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("area")]
        public Area Area { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("alternate_url")]
        public string AlternateUrl { get; set; }

        [JsonProperty("apply_alternate_url")]
        public string ApplyAlternateUrl { get; set; }

        [JsonProperty("relations")]
        public List<object> Relations { get; set; }

        [JsonProperty("employer")]
        public Employer Employer { get; set; }

        [JsonProperty("response_letter_required")]
        public bool ResponseLetterRequired { get; set; }

        [JsonProperty("published_at")]
        public string PublishedAt { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("department")]
        public object Department { get; set; }

        [JsonProperty("sort_point_distance")]
        public object SortPointDistance { get; set; }

        [JsonProperty("type")]
        public PurpleType Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public partial class Address
    {
        [JsonProperty("building")]
        public string Building { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }

        [JsonProperty("metro")]
        public Metro Metro { get; set; }

        [JsonProperty("metro_stations")]
        public List<Metro> MetroStations { get; set; }

        [JsonProperty("raw")]
        public string Raw { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("lat")]
        public double? Lat { get; set; }

        [JsonProperty("lng")]
        public double? Lng { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public partial class Metro
    {
        [JsonProperty("line_name")]
        public string LineName { get; set; }

        [JsonProperty("station_id")]
        public string StationId { get; set; }

        [JsonProperty("line_id")]
        public string LineId { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("station_name")]
        public string StationName { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }
    }

    public partial class Area
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Employer
    {
        [JsonProperty("logo_urls")]
        public LogoUrls LogoUrls { get; set; }

        [JsonProperty("vacancies_url")]
        public string VacanciesUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("alternate_url")]
        public string AlternateUrl { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("trusted")]
        public bool Trusted { get; set; }
    }

    public partial class LogoUrls
    {
        [JsonProperty("90")]
        public string The90 { get; set; }

        [JsonProperty("240")]
        public string The240 { get; set; }

        [JsonProperty("original")]
        public string Original { get; set; }
    }

    public partial class Salary
    {
        [JsonProperty("to")]
        public long? To { get; set; }

        [JsonProperty("gross")]
        public bool? Gross { get; set; }

        [JsonProperty("from")]
        public long? From { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
    }

    public partial class Snippet
    {
        [JsonProperty("requirement")]
        public string Requirement { get; set; }

        [JsonProperty("responsibility")]
        public string Responsibility { get; set; }
    }

    public partial class PurpleType
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Data
    {
        public static Data FromJson(string json) => JsonConvert.DeserializeObject<Data>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Data self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
    }
}
