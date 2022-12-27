import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { LoginModel } from 'src/app/Interfaces/login/login-model';
import { ResponseBaseModel } from 'src/app/Interfaces/response-base/response-base-model';
import { environment } from 'src/environments/environment';
import { BaseService } from '../base/base.service';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private baseService: BaseService<ResponseBaseModel<LoginModel>>) { }

  async fazerLogin(email: string, senha: string): Promise<ResponseBaseModel<LoginModel>> {

    const body = {
      Email: email,
      Senha: senha
    }

    this.baseService.setarExibeLoad(true);

    let requisicaoLogin = await lastValueFrom(this.baseService.post(`${environment.url}Usuario/RealizarLogin`, body))
      .catch(ex => {
        return ex.message;
      });

    if (typeof(requisicaoLogin) === 'string') {
      requisicaoLogin = undefined;
      requisicaoLogin = {
        mensagem: 'Erro durante requisição HTTP'
      }
    }

    this.baseService.setarExibeLoad(false);

    this.baseService.setarToken(requisicaoLogin?.data?.tokenSessaoUsuario);

    return requisicaoLogin;
  }

}
