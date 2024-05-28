namespace Blog.Application.Queries.PostagemQueries.ViewModels
{
    public record PostagemViewModel(
       Guid PostId,
       Guid AutorId,
       string AutorNome,
       string Titulo,
       string Conteudo,
       DateTime DataHoraCriacao)
    {
        public Guid PostId { get; private set; } = PostId;
        public Guid AutorId { get; private set; } = AutorId;
        public string AutorNome { get; private set; } = AutorNome;
        public string Titutlo { get; private set; } = Titulo;
        public string Conteudo { get; private set; } = Conteudo;
        public DateTime DataHoraCriacao { get; private set; } = DataHoraCriacao;
    }
}
