
using System;
using System.Collections.Generic;

namespace SamuraiApp.Core.Domain {
    public class Samurai {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Quote> Quotes { get; set; }
            = new List<Quote>();
        public Guid BattleId { get; set; }
    }
}