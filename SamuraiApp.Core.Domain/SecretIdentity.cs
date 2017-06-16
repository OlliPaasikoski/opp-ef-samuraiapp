using System;

namespace SamuraiApp.Core.Domain {
    public class SecretIdentity {
        public Guid Id { get; set; }
        public string RealName { get; set; }
        public Guid SamuraiId { get; set; }
        public Samurai Samurai { get; set; }
    }
}