﻿using Fiap_Hackathon.Context;
using Fiap_Hackathon.Models;

namespace Fiap_Hackathon.Service
{
    public class ClinicaService
    {
        private readonly ApplicationDbContext _context;
        private readonly ValidationService _validationService;

        public ClinicaService(ApplicationDbContext context, ValidationService validationService)
        {
            _context = context;
            _validationService = validationService;
        }

        public async Task<(bool success, List<string> errors)> CadastrarClinica(ClinicaViewModel clinicaViewModel)
        {
            var (isValid, validationErrors) = await _validationService.ValidateClinica(clinicaViewModel);
            if (!isValid)
            {
                return (false, validationErrors);
            }

            var clinica = new Clinica
            {
                Nome_Clinica = clinicaViewModel.Nome,
                Endereco = clinicaViewModel.Logradouro + ", " + clinicaViewModel.Numero,
                CEP = clinicaViewModel.CEP,
                Data_Cadastro = DateTime.Now,
                Ativo = 1,
            };

            _context.Clinicas.Add(clinica);
            await _context.SaveChangesAsync();

            return (true, new List<string>());
        }

        public List<Clinica> ObterTodasClinicas()
        {
            return _context.Clinicas.ToList();
        }

        public string ObterClinicaPorId(int Id)
        {
            return _context.Clinicas
                   .Where(c => c.Id == Id)
                   .Select(c => c.Nome_Clinica)
                   .FirstOrDefault();
        }
    }
}
