﻿// <copyright file="MetricsReportingConsoleServiceCollectionExtensions.cs" company="Allan Hardy">
// Copyright (c) Allan Hardy. All rights reserved.
// </copyright>

using App.Metrics.Reporting;
using App.Metrics.Reporting.Console;
using App.Metrics.Reporting.Console.Internal;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

// ReSharper disable CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
    // ReSharper restore CheckNamespace
{
    /// <summary>
    /// Extension methods for setting up essential App Metrics console reporting services in an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class MetricsReportingConsoleServiceCollectionExtensions
    {
        /// <summary>
        ///     Adds essential App Metrics Console Reporting services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>
        ///     An <see cref="IMetricsReportingCoreBuilder" /> that can be used to further configure the App Metrics Reporting services.
        /// </returns>
        public static IServiceCollection AddConsoleCore(this IServiceCollection services)
        {
            AddConsoleReportingServices(services);

            return services;
        }

        internal static void AddConsoleReportingServices(IServiceCollection services)
        {
            //
            // Options
            //
            var optionsSetupDescriptor = ServiceDescriptor.Transient<IConfigureOptions<MetricsReportingConsoleOptions>, MetricsReportingConsoleOptionsSetup>();
            services.TryAddEnumerable(optionsSetupDescriptor);

            //
            // Console Reporting Infrastructure
            //
            var consoleReportProviderDescriptor = ServiceDescriptor.Transient<IReporterProvider, ConsoleReporterProvider>();
            services.TryAddEnumerable(consoleReportProviderDescriptor);
        }
    }
}