﻿using GestaoInvestimentos.Application.Interfaces;
using GestaoInvestimentos.Application.Services;
using GestaoInvestimentos.Infra.Data.Context;
using GestaoInvestimentos.Infra.Data.Interfaces;
using GestaoInvestimentos.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoInvestimentos.Infra.IoC
{
    public static class IoCConfiguration
    {
        public static void ConfigureRepository(IServiceCollection services)
        {
            services.AddScoped<IMongoContext, MongoContext>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IAtivoRepository, AtivoRepository>();
            services.AddScoped<IJwtToken, JwtToken>();
        }

        public static void ConfigureService(IServiceCollection services)
        {
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IAtivoService, AtivoService>();
        }

        public static void ConfigureAutoMapper(IServiceCollection services)
        {
            //services.AddAutoMapper(typeof(AutoMapperConfig));
        }
    }
}
