using System.Text.Json.Serialization;

namespace MeetingReservation.Communication.Responses
{
    public class ResponseErrorJson
    {
        [JsonPropertyName("errors")]
        public IList<string>? Errors { get; set; }

        [JsonPropertyName("tokenIsExpired")]
        public bool TokenIsExpired { get; set; }

        public ResponseErrorJson() { }

        public ResponseErrorJson(IList<string> errors) => Errors = errors;

        public ResponseErrorJson(string error)
        {
            Errors = new List<string>
            {
                error
            };

        }
    }

}
