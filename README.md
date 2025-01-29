# Hastane Yönetim Sistemi - ABP Framework & Blazor

Bu proje, **ABP Framework** ile geliştirilmiş bir **hastane yönetim sistemi** uygulamasıdır. **Blazor** tabanlı bir arayüze sahiptir ve ABP Framework'ün  modüler yapısını kullanmaktadır.

## Kurulum

Projenizi yerel ortamda çalıştırmak için aşağıdaki adımları izleyin.

### Gerekli Araçlar
Aşağıdaki araçlar yüklü olmalı
- **.NET SDK 7.0** veya üzeri

### Adımlar

#### 1. Projeyi Klonlayın
```sh
git clone https://github.com/berfin-t/Pusula.HealthCare.git
cd Pusula.Training.HealthCare
```

#### 2. Gereksinimleri Yükleyin

```sh
dotnet restore
```
```sh
cd src/Pusula.Training.HealthCare.Blazor
npm install
install-libs
```
#### 3. Docker'ı Çalıştırın

```sh
docker run 
```

#### 4. Veritabanını Güncelleyin

```sh
dotnet run --project src/Pusula.Training.HealthCare.DbMigrator
```

#### 5. Projeyi Çalıştırın

```sh
dotnet run --project src/Pusula.Training.HealthCare.Tooling.Aspire
```
```sh
dotnet run --project src/Pusula.Training.HealthCare.Blazor
```

#### 6. Uygulamayı Açın
Tarayıcınızda aşağıdaki URL'lere giderek projeyi görüntüleyebilirsiniz:
- **API:** [https://localhost:44301](https://localhost:44301)
- **Blazor UI:** [https://localhost:44302](https://localhost:44302)

---

## Kullanılan Teknolojiler

- **ABP Framework**
- **Blazor**
- **Syncfusion bileşenleri**
- **Entity Framework Core**

