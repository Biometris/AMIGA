using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Amiga_Power_Analysis {
    public partial class MainWindow : Form {

        private Project _project;

        public MainWindow() {
            InitializeComponent();
            Initialize();
        }

        void Initialize() {
            _project = new Project();
            var endpointFactory = new EndpointFactory();
            var endpoints = new List<Endpoint>();
            endpoints.Add(new Endpoint() {
                Name = "Beatle",
                EndpointType = endpointFactory.CreateEndpointType("Predator")
            });
            endpoints.Add(new Endpoint() {
                Name = "Giraffe",
                EndpointType = endpointFactory.CreateEndpointType("Herbivore")
            });
            _project.Endpoints = endpoints;
            BindingSource bs = new BindingSource();
            bs.DataSource = typeof(Endpoint);
            bs.Add(new Endpoint() {
                Name = "Giraffe",
                EndpointType = endpointFactory.CreateEndpointType("Herbivore")
            });
            dataGridEndPoints.DataSource = bs;
        }

        void addEndpointEvent() {

        }


    }
}
