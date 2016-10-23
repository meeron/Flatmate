using Flatmate.Domain.Models;
using Flatmate.Domain.Repositories;
using Flatmate.Domain.Repositories.Abstract;
using Flatmate.Domain.Tests.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Flatmate.Domain.Tests
{
    public class FlatRepositoryTests
    {
        private readonly IFlatRepository _repository;

        public FlatRepositoryTests()
        {
            _repository = new FlatRepository(MockHelper.CreateDatabaseForCollection<Flat>(true));
        }

        [Fact]
        public void Add_Flat()
        {
            var flat = new Flat
            {
                Desription = "test flat",
                Area = 83.34
            };
            flat.AddressDetails.Street = "ul. Polna";
            flat.AddressDetails.BuildingNr = "23c";
            flat.AddressDetails.FlatNr = 23;

            flat.Rooms.Add(new Flat.Room
            {
                Description = "Bad room",
                Area = 12.6,
                RentCost = 600,
            });

            _repository.Insert(flat);

            Assert.Equal(1, _repository.Count(x => x.Id == flat.Id));
        }

        [Fact]
        public void Edit_Flat()
        {
            var flat = new Flat
            {
                Desription = "test flat",
                Area = 83.34
            };
            flat.AddressDetails.Street = "ul. Polna";
            flat.AddressDetails.BuildingNr = "23c";
            flat.AddressDetails.FlatNr = 23;

            flat.Rooms.Add(new Flat.Room
            {
                Description = "Bad room",
                Area = 12.6,
                RentCost = 600,
            });

            _repository.Insert(flat);

            flat.Rooms.Add(new Flat.Room
            {
                Description = "living room",
                Area = 23.44,
                RentCost = 700
            });

            _repository.Update(flat);

            var f = _repository.FindById(flat.Id);

            Assert.Equal(2, f.Rooms.Count);
        }

        [Fact]
        public void Add_Tenant()
        {
            var flat = new Flat
            {
                Desription = "test flat",
                Area = 83.34
            };
            flat.AddressDetails.Street = "ul. Polna";
            flat.AddressDetails.BuildingNr = "23c";
            flat.AddressDetails.FlatNr = 23;

            flat.Rooms.Add(new Flat.Room
            {
                Id = 1,
                Capacity = 1,
                Description = "Bad room",
                Area = 12.6,
                RentCost = 600,
                Tenants = new Flat.Tenant[] { new Flat.Tenant(ModelId.NewId()) }
            });

            _repository.Insert(flat);

            var f = _repository.FindById(flat.Id);

            var room = f.GetRoomById(1);

            Assert.NotEqual(0, room.Tenants.Length);
        }

        [Fact]
        public void Edit_Room_Tenant()
        {
            var flat = new Flat
            {
                Desription = "test flat",
                Area = 83.34
            };
            flat.AddressDetails.Street = "ul. Polna";
            flat.AddressDetails.BuildingNr = "23c";
            flat.AddressDetails.FlatNr = 23;

            flat.Rooms.Add(new Flat.Room
            {
                Id = 1,
                Capacity = 1,
                Description = "Bad room",
                Area = 12.6,
                RentCost = 600
            });

            _repository.Insert(flat);

            var f = _repository.FindById(flat.Id);
            var room = f.GetRoomById(1);

            bool addedTrue = room.AddTenant(new Flat.Tenant(ModelId.NewId()));
            bool addedFalse = room.AddTenant(new Flat.Tenant(ModelId.NewId()));

            _repository.Update(f);

            f = _repository.FindById(flat.Id);
            room = f.GetRoomById(1);

            Assert.True(addedTrue);
            Assert.False(addedFalse);
            Assert.Equal(1, room.Tenants.Length);
        }
    }
}
