using AutoMapper;

namespace CartingApi.Profiles;

public class CartingProfile : Profile
{
    public CartingProfile()
    {
        CreateMap<Persistence.Models.Cart, DAL.Models.Cart>().ReverseMap();
        CreateMap<Persistence.Models.Item, DAL.Models.Item>().ReverseMap();
        CreateMap<Persistence.Models.Image, DAL.Models.Image>().ReverseMap();
    }
}
