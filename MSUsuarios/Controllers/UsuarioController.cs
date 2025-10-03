using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using MSUsuarios.DTOs;
using MSUsuarios.Infrastructure.Repository;

namespace MSUsuarios.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public BaseController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// Lista todos os usuários cadastrados no sistema.
        /// </summary>
        /// <returns>Lista de usuários</returns>
        [HttpGet("GetTodosUsuarios")]
        public IActionResult GetTodosUsuarios()
        {
            try
            {
                var usuarios = _usuarioRepository.ObterTodos();
                return Ok(usuarios);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao listar todos os usuarios: {e.Message}");
            }

        }

        [HttpGet("GetUsuarioPorId")]
        public IActionResult GetUsuarioPorId(int id)
        {
            try
            {
                var usuario = _usuarioRepository.ObterPorId(id);
                if (usuario == null)
                {
                    return NotFound($"Usuário com ID {id} não encontrado.");
                }
                return Ok(usuario);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao obter usuário: {e.Message}");
            }
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>        
        [HttpPost("CadastrarNovoUsuario")]
        public IActionResult CreateUsuario(UsuarioRequest usuario)
        {
            try
            {
                if (usuario == null || string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Senha))
                {
                    return BadRequest("Dados inválidos para criação de usuário.");
                }

                if (_usuarioRepository.ObterTodos().Any(u => u.Email == usuario.Email))
                {
                    return BadRequest("Email já cadastrado.");
                }

                bool senhaValida = Regex.IsMatch(usuario.Senha, @"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,}$");
                if (!senhaValida)
                {
                    return BadRequest("Senha deve conter pelo menos 8 caracteres, incluindo letras, números e símbolos.");
                }

                if (!usuario.Email.Contains('@') || !usuario.Email.Contains("."))
                {
                    return BadRequest("Email inválido.");
                }

                _usuarioRepository.Cadastrar(new Domain.Entities.Usuario
                {
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Senha = usuario.Senha,
                    Lvl = 1
                });
                return StatusCode(200, "Usuario criado com sucesso.");

            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao criar usuario: {e.Message}");
            }
        }

        /// <summary>
        /// Altera o nível (Lvl) de um usuário.
        /// </summary>
        /// <param name="idUsuario">ID do usuário</param>
        /// <param name="novoLvl">Novo nível a ser atribuído</param>
        /// <returns>Resultado da operação</returns>
        [HttpPut("AlterarNivelUsuario")]
        public IActionResult AlterarNivelUsuario(int idUsuario, int novoLvl)
        {
            try
            {
                var usuario = _usuarioRepository.ObterPorId(idUsuario);
                if (usuario == null)
                {
                    return NotFound($"Usuário com ID {idUsuario} não encontrado.");
                }

                usuario.Lvl = novoLvl;
                _usuarioRepository.Alterar(usuario);

                return Ok($"Nível do usuário {idUsuario} alterado para {novoLvl} com sucesso.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao alterar nível do usuário: {e.Message}");
            }
        }

        /// <summary>
        /// Deleta um usuário pelo ID.
        /// </summary>
        /// <param name="idUsuarioDeletar"></param>
        /// <returns></returns>
        [HttpDelete("DeletarUsuario")]
        public IActionResult DeletarUsuario(int idUsuarioDeletar)
        {
            try
            {
                var usuario = _usuarioRepository.ObterPorId(idUsuarioDeletar);
                if (usuario == null)
                {
                    return NotFound($"Usuário com ID {idUsuarioDeletar} não encontrado.");
                }

                // TODO: Implementar deleção em cascata se necessário, para deletar a biblioteca de jogos associada ao usuário.
                //_bibliotecaJogosClienteRepository.Deletar(idUsuarioDeletar);

                _usuarioRepository.Deletar(idUsuarioDeletar);
                return Ok($"Usuário com ID {idUsuarioDeletar} deletado com sucesso.");
            }
            catch (Exception e)
            {
                return BadRequest($"Erro ao deletar usuário: {e.Message}");
            }
        }
    }
}