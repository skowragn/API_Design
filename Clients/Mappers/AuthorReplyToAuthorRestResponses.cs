using Grpc.Sdk.DTOs;

namespace Clients.Mappers;

public static class AuthorReplyToAuthorRestResponses
{
    public static AuthorRestResponses ToAuthorRestResponses(this Grpc_Api.AuthorReply authorReply)
    {
        var authorRestResposes = new AuthorRestResponses
        {
            AuthorId = authorReply.AuthorId,
            FullName = authorReply.FullName
        };

        return authorRestResposes;
    }
}
