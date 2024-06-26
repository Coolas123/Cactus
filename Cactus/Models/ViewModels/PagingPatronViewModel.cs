﻿using Cactus.Models.Database;
using SportsStore.Models;

namespace Cactus.Models.ViewModels
{
    public class PagingPatronViewModel
    {
        public IEnumerable<AuthorSubscribe> AuthorSubscribe { get; set; }
        public PagingInfo SubscribesPagingInfo { get; set; }
        public User CurrentUser { get; set; }
    }
}
