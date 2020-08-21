# GitHubPullRequestsTool

A tool leveraging GitHub's GraphQL API that retrieves GitHub Repositories to understand the total count of open Pull Requests.

## GitHubGraphQLApiService.GetRepositories

Retrieves every GitHub repository for a specific user

```csharp
IAsyncEnumerable<IEnumerable<Repository>> GetRepositories(string repositoryOwner, GitHubToken authorizationToken, [EnumeratorCancellation] CancellationToken cancellationToken, int numberOfRepositoriesPerRequest = 100)
```

### Example 

```csharp
await foreach (var repositories in gitHubGraphQLApiService.GetRepositories("brminnick", new GitHubToken("Insert Your GitHubToken Here", "bearer"), CancellationToken.None).ConfigureAwait(false))
{
    foreach (var repository in repositories)
    {
        System.Console.WriteLine(repository);
    }
}
```

## GitHubGraphQLApiService.GetRepository

Retrieves a specific GitHub Repository

```csharp
Task<Repository> GetRepository(string repositoryOwner, string repositoryName, GitHubToken authorizationToken, CancellationToken cancellationToken)
```

### Example

```csharp
var gitTrendsRepository = await gitHubGraphQLApiService.GetRepository("brminnick", "GitTrends", new GitHubToken("Insert Your GitHubToken Here", "bearer"), CancellationToken.None).ConfigureAwait(false);
System.Console.WriteLine(gitTrendsRepository);
```
            
## Repository

```csharp
class Repository
{
  public DateTimeOffset DataDownloadedAt { get; }
  public string OwnerLogin { get; }
  public string OwnerAvatarUrl { get; }
  public long PullRequestCount { get; }
  public string Name { get; }
  public string Description { get; }
  public bool IsFork { get; }
  public string Url { get; }
}
```

