using System;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Parallel.Module
{
    public class ParallelCakeContext : ICakeContext
    {
        private readonly ICakeContext _inner;
        private readonly ParallelCakeLog _log;

        public ParallelCakeContext(ICakeContext inner)
        {
            if (inner == null) throw new ArgumentNullException(nameof(inner));
            _inner = inner;
            _log = new ParallelCakeLog(inner.Log);
        }

        public void Flush()
        {
            _log.Flush();
        }

        public IFileSystem FileSystem
        {
            get { return _inner.FileSystem; }
        }

        public ICakeEnvironment Environment
        {
            get { return _inner.Environment; }
        }

        public IGlobber Globber
        {
            get { return _inner.Globber; }
        }

        public ICakeLog Log
        {
            get { return _log; }
        }

        public ICakeArguments Arguments
        {
            get { return _inner.Arguments; }
        }

        public IProcessRunner ProcessRunner
        {
            get { return _inner.ProcessRunner; }
        }

        public IRegistry Registry
        {
            get { return _inner.Registry; }
        }

        public IToolLocator Tools
        {
            get { return _inner.Tools; }
        }
    }
}