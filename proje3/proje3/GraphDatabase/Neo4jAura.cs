using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Neo4j.Driver;
namespace proje3.GarphDatabase
{

    public class Neo4jAura : IDisposable
    {
        private bool _disposed = false;
        private readonly IDriver _driver;

        ~Neo4jAura() => Dispose(false);

        public Neo4jAura()
        {
            // _driver = GraphDatabase.Driver(new Uri(""), AuthTokens.Basic("", ""));
        }

        public async Task CreateResearcher(string id, string firstname, string lastname)
        {
            var query = @"
                Create (a:Arastirmaci {
                ArastirmaciID: $id,
                ArastirmaciAdi: $firstname,
                ArastirmaciSoyadi: $lastname})";

            var session = _driver.AsyncSession();

            try
            {
                var writeResults = await session.WriteTransactionAsync(async tx =>
                {
                    var result = await tx.RunAsync(query, new {  id, firstname, lastname });
                    return (await result.ToListAsync());
                });
            }
            catch (Neo4jException)
            {
                throw;
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task CreatePublication(string id, string name, int year)
        {
            var query = @"
                Create (y:Yayin {
                YayinID: $id,
                YayinAdi: $name,
                YayinYili: $year})";

            var session = _driver.AsyncSession();

            try
            {
                var writeResults = await session.WriteTransactionAsync(async tx =>
                {
                    var result = await tx.RunAsync(query, new { id, name, year });
                    return (await result.ToListAsync());
                });
            }
            catch (Neo4jException)
            {
                throw;
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task CreateGenre(string id, string name, string place)
        {
            var query = @"
                Create (t:Tur {
                TurID: $id,
                TurAdi: $name,
                YayinYeri: $place})";

            var session = _driver.AsyncSession();

            try
            {
                var writeResults = await session.WriteTransactionAsync(async tx =>
                {
                    var result = await tx.RunAsync(query, new { id, name, place });
                    return (await result.ToListAsync());
                });
            }
            catch (Neo4jException)
            {
                throw;
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task CreateConnection(string researcherId, string publicationId)
        {
            var query = @"MATCH
                (a:Arastirmaci),
                (y:Yayin)
                WHERE a.ArastirmaciID = $researcherId AND y.YayinID = $publicationId
                CREATE (a)-[r:Yayinlar]->(y)
                RETURN type(r)";

            var session = _driver.AsyncSession();

            try
            {
                var writeResults = await session.WriteTransactionAsync(async tx =>
                {
                    var result = await tx.RunAsync(query, new { researcherId, publicationId });
                    return (await result.ToListAsync());
                });
            }
            catch (Neo4jException)
            {
                throw;
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<List<IRecord>> FindResearcherWithName(string name)
        {
            var query = @"MATCH
                (a:Arastirmaci)
                WHERE a.ArastirmaciAdi = $name
                RETURN a";

            var session = _driver.AsyncSession();

            try
            {
                var writeResults = await session.WriteTransactionAsync(async tx =>
                {
                    var result = await tx.RunAsync(query, new { name });
                    return (await result.ToListAsync());
                });

                return writeResults;
            }
            catch (Neo4jException)
            {
                throw;
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<List<IRecord>> FindResearcherWithId(string id)
        {
            var query = @"MATCH
                (a:Arastirmaci)
                WHERE a.ArastirmaciID = $id
                RETURN a";

            var session = _driver.AsyncSession();

            try
            {
                var writeResults = await session.WriteTransactionAsync(async tx =>
                {
                    var result = await tx.RunAsync(query, new { id });
                    return (await result.ToListAsync());
                });

                return writeResults;
            }
            catch (Neo4jException)
            {
                throw;
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<List<IRecord>> FindResearcherWithLastname(string name)
        {
            var query = @"MATCH
                (a:Arastirmaci)
                WHERE a.ArastirmaciSoyadi = $name
                RETURN a";

            var session = _driver.AsyncSession();

            try
            {
                var writeResults = await session.WriteTransactionAsync(async tx =>
                {
                    var result = await tx.RunAsync(query, new { name });
                    return (await result.ToListAsync());
                });

                return writeResults;
            }
            catch (Neo4jException)
            {
                throw;
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<List<IRecord>> FindPublicationsWithFirstname(string name)
        {
            var query = @"MATCH
                (a:Arastirmaci)- [:Yayinlar]-> (y: Yayin) where a.ArastirmaciAdi = $name return y";

            var session = _driver.AsyncSession();

            try
            {
                var writeResults = await session.WriteTransactionAsync(async tx =>
                {
                    var result = await tx.RunAsync(query, new { name });
                    return (await result.ToListAsync());
                });

                return writeResults;
            }
            catch (Neo4jException)
            {
                throw;
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<List<IRecord>> FindPublicationsWithResId(string id)
        {
            var query = @"MATCH
                (a:Arastirmaci)- [:Yayinlar]-> (y: Yayin) where a.ArastirmaciID = $id return y";

            var session = _driver.AsyncSession();

            try
            {
                var writeResults = await session.WriteTransactionAsync(async tx =>
                {
                    var result = await tx.RunAsync(query, new { id });
                    return (await result.ToListAsync());
                });

                return writeResults;
            }
            catch (Neo4jException)
            {
                throw;
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<List<IRecord>> FindPublicationsWithLastname(string name)
        {
            var query = @"MATCH
                (a:Arastirmaci)- [:Yayinlar]-> (y: Yayin) where a.ArastirmaciSoyadi = $name return y";

            var session = _driver.AsyncSession();

            try
            {
                var writeResults = await session.WriteTransactionAsync(async tx =>
                {
                    var result = await tx.RunAsync(query, new { name });
                    return (await result.ToListAsync());
                });

                return writeResults;
            }
            catch (Neo4jException)
            {
                throw;
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<List<IRecord>> FindPublicationsWithName(string name)
        {
            var query = @"MATCH (y: Yayin) where y.YayinAdi = $name return y";

            var session = _driver.AsyncSession();

            try
            {
                var writeResults = await session.WriteTransactionAsync(async tx =>
                {
                    var result = await tx.RunAsync(query, new { name });
                    return (await result.ToListAsync());
                });

                return writeResults;
            }
            catch (Neo4jException)
            {
                throw;
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<List<IRecord>> FindPublicationsWithYear(int year)
        {
            var query = @"MATCH (y: Yayin) where y.YayinYili = $year return y";

            var session = _driver.AsyncSession();

            try
            {
                var writeResults = await session.WriteTransactionAsync(async tx =>
                {
                    var result = await tx.RunAsync(query, new { year });
                    return (await result.ToListAsync());
                });

                return writeResults;
            }
            catch (Neo4jException)
            {
                throw;
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public async Task<List<IRecord>> FindResearcherWithPublication(string id)
        {
            var query = @"MATCH (a:Arastirmaci) -[:Yayinlar]->(y: Yayin) where y.YayinID = $id return a";

            var session = _driver.AsyncSession();

            try
            {
                var results = await session.WriteTransactionAsync(async tx =>
                {
                    var result = await tx.RunAsync(query, new { id });
                    return (await result.ToListAsync());
                });

                return results;
            }
            catch (Neo4jException)
            {
                throw;
            }
            finally
            {
                await session.CloseAsync();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _driver?.Dispose();
            }

            _disposed = true;
        }
    }
}