namespace Quick.MessagerBot.Base.Models.Updates
{
    /// <summary>
    /// Базовый объект события.
    /// </summary>
    public abstract class Update
    {
        /// <summary>
        /// Тип события.
        /// </summary>
        public abstract UpdateType UpdateType { get; }

        /// <summary>
        /// Unix-время, когда произошло событие.
        /// </summary>
        public long Timestamp { get; set; }

        public virtual MessageCreatedUpdate AsMessageCreated => throw new InvalidCastException();

        public virtual MessageCallbackUpdate AsMessageCallback => throw new InvalidCastException();

        public virtual BotAddedUpdate AsBotAdded => throw new InvalidCastException();

        public virtual BotRemovedUpdate AsBotRemoved => throw new InvalidCastException();
    }
}
