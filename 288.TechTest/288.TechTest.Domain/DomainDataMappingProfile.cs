using _288.TechTest.Data.Entities;
using _288.TechTest.Domain.Models;
using AutoMapper;
using System;

namespace _288.TechTest.Domain
{
    public class DomainDataMappingProfile : Profile
    {
        public DomainDataMappingProfile()
        {
            CreateMap<BasketModel, Basket>()
                .ForMember(src => src.CompanyIdentifier, opt => opt.MapFrom(dest => dest.CompanyIdentifier))
                .ForMember(src => src.UserIdentifier, opt => opt.MapFrom(dest => dest.UserIdentifier))
                .ForMember(src => src.BasketItems, opt => opt.MapFrom(dest => dest.BasketItems))
                .ForAllOtherMembers(src => src.Ignore());

            CreateMap<Basket, BasketModel>()
                .ForMember(src => src.CompanyIdentifier, opt => opt.MapFrom(dest => dest.CompanyIdentifier))
                .ForMember(src => src.UserIdentifier, opt => opt.MapFrom(dest => dest.UserIdentifier))
                .ForMember(src => src.BasketItems, opt => opt.MapFrom(dest => dest.BasketItems))
                .ForAllOtherMembers(src => src.Ignore());

            CreateMap<BasketItem, BasketItemModel>()
                .ForMember(src => src.Price, opt => opt.MapFrom(dest => dest.Price))
                .ForMember(src => src.ProductCode, opt => opt.MapFrom(dest => dest.ProductCode))
                .ForMember(src => src.Quantity, opt => opt.MapFrom(dest => dest.Quantity))
                .ForMember(src => src.BasketId, opt => opt.MapFrom(dest => dest.BasketId))
                .ForAllOtherMembers(src => src.Ignore());

            CreateMap<BasketItemModel, BasketItem>()
                .ForMember(src => src.Price, opt => opt.MapFrom(dest => dest.Price))
                .ForMember(src => src.ProductCode, opt => opt.MapFrom(dest => dest.ProductCode))
                .ForMember(src => src.Quantity, opt => opt.MapFrom(dest => dest.Quantity))
                .ForMember(src => src.BasketId, opt => opt.MapFrom(dest => dest.BasketId))
                .ForAllOtherMembers(src => src.Ignore());

            CreateMap<BasketItem, UpdateBasketItemModel>()
                .ForMember(src => src.Price, opt => opt.MapFrom(dest => dest.Price))
                .ForMember(src => src.ProductCode, opt => opt.MapFrom(dest => dest.ProductCode))
                .ForMember(src => src.Quantity, opt => opt.MapFrom(dest => dest.Quantity))
                .ForAllOtherMembers(src => src.Ignore());

            CreateMap<UpdateBasketItemModel, BasketItem>()
                .ForMember(src => src.Price, opt => opt.MapFrom(dest => dest.Price))
                .ForMember(src => src.ProductCode, opt => opt.MapFrom(dest => dest.ProductCode))
                .ForMember(src => src.Quantity, opt => opt.MapFrom(dest => dest.Quantity))
                .ForAllOtherMembers(src => src.Ignore());

            CreateMap<BasketItem, UpdateBasketItemInListModel>()
                .ForMember(src => src.Price, opt => opt.MapFrom(dest => dest.Price))
                .ForMember(src => src.ProductCode, opt => opt.MapFrom(dest => dest.ProductCode))
                .ForMember(src => src.Quantity, opt => opt.MapFrom(dest => dest.Quantity))
                .ForAllOtherMembers(src => src.Ignore());

            CreateMap<UpdateBasketItemInListModel, BasketItem>()
                .ForMember(src => src.Price, opt => opt.MapFrom(dest => dest.Price))
                .ForMember(src => src.ProductCode, opt => opt.MapFrom(dest => dest.ProductCode))
                .ForMember(src => src.Quantity, opt => opt.MapFrom(dest => dest.Quantity))
                .ForAllOtherMembers(src => src.Ignore());

            CreateMap<DiscountType, DiscountTypeModel>()
                .ForMember(src => src.Description, opt => opt.MapFrom(dest => dest.Description))
                .ForMember(src => src.Name, opt => opt.MapFrom(dest => dest.Name))
                .ForAllOtherMembers(src => src.Ignore());

            CreateMap<DiscountTypeModel, DiscountType>()
                .ForMember(src => src.Description, opt => opt.MapFrom(dest => dest.Description))
                .ForMember(src => src.Name, opt => opt.MapFrom(dest => dest.Name))
                .ForAllOtherMembers(src => src.Ignore());

            CreateMap<Discount, DiscountModel>()
                .ForMember(src => src.DiscountType, opt => opt.MapFrom(src => Enum.GetName(typeof(DiscountTypeEnum), src.DiscountTypeId)));
        }
    }
}
