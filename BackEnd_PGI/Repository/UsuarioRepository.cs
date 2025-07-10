using BackEnd_PGI.Interface;
using BackEnd_PGI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackEnd_PGI.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            usuario.Password = HashPassword(usuario?.Password!);
            _context.Usuarios.Add(usuario!);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            if (!string.IsNullOrEmpty(usuario.Password))
            {
                usuario.Password = HashPassword(usuario.Password);
            }

            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var usuarioToDelete = await _context.Usuarios.FindAsync(id);
            if (usuarioToDelete != null)
            {
                _context.Usuarios.Remove(usuarioToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Usuario> GetUsuarioByCredentialsAsync(string username, string password)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == username);

            if (usuario != null && VerifyPassword(password, usuario?.Password!))
            {
                return usuario;
            }

            return null;
        }

        public async Task<bool> ChangePasswordAsync(int id, string currentPassword, string newPassword)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return false;
            }

            if (!VerifyPassword(currentPassword, usuario?.Password!))
            {
                return false;
            }

            usuario!.Password = HashPassword(newPassword);
            await _context.SaveChangesAsync();

            return true;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedHashedPassword);
        }
    }
}
