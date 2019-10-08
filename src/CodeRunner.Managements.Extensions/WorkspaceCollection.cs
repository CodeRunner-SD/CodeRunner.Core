using CodeRunner.Diagnostics;
using CodeRunner.Extensions;
using CodeRunner.Extensions.Managements;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CodeRunner.Managements.Extensions
{
    public class WorkspaceCollection : IEnumerable<IWorkspaceProvider>
    {
        private Dictionary<IWorkspaceProvider, IExtension> Providers { get; set; } = new Dictionary<IWorkspaceProvider, IExtension>();

        private Dictionary<IWorkspace, IWorkspaceProvider> Workspaces { get; set; } = new Dictionary<IWorkspace, IWorkspaceProvider>();

        public IEnumerable<IWorkspace> GetWorkspaces() => Workspaces.Keys.AsEnumerable();

        public IExtension GetExtension(IWorkspaceProvider provider) => Providers[provider];

        public IExtension GetExtension(IWorkspace workspace) => Providers[GetProvider(workspace)];

        public IWorkspaceProvider GetProvider(IWorkspace workspace) => Workspaces[workspace];

        public IWorkspaceProvider? GetProvider(string fullName)
        {
            foreach ((IWorkspaceProvider k, IExtension v) in Providers)
            {
                if (v.GetFullName(k) == fullName)
                    return k;
            }
            return null;
        }

        public bool Contains(IWorkspaceProvider provider) => Providers.ContainsKey(provider);

        public bool Contains(IWorkspace workspace) => Workspaces.ContainsKey(workspace);

        public void Use(IWorkspace workspace, IWorkspaceProvider provider)
        {
            Assert.ArgumentNotNull(provider, nameof(provider));
            Assert.ArgumentNotNull(workspace, nameof(workspace));
            Assert.IsTrue(Contains(provider));
            Workspaces.Add(workspace, provider);
        }

        public void Unuse(IWorkspace workspace)
        {
            Assert.ArgumentNotNull(workspace, nameof(workspace));
            _ = Workspaces.Remove(workspace);
        }

        public void Register(IWorkspaceProvider provider, IExtension extension)
        {
            Assert.ArgumentNotNull(provider, nameof(provider));
            Assert.ArgumentNotNull(extension, nameof(extension));
            Providers.Add(provider, extension);
        }

        public void Unregister(params IWorkspaceProvider[] providers)
        {
            Assert.ArgumentNotNull(providers, nameof(providers));
            foreach (IWorkspaceProvider provider in providers)
            {
                Assert.ArgumentNotNull(provider, nameof(provider));
                foreach ((IWorkspace k, IWorkspaceProvider v) in Workspaces)
                {
                    if (v == provider)
                        Assert.Fail("The provider is used for some workspace.");
                }
                _ = Providers.Remove(provider);
            }
        }

        public void Unregister(IExtension extension)
        {
            Assert.ArgumentNotNull(extension, nameof(extension));
            List<IWorkspaceProvider> toRemoved = new List<IWorkspaceProvider>();
            foreach ((IWorkspaceProvider k, IExtension v) in Providers)
            {
                if (v == extension)
                    toRemoved.Add(k);
            }
            Unregister(toRemoved.ToArray());
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<IWorkspaceProvider> GetEnumerator() => Providers.Keys.GetEnumerator();
    }
}
