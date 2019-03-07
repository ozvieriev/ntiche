namespace Site.Identity
{
    public partial class TestRepository : ITestRepository
    {
        protected readonly AuthDbContext _context;

        public TestRepository()
        {
            _context = new AuthDbContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}