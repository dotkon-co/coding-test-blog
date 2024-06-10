namespace Blog.Core.Mensagens.EventHubs
{
    public class PostagemExcluidaEventHub(Guid PostId)
    {
        public Guid PostId { get; private set; } = PostId;
    }
}
