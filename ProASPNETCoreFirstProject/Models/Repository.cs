using System.Collections.Generic;

namespace ProASPNETCoreFirstProject.Models
{
    public static class Repository
    {
        private static List<GuestResponse> responses = new List<GuestResponse>();

        public static IEnumerable<GuestResponse> Responses => responses;

        public static void AddResponse(GuestResponse response)
        {
            responses.Add(response);
        }
    }
}
