using System.Security.Claims;
using ConsultasUVV.Data;
using ConsultasUVV.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConsultasUVV.Controllers
{
    /// 
    /// Controller responsável pelo CRUD de consultas.
    /// Toda a controller exige autenticação: apenas o usuário logado
    /// pode ver/criar/editar/excluir SUAS PRÓPRIAS consultas.
    /// 
    [Authorize]
    public class ConsultasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsultasController(ApplicationDbContext context)
        {
            _context = context;
        }

        private int UsuarioLogadoId =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        // GET: /Consultas
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var consultas = await _context.Consultas
                .Where(c => c.UsuarioId == UsuarioLogadoId)
                .OrderBy(c => c.DataHora)
                .ToListAsync();

            return View(consultas);
        }

        // GET: /Consultas/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Consulta { DataHora = DateTime.Now.AddDays(1) });
        }

        // POST: /Consultas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Especialidade,DataHora,Descricao")] Consulta consulta)
        {
            // O UsuarioId nunca vem do formulário: é sempre o usuário autenticado.
            consulta.UsuarioId = UsuarioLogadoId;
            ModelState.Remove(nameof(Consulta.UsuarioId));

            if (!ModelState.IsValid)
            {
                return View(consulta);
            }

            _context.Consultas.Add(consulta);
            await _context.SaveChangesAsync();

            TempData["Mensagem"] = "Consulta cadastrada com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Consultas/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == UsuarioLogadoId);

            if (consulta == null) return NotFound();

            return View(consulta);
        }

        // POST: /Consultas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Especialidade,DataHora,Descricao")] Consulta consultaEditada)
        {
            if (id != consultaEditada.Id) return NotFound();

            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == UsuarioLogadoId);

            if (consulta == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(consultaEditada);
            }

            consulta.Especialidade = consultaEditada.Especialidade;
            consulta.DataHora = consultaEditada.DataHora;
            consulta.Descricao = consultaEditada.Descricao;

            await _context.SaveChangesAsync();

            TempData["Mensagem"] = "Consulta atualizada com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // GET: /Consultas/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == UsuarioLogadoId);

            if (consulta == null) return NotFound();

            return View(consulta);
        }

        // POST: /Consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consulta = await _context.Consultas
                .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioId == UsuarioLogadoId);

            if (consulta != null)
            {
                _context.Consultas.Remove(consulta);
                await _context.SaveChangesAsync();
                TempData["Mensagem"] = "Consulta excluída com sucesso!";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
