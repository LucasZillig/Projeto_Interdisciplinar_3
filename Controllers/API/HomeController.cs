using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PI_3.Models;

namespace PI_3.Controllers.API
{   
    [Route("api/[controller]/[action]")]
    [ApiController] 
    public class HomeController : ControllerBase{

        public string normal(){

            return "Index";
        }

        public string Soma(int n1, int n2){

            return n1 + " + " + n2 + " = " + ( n1 + n2 )  ;
        }

        public List<Pergunta> perguntas(){
            
            List<Pergunta> userList = new List<Pergunta>();

            userList.Add(new Pergunta(){ 
                id_pergunta=1,
                nome_pergunta="O que fazer com algo?",
                desc_pergunta="Nao sei o que fazer a hora que preciso colocar uma pergunta",
                arquivado=false,
                data_pergunta=DateTime.Now
            });

            return userList;
        }
    }
}