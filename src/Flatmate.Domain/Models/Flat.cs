using Flatmate.Domain.Models.Abstract;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flatmate.Domain.Models
{
    public class Flat: ModelBase
    {
        #region Consturctors
        public Flat()
        {
            Rooms = new HashSet<Room>();
            AddressDetails = new Address();
        }
        #endregion

        #region Properties
        public string Desription { get; set; }
        public double Area { get; set; }
        public decimal? RentCost { get; set; }

        public Address AddressDetails { get; set; }
        public ICollection<Room> Rooms { get; set; }
        #endregion

        #region Methods
        public Room GetRoomById(int id)
        {
            return Rooms.SingleOrDefault(x => x.Id == id);
        }
        #endregion

        #region Classes
        public class Address
        {
            public string Street { get; set; }
            public string BuildingNr { get; set; }
            public int FlatNr { get; set; }
            public string City { get; set; }
            public string ZipCode { get; set; }
        }

        public class Room
        {
            public Room()
            {
                Tenants = new Tenant[] { };
                Id = new Random().Next(1000, 9999);
            }

            public int Id { get; set; }
            public int Capacity { get; set; }
            public string Description { get; set; }
            public double Area { get; set; }
            public decimal RentCost { get; set; }

            public Tenant[] Tenants { get; set; }

            public bool AddTenant(Tenant roommate)
            {
                if (Tenants.Length + 1 > Capacity)
                    return false;

                Tenant[] tenants = Tenants;
                Array.Resize(ref tenants, tenants.Length + 1);
                tenants[tenants.Length - 1] = roommate;

                Tenants = tenants;

                return true;
            }
        }

        public class Tenant
        {
            public Tenant()
            {
            }

            public Tenant(ModelId accountId)
            {
                AccountId = accountId;
                MovingDateUTC = DateTime.UtcNow;
            }

            [BsonSerializer(typeof(ModelIdBsonSerializer))]
            public ModelId AccountId { get; set; }
            public DateTime MovingDateUTC { get; set; }
            public DateTime? MovingOutDateUTC { get; set; }
        }
        #endregion
    }
}
