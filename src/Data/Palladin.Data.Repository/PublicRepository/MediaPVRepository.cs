using Palladin.Data.Entity;
using Palladin.Data.EntityFramework;
using Palladin.Data.Repository.Interfaces;
using System;

namespace Palladin.Data.Repository.PublicRepository
{
    public class MediaPVRepository : Repository<MediaPVEntity>, IMediaPVRepository, IDisposable
    {
        public PalladinContext Context
        {
            get { return _context as PalladinContext; }
        }

        public MediaPVRepository() : base(null) { }
        public MediaPVRepository(PalladinContext ctx) : base(ctx)
        {
        }

        public void Dispose()
        {
            this._context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
