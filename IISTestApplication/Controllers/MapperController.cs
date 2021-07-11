using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using AutoMapper.QueryableExtensions;
using IISTestApplication.Models.MapperModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IISTestApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapperController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public MapperController(
            IMapper mapper,
            DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpPost("1")]
        public IActionResult Post1()
        {
            var res1 = _mapper.Map<ModelWithInt>(new ModelWithShort { Number = short.MaxValue });
            var res2 = _mapper.Map<ModelWithShort>(new ModelWithInt { Number = int.MaxValue });
            var res3 = _mapper.Map<ModelWithDouble>(new ModelWithInt { Number = int.MaxValue });
            var res4 = _mapper.Map<ModelWithInt>(new ModelWithDouble { Number = 5.4 });

            return Ok();
        }

        [HttpPost("2")]
        public IActionResult Post2()
        {
            var res1 = _mapper.Map<ModelWithNullable>(new ModelWithoutNullable { Number = 100 });
            var res2 = _mapper.Map<ModelWithoutNullable>(new ModelWithNullable { Number = 101 });
            var res3 = _mapper.Map<ModelWithoutNullable>(new ModelWithNullable { Number = null });

            return Ok();
        }

        [HttpPost("3")]
        public IActionResult Post3()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>());
            var mapper = config.CreateMapper();
            var dto = mapper.Map<OrderDTO>(new Order { Name = "Shoes" });

            return Ok();
        }

        [HttpPost("4")]
        public IActionResult Post4()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>());
            var plan = config.BuildExecutionPlan(typeof(Order), typeof(OrderDTO));

            return Ok();
        }

        [HttpPost("5")]
        public IActionResult Post5()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
                cfg.DestinationMemberNamingConvention = new PascalCaseNamingConvention();
                cfg.CreateMap<LowerUnderscoreModel, PascalCaseModel>().ReverseMap();
            });
            var mapper = config.CreateMapper();

            var res1 = mapper.Map<PascalCaseModel>(new LowerUnderscoreModel { test_number = 101 });
            var res2 = mapper.Map<LowerUnderscoreModel>(new PascalCaseModel { TestNumber = 102 });
            var res3 = mapper.Map<LowerUnderscoreModel>(new LowerUnderscoreModel { test_number = 103 });
            var res4 = mapper.Map<PascalCaseModel>(new PascalCaseModel { TestNumber = 104 });


            return Ok();
        }

        [HttpPost("6")]
        public IActionResult Post6()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ReplaceMemberName("Ä", "A");
                cfg.ReplaceMemberName("í", "i");
                cfg.ReplaceMemberName("Airlina", "Airline");
                cfg.CreateMap<ModelWithWrongCharacters, ModelWithEn>().ReverseMap();
            });
            var mapper = config.CreateMapper();

            var res1 = mapper.Map<ModelWithWrongCharacters>(new ModelWithEn { Aviator = 1, SubAirlineFlight = 2, Value = 3 });
            var res2 = mapper.Map<ModelWithEn>(new ModelWithWrongCharacters { Ävíator = 4, SubAirlinaFlight = 5, Value = 6 });

            return Ok();
        }

        [HttpPost("7")]
        public IActionResult Post7()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.RecognizePrefixes("pref1", "pref2");
                cfg.RecognizePostfixes("postf1", "postf2");

                cfg.CreateMap<ModelWithoutPostPrefixe, ModelWithPrefix>().ReverseMap();
                cfg.CreateMap<ModelWithPrefix, ModelWithPostfix>().ReverseMap();
                cfg.CreateMap<ModelWithPostfix, ModelWithoutPostPrefixe>().ReverseMap();
            });
            var mapper = config.CreateMapper();

            var res1 = mapper.Map<ModelWithoutPostPrefixe>(new ModelWithPrefix { pref1Name = "Name1", pref2Price = 1 });
            var res2 = mapper.Map<ModelWithPrefix>(new ModelWithoutPostPrefixe { Name = "Name2", Price = 2 });
            var res3 = mapper.Map<ModelWithPrefix>(new ModelWithPostfix { Namepostf1 = "Name3", Pricepostf2 = 3 });
            var res4 = mapper.Map<ModelWithPostfix>(new ModelWithPrefix { pref1Name = "Name4", pref2Price = 4 });
            var res5 = mapper.Map<ModelWithPostfix>(new ModelWithoutPostPrefixe { Name = "Name5", Price = 5 });
            var res6 = mapper.Map<ModelWithoutPostPrefixe>(new ModelWithPostfix { Namepostf1 = "Name6", Pricepostf2 = 6 });

            return Ok();
        }

        [HttpPost("8")]
        public IActionResult Post8()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Model1, Model2>().ReverseMap();
            });
            var mapper = config.CreateMapper();

            var res1 = mapper.Map<Model2>(new Model1(1, 2, 3, 4, 5, 6));

            config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => false;
                cfg.CreateMap<Model1, Model2>().ReverseMap();
            });
            mapper = config.CreateMapper();

            var res2 = mapper.Map<Model2>(new Model1(1, 2, 3, 4, 5, 6));

            config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod != null && (p.GetMethod.IsPublic || p.GetMethod.IsPrivate);
                cfg.CreateMap<Model1, Model2>().ReverseMap();
            });
            mapper = config.CreateMapper();

            var res3 = mapper.Map<Model2>(new Model1(1, 2, 3, 4, 5, 6));

            config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapField = f => f.IsPrivate;
                cfg.CreateMap<Model1, Model2>().ReverseMap();
            });
            mapper = config.CreateMapper();

            var res4 = mapper.Map<Model2>(new Model1(1, 2, 3, 4, 5, 6));

            return Ok();
        }

        [HttpPost("9")]
        public IActionResult Post9()
        {
            var res1 = _mapper.Map<CalendarEventForm>(new CalendarEvent { Date = new DateTime(2000, 6, 1, 10, 37, 21), Title = "Title" });

            return Ok();
        }

        [HttpPost("10")]
        public IActionResult Post10()
        {
            var res1 = _mapper.Map<CarDTO>(new Car { Year = 2015, Engine = new Engine { Model = "KN 21" } });

            return Ok();
        }

        [HttpPost("11")]
        public IActionResult Post11()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.CreateMap<Order, OrderDTO>();
            });
            var mapper = config.CreateMapper();

            var orders = new Order[] { new Order { Name = "Order1" }, new Order { Name = "Order1" } };
            //Order[] orders = null;

            var res1 = mapper.Map<IEnumerable<OrderDTO>>(orders);
            var res2 = mapper.Map<List<OrderDTO>>(orders);
            var res3 = mapper.Map<OrderDTO[]>(orders);

            return Ok();
        }

        [HttpPost("12")]
        public IActionResult Post12()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ParentSource, ParentDestination>().IncludeAllDerived();//.Include<ChildSource, ChildDestination>();
                cfg.CreateMap<ChildSource, ChildDestination>();//.IncludeBase<ParentSource, ParentDestination>();
            });
            var mapper = config.CreateMapper();

            var res1 = mapper.Map<ChildDestination>(new ChildSource { Value1 = 1, Value2 = 2 });
            var res2 = mapper.Map<ChildDestination[]>(new ChildSource[] { new ChildSource { Value1 = 1, Value2 = 2 }, new ChildSource { Value1 = 3, Value2 = 4 } });

            var polymorphicArray = new[] { new ParentSource(), new ChildSource(), new ParentSource() };

            var res3 = mapper.Map<ParentDestination[]>(polymorphicArray);
            var type1 = res3[0].GetType().FullName;
            var type2 = res3[1].GetType().FullName;
            var type3 = res3[2].GetType().FullName;


            return Ok();
        }

        [HttpPost("13")]
        public IActionResult Post13()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //cfg.DisableConstructorMapping();
                cfg.CreateMap<Source, SourceDTO>().ForCtorParam("newName", opt => opt.MapFrom(s => s.Value));
            });
            var mapper = config.CreateMapper();

            var res1 = mapper.Map<SourceDTO>(new Source { Value = 5 });


            return Ok();
        }

        [HttpPost("14")]
        public IActionResult Post14()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<FlatterOrder, FlatterOrderDTO>();
            });
            var mapper = config.CreateMapper();

            var p1 = new FlatterProduct { Name = "Water", Price = 20 };
            var p2 = new FlatterProduct { Name = "Eggs", Price = 11 };
            var order = new FlatterOrder();
            order.Customer = new FlatterCustomer { Name = "Vlad" };
            order.AddOrderLineItem(p1, 5);
            order.AddOrderLineItem(p2, 5);

            var res1 = mapper.Map<FlatterOrderDTO>(order);


            return Ok();
        }

        [HttpPost("15")]
        public IActionResult Post15()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IncludeMemberSource, IncludeMemberDestination>().IncludeMembers(s => s.InnerSource, s => s.OtherInnerSource);

                //cfg.CreateMap<IncludeMemberSource, IncludeMemberDestination>()
                //    .IncludeMembers(s => s.InnerSource, s => s.OtherInnerSource)
                //    .ReverseMap()
                //    .ForMember(s => s.Name, opt => opt.MapFrom(d => d.Name))
                //    .ForPath(s => s.InnerSource.Name, opt => opt.MapFrom(d => d.Name))
                //    .ForPath(s => s.InnerSource.Description, opt => opt.MapFrom(d => d.Description))
                //    .ForPath(s => s.OtherInnerSource.Name, opt => opt.MapFrom(d => d.Name))
                //    .ForPath(s => s.OtherInnerSource.Description, opt => opt.MapFrom(d => d.Description))
                //    .ForPath(s => s.OtherInnerSource.Title, opt => opt.MapFrom(d => d.Title));

                cfg.CreateMap<IncludeMemberInnerSource, IncludeMemberDestination>();
                cfg.CreateMap<IncludeMemberOtherInnerSource, IncludeMemberDestination>();

            });
            var mapper = config.CreateMapper();

            var s1 = new IncludeMemberInnerSource { Name = "S1", Description = "Desc1" };
            var s2 = new IncludeMemberOtherInnerSource { Name = "S2", Description = "Desc2", Title = "Title2" };
            var s = new IncludeMemberSource { Name = "S", InnerSource = s1, OtherInnerSource = s2 };

            var res1 = mapper.Map<IncludeMemberDestination>(s);
            var res2 = mapper.Map<IncludeMemberSource>(res1);

            return Ok();
        }

        [HttpPost("16")]
        public IActionResult Post16()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<ReverseOrder, ReverseOrderDTO>().ReverseMap();
                cfg.CreateMap<ReverseOrder, ReverseOrderDTO>()
                    .ForMember(d => d.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                    .ReverseMap()
                    .ForPath(s => s.Customer.Name, opt => opt.MapFrom(src => src.CustomerName));
            });
            var mapper = config.CreateMapper();

            var res1 = mapper.Map<ReverseOrderDTO>(new ReverseOrder { Customer = new ReverseCustomer { Name = "Cus1" }, Total = 1 });
            var res2 = mapper.Map<ReverseOrder>(new ReverseOrderDTO { CustomerName = "Cus2", Total = 2 });

            return Ok();
        }

        [HttpPost("17")]
        public IActionResult Post17()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HierarchyOrder, HierarchyMailOrderDto>();
                cfg.CreateMap<HierarchyOrder, HierarchyOrderDTO>().As<HierarchyMailOrderDto>();
            });
            var mapper = config.CreateMapper();

            var res1 = mapper.Map<HierarchyOrderDTO>(new HierarchyOrder());
            var type1 = res1.GetType().FullName;

            return Ok();
        }

        [HttpPost("18")]
        public IActionResult Post18()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<HierarchyOrder, HierarchyOrderDTO>()
                    .Include<HierarchyOnlineOrder, HierarchyOrderDTO>()
                    .Include<HierarchyMailOrder, HierarchyOrderDTO>()
                    .ForMember(o => o.Referrer, m => m.Ignore());

                cfg.CreateMap<HierarchyOnlineOrder, HierarchyOrderDTO>().ForMember(o => o.Referrer, m => m.MapFrom(x => x.Referrer));
                cfg.CreateMap<HierarchyMailOrder, HierarchyOrderDTO>();
            });
            var mapper = config.CreateMapper();

            var order = new HierarchyOnlineOrder { Referrer = "Ref" };
            var res1 = mapper.Map<HierarchyOrderDTO>(order);

            return Ok();
        }

        [HttpPost("19")]
        public IActionResult Post19()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps("IISTestApplication");
            });
            var mapper = config.CreateMapper();

            var res1 = mapper.Map<AutoMapModelDTO>(new AutoMapModel { Value = 5, Total = 25 });

            return Ok();
        }

        [HttpPost("20")]
        public IActionResult Post20()
        {
            var config = new MapperConfiguration(cfg => { });
            var mapper = config.CreateMapper();

            dynamic foo = new Foo { Bar = 1, Baz = 2 };

            var res1 = mapper.Map<Foo>(foo);
            var res2 = mapper.Map<dynamic>(res1);

            var res3 = mapper.Map<Foo>(new Dictionary<string, object> { ["InnerFoo.Bar"] = 42 });

            return Ok();
        }

        [HttpPost("21")]
        public IActionResult Post21()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap(typeof(GenericSource<>), typeof(GenericDestination<>));
            });
            var mapper = config.CreateMapper();

            var s1 = new GenericSource<int> { Value = 1 };
            var res1 = mapper.Map<GenericDestination<int>>(s1);

            var s2 = new GenericSource<Source> { Value = new Source { Value = 2 } };
            var res2 = mapper.Map<GenericDestination<Source>>(s2);

            return Ok();
        }

        [HttpPost("22")]
        public async Task<IActionResult> Post22()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.OrderLine, Models.OrderLineDTO>()
                    .ForMember(dto => dto.Product, conf => conf.MapFrom(ol => ol.Product.Name));
            });

            var res1 = await _context.OrderLines.Where(ol => ol.OrderId == 1).ProjectTo<Models.OrderLineDTO>(configuration).ToListAsync();

            return Ok();
        }

        [HttpPost("23")]
        public async Task<IActionResult> Post23()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.OrderLine, Models.OrderLineResultDTO>()
                    .ForMember(dto => dto.Quantity, conf => conf.MapFrom(ol => ol.Quantity * ol.Product.Price));
            });

            var res1 = await _context.OrderLines.Where(ol => ol.OrderId == 1).ProjectTo<Models.OrderLineResultDTO>(configuration).ToListAsync();

            return Ok();
        }

        [HttpPost("24")]
        public async Task<IActionResult> Post24()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.OrderLine, Models.OrderLineResultDTO>()
                    .ConvertUsing(ol => new Models.OrderLineResultDTO { Id = ol.Id, Quantity = ol.Quantity * 2 });
            });

            var res1 = await _context.OrderLines.ProjectTo<Models.OrderLineResultDTO>(configuration).ToListAsync();

            return Ok();
        }

        [HttpPost("25")]
        public async Task<IActionResult> Post25()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.OrderLine, Models.OrderLineResultDTO>()
                    .ConstructUsing(ol => new Models.OrderLineResultDTO(ol.Quantity + 15));
            });

            var res1 = await _context.OrderLines.ProjectTo<Models.OrderLineResultDTO>(configuration).ToListAsync();

            return Ok();
        }

        [HttpPost("26")]
        public async Task<IActionResult> Post26()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.OrderLine, Models.OrderLineWithStringQuantityDTO>();
            });

            var res1 = await _context.OrderLines.ProjectTo<Models.OrderLineWithStringQuantityDTO>(configuration).ToListAsync();

            return Ok();
        }

        [HttpPost("27")]
        public async Task<IActionResult> Post27()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Models.OrderLine, Models.OrderLineRichDTO>();
                cfg.CreateMap<Models.Order, Models.OrderRich>();
                cfg.CreateMap<Models.Product, Models.ProductRich>();
            });

            var res1 = await _context.OrderLines.ProjectTo<Models.OrderLineRichDTO>(configuration, dest => dest.Product, dest => dest.Order).ToListAsync();

            return Ok();
        }

        [HttpPost("28")]
        public async Task<IActionResult> Post28()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                string currentUserName = null;
                cfg.CreateMap<Models.Product, Models.ProductRich>()
                    .ForMember(m => m.Name, opt => opt.MapFrom(src => currentUserName));
            });

            var res1 = await _context.Products.Where(p => p.Id == 1).ProjectTo<Models.ProductRich>(configuration, new { currentUserName = "Test" }).ToListAsync();

            return Ok();
        }

        [HttpPost("29")]
        public IActionResult Post29()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConditionalFoo, ConditionalBar>()
                    .ForMember(m => m.Value, opt => opt.Condition(s => s.Value < 30000));
            });
            var mapper = config.CreateMapper();

            var res1 = mapper.Map<ConditionalBar>(new ConditionalFoo { Value = 32000 });
            var res2 = mapper.Map<ConditionalBar>(new ConditionalFoo { Value = 10 });
            var res3 = mapper.Map<ConditionalBar>(new ConditionalFoo { Value = 32770 });

            return Ok();
        }

        [HttpPost("30")]
        public IActionResult Post30()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ConditionalFoo, ConditionalBar>()
                    .ForMember(dest => dest.Value, opt =>
                    {
                        opt.PreCondition(src => src.Value < short.MaxValue);
                        opt.MapFrom(src => src.Value);
                    });
            });
            var mapper = config.CreateMapper();

            var res1 = mapper.Map<ConditionalBar>(new ConditionalFoo { Value = 32770 });
            var res2 = mapper.Map<ConditionalBar>(new ConditionalFoo { Value = 10 });

            return Ok();
        }

        [HttpPost("31")]
        public IActionResult Post31()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<NullSubstitutionFoo, NullSubstitutionBar>()
                    .ForMember(dest => dest.Value, opt => opt.NullSubstitute(11));
            });
            var mapper = config.CreateMapper();

            var res1 = mapper.Map<NullSubstitutionBar>(new NullSubstitutionFoo { Value = 1 });
            var res2 = mapper.Map<NullSubstitutionBar>(new NullSubstitutionFoo { Value = null });

            return Ok();
        }

        [HttpPost("32")]
        public IActionResult Post32()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<SourceEnum, DestinationEnum>()
                    .ConvertUsingEnumMapping(opt => opt.MapValue(SourceEnum.First, DestinationEnum.Default))
                    .ReverseMap();
            });
            var mapper = config.CreateMapper();

            var res1 = mapper.Map<DestinationEnum>(SourceEnum.Default);
            var res2 = mapper.Map<DestinationEnum>(SourceEnum.First);

            return Ok();
        }
    }
}
