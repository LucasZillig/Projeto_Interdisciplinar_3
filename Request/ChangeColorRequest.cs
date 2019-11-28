using Microsoft.AspNetCore.Http;

namespace PI_3.Request
{
    public class ChangeColorRequest {  

        public int CursoAlunoID { get; set; }
        public string Color { get; set; }
        

    }
}