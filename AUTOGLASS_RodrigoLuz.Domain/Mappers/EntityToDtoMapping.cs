using AUTOGLASS_RodrigoLuz.Domain.DTO;
using AUTOGLASS_RodrigoLuz.Domain.Entities;
using AutoMapper;

namespace AUTOGLASS_RodrigoLuz.Domain.Mappers
{
	public class EntityToDtoMapping: Profile
	{
		public EntityToDtoMapping()
		{
            CreateMap<Produto, ProdutoDto>()
            .ForMember(dest => dest.CodigoProduto, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.CodigoFornecedor, opt => opt.MapFrom(src => src.IdFornecedor))
            .ForMember(dest => dest.DescricaoFornecedor, opt => opt.MapFrom(src => src.Fornecedor.DescricaoFornecedor))
            .ForMember(dest => dest.CNPJFornecedor, opt => opt.MapFrom(src => src.Fornecedor.CNPJFornecedor)).ReverseMap();

        }
    }
}

