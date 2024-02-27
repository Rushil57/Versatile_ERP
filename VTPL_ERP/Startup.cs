using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ERP.DAL.Abstract;
using ERP.DAL.Service;

namespace VTPL_ERP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(2400);
                options.Cookie.HttpOnly = true;
            });
            services.AddSingleton<IUnitMasterService>(sp => new UnitMasterService(Configuration));
            services.AddSingleton<ICourierMasterService>(sp => new CourierMasterService(Configuration));
            services.AddSingleton<IBranchMasterService>(sp => new BranchMasterService(Configuration));
            services.AddSingleton<IProblemMasterService>(sp => new ProblemMasterService(Configuration));
            services.AddSingleton<IRolesMasterService>(sp => new RolesMasterService(Configuration));
            services.AddSingleton<IOtherChargesMasterService>(sp => new OtherChargesMasterService(Configuration));
            services.AddSingleton<IRoleActionMasterService>(sp => new RoleActionMasterService(Configuration));
            services.AddSingleton<IInventoryBrandMasterService>(sp => new InventoryBrandMasterService(Configuration));
            services.AddSingleton<IInventoryCategoryMasterService>(sp => new InventoryCategoryMasterService(Configuration));
            services.AddSingleton<IAccountPartyMasterService>(sp => new AccountPartyMasterService(Configuration));
            services.AddSingleton<IEmployeeMasterService>(sp => new EmployeeMasterService(Configuration));
            services.AddSingleton<IERP_CommonService>(sp => new ERP_CommonService(Configuration));
            services.AddSingleton<IUserService>(sp => new UserService(Configuration));
            services.AddSingleton<IInventoryMasterService>(sp => new InventoryMasterService(Configuration));
            services.AddSingleton<IInventoryGSTDetailsService>(sp => new InventoryGSTDetailsService(Configuration));
            services.AddSingleton<IPurchaseQuotationService>(sp => new PurchaseQuotationService(Configuration));
            services.AddSingleton<IPurchaseEntryService>(sp => new PurchaseEntryService(Configuration));
            services.AddSingleton<IPageMasterService>(sp => new PageMasterService(Configuration));
            services.AddSingleton<IRolesAndRightsMasterService>(sp => new RolesAndRightsMasterService(Configuration));
            services.AddSingleton<ICompanyService>(sp => new CompanyService(Configuration));
            services.AddSingleton<IPurchaseReturnEntryService>(sp => new PurchaseReturnEntryService(Configuration));
            services.AddSingleton<IPhoneCallsEntryService>(sp => new PhoneCallsEntryService(Configuration));
            services.AddSingleton<ICompanyBankAccountService>(sp => new CompanyBankAccountService(Configuration));
            services.AddSingleton<ISalesOrderService>(sp => new SalesOrderService(Configuration));
            services.AddSingleton<ISalesEntryService>(sp => new SalesEntryService(Configuration));
            services.AddSingleton<ISalesReturnEntryService>(sp => new SalesReturnEntryService(Configuration));
            services.AddSingleton<IPartyPaymentDebitEntryService>(sp => new PartyPaymentDebitEntryService(Configuration));
            services.AddSingleton<IInwardService>(sp => new InwardService(Configuration));
            services.AddSingleton<IOutwardService>(sp => new OutwardService(Configuration));
            services.AddSingleton<IContraEntryService>(sp => new ContraEntryService(Configuration));
            services.AddSingleton<ITransactionService>(sp => new TransactionService(Configuration));
            services.AddSingleton<IStockTransferService>(sp => new StockTransferService(Configuration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
