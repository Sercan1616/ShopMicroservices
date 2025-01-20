using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public static class Extentions
{
    public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
    {
        // İlk değişken dbcontext'i almamıza yarar.
        using var scope = app.ApplicationServices.CreateScope();
        // DI Container'dan DiscountContext türündeki bir nesneyi alır.
        using var context = scope.ServiceProvider.GetRequiredService<DiscountContext>();
        // MigrateAsync metodu ile veritabanı oluşturulur.
        context.Database.MigrateAsync();

        // app değişkeni geri döndürülür.
        return app;
    }
}
