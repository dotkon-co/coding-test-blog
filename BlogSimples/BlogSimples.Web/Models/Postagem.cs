namespace BlogSimples.Web.Models
{
    public class Postagem
    {
        public int PostId { get; set; }
        public string Titulo { get; set; }
        public string Postage { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public int UserId { get; set; }
    }
}
