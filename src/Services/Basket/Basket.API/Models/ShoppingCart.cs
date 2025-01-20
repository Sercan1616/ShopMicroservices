namespace Basket.API.Models;

public class ShoppingCart
{
    public string UserName { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = new();
    public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

    // Bu yapıcı, nesne oluşturulurken bir kullanıcı adı (userName) almayı zorunlu kılar ve alınan değeri UserName özelliğine atar.
    // Bu işlem, ShoppingCart nesnesinin her bir örneği için benzersiz bir kullanıcı adının (username) belirlenmesini sağlar.
    // Bu sayede her alışveriş sepetinin (ShoppingCart) hangi kullanıcıya ait olduğu belirlenir.
    // Kullanıcıya özgü bir ShoppingCart nesnesi oluşturmak için username parametresini UserName özelliğine atamak gerekir. Bu, nesnenin hangi kullanıcıya ait olduğunu belirlemek için önemlidir.
    public ShoppingCart(string userName)
    {
        UserName = userName;
    }

    // Required for Mapping
    // ORM araçları (Entity Framework gibi), sınıflarınızın veritabanı ile eşleştirilmiş nesnelerini oluştururken parametresiz bir yapıcıya ihtiyaç duyar.
    // Parametresiz yapıcı sayesinde ORM, önce nesneyi oluşturur ve ardından özellikleri doldurur.
    public ShoppingCart()
    {
        // Boş bırakılabilir, çünkü ORM özellikleri dolduracak
    }
}
