using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using AmigaPowerAnalysis.Core.Data;
using Biometris.ExtensionMethods;

namespace AmigaPowerAnalysis.Core {
    public static class ProjectManager {

        public static Project CreateNewProject() {
            var project = new Project();
            project.EndpointTypes = EndpointTypeProvider.NewProjectDefaultEndpointTypes();
            project.UpdateEndpointFactors();
            return project;
        }

        /// <summary>
        /// Stores the project in a file with the specified name.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="filename"></param>
        public static void SaveProject(Project project, string filename) {
            project.ProjectName = Path.GetFileNameWithoutExtension(filename);
            var settings = new XmlWriterSettings() { Indent = true };
            var serializer = new DataContractSerializer(typeof(Project), null, 0x7FFF, false, true, null);
            using (var fileWriter = XmlWriter.Create(filename, settings)) {
                serializer.WriteObject(fileWriter, project);
                fileWriter.Close();
            }
        }

        /// <summary>
        /// Tries to load a project file from the given file name.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Project LoadProject(string filename) {
            var serializer = new DataContractSerializer(typeof(Project));
            using (var fileStream = new FileStream(filename, FileMode.Open)) {
                var project = (Project)serializer.ReadObject(fileStream);
                fileStream.Close();
                project.ProjectName = Path.GetFileNameWithoutExtension(filename);
                return project;
            }
        }

        /// <summary>
        /// Stores the project in an xml file with the specified name.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="filename"></param>
        public static void SaveProjectXml(Project project, string filename) {
            var dto = ProjectDTO.ToDTO(project);
            dto.ToXmlFile(filename);
            project.ProjectName = Path.GetFileNameWithoutExtension(filename);
            SaveCurrentProjectOutput(project, filename);
        }

        /// <summary>
        /// Saves the current project output of the project.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="projectFileName"></param>
        public static void SaveCurrentProjectOutput(Project project, string projectFileName) {
            var projectPath = Path.GetDirectoryName(projectFileName);
            var projectName = project.ProjectName;
            var filesPath = Path.Combine(projectPath, projectName);
            if (project.PrimaryOutput != null) {
                project.PrimaryOutput.ToXmlFile(Path.Combine(filesPath, project.PrimaryOutputId + ".xml"));
            }
        }

        /// <summary>
        /// Tries to load a project file xml from the file specified by the file name.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Project LoadProjectXml(string filename) {
            var dto = SerializationExtensions.FromXmlFile<ProjectDTO>(filename);
            var project = ProjectDTO.FromDTO(dto);
            project.ProjectName = Path.GetFileNameWithoutExtension(filename);
            project.LoadPrimaryOutput(Path.Combine(Path.GetDirectoryName(filename), project.ProjectName));
            return project;
        }
    }
}
