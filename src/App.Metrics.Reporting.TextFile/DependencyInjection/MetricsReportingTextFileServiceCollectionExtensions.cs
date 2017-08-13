﻿// <copyright file="MetricsReportingTextFileServiceCollectionExtensions.cs" company="Allan Hardy">
// Copyright (c) Allan Hardy. All rights reserved.
// </copyright>

using App.Metrics.Reporting;
using App.Metrics.Reporting.TextFile;
using App.Metrics.Reporting.TextFile.Internal;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

// ReSharper disable CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
    // ReSharper restore CheckNamespace
{
    /// <summary>
    /// Extension methods for setting up essential App Metrics Reporting services in an <see cref="IServiceCollection"/>.
    /// </summary>
    public static class MetricsReportingTextFileServiceCollectionExtensions
    {
        /// <summary>
        ///     Adds essential App Metrics Text File Reporting services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
        /// <returns>
        ///     An <see cref="IMetricsReportingCoreBuilder" /> that can be used to further configure the App Metrics Reporting
        ///     services.
        /// </returns>
        public static IServiceCollection AddTextFileCore(this IServiceCollection services)
        {
            AddTextFileReportingServices(services);

            return services;
        }

        internal static void AddTextFileReportingServices(IServiceCollection services)
        {
            //
            // Options
            //
            var optionsSetupDescriptor = ServiceDescriptor.Transient<IConfigureOptions<MetricsReportingTextFileOptions>, MetricsReportingTextFileOptionsSetup>();
            services.TryAddEnumerable(optionsSetupDescriptor);

            //
            // Text File Reporting Infrastructure
            //
            var consoleReportProviderDescriptor = ServiceDescriptor.Transient<IReporterProvider, TextFileReporterProvider>();
            services.TryAddEnumerable(consoleReportProviderDescriptor);
        }
    }
}