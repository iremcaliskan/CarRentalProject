﻿using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Text;

namespace Core.Aspects.Autofac.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        private readonly int _interval;
        private readonly Stopwatch _stopwatch;

        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }

        protected override void OnBefore(IInvocation invocation)
        { // Timer starts on before time of method
            _stopwatch.Start();
        }
        protected override void OnAfter(IInvocation invocation)
        { // Calculate time elapsed at the end of the method
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                Debug.WriteLine($"Performance: {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopwatch.Elapsed.TotalSeconds}"); // Write to Console Log
            }
            _stopwatch.Reset(); // Reset the timer
        }
    }
}
