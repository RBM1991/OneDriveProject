using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;

namespace OneDriveProject.Data.Models
{
    public class OneDriveUser
    {        
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("surName")]
        public string SurName { get; set; }

        [JsonProperty("givenName")]
        public string GivenName { get; set; }

        public string Id { get; set; }

        public string UserPrincipalName { get; set; }

        public string[] BusinessPhones { get; set; }

        public string JobTitle { get; set; }

        public string Mail { get; set; }

        public string MobilePhone { get; set; }

        public string OfficeLocation { get; set; }

        public string PreferredLanguage { get; set; }
    }
}

//{"@odata.context":"https://graph.microsoft.com/v1.0/$metadata#users/$entity","displayName":"Rasmus Bjerg Mogensen","surname"
//:"Bjerg Mogensen","givenName":"Rasmus","id":"676b5cb7b65ba097","userPrincipalName":"rasmusbjergmogensen@gmail.com","businessPhones":[],"
//jobTitle":null,"mail":null,"mobilePhone":null,"officeLocation":null,"preferredLanguage":null}

