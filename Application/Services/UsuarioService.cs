using Application.Base;
using Application.Interfaces;
using Application.Objects.Requests.Usuario;
using Application.Objects.Responses.Usuario;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using Infra.Data.Interfaces;

namespace Application.Services;

public class UsuarioService : DadosSessaoBase, IUsuarioService
{
    private readonly IMapper _mapper;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IAutenticacaoService _autenticacaoService;
    private readonly IValidator<UsuarioCadastroRequest> _usuarioCadastroRequestValidator;
    private readonly IValidator<UsuarioLoginRequest> _usuarioLoginRequestValidator;

    public UsuarioService(IMapper mapper, IUsuarioRepository usuarioRepository, IAutenticacaoService autenticacaoService, IValidator<UsuarioCadastroRequest> usuarioCadastroRequestValidator, IValidator<UsuarioLoginRequest> usuarioLoginRequestValidator)
    {
        _mapper = mapper;
        _usuarioRepository = usuarioRepository;
        _autenticacaoService = autenticacaoService;
        _usuarioCadastroRequestValidator = usuarioCadastroRequestValidator;
        _usuarioLoginRequestValidator = usuarioLoginRequestValidator;
    }

    public UsuarioResponse CadastrarUsuario(UsuarioCadastroRequest usuarioCadastroRequest)
    {
        var validarUsuario = _usuarioCadastroRequestValidator.Validate(usuarioCadastroRequest);

        if (!validarUsuario.IsValid)
            throw new Exception(validarUsuario.Errors.FirstOrDefault()?.ToString() ?? "Erro ao validar usuário");

        usuarioCadastroRequest.Senha = _autenticacaoService.GerarSenhaHashMd5(usuarioCadastroRequest.Senha);

        var usuarioJaExiste = _usuarioRepository.ConsultarUsuarioIdPorEmailESenha(usuarioCadastroRequest.Email, usuarioCadastroRequest.Senha);

        if (usuarioJaExiste != 0 && usuarioJaExiste != null)
            throw new Exception("Usuário já cadastrado no sistema");

        var lUsuario = _mapper.Map<Usuario>(usuarioCadastroRequest);

        var cadastrarUsuario = _usuarioRepository.SalvarUsuario(lUsuario);

        if (cadastrarUsuario == 0)
            throw new Exception("Erro ao salvar usuário");

        var lUsuarioResponse = _mapper.Map<UsuarioResponse>(lUsuario);

        lUsuarioResponse.TokenSessaoUsuario =
            _autenticacaoService.GerarTokenSessao(lUsuario.Email, lUsuario.Senha);

        if (string.IsNullOrEmpty(lUsuarioResponse.TokenSessaoUsuario))
            throw new Exception("Erro ao gerar token de sessão");

        return lUsuarioResponse;
    }

    public UsuarioResponse RealizarLogin(UsuarioLoginRequest usuarioLoginRequest)
    {
        var validarUsuario = _usuarioLoginRequestValidator.Validate(usuarioLoginRequest);

        if (!validarUsuario.IsValid)
            throw new Exception(validarUsuario.Errors.FirstOrDefault()?.ToString() ?? "Erro ao validar usuário");

        var usuarioRegistroId = _usuarioRepository.ConsultarUsuarioIdPorEmailESenha(usuarioLoginRequest.Email,
            _autenticacaoService.GerarSenhaHashMd5(usuarioLoginRequest.Senha)) ?? throw new NullReferenceException("Usuário ou senha inválidos");

        var tokenSessaoUsuario = _autenticacaoService.GerarTokenSessao(usuarioLoginRequest.Email,
            _autenticacaoService.GerarSenhaHashMd5(usuarioLoginRequest.Senha));

        if (string.IsNullOrEmpty(tokenSessaoUsuario))
            throw new Exception("Erro ao gerar token de sessão");

        return new UsuarioResponse
        {
            UsuarioId = usuarioRegistroId,
            TokenSessaoUsuario = tokenSessaoUsuario
        };
    }
}