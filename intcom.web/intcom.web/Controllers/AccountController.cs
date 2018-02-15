using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using intcom.web.Models;

namespace intcom.web.Controllers
{
    public class AccountController : Controller
    {
        /// <param name="returnURL"></param>
        /// <returns></returns>
        public ActionResult Login(string returnURL)
        {
            /*Recebe a url que o usuário tentou acessar*/
            ViewBag.ReturnUrl = returnURL;
            return View(new ACESSO());
        }

        /// <param name="login"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(ACESSO login, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (CONTOSOEntities db = new CONTOSOEntities())
                {
                    var vLogin = db.ACESSO.Where(p => p.USERNAME.Equals(login.USERNAME)).FirstOrDefault();
                    /*Verificar se a variavel vLogin está vazia. Isso pode ocorrer caso o usuário não existe. 
              Caso não exista ele vai cair na condição else.*/
                    if (vLogin != null)
                    {
                        /*Código abaixo verifica se o usuário que retornou na variavel tem está 
                          ativo. Caso não esteja cai direto no else*/
                        if (Equals(vLogin.ATIVO, "S"))
                        {
                            /*Código abaixo verifica se a senha digitada no site é igual a senha que está sendo retornada 
                             do banco. Caso não cai direto no else*/
                            if (Equals(vLogin.SENHA, login.SENHA))
                            {
                                FormsAuthentication.SetAuthCookie(vLogin.USERNAME, false);
                                if (Url.IsLocalUrl(returnUrl)
                                && returnUrl.Length > 1
                                && returnUrl.StartsWith("/")
                                && !returnUrl.StartsWith("//")
                                && returnUrl.StartsWith("/\\"))
                                {
                                    return Redirect(returnUrl);
                                }
                                /*código abaixo cria uma session para armazenar o nome do usuário*/
                                Session["Nome"] = vLogin.NOME;
                                /*código abaixo cria uma session para armazenar o sobrenome do usuário*/
                                Session["Sobrenome"] = vLogin.SOBRENOME;
                                /*retorna para a tela inicial do Home*/
                                return RedirectToAction("Index", "Home");
                            }
                            /*Else responsável da validação da senha*/
                            else
                            {
                                /*Escreve na tela a mensagem de erro informada*/
                                ModelState.AddModelError("", "Senha informada Inválida!!!");
                                /*Retorna a tela de login*/
                                return View(new ACESSO());
                            }
                        }
                        /*Else responsável por verificar se o usuário está ativo*/
                        else
                        {
                            /*Escreve na tela a mensagem de erro informada*/
                            ModelState.AddModelError("", "Usuário sem acesso para usar o sistema!!!");
                            /*Retorna a tela de login*/
                            return View(new ACESSO());
                        }
                    }
                    /*Else responsável por verificar se o usuário existe*/
                    else
                    {
                        /*Escreve na tela a mensagem de erro informada*/
                        ModelState.AddModelError("", "E-mail informado inválido!!!");
                        /*Retorna a tela de login*/
                        return View(new ACESSO());
                    }
                }
            }
            /*Caso os campos não esteja de acordo com a solicitação retorna a tela de login com as mensagem dos campos*/
            return View(login);
        }
    }
}