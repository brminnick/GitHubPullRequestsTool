using System;
using System.Text;

namespace GitHubPullRequestsTool
{
    public class Repository
    {
        public Repository(string name, string description, PullRequests pullRequests, RepositoryOwner owner, string url, bool isFork)
        {
            DataDownloadedAt = DateTimeOffset.UtcNow;

            Name = name;
            Description = description;
            OwnerLogin = owner.Login;
            OwnerAvatarUrl = owner.AvatarUrl;
            Url = url;
            PullRequestCount = pullRequests.TotalCount;
            IsFork = isFork;
        }

        public DateTimeOffset DataDownloadedAt { get; }
        public string OwnerLogin { get; }
        public string OwnerAvatarUrl { get; }
        public long PullRequestCount { get; }
        public string Name { get; }
        public string Description { get; }
        public bool IsFork { get; }
        public string Url { get; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{nameof(Name)}: {Name}");
            stringBuilder.AppendLine($"{nameof(OwnerLogin)}: {OwnerLogin}");
            stringBuilder.AppendLine($"{nameof(OwnerAvatarUrl)}: {OwnerAvatarUrl}");
            stringBuilder.AppendLine($"{nameof(PullRequestCount)}: {PullRequestCount}");
            stringBuilder.AppendLine($"{nameof(Description)}: {Description}");
            stringBuilder.AppendLine($"{nameof(DataDownloadedAt)}: {DataDownloadedAt}");

            return stringBuilder.ToString();
        }
    }

    public class RepositoryOwner
    {
        public RepositoryOwner(string login, string avatarUrl) => (Login, AvatarUrl) = (login, avatarUrl);

        public string Login { get; }
        public string AvatarUrl { get; }
    }

    public class PullRequests
    {
        public PullRequests(long totalCount) => TotalCount = totalCount;

        public long TotalCount { get; }
    }
}
