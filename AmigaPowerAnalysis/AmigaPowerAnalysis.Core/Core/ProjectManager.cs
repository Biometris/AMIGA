using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Serialization;
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

        /// <summary>
        /// Checks whether the project is changed by opening the base file that contains
        /// the project and comparing the xml-checksums.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool HasUnsavedChanges(Project project, string filename) {
            if (!string.IsNullOrEmpty(filename)) {
                var oldCheckSum = ProjectManager.GetProjectFileCheckSum(filename);
                var newCheckSum = ProjectManager.GetProjectCheckSum(project);
                return !oldCheckSum.SequenceEqual(newCheckSum);
            }
            return true;
        }

        /// <summary>
        /// Returns an MD5 checksum of the file with the given filename. This method
        /// assumes that this file contains an APA project.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static byte[] GetProjectFileCheckSum(string filename) {
            using (var md5 = MD5.Create()) {
                var xml = File.ReadAllText(filename);
                return md5.ComputeHash(xml.GetByteArray());
            }
        }

        /// <summary>
        /// Returns an MD5 checksum of the APA project.
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public static byte[] GetProjectCheckSum(Project project) {
            var dto = ProjectDTO.ToDTO(project);
            var serializer = new XmlSerializer(typeof(ProjectDTO));
            using(var textWriter = new StringWriter()) {
                using (var md5 = MD5.Create()) {
                    serializer.Serialize(textWriter, dto);
                    var xml = textWriter.ToString();
                    return md5.ComputeHash(xml.GetByteArray());
                }
            }
        }
    }
}
