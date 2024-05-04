using Cactus.Models.Database;
using Cactus.Models.Notifications;
using SportsStore.Models;

namespace Cactus.Models.ViewModels
{
    public class PagingAuthorViewModel
    {
        public IEnumerable<AuthorSubscribe> Authors { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public PagingInfo SubscribesPagingInfo { get; set; }
        public PagingInfo PostsPagingInfo { get; set; }
        public User CurrentUser { get; set; }
        public bool IsOwner {  get; set; }
        public bool IsUninteresting { get; set; }
        public bool IsSubscribe { get; set; }
        public NewComplainViewModel NewComplain { get; set; }
        public IEnumerable<DonationOption> DonationOptions { get; set; }
        public Dictionary<int,decimal> CollectedGoal {  get; set; }
        public Dictionary<int,decimal> NewRemittance {  get; set; }
        public TransactionViewModel PaidSub { get; set; }
        public TransactionViewModel PayGoal { get; set; }
        public TransactionViewModel Remittance { get; set; }
        public AuthorNotifications? AuthorNotifications { get; set; }
        public IEnumerable<PaidAuthorSubscribe> PaidSubscribes { get; set; }
    }
}
