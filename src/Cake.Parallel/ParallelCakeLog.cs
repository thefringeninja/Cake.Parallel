using System;
using System.Collections.Generic;
using Cake.Core.Diagnostics;

namespace Cake.Parallel.Module
{
   internal sealed class ParallelCakeLog : ICakeLog
   {
      private readonly ICakeLog _inner;
      private readonly Queue<Action<ICakeLog>> _instructions;
      private Verbosity _verbosity;

      public ParallelCakeLog(ICakeLog inner)
      {
         if (inner == null) throw new ArgumentNullException(nameof(inner));
         _inner = inner;
         _instructions = new Queue<Action<ICakeLog>>();
      }

      public void Write(Verbosity verbosity, LogLevel level, string format, params object[] args)
      {
         _instructions.Enqueue(log => log.Write(verbosity, level, format, args));
      }

      public Verbosity Verbosity
      {
         get { return _verbosity; }
         set
         {
            _verbosity = value; 
            _instructions.Enqueue(log => log.Verbosity = value);
         }
      }

      public void Flush()
      {
         while (_instructions.Count > 0)
         {
            var instruction = _instructions.Dequeue();
            instruction(_inner);
         }
      }
   }
}