using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFDAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EF_ProductCategories",
                columns: table => new
                {
                    category_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    category_name = table.Column<string>(type: "varchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ProductCategoriesPK", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "EF_Vendors",
                columns: table => new
                {
                    vendor_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    vendor_name = table.Column<string>(type: "varchar(250)", nullable: false),
                    vendor_address = table.Column<string>(type: "varchar(250)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("VendorsPK", x => x.vendor_id);
                });

            migrationBuilder.CreateTable(
                name: "EF_Products",
                columns: table => new
                {
                    product_gtin = table.Column<string>(type: "varchar(14)", nullable: false),
                    product_name = table.Column<string>(type: "varchar(250)", nullable: false),
                    product_description = table.Column<string>(type: "varchar(1000)", nullable: true),
                    product_price = table.Column<decimal>(type: "money", nullable: true),
                    vendor_id = table.Column<int>(nullable: true),
                    category_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ProductsPK", x => x.product_gtin);
                    table.ForeignKey(
                        name: "FK_EF_Products_EF_ProductCategories_category_id",
                        column: x => x.category_id,
                        principalTable: "EF_ProductCategories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_EF_Products_EF_Vendors_vendor_id",
                        column: x => x.vendor_id,
                        principalTable: "EF_Vendors",
                        principalColumn: "vendor_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "EF_ProductCategories",
                columns: new[] { "category_id", "category_name" },
                values: new object[,]
                {
                    { 1, "Телефоны и аксессуары" },
                    { 2, "Компьютеры и оргтехника" },
                    { 3, "Электроника" },
                    { 4, "Бытовая техника" },
                    { 5, "Автотовары" },
                    { 6, "Одежда для мужчин" },
                    { 7, "Спорт и развлечения" },
                    { 8, "Для дома и сада" }
                });

            migrationBuilder.InsertData(
                table: "EF_Vendors",
                columns: new[] { "vendor_id", "vendor_address", "vendor_name" },
                values: new object[,]
                {
                    { 9, "China", "Animelover" },
                    { 8, "Shenzhen, Guangdong, China", "LiitoKala Official Flagship" },
                    { 7, "Guangdong China", "Ammoon Official" },
                    { 6, "China", "SanDiskSD" },
                    { 2, "Guangdong China", "ZAIR Official Vendor" },
                    { 4, "Guangdong China", "AsusISonyIHuaweiIMeizuIXiaomiOfficialVendor" },
                    { 3, "China", "MAOSHUANG Store" },
                    { 10, "China", "The Dark Knight" },
                    { 1, "ShenZhen City, Guangdong, China", "Mi Global Store" },
                    { 5, "Guangdong China", "SpeedmotoVendor" },
                    { 11, "China", "ANIMORE Official Vendor" }
                });

            migrationBuilder.InsertData(
                table: "EF_Products",
                columns: new[] { "product_gtin", "category_id", "product_description", "product_name", "product_price", "vendor_id" },
                values: new object[,]
                {
                    { "00032904589723", 4, "Умный робот-пылесос Xiaomi SKV4022GL для сухой уборки, белый", "Робот-пылесос Xiaomi SKV4022GL", 254.89m, 1 },
                    { "00032967492017", 1, "Оригинальные xiaomi mi note 3 чехол для xiaomi note3 задняя крышка Силиконовая окантовкка тканевый чехол подлинный xiaomi брендовая Мягкая прочная оболочка", "Оригинальные xiaomi mi note 3 ", 4.99m, 1 },
                    { "00032977895029", 1, "Смартфон Xiaomi Redmi Note 7 с глобальной версией, 4 ГБ, 64 ГБ, Восьмиядерный процессор Snapdragon 660, 4000 мАч, 2340x1080, 48мп, двойная камера, мобильный телефон", "Смартфон Xiaomi Redmi Note 7", 210.99m, 1 },
                    { "00033025568455", 1, "Смартфон Xiaomi Redmi K20 Pro с глобальной прошивкой, 6 ГБ, 64 ГБ, Восьмиядерный процессор Snapdragon 855 4000 мАч, всплывающая фронтальная камера 48мп, AMOLED 6,39'", "Смартфон Xiaomi Redmi K20 Pro", 331.99m, 1 },
                    { "00033035890038", 4, "Xiaomi 3 life Новый мини воздушный Циркуляционный Вентилятор 180 градусов вращение 330 сильный ветер мощность Usb зарядка низкий уровень шума высокий ветер", "Xiaomi 3 life Новый мини воздушный Циркуляционный Вентилятор", 12.99m, 1 },
                    { "00033059439610", 2, "Капа для Samsung Galaxy Tab A 10,1 чехол, Премиум PU кожаный чехол-подставка Tab A6 10,1' 2016 планшет SM-T580/T585 авто сна/Пробуждение чехол", "Капа для Samsung Galaxy Tab A 10", 10.18m, 2 },
                    { "04000002194089", 2, "Ноутбук Dell Inspiron 5370 (Intel Core i5-8250U/4 GB ram/128G SSD/13,3 ''FHD) Ноутбук Dell-brande", "Ноутбук Dell Inspiron 5370", 10.18m, 3 },
                    { "00033046647870", 1, "Новый оригинальный аккумулятор 2500 мАч для телефона INOI 2 Lite INOI2 Lite замена батареи высокого качества + номер отслеживания", "Аккумулятор 2500 мАч для телефона INOI 2 Lite INOI2 Lite", 10.18m, 4 },
                    { "00033031000740", 5, "CBR650R CB650R 2019 Новый Мото кластер Защита от царапин пленка приборная панель Крышка ЗАЩИТА ТПУ Blu-Ray для HONDA CBR650R", "ЗАЩИТА ТПУ Blu-Ray для HONDA CBR650R", 8.37m, 5 },
                    { "00032984767921", 3, "SanDisk A1 карты памяти 200 GB 128 GB 64 GB 98 МБ/с. 32 GB Micro sd Card Class10 UHS-1 флэш-карты памяти Microsd TF/sd карты s для планшета карта памяти флешка", "SanDisk A1 карты памяти 200 GB", 71.99m, 6 },
                    { "00032758904354", 7, "Ammoon AP-09 педаль для гитары Looper Nano Серия Петля электрогитарная педаль эффектов настоящий обход неограниченное количество гитарных деталей", "Ammoon AP-09 педаль для гитары", 32.90m, 7 },
                    { "00033051637203", 3, "Мини Портативный микшер с USB DJ микшерная консоль MP3 Jack 4 канала караоке 48 V усилитель для караоке ktv матч Вечерние", "Мини Портативный микшер с USB", 10.18m, 7 },
                    { "00032662684547", 3, "Liitokala 100% оригинал INR18650 30Q батарея 3000 мАч литиевая батарея inr18650 Питание аккумуляторная батарея электрические инструменты", "Liitokala 100% оригинал INR18650", 15.39m, 8 },
                    { "00032908549186", 6, "Космический корабль Timeline футболка вдохновлен Доктор Кто Звездные войны Звезда Забавный Бесплатная доставка Harajuku Модные топы Классический уникальный", "Футболка вдохновлен Доктор Кто", 12.64m, 9 },
                    { "00032988897730", 6, "Вентилятор воин Самурай японский семь добродетелей Bushido Новинка 2019 года короткий рукав для мужчин модные круглый средства ухода за кожей Шеи х", "Футболка 'Самурай'", 14.71m, 10 },
                    { "00032917974724", 4, "ANIMORE отпариватель для одежды бытовая техника Вертикальный Отпариватель с паровой железной щеткой утюг для глажки одежды для дома 110-220 В", "ANIMORE отпариватель для одежды", 22.00m, 11 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EF_Products_category_id",
                table: "EF_Products",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_EF_Products_vendor_id",
                table: "EF_Products",
                column: "vendor_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EF_Products");

            migrationBuilder.DropTable(
                name: "EF_ProductCategories");

            migrationBuilder.DropTable(
                name: "EF_Vendors");
        }
    }
}
