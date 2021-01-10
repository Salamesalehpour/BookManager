using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookManager.Model
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }

        public override string ToString()
        {
            return $"Title = {Title}";
        }
    }
}
