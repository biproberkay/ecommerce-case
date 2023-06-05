# .NET Core E-Ticaret Uygulaması

Bu proje, .NET Core teknolojileriyle geliştirilmiş bir e-ticaret uygulamasıdır. 

## Proje Açıklaması

Bu uygulama, kullanıcıların ürünleri listeleyebildiği, sepetlerine ürün ekleyebildiği ve indirimlerin uygulandığı bir e-ticaret platformunu simüle etmektedir. Uygulama, aşağıdaki temel özellikleri içermektedir:

- Kullanıcıların oturum açabilmesi ve yetkilendirme mekanizması
- API endpoint'lerinin Swagger dökümantasyonu
- Ürünlerin listelenmesi ve sepete eklenmesi
- Stok kontrolü ve hata yönetimi
- Sepette indirimlerin uygulanması

Bu projede, Docker platformunda çalışabilen bir uygulama geliştirilecekir.

## Teknolojiler

Bu projede aşağıdaki teknolojiler kullanılmaktadır:

- .NET Core
- Entity Framework Core
- JWT Bearer Token Authentication
- Docker

## Projeyi Çalıştırma

Projenin başarıyla çalıştırılması için aşağıdaki adımları izleyin:

1. Docker kurulu olduğundan emin olun.
2. Repository'i klonlayın veya ZIP dosyasını indirin.
3. Proje klasörüne gidin ve terminale aşağıdaki komutu girin:
> `docker-compose up`   
4. Uygulama Docker üzerinde ayağa kalkacaktır. Tarayıcınızdan `http://localhost:5000` adresine giderek uygulamayı kullanabilirsiniz.

## Dış Kaynaklar ve Referanslar

Bu proje geliştirilirken aşağıdaki kaynaklardan ve kod örneklerinden faydalanılmıştır:

- [JWT Authentication in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/jwt?view=aspnetcore-5.0)
- [Entity Framework Core Documentation](https://docs.microsoft.com/en-us/ef/core/)

Lisans
----

Bu proje MIT Lisansı altında lisanslanmıştır. Daha fazla bilgi için [LICENSE](LICENSE) dosyasına bakınız.


