using System;

namespace CodeRunner.Packaging
{
    public class PackageMetadata
    {
        public string Author { get; set; } = "";

        public DateTimeOffset CreationTime { get; set; } = DateTimeOffset.Now;

        public Version Version { get; set; } = new Version();

        public string? Name { get; set; }
    }
}
