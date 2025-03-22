using Microsoft.AspNetCore.Routing.Constraints;

namespace LunaSite
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAllOrigins", builder =>
				{
					builder.AllowAnyOrigin()
						   .AllowAnyMethod()
						   .AllowAnyHeader();
				});
			});
			builder.Services.AddRazorPages();
			builder.Services.Configure<RouteOptions>(options =>
			{
				options.ConstraintMap.Add("ulong", typeof(LongRouteConstraint));
			});
			builder.Services.AddControllers();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			//app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
			app.UseCors("AllowAllOrigins");
			app.UseAuthorization();

			app.MapRazorPages();
			app.MapControllers();

			app.Run();
		}
	}
}