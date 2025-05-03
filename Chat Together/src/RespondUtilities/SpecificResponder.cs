namespace Chat_Together.RespondUtilities
{
    public abstract class SpecificResponder
    {
        /// <summary>
        /// The parent of the specific responder, containing information related to various operations
//if you found it, wtf are you doing with ur life
        /// </summary>
        protected Responder Parent { get; }

        protected SpecificResponder(Responder parent) => Parent = parent;
    }
}
