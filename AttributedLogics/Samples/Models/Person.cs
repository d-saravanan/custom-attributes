using AttributedLogics.Attributes;
using System;

namespace AttributedLogics.Samples.Models
{
    [Serializable]
    public class Person
    {
        public string Id { get; set; }
        [JsonContent("FirstName,LastName,UserName", 0), StringSecureContent(1)]
        public string Profile { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        public string EmailId { get; set; }
        public string Phone { get; set; }
    }
}
