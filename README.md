# EgeYurt - Ürün Yönetim Sistemi

## Proje Tanımı
Kullanıcıların ürünleri görüntüleyebildiği, ekleyebildiği, güncelleyebildiği ve silebildiği bir web uygulamasıdır. RESTful API ile yapılandırılmış olup kullanıcı dostu bir arayüze sahiptir.

## Teknolojiler
- Backend: .NET 6, ASP.NET Core, Entity Framework Core
- Frontend: ASP.NET MVC
- Veritabanı: Microsoft SQL Server
- Container: Docker
- Logging: Serilog
- Validasyon: Fluent Validation

## Proje Bileşenleri
- **Backend**: Ürün CRUD işlemleri için `ProductController`, kimlik doğrulama için `AuthController`.
- **Frontend**: Kullanıcı giriş ve ürün yönetimi arayüzleri.
- **Veritabanı**: `Products` tablosu (Id, Name, Brand, Price, StockQuantity, vb.)

## API Endpoint'leri
- **GET** `/api/Product`: Tüm ürünleri getirir.
- **POST** `/api/Product`: Yeni ürün ekler.
- **PUT** `/api/Product/{id}`: Ürünü günceller.
- **DELETE** `/api/Product/{id}`: Ürünü siler.
- **POST** `/api/Auth/login`: Kullanıcı girişi (JWT token döner).
- **POST** `/api/Auth/logout`: Kullanıcı çıkışı.

## Kullanım Senaryoları
- **Kullanıcı Girişi**: Giriş yaparak ürün listesine erişim.
- **Ürün Yönetimi**: Admin tüm CRUD işlemlerini yapabilir, User rolü ise yalnızca ekleyip görüntüleyebilir.
- **Token Yönetimi**: JWT token kullanımı.

## Docker Kurulumu
1. Projeyi klonlayın.
2. Docker ile çalıştırın:
   ```bash
   docker-compose up --build
