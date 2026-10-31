using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaMedico.Application.DTOs;
using SistemaMedico.Application.Interfaces;

namespace SistemaMedico.Controllers
{
    public class ProductosController : Controller
    {
        private readonly IProductoService _productoService;
        private readonly ICategoriaService _categoriaService;

        public ProductosController(IProductoService productoService, ICategoriaService categoriaService)
        {
            _productoService = productoService;
            _categoriaService = categoriaService;
        }

        public async Task<IActionResult> Index(string buscar, int? categoriaId, decimal? precioMax)
        {
            //Carga las categorías para el ComboBox del filtro
            ViewBag.Categorias = new SelectList(await _categoriaService.GetAllAsync(), "Id", "Nombre");

            var productos = await _productoService.GetAllAsync();

            //Lógica de filtrado
            if (!string.IsNullOrEmpty(buscar))
            {
                productos = productos.Where(p => p.Nombre.Contains(buscar, StringComparison.OrdinalIgnoreCase));
            }
            if (categoriaId.HasValue)
            {
                productos = productos.Where(p => p.CategoriaId == categoriaId.Value);
            }
            if (precioMax.HasValue)
            {
                productos = productos.Where(p => p.Precio <= precioMax.Value);
            }

            return View(productos);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categorias = new SelectList(await _categoriaService.GetAllAsync(), "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductoDTO productoDto)
        {
            if (ModelState.IsValid)
            {
                await _productoService.CreateAsync(productoDto);
                return RedirectToAction(nameof(Index));
            }
            // Recargamos el combo box si hay un error de validación
            ViewBag.Categorias = new SelectList(await _categoriaService.GetAllAsync(), "Id", "Nombre", productoDto.CategoriaId);
            return View(productoDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var producto = await _productoService.GetByIdAsync(id);
            if (producto == null) return NotFound();

            ViewBag.Categorias = new SelectList(await _categoriaService.GetAllAsync(), "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductoDTO productoDto)
        {
            if (id != productoDto.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _productoService.UpdateAsync(productoDto);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categorias = new SelectList(await _categoriaService.GetAllAsync(), "Id", "Nombre", productoDto.CategoriaId);
            return View(productoDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _productoService.GetByIdAsync(id);
            if (producto == null) return NotFound();
            return View(producto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productoService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}