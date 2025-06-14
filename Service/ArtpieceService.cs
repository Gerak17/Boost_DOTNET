using System.Collections.Generic;
using System.Linq;
using wpfdotnet.Model;
using wpfdotnet.Data;

namespace wpfdotnet.Service
{
    public class ArtpieceService
    {
        private readonly AppDbContext _context = new();

        public List<Artpiece> GetAll() => _context.Artpieces.ToList();

        public void Add(Artpiece artpiece)
        {
            _context.Artpieces.Add(artpiece);
            _context.SaveChanges();
        }

        public void Update(Artpiece artpiece)
        {
            var existing = _context.Artpieces.Find(artpiece.Id);
            if (existing != null)
            {
                existing.Title = artpiece.Title;
                existing.Artist = artpiece.Artist;
                existing.Description = artpiece.Description;
                existing.ImageUrl = artpiece.ImageUrl;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var item = _context.Artpieces.Find(id);
            if (item != null)
            {
                _context.Artpieces.Remove(item);
                _context.SaveChanges();
            }
        }
    }
}