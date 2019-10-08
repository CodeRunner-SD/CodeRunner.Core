using CodeRunner.Commands;
using CodeRunner.Diagnostics;
using CodeRunner.Extensions;
using CodeRunner.Extensions.Commands;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CodeRunner.Managements.Extensions
{
    public class CommandCollection : IEnumerable<ICommandBuilder>
    {
        private Dictionary<ICommandBuilder, IExtension> Builders { get; set; } = new Dictionary<ICommandBuilder, IExtension>();

        private Dictionary<Command, ICommandBuilder> Commands { get; set; } = new Dictionary<Command, ICommandBuilder>();

        public IEnumerable<Command> GetCommands() => Commands.Keys.AsEnumerable();

        public IExtension GetExtension(ICommandBuilder builder) => Builders[builder];

        public IExtension GetExtension(Command command) => Builders[GetBuilder(command)];

        public ICommandBuilder GetBuilder(Command command) => Commands[command];

        public ICommandBuilder? GetBuilder(string fullName)
        {
            foreach ((ICommandBuilder k, IExtension v) in Builders)
            {
                if (v.GetFullName(k) == fullName)
                    return k;
            }
            return null;
        }

        public bool Contains(ICommandBuilder builder) => Builders.ContainsKey(builder);

        public bool Contains(Command command) => Commands.ContainsKey(command);

        public void Use(Command command, ICommandBuilder builder)
        {
            Assert.ArgumentNotNull(builder, nameof(builder));
            Assert.ArgumentNotNull(command, nameof(command));
            Assert.IsTrue(Contains(builder));
            Commands.Add(command, builder);
        }

        public void Unuse(Command command)
        {
            Assert.ArgumentNotNull(command, nameof(command));
            _ = Commands.Remove(command);
        }

        public void Register(ICommandBuilder builder, IExtension extension)
        {
            Assert.ArgumentNotNull(builder, nameof(builder));
            Assert.ArgumentNotNull(extension, nameof(extension));
            Builders.Add(builder, extension);
        }

        public void Unregister(params ICommandBuilder[] builders)
        {
            Assert.ArgumentNotNull(builders, nameof(builders));
            foreach (ICommandBuilder builder in builders)
            {
                Assert.ArgumentNotNull(builder, nameof(builder));
                foreach ((Command k, ICommandBuilder v) in Commands)
                {
                    if (v == builder)
                        Assert.Fail("The provider is used for some workspace.");
                }
                _ = Builders.Remove(builder);
            }
        }

        public void Unregister(IExtension extension)
        {
            Assert.ArgumentNotNull(extension, nameof(extension));
            List<ICommandBuilder> toRemoved = new List<ICommandBuilder>();
            foreach ((ICommandBuilder k, IExtension v) in Builders)
            {
                if (v == extension)
                    toRemoved.Add(k);
            }
            Unregister(toRemoved.ToArray());
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<ICommandBuilder> GetEnumerator() => Builders.Keys.GetEnumerator();
    }
}
