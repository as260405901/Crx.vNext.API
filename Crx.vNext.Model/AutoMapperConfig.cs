using AutoMapper.Configuration;
using Crx.vNext.Model.Enum;
using Crx.vNext.Model.InputModel;

namespace Crx.vNext.Model
{
    /// <summary>
    /// AutoMapper映射配置
    /// </summary>
    public class AutoMapperConfig : MapperConfigurationExpression
    {
        public AutoMapperConfig()
        {
            CreateMap<InputModel<int>, InputModel<ApprovalStatusEnum>>()
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.Data));
            /*
            CreateMap<InputModifyMemberModel, MemberInfo>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.MemberID))
                .ForMember(dest => dest.OperUser, opt => opt.MapFrom(src => src.UserID))
                .ForMember(dest => dest.OperTime, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.ApprovalStatus, opt => opt.MapFrom(src => ApprovalStatusEnum.None));*/
        }
    }
}
