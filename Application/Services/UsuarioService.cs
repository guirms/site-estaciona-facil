using System.ComponentModel.DataAnnotations;
using Application.Base;
using Application.Interfaces;
using Application.Objects.Requests.Usuario;
using Application.Objects.Responses.Usuario;
using AutoMapper;
using Domain.Models;
using Infra.Data.Interfaces;

namespace Application.Services;

public class UsuarioService: DadosSessaoBase, IUsuarioService
{
    private readonly IMapper _mapper;
    private readonly IUsuarioRepository _usuarioRepository; 
    private readonly IAutenticacaoService _autenticacaoService;
    
    public UsuarioService(IMapper mapper, IUsuarioRepository usuarioRepository, IAutenticacaoService autenticacaoService)
    {
        _mapper = mapper;
        _usuarioRepository = usuarioRepository;
        _autenticacaoService = autenticacaoService;
    }

    public UsuarioResponse CadastrarUsuario(UsuarioCadastroRequest usuarioCadastroRequest)
    {
         if (usuarioCadastroRequest.Senha != usuarioCadastroRequest.ConfirmacaoSenha)
             throw new Exception("As senhas não são iguais");
        
         if (!EmailValido(usuarioCadastroRequest.Email))
             throw new Exception("Email em formato inválido");
        
         if (string.IsNullOrEmpty(usuarioCadastroRequest.Senha))
             throw new NullReferenceException("Senha nula é inválida");
        
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
        if (!EmailValido(usuarioLoginRequest.Email))
            throw new Exception("Email em formato inválido");
        
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
    

    private bool EmailValido(string email)
    {
        if(new EmailAddressAttribute().IsValid(email))
        {
            return true;
        }
        
        return false;
    }
}