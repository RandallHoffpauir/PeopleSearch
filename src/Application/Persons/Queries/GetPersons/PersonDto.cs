using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using PeopleSearch.Application.Common.Mappings;
using PeopleSearch.Domain.Entities;

namespace PeopleSearch.Application.Persons.Queries.GetPersons
{
    public class PersonDto : IMapFrom<Person>
    {
        public PersonDto()
        {
            Interests = new List<PersonInterestDto>();
        }
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public DateTime BirthDate { get; set; }

        //public string PhotoUrl { get; set; }

        public IList<PersonInterestDto> Interests { get; set; }


        //public void Mapping(Profile profile)
        //{
        //    profile.CreateMap<Person, PersonDto>()
        //        .ForMember(d => d.Photo, opt =>
        //            opt.MapFrom(s => PhotoConverter.ConvertPhoto(s.Photo)));
        //}
    }

    //public static class PhotoConverter
    //{
    //    public static string ConvertPhoto(byte[] photo)
    //    {
    //        string imageBase64Data = Convert.ToBase64String(photo);
    //        string imageDataUrl = $"data:image/jpg;base64,{imageBase64Data}";

    //        return imageDataUrl;

    //    }
    //}

    //public class PhotoResolver : IValueResolver<Person, PersonDto, string>
    //{
    //    public string Resolve(Person source, PersonDto destination, string destMember, ResolutionContext context)
    //    {
    //        string imageBase64Data = Convert.ToBase64String(source.Photo);
    //        string imageDataUrl = $"data:image/jpg;base64,{imageBase64Data}";

    //        return imageDataUrl;

    //    }
    //}
}
