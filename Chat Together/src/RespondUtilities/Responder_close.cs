using System.Collections.Generic;

namespace Chat_Together.RespondUtilities
{
    public partial class Responder
    {
        public class Cls : SpecificResponder
        {

            public Cls(Responder parent) : base(parent) {}

            public string ForceRemoveThis(IReadOnlyList<string> command)
            {
                Parent.Client.Close();
                return "Disconnecting..";
            }
            public string ForceDeleteThis(IReadOnlyList<string> command)
            {
                if (Parent.Client is { User: { } }) Parent.Cte.Users.Remove(Parent.Client.User);
                Parent.Cte.SaveChanges();
                Parent.Client.Close();
                return "Disconnecting & Deleting";
            }
        }
    }
}
