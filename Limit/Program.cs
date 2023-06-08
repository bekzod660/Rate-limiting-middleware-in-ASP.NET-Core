using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace Limit
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //Window Rate Limiter

            #region Fixed
            //builder.Services.AddRateLimiter(options =>
            //{
            //    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            //    options.AddFixedWindowLimiter("Fixed", opt =>
            //    {
            //        opt.Window = TimeSpan.FromSeconds(10);
            //        opt.PermitLimit = 5;

            //        // opt.QueueLimit = 2;
            //        // opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            //    });
            //});
            #endregion
            // builder.Services.AddMemoryCache();
            #region Sliding
            //builder.Services.AddRateLimiter(opt =>
            //{
            //    opt.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            //    opt.AddSlidingWindowLimiter(policyName: "Test", options =>
            //    {
            //        options.PermitLimit = 6;
            //        options.Window = TimeSpan.FromSeconds(20);
            //        options.SegmentsPerWindow = 2;
            //        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            //        options.QueueLimit = 0;
            //    });
            //});
            #endregion

            #region Concurrency
            //builder.Services.AddRateLimiter(opt =>
            //{
            //    opt.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            //    opt.AddConcurrencyLimiter(policyName: "Concurrency", options =>
            //    {

            //        options.PermitLimit = 1;
            //        //options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            //        //options.QueueLimit = 0;
            //    });
            //});


            #endregion

            #region 
            //Token Bucket Rate Limiter
            builder.Services.AddRateLimiter(options =>
            {
                options.AddTokenBucketLimiter("Token", opt =>
                {
                    opt.TokenLimit = 4;
                    opt.QueueLimit = 2;
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    opt.ReplenishmentPeriod = TimeSpan.FromSeconds(20);
                    opt.TokensPerPeriod = 2;
                    opt.AutoReplenishment = true;
                });
            });
            #endregion


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();



            app.MapControllers();
            app.UseRateLimiter();
            app.UseAuthorization();
            app.Run();




        }
    }
}