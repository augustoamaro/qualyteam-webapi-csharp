using System;
using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ReceitasWebApi.Domain.Entities;
using ReceitasWebApi.Domain.Services;
using ReceitasWebApi.Infrastructure;
using ReceitasWebApi.Services;
using Xunit;

namespace ReceitasWebApiTests.Domain.Services
{
    public class ReceitaServiceTests
    {
        IReceitaService _service;
        Context _context;

        public ReceitaServiceTests()
        {
            var option = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new Context(option);
            _service = new ReceitaService(_context);
        }

        [Fact]
        public void Insert_DeveSalvarUmaReceita()
        {
            var novaReceita = new Receita()
            {
                Titulo = "a"
            };

            _service.Insert(novaReceita);

            _context.Receitas
                .Should()
                .HaveCount(1);

            var receitaDoBanco = _context.Receitas.FirstOrDefault();
            receitaDoBanco.Titulo.Should().Be(novaReceita.Titulo);
        }

        [Fact]
        public void Insert_QuandoTituloNaoInformado_NaoDeveSalvar()
        {
            var novaReceita = new Receita("a");

            _service.Insert(novaReceita);

            _context.Receitas.Should().HaveCount(0);

        }

        [Fact]
        public void Insert_QuandoTamanhoTituloMaiorQueSessenta_NaoDeveSalvar()
        {
            var novaReceita = new Receita("a");

            _service.Insert(novaReceita);

            _context.Receitas.Should().HaveCount(1);

        }


        [Fact]
        public async void GetAll_DeveRetornarTodasAsReceitas()
        {
            var feijoada = new Receita("Feijoada")
            {
                ImagemUrl = "ImageUrl",
                Ingredientes = "Ingredientes",
                Descricao = "Descricao",
                MetodoDePreparo = "MetodoDePreparo"
            };

            var burguer = new Receita("Buruger")
            {
                ImagemUrl = "ImageUrl",
                Ingredientes = "Ingredientes",
                Descricao = "Descricao",
                MetodoDePreparo = "MetodoDePreparo"
            };

            _context.Receitas.AddRange(feijoada, burguer);
            _context.SaveChanges();

            var retorno = await _service.GetAll();
            retorno.Should().HaveCount(2);
            var feijoadaDoRetorno = retorno.FirstOrDefault(receita => receita.Title == feijoada.Titulo
            );
            feijoadaDoRetorno.Should().NotBeNull();

        }

        [Fact]
        public async void GetOne_DeveRetornarSomenteUmaReceita()
        {
            var feijoada = new Receita("Feijoada")
            {
                ImagemUrl = "ImageUrl",
                Ingredientes = "Ingredientes",
                Descricao = "Descricao",
                MetodoDePreparo = "MetodoDePreparo"
            };

            var burguer = new Receita("Buruger")
            {
                ImagemUrl = "ImageUrl",
                Ingredientes = "Ingredientes",
                Descricao = "Descricao",
                MetodoDePreparo = "MetodoDePreparo"
            };

            _context.Receitas.AddRange(feijoada, burguer);
            _context.SaveChanges();

            var retorno = await _service.GetOne(feijoada.Id);
            retorno.Title.Should().Be(feijoada.Titulo);
            retorno.Description.Should().Be(feijoada.Descricao);
            retorno.Ingredients.Should().Be(feijoada.Ingredientes);
            retorno.ImageUrl.Should().Be(feijoada.ImagemUrl);
           retorno.Preparation.Should().Be(feijoada.MetodoDePreparo);
         
        }
    
    }

    
}