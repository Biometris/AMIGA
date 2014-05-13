using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amiga_Power_Analysis {
    public sealed class Project {

        public List<Endpoint> Endpoints { get; set; }

        public Project() {
            Endpoints = new List<Endpoint>();
        }

        public void AddEndpoint(Endpoint endpoint) {
            Endpoints.Add(endpoint);
        }

    }
}
