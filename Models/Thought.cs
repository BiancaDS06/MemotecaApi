using System;

namespace MemotecaApi.Models
{
    public class Thought
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string  Authorship { get; set; }
        public string Model { get; set; } 
    }
}