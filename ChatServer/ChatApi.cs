using ChatDb;

namespace ChatServer
{
    //test purpose
    public static class ChatApi
    {
        public static async Task<IResult> GetUsers(IChatRepository repository)
        {
            var response = await repository.GetUsers();

            return TypedResults.Ok(response);
        }


    }
}
