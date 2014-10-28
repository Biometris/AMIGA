using System.IO;
using System.Runtime.Serialization;

namespace AmigaPowerAnalysis.Core {
    public static class ProjectManager {

        public static Project CreateNewProject() {
            var project = new Project();
            project.EndpointTypes = EndpointTypeProvider.NewProjectDefaultEndpointTypes();
            //project.Endpoints.Add(new Endpoint("Beatle", project.EndpointTypes.First(ept => ept.Name == "Predator")));
            //project.Endpoints.Add(new Endpoint("Giraffe", project.EndpointTypes.First(ept => ept.Name == "Herbivore")));
            //project.Factors.Add(new Factor("Spraying", 3));
            //project.Factors.Add(new Factor("Raking", 2));
            project.UpdateEndpointFactors();
            return project;
        }

        public static void SaveProject(Project project, string filename) {
            var serializer = new DataContractSerializer(typeof(Project), null, 0x7FFF, false, true, null);
            using (var fileWriter = new FileStream(filename, FileMode.Create)) {
                serializer.WriteObject(fileWriter, project);
                fileWriter.Close();
            }
        }

        public static Project LoadProject(string filename) {
            var serializer = new DataContractSerializer(typeof(Project));
            using (var fileStream = new FileStream(filename, FileMode.Open)) {
                var project = (Project)serializer.ReadObject(fileStream);
                fileStream.Close();
                return project;
            }
        }
    }
}
