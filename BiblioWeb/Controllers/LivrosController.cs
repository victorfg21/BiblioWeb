using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BiblioWeb.Data;
using BiblioWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace BiblioWeb.Controllers
{
    public class LivrosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _hostingEnvironment;

        public LivrosController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _hostingEnvironment = environment;
        }

        // GET: Livros
        public async Task<IActionResult> Index()
        {
            var Livros = await _context.Livro.ToListAsync();
            var Fotos = await _context.Imagem.ToListAsync();

            return View(Livros);
        }

        // GET: Livros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro
                .SingleOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // GET: Livros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Livros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Subtitulo,Sinopse,Edicao,Ano,Genero,Editora,Autor,ISBN,IdFotoCapa,IdFotoSumario")] Livro livro, IFormFile FotoCapa)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (FotoCapa.Length > 0)
                    {
                        Imagem imagemCapa = new Imagem();

                        var nomeExtensao = FotoCapa.FileName.Split(".");

                        if (nomeExtensao.Count() == 2)
                        {
                            imagemCapa.Description = nomeExtensao[0];
                            imagemCapa.Extension = nomeExtensao[1];
                            imagemCapa.ContentType = FotoCapa.ContentType;
                            imagemCapa.Length = Convert.ToInt32(FotoCapa.Length);

                            imagemCapa.Picture = Utils.Utils.ToByteArray(FotoCapa);

                            livro.FotoCapa = imagemCapa;
                        }
                    }
                    _context.Add(livro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroExists(livro.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(livro);
        }

        // GET: Livros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro.SingleOrDefaultAsync(m => m.Id == id);
            var foto = await _context.Imagem.ToListAsync();


            if (livro == null)
            {
                return NotFound();
            }
            return View(livro);
        }

        // POST: Livros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Subtitulo,Sinopse,Edicao,Ano,Genero,Editora,Autor,ISBN")] Livro livro, IFormFile fotoCapa)
        {
            if (id != livro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Imagem imagemCapa = new Imagem();

                    if (fotoCapa.Length > 0)
                    {
                        var nomeExtensao = fotoCapa.FileName.Split(".");

                        if (nomeExtensao.Count() == 2)
                        {
                            var query = from e in _context.Livro
                                        where e.Id == id
                                        select e.FotoCapa.Id;

                            var result = query.Single();

                            imagemCapa.Id = result;
                            imagemCapa.Description = nomeExtensao[0];
                            imagemCapa.Extension = nomeExtensao[1];
                            imagemCapa.ContentType = fotoCapa.ContentType;
                            imagemCapa.Length = Convert.ToInt32(fotoCapa.Length);

                            imagemCapa.Picture = Utils.Utils.ToByteArray(fotoCapa);
                        }
                    }
                    else
                    {
                        var query = from e in _context.Livro
                                    where e.Id == id
                                    select e.FotoCapa.Id;

                        var result = query.Single();

                        imagemCapa.Id = result;
                    }
                    _context.Update(livro);
                    _context.Update(imagemCapa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroExists(livro.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(livro);
        }

        // GET: Livros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro
                .SingleOrDefaultAsync(m => m.Id == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livro = await _context.Livro.SingleOrDefaultAsync(m => m.Id == id);
            _context.Livro.Remove(livro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivroExists(int id)
        {
            return _context.Livro.Any(e => e.Id == id);
        }
    }
}
