
using System;
using System.Collections.Generic;


namespace SamuraiApp.Core.Domain {
    public class Quote {

        public Guid Id { get; set; }
        public string Text { get; set; }
        public Samurai Samurai { get; set; }
        public Guid SamuraiId { get; set; }


    }
}