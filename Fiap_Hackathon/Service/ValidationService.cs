﻿using Fiap_Hackathon.Context;
using Microsoft.EntityFrameworkCore;
using Fiap_Hackathon.Models;


namespace Fiap_Hackathon.Service
{
    public class ValidationService
    {
        private readonly ApplicationDbContext _context;

        public ValidationService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método para validar se o email já está em uso
        public async Task<bool> IsEmailInUse(string email)
        {
            return await _context.Usuarios.AnyAsync(u => u.Email == email);
        }

        // Método para validar se a senha atende aos requisitos mínimos
        public bool IsPasswordValid(string password)
        {
            if (password.Length < 6)
                return false;

            return true;
        }

        public async Task<bool> IsNameInUse(string nome)
        {
            return await _context.Clinicas.AnyAsync(u => u.Nome_Clinica == nome);
        }

        public async Task<bool> ConsultaConflitanteAsync(DateTime data, int medico, int paciente)
        {
            return await _context.Consultas.AnyAsync(c =>
                c.Data_Consulta == data &&
                (c.Id_Medico == medico || c.Id_Usuario == paciente));
        }

        // Método para validação geral de cadastro (pode incluir mais regras conforme necessário)
        public async Task<(bool isValid, List<string> errors)> ValidateUser(UsuarioViewModel usuario)
        {
            var errors = new List<string>();

            // Verifica se o email já está em uso
            if (await IsEmailInUse(usuario.Email))
            {
                errors.Add("O e-mail já está em uso.");
            }

            // Verifica se a senha atende aos critérios
            if (!IsPasswordValid(usuario.Senha))
            {
                errors.Add("A senha deve ter pelo menos 6 caracteres.");
            }

            // Validações específicas se o tipo de usuário for Médico
            if (usuario.Tipo == "Médico")
            {
                if (string.IsNullOrWhiteSpace(usuario.Especialidade))
                {
                    errors.Add("A especialidade é obrigatória para médicos.");
                }

                if (string.IsNullOrWhiteSpace(usuario.CRM))
                {
                    errors.Add("O CRM é obrigatório para médicos.");
                }
            }

            return (errors.Count == 0, errors);
        }

        public async Task<(bool isValid, List<string> errors)> ValidateClinica(ClinicaViewModel clinica)
        {
            var errors = new List<string>();

            // Verifica se o email já está em uso
            if (await IsNameInUse(clinica.Nome))
            {
                errors.Add("O nome da cliníca ja existe.");
            }

            return (errors.Count == 0, errors);
        }

        public async Task<(bool isValid, List<string> errors)> ValidateConsulta(AgendarConsultaViewModel consulta)
        {
            var errors = new List<string>();

            // Verifica se o email já está em uso
            if (await ConsultaConflitanteAsync(consulta.Data, consulta.MedicoId, consulta.Paciente))
            {
                errors.Add("Já existe uma consulta marcada para o mesmo dia e horário.");
            }

            return (errors.Count == 0, errors);
        }
    }
}