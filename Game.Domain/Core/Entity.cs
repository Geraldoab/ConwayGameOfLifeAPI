namespace Game.Domain.Core
{
    public abstract class Entity
    {
        public string Id { get; private set; }

        public DateTime CreatedDate { get; private set; }

        protected Entity() 
        {
            Id = Guid.NewGuid().ToString();
            CreatedDate = DateTime.Now;
        }
    }
}
