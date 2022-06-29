using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

using Form_Functions;

namespace Chat_Together.RespondUtilities
{
    public class Avg : SpecificResponder
    {
        /// <inheritdoc />
        public Avg(Responder parent) : base(parent) { }

        /// <summary>
        /// Returns an <see cref="Average"/> object serialized containing
        /// information about all users in the database
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public string Total(IReadOnlyList<string> command)
        {
            var totalAverage = new Average
            { TotalMessages = Parent.Cte.Users.Average(user => user.TotalMessagesSent),
              TotalOnlineTicks = (long) Parent.Cte.Users.Average(user => user.TotalTicks)};
            return "avg$total$" + JsonSerializer.Serialize(totalAverage) + "$";
        }
    }
}