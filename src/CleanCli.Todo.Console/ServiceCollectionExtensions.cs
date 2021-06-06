namespace CleanCli.Todo.Console
{
    using System;
    using System.CommandLine.Parsing;
    using System.Threading;
    using System.Threading.Tasks;
    using CleanCli.Todo.Application.Common.Interfaces;
    using MediatR;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
    using Spectre.Console;

    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers CLI services.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCli(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddSingleton<ICurrentUserService, UserServiceStub>();
            return services;
        }

        /// <summary>
        /// Registers Serilog from configuration.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSerilog(this IServiceCollection services)
        {
            Log.Logger = CreateLogger(services);
            return services;
        }
        private static Serilog.Core.Logger CreateLogger(IServiceCollection services)
        {
            var scope = services.BuildServiceProvider();
            var parseResult = scope.GetRequiredService<ParseResult>();
            var isSilentLogger = parseResult.ValueForOption<bool>("--silent");
            var loggerConfiguration = new LoggerConfiguration()
                .ReadFrom.Configuration(scope.GetRequiredService<IConfiguration>());

            if (isSilentLogger)
            {
                loggerConfiguration.MinimumLevel.Override("CleanCli", Serilog.Events.LogEventLevel.Warning);
                loggerConfiguration.MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning);
            }

            return loggerConfiguration.CreateLogger();
        }

        public class UserServiceStub : ICurrentUserService
        {
            public string UserId => "admin@cli";
        }

        /// <summary>
        /// NOTE: Pipeline behavior registration order is important.
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        {
            public async Task<TResponse> Handle(
                TRequest request,
                CancellationToken cancellationToken,
                RequestHandlerDelegate<TResponse> next)
            {
                try
                {
                    return await next();
                }
                catch (Exception ex)
                {
                    AnsiConsole.WriteException(ex);
                    return default;
                }
            }
        }
    }
}
