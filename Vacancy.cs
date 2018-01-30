using System;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JSON_Vacancy
{
    public partial class Vacancy
    {
        [JsonProperty("alternate_url")]
        public string AlternateUrl { get; set; }

        [JsonProperty("code")]
        public object Code { get; set; }

        [JsonProperty("premium")]
        public bool Premium { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("test")]
        public object Test { get; set; }

        [JsonProperty("schedule")]
        public BillingType Schedule { get; set; }

        [JsonProperty("driver_license_types")]
        public List<object> DriverLicenseTypes { get; set; }

        [JsonProperty("suitable_resumes_url")]
        public object SuitableResumesUrl { get; set; }

        [JsonProperty("site")]
        public BillingType Site { get; set; }

        [JsonProperty("billing_type")]
        public BillingType BillingType { get; set; }

        [JsonProperty("published_at")]
        public string PublishedAt { get; set; }

        [JsonProperty("accept_incomplete_resumes")]
        public bool AcceptIncompleteResumes { get; set; }

        [JsonProperty("accept_handicapped")]
        public bool AcceptHandicapped { get; set; }

        [JsonProperty("experience")]
        public BillingType Experience { get; set; }

        [JsonProperty("address")]
        public object Address { get; set; }

        [JsonProperty("key_skills")]
        public List<Skill> KeySkills { get; set; }

        [JsonProperty("allow_messages")]
        public bool AllowMessages { get; set; }

        [JsonProperty("employment")]
        public BillingType Employment { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("response_url")]
        public object ResponseUrl { get; set; }

        [JsonProperty("salary")]
        public Salary Salary { get; set; }

        [JsonProperty("archived")]
        public bool Archived { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("contacts")]
        public object Contacts { get; set; }

        [JsonProperty("employer")]
        public Employer Employer { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("area")]
        public Area Area { get; set; }

        [JsonProperty("relations")]
        public List<object> Relations { get; set; }

        [JsonProperty("accept_kids")]
        public bool AcceptKids { get; set; }

        [JsonProperty("response_letter_required")]
        public bool ResponseLetterRequired { get; set; }

        [JsonProperty("apply_alternate_url")]
        public string ApplyAlternateUrl { get; set; }

        [JsonProperty("quick_responses_allowed")]
        public bool QuickResponsesAllowed { get; set; }

        [JsonProperty("negotiations_url")]
        public object NegotiationsUrl { get; set; }

        [JsonProperty("department")]
        public object Department { get; set; }

        [JsonProperty("branded_description")]
        public object BrandedDescription { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("type")]
        public BillingType Type { get; set; }

        [JsonProperty("specializations")]
        public List<Specialization> Specializations { get; set; }
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

    public partial class BillingType
    {
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

    public partial class Specialization
    {
        [JsonProperty("profarea_id")]
        public string ProfareaId { get; set; }

        [JsonProperty("profarea_name")]
        public string ProfareaName { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class Skill
    {
        [JsonProperty("name")]
        public string Name { get; set; }
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

    public partial class Vacancy
    {
        public static Vacancy FromJson(string json) => JsonConvert.DeserializeObject<Vacancy>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Vacancy self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
