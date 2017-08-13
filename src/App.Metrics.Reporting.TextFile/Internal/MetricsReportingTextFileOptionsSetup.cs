﻿// <copyright file="MetricsReportingTextFileOptionsSetup.cs" company="Allan Hardy">
// Copyright (c) Allan Hardy. All rights reserved.
// </copyright>

using System;
using App.Metrics.Formatters;
using App.Metrics.Formatters.Ascii;
using Microsoft.Extensions.Options;

namespace App.Metrics.Reporting.TextFile.Internal
{
    /// <summary>
    ///     Sets up default Text File Reporting options for <see cref="MetricsReportingTextFileOptions"/>.
    /// </summary>
    public class MetricsReportingTextFileOptionsSetup : IConfigureOptions<MetricsReportingTextFileOptions>
    {
        private readonly MetricsOptions _metricsOptionsAccessor;

        public MetricsReportingTextFileOptionsSetup(IOptions<MetricsOptions> metricsOptionsAccessor)
        {
            _metricsOptionsAccessor = metricsOptionsAccessor.Value ?? throw new ArgumentNullException(nameof(metricsOptionsAccessor));
        }

        /// <inheritdoc/>
        public void Configure(MetricsReportingTextFileOptions options)
        {
            if (options.MetricsOutputFormatter == default(IMetricsOutputFormatter))
            {
                var textFormatter = _metricsOptionsAccessor.OutputMetricsFormatters.GetType<MetricsTextOutputFormatter>();

                options.MetricsOutputFormatter = textFormatter == default(IMetricsOutputFormatter)
                    ? _metricsOptionsAccessor.DefaultOutputMetricsFormatter
                    : textFormatter;
            }
        }
    }
}