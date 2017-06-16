
using System;
using System.Collections.Generic;

namespace SamuraiApp.Core.Domain {
    public class Battle {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public ICollection<Samurai> Samurais { get; set; }
    }
}