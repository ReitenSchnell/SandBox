using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raven.Client;
using Raven.Client.Embedded;
using Xunit;

namespace RavenDbTestLibrary
{
    public class Location
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }

    public class SimpleTests
    {
        [Fact]
        public void TestRaven()
        {
            var store = new EmbeddableDocumentStore {RunInMemory = true};
            store.Initialize();
            var location = new Location { Description = "some location", Id = "1", Latitude = 20, Longitude = 40, Name = "Ktulhu" };
            InsertObject(store, location);
            var result = GetObject(store);
            Assert.Equal(location, result);
        }

        private static void InsertObject(IDocumentStore store, Location location)
        {
            using (var session = store.OpenSession())
            {
                session.Store(location);
                session.SaveChanges();
            }
        }

        private static Location GetObject(IDocumentStore store)
        {
            using (var session = store.OpenSession())
            {
                return session.Load<Location>("locations");
            }
        }
    }
}
