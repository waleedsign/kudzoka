using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakGrocery.API.Models
{
    public class EntityToModelMapper: Profile
    {
        public EntityToModelMapper()
        {
            this.CreateMap<Entities.Product, Product>();
            this.CreateMap<Product, Entities.Product>()
                .ForMember(p => p.CreatedBy, opt => opt.Ignore())
                .ForMember(p => p.CreatedIP, opt => opt.Ignore())
                .ForMember(p => p.ModifiedBy, opt => opt.Ignore())
                .ForMember(p => p.ModifiedOn, opt => opt.Ignore())
                .ForMember(p => p.ModifiedIP, opt => opt.Ignore());

            this.CreateMap<Entities.Category, Category>();
            this.CreateMap<Category, Entities.Category>()
               .ForMember(cat => cat.CreatedBy, opt => opt.Ignore())
               .ForMember(cat => cat.CreatedIP, opt => opt.Ignore())
               .ForMember(cat => cat.ModifiedBy, opt => opt.Ignore())
               .ForMember(cat => cat.ModifiedOn, opt => opt.Ignore())
               .ForMember(cat => cat.ModifiedIP, opt => opt.Ignore());

            this.CreateMap<Entities.Order, Order>();
            this.CreateMap<Order, Entities.Order>()
               .ForMember(o => o.CreatedBy, opt => opt.Ignore())
               .ForMember(o => o.CreatedIP, opt => opt.Ignore())
               .ForMember(o => o.ModifiedBy, opt => opt.Ignore())
               .ForMember(o => o.ModifiedOn, opt => opt.Ignore())
               .ForMember(o => o.ModifiedIP, opt => opt.Ignore());

            this.CreateMap<Entities.Promotion, Promotion>();
            this.CreateMap<Promotion, Entities.Promotion>()
               .ForMember(p => p.CreatedBy, opt => opt.Ignore())
               .ForMember(p => p.CreatedIP, opt => opt.Ignore())
               .ForMember(p => p.ModifiedBy, opt => opt.Ignore())
               .ForMember(p => p.ModifiedOn, opt => opt.Ignore())
               .ForMember(p => p.ModifiedIP, opt => opt.Ignore());

            this.CreateMap<Entities.Status, Status>();
            this.CreateMap<Status, Entities.Status>()
               .ForMember(s => s.CreatedBy, opt => opt.Ignore())
               .ForMember(s => s.CreatedIP, opt => opt.Ignore())
               .ForMember(s => s.ModifiedBy, opt => opt.Ignore())
               .ForMember(s => s.ModifiedOn, opt => opt.Ignore())
               .ForMember(s => s.ModifiedIP, opt => opt.Ignore());

            this.CreateMap<Entities.Store, Store>();
            this.CreateMap<Store, Entities.Store>()
               .ForMember(s => s.CreatedBy, opt => opt.Ignore())
               .ForMember(s => s.CreatedIP, opt => opt.Ignore())
               .ForMember(s => s.ModifiedBy, opt => opt.Ignore())
               .ForMember(s => s.ModifiedOn, opt => opt.Ignore())
               .ForMember(s => s.ModifiedIP, opt => opt.Ignore());

            this.CreateMap<Entities.Tax, Tax>();
            this.CreateMap<Tax, Entities.Tax>()
               .ForMember(t => t.CreatedBy, opt => opt.Ignore())
               .ForMember(t => t.CreatedIP, opt => opt.Ignore())
               .ForMember(t => t.ModifiedBy, opt => opt.Ignore())
               .ForMember(t => t.ModifiedOn, opt => opt.Ignore())
               .ForMember(t => t.ModifiedIP, opt => opt.Ignore());

            this.CreateMap<Entities.UserRole, UserRole>();
            this.CreateMap<UserRole, Entities.UserRole>()
               .ForMember(ur => ur.CreatedBy, opt => opt.Ignore())
               .ForMember(ur => ur.CreatedIP, opt => opt.Ignore())
               .ForMember(ur => ur.ModifiedBy, opt => opt.Ignore())
               .ForMember(ur => ur.ModifiedOn, opt => opt.Ignore())
               .ForMember(ur => ur.ModifiedIP, opt => opt.Ignore());

            this.CreateMap<Entities.User, User>();
            this.CreateMap<User, Entities.User>()
               .ForMember(u => u.CreatedBy, opt => opt.Ignore())
               .ForMember(u => u.CreatedIP, opt => opt.Ignore())
               .ForMember(u => u.ModifiedBy, opt => opt.Ignore())
               .ForMember(u => u.ModifiedOn, opt => opt.Ignore())
               .ForMember(u => u.ModifiedIP, opt => opt.Ignore());
        }

    }
}
