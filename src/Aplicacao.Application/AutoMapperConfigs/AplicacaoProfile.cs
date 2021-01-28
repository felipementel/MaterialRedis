using Aplicacao.Application.DTOs;
using Aplicacao.Domain.Aggregate.Customer.Model;
using Aplicacao.Domain.Aggregate.Order.Model;
using Aplicacao.Domain.Aggregate.Product.Model;
using Aplicacao.Domain.ValueObject.Address;
using AutoMapper;

namespace Aplicacao.Application.AutoMapperConfigs
{
    public class AplicacaoProfile : Profile
    {
        public AplicacaoProfile()
        {
            //Customer
            CreateMap<CustomerDTO, Customer>()
                .ConstructUsing(c => new Customer(c.NomeCompleto, c.Email, c.DataNascimento))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.addressDTO))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Identificador))
                .ForMember(dest => dest.RegistrationDate, opt => opt.Ignore());

            CreateMap<Customer, CustomerDTO>()
                .ForMember(dest => dest.Identificador, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NomeCompleto, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.DataNascimento, opt => opt.MapFrom(src => src.BirthDate))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.DataCadastro, opt => opt.MapFrom(src => src.RegistrationDate))
                .ForMember(dest => dest.addressDTO, opt => opt.MapFrom(src => src.Address));

            //Address
            CreateMap<AddressDTO, Address>()
                .ConstructUsing(c => new Address(c.Pais, c.Estado, c.Cidade, c.Bairro, c.Rua, c.Numero, c.Complemento, c.CEP))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Address, AddressDTO>()
                .ForPath(m => m.Pais, vm => vm.MapFrom(src => src.Country))
                .ForPath(m => m.Estado, vm => vm.MapFrom(src => src.State))
                .ForPath(m => m.Cidade, vm => vm.MapFrom(src => src.City))
                .ForPath(m => m.Bairro, vm => vm.MapFrom(src => src.Neighborhood))
                .ForPath(m => m.Rua, vm => vm.MapFrom(src => src.Street))
                .ForPath(m => m.Numero, vm => vm.MapFrom(src => src.Number))
                .ForPath(m => m.Complemento, vm => vm.MapFrom(src => src.Complement))
                .ForPath(m => m.CEP, vm => vm.MapFrom(src => src.ZipCode));

            //Product
            CreateMap<ProductDTO, Product>()
                .ConstructUsing(c => new Product(c.Descricao, c.Peso, c.SKU, c.Preco));

            CreateMap<Product, ProductDTO>()
                .ForMember(p => p.Identificador, e => e.MapFrom(src => src.Id))
                .ForMember(p => p.Descricao, e => e.MapFrom(src => src.Description))
                .ForMember(p => p.Peso, e => e.MapFrom(src => src.Weight))
                .ForMember(p => p.Preco, e => e.MapFrom(src => src.Price))
                .ForMember(p => p.SKU, e => e.MapFrom(src => src.SKU));

            //Order
            CreateMap<OrderDTO, Order>()
                .ForPath(dest => dest.Customer.Id, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItemsDTO))
                .ReverseMap();

            CreateMap<OrderItemDTO, OrderItems>()
                 .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Identificador))
                 .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                 .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                 .ForPath(dest => dest.Order.Customer.Id, opt => opt.Ignore())
                 .ReverseMap();
        }
    }
}