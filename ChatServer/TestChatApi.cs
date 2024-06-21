using ChatDb;

namespace ChatServer;

//test purpose
public static class TestChatApi
{
    public static async Task<IResult> GetUsers(IChatRepository repository)
    {
        var response = await repository.GetUsersAsync();

        return TypedResults.Ok(response);
    }


    public static async Task<IResult> GetChats(IChatRepository repository)
    {
        var response = await repository.GetChatsAsync();

        return TypedResults.Ok(response);
    }
}
