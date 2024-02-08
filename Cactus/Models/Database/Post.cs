﻿namespace Cactus.Models.Database
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId {  get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; }
    }
}
