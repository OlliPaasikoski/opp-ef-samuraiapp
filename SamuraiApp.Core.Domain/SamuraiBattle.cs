
using System;
using System.Collections.Generic;

namespace SamuraiApp.Core.Domain {
    public class SamuraiBattle {
        public Guid SamuraiId { get; set; }
        public Samurai Samurai { get; set; }
        public Guid BattleId { get; set; }
        public Battle Battle { get; set; }
    }
}