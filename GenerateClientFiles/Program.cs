using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace GenerateClientFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            string UserRootFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string ApplicationFolder = Path.Combine(UserRootFolder, "PgJsonParse");
            string VersionCacheFolder = Path.Combine(ApplicationFolder, "Versions");

            int MaxVersion = 0;
            try
            {
                foreach (string Folder in Directory.GetDirectories(VersionCacheFolder))
                {
                    if (int.TryParse(Path.GetFileName(Folder), out int Version))
                        if (MaxVersion < Version)
                            MaxVersion = Version;
                }
            }
            catch
            {
            }

            if (MaxVersion == 0)
                return;

            string VersionFolder = Path.Combine(VersionCacheFolder, MaxVersion.ToString());

            List<string> IndexFiles = new List<string>();
            foreach (string FileName in Directory.GetFiles(VersionFolder, "*.txt"))
                IndexFiles.Add(FileName);

            string CacheFile = null;
            foreach (string FileName in Directory.GetFiles(VersionFolder, "*.pg"))
            {
                CacheFile = FileName;
                break;
            }

            if (IndexFiles.Count > 0 && CacheFile != null)
                Generate(IndexFiles, CacheFile);
        }

        private static void Generate(List<string> indexFiles, string cacheFile)
        {
            string ExePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (ExePath.EndsWith("Release") || ExePath.EndsWith("Debug"))
                ExePath = Path.GetDirectoryName(ExePath);
            if (ExePath.EndsWith("x64"))
                ExePath = Path.GetDirectoryName(ExePath);
            if (ExePath.EndsWith("bin"))
                ExePath = Path.GetDirectoryName(ExePath);
            string ClientFilePath = Path.Combine(Path.GetDirectoryName(ExePath), "ClientFiles");

            GenerateProject(ClientFilePath, indexFiles, cacheFile);
            GenerateIndexes(ClientFilePath, indexFiles);
            GenerateCache(ClientFilePath, indexFiles, cacheFile);
        }

        private static void GenerateProject(string clientFilePath, List<string> indexFiles, string cacheFile)
        {
            string FileName = Path.Combine(clientFilePath, "ClientFiles.csproj");
            using (FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    sw.WriteLine("<Project ToolsVersion=\"4.0\" DefaultTargets=\"Build\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">");
                    sw.WriteLine("  <Import Project=\"$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props\" Condition=\"Exists('$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props')\" />");
                    sw.WriteLine("  <PropertyGroup>");
                    sw.WriteLine("    <Configuration Condition=\" '$(Configuration)' == '' \">Debug</Configuration>");
                    sw.WriteLine("    <Platform Condition=\" '$(Platform)' == '' \">AnyCPU</Platform>");
                    sw.WriteLine("    <ProjectGuid>{A72C334B-E5AD-406C-8FFD-7750FFED149C}</ProjectGuid>");
                    sw.WriteLine("    <OutputType>Library</OutputType>");
                    sw.WriteLine("    <AppDesignerFolder>Properties</AppDesignerFolder>");
                    sw.WriteLine("    <RootNamespace>ClientFiles</RootNamespace>");
                    sw.WriteLine("    <AssemblyName>ClientFiles</AssemblyName>");
                    sw.WriteLine("    <TargetFrameworkVersion>v4.5</TargetFrameworkVersion>");
                    sw.WriteLine("    <FileAlignment>512</FileAlignment>");
                    sw.WriteLine("    <IsCSharpXamlForHtml5>true</IsCSharpXamlForHtml5>");
                    sw.WriteLine("    <CSharpXamlForHtml5OutputType>Library</CSharpXamlForHtml5OutputType>");
                    sw.WriteLine("  </PropertyGroup>");
                    sw.WriteLine("  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \">");
                    sw.WriteLine("    <DebugSymbols>true</DebugSymbols>");
                    sw.WriteLine("    <DebugType>full</DebugType>");
                    sw.WriteLine("    <Optimize>false</Optimize>");
                    sw.WriteLine("    <OutputPath>bin\\Debug\\</OutputPath>");
                    sw.WriteLine("    <DefineConstants>DEBUG;TRACE;CSHARP_XAML_FOR_HTML5;CSHTML5</DefineConstants>");
                    sw.WriteLine("    <ErrorReport>prompt</ErrorReport>");
                    sw.WriteLine("    <WarningLevel>4</WarningLevel>");
                    sw.WriteLine("  </PropertyGroup>");
                    sw.WriteLine("  <PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' \">");
                    sw.WriteLine("    <DebugType>pdbonly</DebugType>");
                    sw.WriteLine("    <Optimize>true</Optimize>");
                    sw.WriteLine("    <OutputPath>bin\\Release\\</OutputPath>");
                    sw.WriteLine("    <DefineConstants>TRACE;CSHARP_XAML_FOR_HTML5;CSHTML5</DefineConstants>");
                    sw.WriteLine("    <ErrorReport>prompt</ErrorReport>");
                    sw.WriteLine("    <WarningLevel>4</WarningLevel>");
                    sw.WriteLine("  </PropertyGroup>");
                    sw.WriteLine("  <ItemGroup>");
                    sw.WriteLine("    <Reference Include=\"CSharpXamlForHtml5\">");
                    sw.WriteLine("      <HintPath>$(MSBuildProgramFiles32)\\MSBuild\\CSharpXamlForHtml5\\AssembliesToReference\\CSharpXamlForHtml5.dll</HintPath>");
                    sw.WriteLine("    </Reference>");
                    sw.WriteLine("    <Reference Include=\"CSharpXamlForHtml5.System.dll\">");
                    sw.WriteLine("      <HintPath>$(MSBuildProgramFiles32)\\MSBuild\\CSharpXamlForHtml5\\AssembliesToReference\\CSharpXamlForHtml5.System.dll.dll</HintPath>");
                    sw.WriteLine("    </Reference>");
                    sw.WriteLine("    <Reference Include=\"CSharpXamlForHtml5.System.Runtime.Serialization.dll\">");
                    sw.WriteLine("      <HintPath>$(MSBuildProgramFiles32)\\MSBuild\\CSharpXamlForHtml5\\AssembliesToReference\\CSharpXamlForHtml5.System.Runtime.Serialization.dll.dll</HintPath>");
                    sw.WriteLine("    </Reference>");
                    sw.WriteLine("    <Reference Include=\"CSharpXamlForHtml5.System.ServiceModel.dll\">");
                    sw.WriteLine("      <HintPath>$(MSBuildProgramFiles32)\\MSBuild\\CSharpXamlForHtml5\\AssembliesToReference\\CSharpXamlForHtml5.System.ServiceModel.dll.dll</HintPath>");
                    sw.WriteLine("    </Reference>");
                    sw.WriteLine("    <Reference Include=\"CSharpXamlForHtml5.System.Xaml.dll\">");
                    sw.WriteLine("      <HintPath>$(MSBuildProgramFiles32)\\MSBuild\\CSharpXamlForHtml5\\AssembliesToReference\\CSharpXamlForHtml5.System.Xaml.dll.dll</HintPath>");
                    sw.WriteLine("    </Reference>");
                    sw.WriteLine("    <Reference Include=\"CSharpXamlForHtml5.System.Xml.dll\">");
                    sw.WriteLine("      <HintPath>$(MSBuildProgramFiles32)\\MSBuild\\CSharpXamlForHtml5\\AssembliesToReference\\CSharpXamlForHtml5.System.Xml.dll.dll</HintPath>");
                    sw.WriteLine("    </Reference>");
                    sw.WriteLine("    <Reference Include=\"JSIL.Meta\">");
                    sw.WriteLine("      <HintPath>$(MSBuildProgramFiles32)\\MSBuild\\CSharpXamlForHtml5\\AssembliesToReference\\JSIL.Meta.dll</HintPath>");
                    sw.WriteLine("    </Reference>");
                    sw.WriteLine("    <Reference Include=\"Microsoft.CSharp\" />");
                    sw.WriteLine("  </ItemGroup>");
                    sw.WriteLine("  <ItemGroup>");

                    foreach (string Index in indexFiles)
                        sw.WriteLine($"    <Compile Include=\"{ToClassName(Path.GetFileName(Index))}.cs\" />");

                    sw.WriteLine("    <Compile Include=\"all.cs\" />");
                    sw.WriteLine("    <Compile Include=\"Properties\\AssemblyInfo.cs\" />");
                    sw.WriteLine("  </ItemGroup>");
                    sw.WriteLine("  <Import Project=\"$(MSBuildProgramFiles32)\\MSBuild\\CSharpXamlForHtml5\\InternalStuff\\Targets\\CSharpXamlForHtml5.Build.targets\" />");
                    sw.WriteLine("</Project>");
                }
            }
        }

        private static void GenerateIndexes(string clientFilePath, List<string> indexFiles)
        {
            foreach (string Index in indexFiles)
            {
                string ClassName = ToClassName(Path.GetFileName(Index));
                string FileName = Path.Combine(clientFilePath, $"{ClassName}.cs");
                using (FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        sw.WriteLine("namespace ClientFiles");
                        sw.WriteLine("{");
                        sw.WriteLine($"    public class {ClassName}");
                        sw.WriteLine("    {");
                        sw.WriteLine("        public static byte[] data = new byte[]");
                        sw.WriteLine("        {");

                        GenerateBinaryContent(sw, 12, Index);

                        sw.WriteLine("        };");
                        sw.WriteLine("    }");
                        sw.WriteLine("}");
                    }
                }
            }
        }

        private static void GenerateCache(string clientFilePath, List<string> indexFiles, string cacheFile)
        {
            string FileName = Path.Combine(clientFilePath, "all.cs");
            using (FileStream fs = new FileStream(FileName, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine("using System.Collections.Generic;");
                    sw.WriteLine();
                    sw.WriteLine("namespace ClientFiles");
                    sw.WriteLine("{");
                    sw.WriteLine("    public class all");
                    sw.WriteLine("    {");
                    sw.WriteLine("        public static Dictionary<string, byte[]> data = new Dictionary<string, byte[]>()");
                    sw.WriteLine("        {");

                    foreach (string Index in indexFiles)
                    {
                        string ClassName = ToClassName(Path.GetFileName(Index));
                        sw.WriteLine($"            {{ \"{ClassName}\", {ClassName}.data }},");
                    }

                    sw.WriteLine("            { \"cache.pg\", new byte[]");
                    sw.WriteLine("                {");

                    GenerateBinaryContent(sw, 20, cacheFile);

                    sw.WriteLine("                }");
                    sw.WriteLine("            },");
                    sw.WriteLine("        };");
                    sw.WriteLine("    }");
                    sw.WriteLine("}");
                }
            }
        }

        private static void GenerateBinaryContent(StreamWriter sw, int indentation, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    byte[] data = br.ReadBytes((int)fs.Length);

                    for (int i = 0; i < data.Length; i++)
                    {
                        if ((i % 32) == 0)
                        {
                            if (i > 0)
                                sw.WriteLine();
                            for (int j = 0; j < indentation; j++)
                                sw.Write(" ");
                        }
                        else
                        {
                            string Digits = data[i].ToString("X02");
                            sw.Write($"0x{Digits}, ");
                        }
                    }
                }
            }
        }

        private static string ToClassName(string indexName)
        {
            return indexName.Replace("-", "_").Replace(".", "_");
        }
    }
}
