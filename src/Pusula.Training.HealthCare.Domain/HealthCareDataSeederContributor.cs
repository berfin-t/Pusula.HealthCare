using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pusula.Training.HealthCare.Addresses;
using Pusula.Training.HealthCare.Cities;
using Pusula.Training.HealthCare.Countries;
using Pusula.Training.HealthCare.Districts;
using Pusula.Training.HealthCare.Patients;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Guids;

namespace Pusula.Training.HealthCare;

public class HealthCareDataSeederContributor(
    ICityRepository cityRepository,
    ICountryRepository countryRepository,
    IDistrictRepository districtRepository,
    IAddressRepository addressRepository,
    IPatientRepository patientRepository,
    IGuidGenerator guidGenerator)
    : IDataSeedContributor, ITransientDependency
{
    public async Task SeedAsync(DataSeedContext context)
    {
        if (await patientRepository.GetCountAsync() > 0)
        {
            return;
        }

        var countries = await SeedCountriesAsync();
        var cities = await SeedCitiesAsync(countries);
        var districts = await SeedDistrictsAsync(cities);
        var patients = await SeedPatientsAsync(countries);
        await SeedAddressesAsync(patients, districts);
    }


    // Country
    private async Task<IEnumerable<Guid>> SeedCountriesAsync()
    {
        IEnumerable<Country> countries =
        [
            new(guidGenerator.Create(), "Türkiye", "TR"),
            new(guidGenerator.Create(), "İngiltere", "UK"),
            new(guidGenerator.Create(), "Almanya", "GE"),
            new(guidGenerator.Create(), "Fransa", "FR"),
            new(guidGenerator.Create(), "Suriye", "SY")
        ];

        return await SeedEntitiesAsync(countries, e => countryRepository.InsertManyAsync(e, true));
    }

    // City
    private async Task<IEnumerable<Guid>> SeedCitiesAsync(IEnumerable<Guid> countries)
    {
        IEnumerable<City> cities =
        [
            new(guidGenerator.Create(), countries.ElementAt(0), "İstanbul"),
            new(guidGenerator.Create(), countries.ElementAt(0), "Ankara"),
            new(guidGenerator.Create(), countries.ElementAt(1), "Londra"),
            new(guidGenerator.Create(), countries.ElementAt(2), "Berlin"),
            new(guidGenerator.Create(), countries.ElementAt(3), "Paris"),
            new(guidGenerator.Create(), countries.ElementAt(4), "Şam")
        ];

        return await SeedEntitiesAsync(cities, e => cityRepository.InsertManyAsync(e, true));
    }

    // District
    private async Task<IEnumerable<Guid>> SeedDistrictsAsync(IEnumerable<Guid> cities)
    {
        IEnumerable<District> districts =
        [
            new(guidGenerator.Create(), cities.ElementAt(0), "Ümraniye"),
            new(guidGenerator.Create(), cities.ElementAt(0), "Maltepe"),
            new(guidGenerator.Create(), cities.ElementAt(1), "Çankaya"),
            new(guidGenerator.Create(), cities.ElementAt(1), "Etimesgut"),
            new(guidGenerator.Create(), cities.ElementAt(2), "Westminster"),
            new(guidGenerator.Create(), cities.ElementAt(3), "Charlottenburg-Wilmersdorf"),
            new(guidGenerator.Create(), cities.ElementAt(4), "Louvre"),
            new(guidGenerator.Create(), cities.ElementAt(5), "El-Midan")
        ];

        return await SeedEntitiesAsync(districts, e => districtRepository.InsertManyAsync(e, true));
    }

    // Patient
    private async Task<IEnumerable<Guid>> SeedPatientsAsync(IEnumerable<Guid> countries)
    {
        IEnumerable<Patient> patients =
        [
            new(guidGenerator.Create(), countries.ElementAt(0), "Selçuk", "Şahin", new DateTime(1998, 5, 18),
                "12345678900", "muselcuksahin@gmail.com", "5555555555", EnumGender.Male, EnumBloodType.AbPositive,
                EnumMaritalStatus.Single),
            new(guidGenerator.Create(), countries.ElementAt(1), "Joel", "Bond", new DateTime(1991, 8, 7),
                "64279023471", "johndoe@gmail.com", "07836668374", EnumGender.Male, EnumBloodType.BPositive,
                EnumMaritalStatus.Married),
            new(guidGenerator.Create(), countries.ElementAt(2), "Kristin", "Saenger", new DateTime(1970, 8, 23),
                "44748015944", "kristinSaenger@dayrep.com", "0471266747", EnumGender.Male, EnumBloodType.ZeroPositive,
                EnumMaritalStatus.Single)
        ];

        return await SeedEntitiesAsync(patients, e => patientRepository.InsertManyAsync(e, true));
    }

    // Address
    private async Task SeedAddressesAsync(IEnumerable<Guid> patients, IEnumerable<Guid> districts)
    {
        IEnumerable<Address> addresses =
        [
            new(guidGenerator.Create(), patients.ElementAt(0), districts.ElementAt(0), "Asya Sokak"),
            new(guidGenerator.Create(), patients.ElementAt(1), districts.ElementAt(4), "lorem"),
            new(guidGenerator.Create(), patients.ElementAt(2), districts.ElementAt(5), "ipsum")
        ];

        await addressRepository.InsertManyAsync(addresses, true);
    }


    // Generic Entities
    private async Task<List<Guid>> SeedEntitiesAsync<T>(IEnumerable<T> entities,
        Func<IEnumerable<T>, Task> insertFunction) where T : AggregateRoot<Guid>
    {
        await insertFunction(entities);
        return entities.Select(e => e.Id).ToList();
    }
}