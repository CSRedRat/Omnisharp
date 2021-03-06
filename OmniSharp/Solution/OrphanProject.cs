﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using ICSharpCode.NRefactory.Utils;

namespace OmniSharp.Solution
{
    /// <summary>
    /// Placeholder that can be used for files that don't belong to a project.
    /// </summary>
    public class OrphanProject : IProject
    {
        public string Title { get; private set; }
        public List<CSharpFile> Files { get; private set; }
        public IProjectContent ProjectContent { get; set; }

        private CSharpFile _file;

        public OrphanProject(ISolution solution)
        {
            Title = "Orphan Project";
            _file = new CSharpFile(this, "dummy_file", "");
            Files = new List<CSharpFile>();
            Files.Add(_file);

            string mscorlib = CSharpProject.FindAssembly(CSharpProject.AssemblySearchPaths, "mscorlib");
            ProjectContent = new CSharpProjectContent()
                .SetAssemblyName("OrphanProject")
                .AddAssemblyReferences(CSharpProject.LoadAssembly(mscorlib));
        }

        public CSharpFile GetFile(string fileName)
        {
            return _file;
        }

        public CSharpParser CreateParser()
        {
            return new CSharpParser();
        }
    }
}
