using Microsoft.AspNetCore.Mvc;
using SistemaMedico.Application.DTOs;
using SistemaMedico.Application.Interfaces;

namespace SistemaMedico.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        public async Task<IActionResult> Index()
        {
            var categorias = await _categoriaService.GetAllAsync();
            return View(categorias);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoriaDTO categoriaDto)
        {
            if (ModelState.IsValid)
            {
                await _categoriaService.CreateAsync(categoriaDto);
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var categoria = await _categoriaService.GetByIdAsync(id);
            if (categoria == null) return NotFound();
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoriaDTO categoriaDto)
        {
            if (id != categoriaDto.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _categoriaService.UpdateAsync(categoriaDto);
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaDto);
        }

        // GET: Muestra la pantalla de confirmación
        public async Task<IActionResult> Delete(int id)
        {
            var categoria = await _categoriaService.GetByIdAsync(id);
            if (categoria == null) return NotFound();
            return View(categoria);
        }

        // POST: Ejecuta la eliminación real
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _categoriaService.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                // Si tiene productos, atrapamos el error y lo enviamos a la vista
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Delete), new { id });
            }
        }
    }
}