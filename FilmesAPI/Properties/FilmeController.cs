using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Properties;

[ApiController]
[Microsoft.AspNetCore.Mvc.Route("[controller]")]
public class FilmeController : ControllerBase
{

    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] UpdateFilmeDto filmeDto)
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaFilmePorId),
        new { id = filme.Id },
        filme);

    }
    [HttpGet]
    public IEnumerable<Filme> BuscaFilme([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {


        return _context.Filmes.Skip(skip).Take(take);



    }
    [HttpGet("{id}")]
    public IActionResult RecuperaFilmePorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        return Ok(filme);
    }
    [HttpPut("{id}")]
    public IActionResult AtualizaFilme(int id, [FromBody]
    UpdateFilmeDto filmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault(
        filme => filme.Id == id);
        if (filme == null) return NotFound();
        _mapper.Map(filmeDto, filme);
        _context.SaveChanges();
        return NoContent();
    }
}

