using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReceitasWebApi.Domain.Entities;
using ReceitasWebApi.Domain.Services;
using ReceitasWebApi.Domain.ViewModel;
using ReceitasWebApi.Infrastructure;

namespace ReceitasWebApi.Services
{
    public class ReceitaService : IReceitaService
    {
        private readonly Context _context;
        public ReceitaService(Context context)
        {
            _context = context;
        }

        public async Task<ReceitaViewModel[]> GetAll()
        {
            return await _context.Receitas.Select(receita =>new ReceitaViewModel(){
                 Id = receita.Id,
                Title = receita.Titulo,
                Description = receita.Descricao,
                Ingredients = receita.Ingredientes,
                Preparation = receita.MetodoDePreparo,
                ImageUrl = receita.ImagemUrl
            }).ToArrayAsync();
        }

        public async Task<ReceitaViewModel> GetOne(Guid id)
        {
            return await _context.Receitas.Select(receita => new ReceitaViewModel(){
                Id = receita.Id,
                Title = receita.Titulo,
                Description = receita.Descricao,
                Ingredients = receita.Ingredientes,
                Preparation = receita.MetodoDePreparo,
                ImageUrl = receita.ImagemUrl

            }).Where(receita => receita.Id == id).FirstOrDefaultAsync();
        }

        public async Task Insert(Receita receita)
        {
            if (String.IsNullOrEmpty (receita.Titulo)) 
                throw new System.Exception("Titulo da receita inválida");

            if(receita.Titulo.Length > 2) 
                throw new System.Exception("Título tem mais de dois caracteres");

            _context.Receitas.Add(receita);
            await _context.SaveChangesAsync(); 
        }

    }
}