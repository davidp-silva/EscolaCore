using Ds.Business.Interfaces;
using Ds.Business.Models;
using Ds.Business.Notificacoes;
using Ds.Business.Services;
using Ds.Data.Context;
using Ds.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ds.App.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<EscolaContext>();
            services.AddScoped<IAlunoRepository, AlunoRepository>();
            services.AddScoped<IDiarioRepository, DiarioRepository>();
            services.AddScoped<IDisciplinaRepository, DisciplinaRepository>();
            services.AddScoped<IDisciplinaProfessorRepository, DisciplinaProfessorRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<INotaRepository, NotaRepository>();
            services.AddScoped<IPeriodoRepository,PeriodoRepository>();
            services.AddScoped<IPessoaRepository,PessoaRepository>();
            services.AddScoped<IProfessorRepository, ProfessorRepository>();
            services.AddScoped<IResponsavelRepository,ResponsavelRepository>();
            services.AddScoped<ISerieRepository,SerieRepository>();
            services.AddScoped<ITurmaRepository,TurmaRepository>();
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IAlunoService, AlunoService>();
            services.AddScoped<IDisciplinaService, DisciplinaService>();
            services.AddScoped<IPeriodoService, PeriodoService>();
            services.AddScoped<INotaService, NotaService>();

            return services;
        }
    }
}
