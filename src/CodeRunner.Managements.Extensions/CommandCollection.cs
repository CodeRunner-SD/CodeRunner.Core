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
        private Dictionary<ICommandBuilder, IExtension> Builers { get; set; } = new Dictionary<ICommandBuilder, IExtension>();

        private Dictionary<Command, ICommandBuilder> Commands { get; set; } = new Dictionary<Command, ICommandBuilder>();

        public IEnumerable<Command> GetCommands() => Commands.Keys.AsEnumerable();

        public IExtension GetExtension(ICommandBuilder builder) => Builers[builder];

        public IExtension GetExtension(Command command) => Builers[GetProvider(command)];

        public ICommandBuilder GetProvider(Command command) => Commands[command];

        public ICommandBuilder? GetProvider(string name) => Builers.Keys.Where(x => x.Name == name).FirstOrDefault();

        public bool Contains(ICommandBuilder builder) => Builers.ContainsKey(builder);

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
            Builers.Add(builder, extension);
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
                _ = Builers.Remove(builder);
            }
        }

        public void Unregister(IExtension extension)
        {
            Assert.ArgumentNotNull(extension, nameof(extension));
            List<ICommandBuilder> toRemoved = new List<ICommandBuilder>();
            foreach ((ICommandBuilder k, IExtension v) in Builers)
            {
                if (v == extension)
                    toRemoved.Add(k);
            }
            Unregister(toRemoved.ToArray());
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<ICommandBuilder> GetEnumerator() => Builers.Keys.GetEnumerator();
    }
}
