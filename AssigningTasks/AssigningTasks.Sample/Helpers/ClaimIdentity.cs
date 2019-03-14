using System;
using System.Security.Principal;

namespace AssigningTasks.Sample.Helpers
{
    public class ClaimIdentity : IIdentity
    {
        private string _Name { get; set; }
        private string _AuthenticationType { get; set; }

        public ClaimIdentity(string name, string authName)
        {
            _Name = name;
            _AuthenticationType = authName;
        }
        public string AuthenticationType => _AuthenticationType;

        public bool IsAuthenticated => true;

        public string Name => _Name;
    }
}
