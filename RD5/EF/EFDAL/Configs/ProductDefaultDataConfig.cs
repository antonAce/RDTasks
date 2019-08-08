using EFDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFDAL.Configs
{
	/// <summary>
    /// Not necessary configuration class for "Product" entity that adds default data to the DB
    /// </summary>
    public class ProductDefaultDataConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new Product[] {
                new Product { GTIN = "00032662684547", Name = "Liitokala 100% оригинал INR18650", Description = "Liitokala 100% оригинал INR18650 30Q батарея 3000 мАч литиевая батарея inr18650 Питание аккумуляторная батарея электрические инструменты", Price = 15.39m, CategoryId = 3, VendorId = 8 },
                new Product { GTIN = "00032758904354", Name = "Ammoon AP-09 педаль для гитары", Description = "Ammoon AP-09 педаль для гитары Looper Nano Серия Петля электрогитарная педаль эффектов настоящий обход неограниченное количество гитарных деталей", Price = 32.90m, CategoryId = 7, VendorId = 7 },
                new Product { GTIN = "00032904589723", Name = "Робот-пылесос Xiaomi SKV4022GL", Description = "Умный робот-пылесос Xiaomi SKV4022GL для сухой уборки, белый", Price = 254.89m, CategoryId = 4, VendorId = 1 },
                new Product { GTIN = "00032908549186", Name = "Футболка вдохновлен Доктор Кто", Description = "Космический корабль Timeline футболка вдохновлен Доктор Кто Звездные войны Звезда Забавный Бесплатная доставка Harajuku Модные топы Классический уникальный", Price = 12.64m, CategoryId = 6, VendorId = 9 },
                new Product { GTIN = "00032917974724", Name = "ANIMORE отпариватель для одежды", Description = "ANIMORE отпариватель для одежды бытовая техника Вертикальный Отпариватель с паровой железной щеткой утюг для глажки одежды для дома 110-220 В", Price = 22.00m, CategoryId = 4, VendorId = 11 },
                new Product { GTIN = "00032967492017", Name = "Оригинальные xiaomi mi note 3 ", Description = "Оригинальные xiaomi mi note 3 чехол для xiaomi note3 задняя крышка Силиконовая окантовкка тканевый чехол подлинный xiaomi брендовая Мягкая прочная оболочка", Price = 4.99m, CategoryId = 1, VendorId = 1 },
                new Product { GTIN = "00032977895029", Name = "Смартфон Xiaomi Redmi Note 7", Description = "Смартфон Xiaomi Redmi Note 7 с глобальной версией, 4 ГБ, 64 ГБ, Восьмиядерный процессор Snapdragon 660, 4000 мАч, 2340x1080, 48мп, двойная камера, мобильный телефон", Price = 210.99m, CategoryId = 1, VendorId = 1 },
                new Product { GTIN = "00032984767921", Name = "SanDisk A1 карты памяти 200 GB", Description = "SanDisk A1 карты памяти 200 GB 128 GB 64 GB 98 МБ/с. 32 GB Micro sd Card Class10 UHS-1 флэш-карты памяти Microsd TF/sd карты s для планшета карта памяти флешка", Price = 71.99m, CategoryId = 3, VendorId = 6 },
                new Product { GTIN = "00032988897730", Name = "Футболка 'Самурай'", Description = "Вентилятор воин Самурай японский семь добродетелей Bushido Новинка 2019 года короткий рукав для мужчин модные круглый средства ухода за кожей Шеи х", Price = 14.71m, CategoryId = 6, VendorId = 10 },
                new Product { GTIN = "00033025568455", Name = "Смартфон Xiaomi Redmi K20 Pro", Description = "Смартфон Xiaomi Redmi K20 Pro с глобальной прошивкой, 6 ГБ, 64 ГБ, Восьмиядерный процессор Snapdragon 855 4000 мАч, всплывающая фронтальная камера 48мп, AMOLED 6,39'", Price = 331.99m, CategoryId = 1, VendorId = 1 },
                new Product { GTIN = "00033031000740", Name = "ЗАЩИТА ТПУ Blu-Ray для HONDA CBR650R", Description = "CBR650R CB650R 2019 Новый Мото кластер Защита от царапин пленка приборная панель Крышка ЗАЩИТА ТПУ Blu-Ray для HONDA CBR650R", Price = 8.37m, CategoryId = 5, VendorId = 5 },
                new Product { GTIN = "00033035890038", Name = "Xiaomi 3 life Новый мини воздушный Циркуляционный Вентилятор", Description = "Xiaomi 3 life Новый мини воздушный Циркуляционный Вентилятор 180 градусов вращение 330 сильный ветер мощность Usb зарядка низкий уровень шума высокий ветер", Price = 12.99m, CategoryId = 4, VendorId = 1 },
                new Product { GTIN = "00033046647870", Name = "Аккумулятор 2500 мАч для телефона INOI 2 Lite INOI2 Lite", Description = "Новый оригинальный аккумулятор 2500 мАч для телефона INOI 2 Lite INOI2 Lite замена батареи высокого качества + номер отслеживания", Price = 10.18m, CategoryId = 1, VendorId = 4 },
                new Product { GTIN = "00033051637203", Name = "Мини Портативный микшер с USB", Description = "Мини Портативный микшер с USB DJ микшерная консоль MP3 Jack 4 канала караоке 48 V усилитель для караоке ktv матч Вечерние", Price = 10.18m, CategoryId = 3, VendorId = 7 },
                new Product { GTIN = "00033059439610", Name = "Капа для Samsung Galaxy Tab A 10", Description = "Капа для Samsung Galaxy Tab A 10,1 чехол, Премиум PU кожаный чехол-подставка Tab A6 10,1' 2016 планшет SM-T580/T585 авто сна/Пробуждение чехол", Price = 10.18m, CategoryId = 2, VendorId = 2 },
                new Product { GTIN = "04000002194089", Name = "Ноутбук Dell Inspiron 5370", Description = "Ноутбук Dell Inspiron 5370 (Intel Core i5-8250U/4 GB ram/128G SSD/13,3 ''FHD) Ноутбук Dell-brande", Price = 10.18m, CategoryId = 2, VendorId = 3 }
            });
        }
    }
}
