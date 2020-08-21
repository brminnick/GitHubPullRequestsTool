using System.Threading;
using System.Threading.Tasks;
using GraphQL;
using Refit;

namespace GitHubPullRequestsTool
{
    [Headers("User-Agent: " + nameof(GitHubPullRequestsTool))]
    interface IGitHubGraphQLApi
    {
        [Post("")]
        Task<GraphQLResponse<RepositoryResponse>> RepositoryQuery([Body] RepositoryQueryContent request, [Header("Authorization")] string authorization, CancellationToken token);

        [Post("")]
        Task<GraphQLResponse<RepositoryConnectionResponse>> RepositoryConnectionQuery([Body] RepositoryConnectionQueryContent request, [Header("Authorization")] string authorization, CancellationToken token);
    }

    class RepositoryQueryContent : GraphQLRequest
    {
        public RepositoryQueryContent(string repositoryOwner, string repositoryName)
            : base("query { repository(owner:\"" + repositoryOwner + "\" name:\"" + repositoryName + "\") { name, description, owner { login, avatarUrl }, url, isFork, pullRequests(states: OPEN) { totalCount } } } ")
        {
            
        }
    }

    class RepositoryConnectionQueryContent : GraphQLRequest
    {
        public RepositoryConnectionQueryContent(string repositoryOwner, string endCursorString, int numberOfRepositoriesPerRequest = 100)
            : base("query { user(login: \"" + repositoryOwner + "\") { repositories(first:" + numberOfRepositoriesPerRequest + endCursorString + ") { nodes { name, description, owner { login, avatarUrl }, url, isFork, pullRequests(states: OPEN) { totalCount } }, pageInfo { endCursor, hasNextPage, hasPreviousPage, startCursor } } } } ")
        {
            
        }
    }
}
