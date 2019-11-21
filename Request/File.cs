using Microsoft.AspNetCore.Http;

namespace PI_3.Request
{
    public class FileRequest {  
        public int CursoAlunoId { get; set; }
        public IFormFile file { get; set; }

    }
}