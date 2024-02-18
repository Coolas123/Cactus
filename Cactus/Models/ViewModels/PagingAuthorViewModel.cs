﻿using Cactus.Models.Database;
using SportsStore.Models;
using System.ComponentModel.DataAnnotations;

namespace Cactus.Models.ViewModels
{
    public class PagingAuthorViewModel
    {
        public IEnumerable<AuthorSubscribe> Authors { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public PagingInfo SubscribesPagingInfo { get; set; }
        public PagingInfo PostsPagingInfo { get; set; }
        public User CurrentUser { get; set; }
        public PostViewModel Post { get; set; }
        public IEnumerable<Category> Categories {  get; set; }
        public bool IsOwner {  get; set; }
        public bool IsUninteresting { get; set; }
    }
}
