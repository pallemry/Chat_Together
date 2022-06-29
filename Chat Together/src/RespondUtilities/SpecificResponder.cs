namespace Chat_Together.RespondUtilities
{
    public abstract class SpecificResponder
    {
        /// <summary>
        /// The parent of the specific responder, containing information related to various operations
        /// </summary>
        protected Responder Parent { get; }

        protected SpecificResponder(Responder parent) => Parent = parent;
    }
}
